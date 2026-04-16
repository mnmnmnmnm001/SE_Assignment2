using System.Collections.Generic;
using System.Data.SqlClient;

namespace WindowsFormsApp.DAL
{
    public class ItemRepository
    {
        private readonly DbCon _con;
        public ItemRepository()
        {
            _con = new DbCon();
        }
        public void Add(Item a)
        {
            _con.Item.Add(new Item
            {
                ItemName = a.ItemName,
                Size = a.Size,
                Price = a.Price,
                Stock = a.Stock
            });
            _con.SaveChanges();
        }
        public List<Item> GetAll()
        {
            return _con.Item.ToList();
        }

        public Item? GetByID(int id)
        {
            return _con.Item.FirstOrDefault(c => c.ItemID == id);
        }


        public void Update(Item a)
        {
            var b = _con.Item.FirstOrDefault(c => c.ItemID == a.ItemID);
            if (b != null)
            {
                b.ItemName = a.ItemName;
                b.Size = a.Size;
                b.Price = a.Price;
                b.Stock = a.Stock;
                _con.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var a = _con.Item.FirstOrDefault(c => c.ItemID == id);
            if (a != null)
            {
                _con.Item.Remove(a);
                _con.SaveChanges();
            }
        }
        /*
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
    */
    }
}
