using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp.DAL;

namespace WindowsFormsApp.DAL
{
    public class OrderRepository
    {
        private readonly DbCon _con;
        public OrderRepository()
        {
            _con = new DbCon();
        }
        public void Add(Order a)
        {
            _con.Order.Add(new Order
            {
                OrderID = a.OrderID,
                OrderDate = a.OrderDate,
                AgentID = a.AgentID
            });
            _con.SaveChanges();
        }
        public List<Order> GetAll()
        {
            return _con.Order.ToList();
        }

        public Order? GetByID(int id)
        {
            return _con.Order.FirstOrDefault(c => c.OrderID == id);
        }


        public void Update(Order a)
        {
            var b = _con.Order.FirstOrDefault(c => c.OrderID == a.OrderID);
            if (b != null)
            {
                b.OrderID = a.OrderID;
                b.OrderDate = a.OrderDate;
                b.AgentID = a.AgentID;
                _con.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var a = _con.Order.FirstOrDefault(c => c.OrderID == id);
            if (a != null)
            {
                _con.Order.Remove(a);
                _con.SaveChanges();
            }
        }
        /*
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
        };*/
    }
}
