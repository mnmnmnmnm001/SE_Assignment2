// ============================================================
//  DAL/OrderDAL.cs
// ============================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WinformApp.DTO;

namespace WinformApp.DAL
{
    public class OrderDAL
    {
        // ── Orders ──────────────────────────────────────────────

        public List<OrderDTO> GetAll()
        {
            var list = new List<OrderDTO>();
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                @"SELECT o.*, a.AgentName
                  FROM [Order] o
                  JOIN Agent a ON o.AgentID = a.AgentID
                  ORDER BY o.OrderDate DESC", conn))
            using (var r = cmd.ExecuteReader())
                while (r.Read()) list.Add(MapOrder(r));
            return list;
        }

        public OrderDTO GetByID(int orderID)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                @"SELECT o.*, a.AgentName
                  FROM [Order] o
                  JOIN Agent a ON o.AgentID = a.AgentID
                  WHERE o.OrderID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", orderID);
                using (var r = cmd.ExecuteReader())
                    return r.Read() ? MapOrder(r) : null;
            }
        }

        /// <summary>
        /// Inserts an Order header + all its detail rows in one transaction.
        /// Returns the new OrderID.
        /// </summary>
        public int InsertWithDetails(OrderDTO order, List<OrderDetailDTO> details)
        {
            using (var conn = DBConnection.GetConnection())
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    // 1. Insert header
                    var cmdO = new SqlCommand(
                        @"INSERT INTO [Order](OrderDate,AgentID,TotalAmount,Note)
                          VALUES(@od,@aid,@ta,@n); SELECT SCOPE_IDENTITY();", conn, tran);
                    cmdO.Parameters.AddWithValue("@od",  order.OrderDate);
                    cmdO.Parameters.AddWithValue("@aid", order.AgentID);
                    cmdO.Parameters.AddWithValue("@ta",  order.TotalAmount);
                    cmdO.Parameters.AddWithValue("@n",   (object)order.Note ?? DBNull.Value);
                    int newOrderID = (int)(decimal)cmdO.ExecuteScalar();

                    // 2. Insert details
                    foreach (var d in details)
                    {
                        var cmdD = new SqlCommand(
                            @"INSERT INTO OrderDetail(OrderID,ItemID,Quantity,UnitAmount)
                              VALUES(@oid,@iid,@qty,@ua)", conn, tran);
                        cmdD.Parameters.AddWithValue("@oid", newOrderID);
                        cmdD.Parameters.AddWithValue("@iid", d.ItemID);
                        cmdD.Parameters.AddWithValue("@qty", d.Quantity);
                        cmdD.Parameters.AddWithValue("@ua",  d.UnitAmount);
                        cmdD.ExecuteNonQuery();
                    }

                    tran.Commit();
                    return newOrderID;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        /// <summary>Update header + replace all details (delete-insert).</summary>
        public void UpdateWithDetails(OrderDTO order, List<OrderDetailDTO> details)
        {
            using (var conn = DBConnection.GetConnection())
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    var cmdO = new SqlCommand(
                        @"UPDATE [Order] SET OrderDate=@od,AgentID=@aid,
                          TotalAmount=@ta,Note=@n WHERE OrderID=@id", conn, tran);
                    cmdO.Parameters.AddWithValue("@od",  order.OrderDate);
                    cmdO.Parameters.AddWithValue("@aid", order.AgentID);
                    cmdO.Parameters.AddWithValue("@ta",  order.TotalAmount);
                    cmdO.Parameters.AddWithValue("@n",   (object)order.Note ?? DBNull.Value);
                    cmdO.Parameters.AddWithValue("@id",  order.OrderID);
                    cmdO.ExecuteNonQuery();

                    // Replace details
                    var del = new SqlCommand(
                        "DELETE FROM OrderDetail WHERE OrderID=@id", conn, tran);
                    del.Parameters.AddWithValue("@id", order.OrderID);
                    del.ExecuteNonQuery();

                    foreach (var d in details)
                    {
                        var cmdD = new SqlCommand(
                            @"INSERT INTO OrderDetail(OrderID,ItemID,Quantity,UnitAmount)
                              VALUES(@oid,@iid,@qty,@ua)", conn, tran);
                        cmdD.Parameters.AddWithValue("@oid", order.OrderID);
                        cmdD.Parameters.AddWithValue("@iid", d.ItemID);
                        cmdD.Parameters.AddWithValue("@qty", d.Quantity);
                        cmdD.Parameters.AddWithValue("@ua",  d.UnitAmount);
                        cmdD.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void Delete(int orderID)
        {
            using (var conn = DBConnection.GetConnection())
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    var d = new SqlCommand(
                        "DELETE FROM OrderDetail WHERE OrderID=@id", conn, tran);
                    d.Parameters.AddWithValue("@id", orderID);
                    d.ExecuteNonQuery();

                    var o = new SqlCommand(
                        "DELETE FROM [Order] WHERE OrderID=@id", conn, tran);
                    o.Parameters.AddWithValue("@id", orderID);
                    o.ExecuteNonQuery();

                    tran.Commit();
                }
                catch { tran.Rollback(); throw; }
            }
        }

        // ── Order Details ────────────────────────────────────────

        public List<OrderDetailDTO> GetDetails(int orderID)
        {
            var list = new List<OrderDetailDTO>();
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                @"SELECT od.*, i.ItemName
                  FROM OrderDetail od
                  JOIN Item i ON od.ItemID = i.ItemID
                  WHERE od.OrderID = @oid", conn))
            {
                cmd.Parameters.AddWithValue("@oid", orderID);
                using (var r = cmd.ExecuteReader())
                    while (r.Read()) list.Add(MapDetail(r));
            }
            return list;
        }

        // ── Helpers ──────────────────────────────────────────────

        private OrderDTO MapOrder(SqlDataReader r) => new OrderDTO
        {
            OrderID     = (int)    r["OrderID"],
            OrderDate   = (DateTime)r["OrderDate"],
            AgentID     = (int)    r["AgentID"],
            AgentName   = (string) r["AgentName"],
            TotalAmount = (decimal)r["TotalAmount"],
            Note        = r["Note"] as string
        };

        private OrderDetailDTO MapDetail(SqlDataReader r) => new OrderDetailDTO
        {
            ID         = (int)    r["ID"],
            OrderID    = (int)    r["OrderID"],
            ItemID     = (int)    r["ItemID"],
            ItemName   = (string) r["ItemName"],
            Quantity   = (int)    r["Quantity"],
            UnitAmount = (decimal)r["UnitAmount"]
        };
    }
}
