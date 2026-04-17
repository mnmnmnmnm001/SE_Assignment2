// ============================================================
//  DAL/ItemDAL.cs
// ============================================================
using System.Collections.Generic;
using System.Data.SqlClient;
using WinformApp.DTO;

namespace WinformApp.DAL
{
    public class ItemDAL
    {
        public List<ItemDTO> GetAll()
        {
            var list = new List<ItemDTO>();
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("SELECT * FROM Item ORDER BY ItemName", conn))
            using (var r = cmd.ExecuteReader())
                while (r.Read()) list.Add(Map(r));
            return list;
        }

        public ItemDTO GetByID(int id)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("SELECT * FROM Item WHERE ItemID=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var r = cmd.ExecuteReader())
                    return r.Read() ? Map(r) : null;
            }
        }

        public int Insert(ItemDTO item)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                @"INSERT INTO Item(ItemName,Size,Unit,UnitPrice,Stock,Description)
                  VALUES(@n,@s,@u,@up,@st,@d); SELECT SCOPE_IDENTITY();", conn))
            {
                AddParams(cmd, item);
                return (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public void Update(ItemDTO item)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                @"UPDATE Item SET ItemName=@n,Size=@s,Unit=@u,
                  UnitPrice=@up,Stock=@st,Description=@d
                  WHERE ItemID=@id", conn))
            {
                AddParams(cmd, item);
                cmd.Parameters.AddWithValue("@id", item.ItemID);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("DELETE FROM Item WHERE ItemID=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // --- filter helpers ---

        public List<BestItemDTO> GetBestItems(int topN)
        {
            var list = new List<BestItemDTO>();
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                @"SELECT TOP(@n)
                    i.ItemID, i.ItemName,
                    SUM(od.Quantity)              AS TotalQty,
                    SUM(od.Quantity*od.UnitAmount) AS TotalRevenue
                  FROM Item i
                  JOIN OrderDetail od ON i.ItemID = od.ItemID
                  GROUP BY i.ItemID, i.ItemName
                  ORDER BY TotalQty DESC", conn))
            {
                cmd.Parameters.AddWithValue("@n", topN);
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(new BestItemDTO
                        {
                            ItemID       = (int)   r["ItemID"],
                            ItemName     = (string) r["ItemName"],
                            TotalQty     = (int)   r["TotalQty"],
                            TotalRevenue = (decimal)r["TotalRevenue"]
                        });
            }
            return list;
        }

        /// <summary>Items purchased by a specific agent.</summary>
        public List<BestItemDTO> GetItemsByAgent(int agentID)
        {
            var list = new List<BestItemDTO>();
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                @"SELECT i.ItemID, i.ItemName,
                    SUM(od.Quantity)              AS TotalQty,
                    SUM(od.Quantity*od.UnitAmount) AS TotalRevenue
                  FROM Item i
                  JOIN OrderDetail od ON i.ItemID  = od.ItemID
                  JOIN [Order]     o  ON od.OrderID = o.OrderID
                  WHERE o.AgentID = @aid
                  GROUP BY i.ItemID, i.ItemName
                  ORDER BY TotalQty DESC", conn))
            {
                cmd.Parameters.AddWithValue("@aid", agentID);
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(new BestItemDTO
                        {
                            ItemID       = (int)   r["ItemID"],
                            ItemName     = (string) r["ItemName"],
                            TotalQty     = (int)   r["TotalQty"],
                            TotalRevenue = (decimal)r["TotalRevenue"]
                        });
            }
            return list;
        }

        private void AddParams(SqlCommand cmd, ItemDTO item)
        {
            cmd.Parameters.AddWithValue("@n",  item.ItemName);
            cmd.Parameters.AddWithValue("@s",  item.Size);
            cmd.Parameters.AddWithValue("@u",  item.Unit);
            cmd.Parameters.AddWithValue("@up", item.UnitPrice);
            cmd.Parameters.AddWithValue("@st", item.Stock);
            cmd.Parameters.AddWithValue("@d",  (object)item.Description ?? System.DBNull.Value);
        }

        private ItemDTO Map(SqlDataReader r) => new ItemDTO
        {
            ItemID      = (int)    r["ItemID"],
            ItemName    = (string) r["ItemName"],
            Size        = (string) r["Size"],
            Unit        = (string) r["Unit"],
            UnitPrice   = (decimal)r["UnitPrice"],
            Stock       = (int)    r["Stock"],
            Description = r["Description"] as string
        };
    }
}
