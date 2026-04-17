// ============================================================
//  DAL/AgentDAL.cs
// ============================================================
using System.Collections.Generic;
using System.Data.SqlClient;
using WinformApp.DTO;

namespace WinformApp.DAL
{
    public class AgentDAL
    {
        public List<AgentDTO> GetAll()
        {
            var list = new List<AgentDTO>();
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("SELECT * FROM Agent ORDER BY AgentName", conn))
            using (var r = cmd.ExecuteReader())
                while (r.Read()) list.Add(Map(r));
            return list;
        }

        public AgentDTO GetByID(int id)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("SELECT * FROM Agent WHERE AgentID=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var r = cmd.ExecuteReader())
                    return r.Read() ? Map(r) : null;
            }
        }

        public int Insert(AgentDTO a)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                @"INSERT INTO Agent(AgentName,Address,Phone,Email)
                  VALUES(@n,@ad,@ph,@e); SELECT SCOPE_IDENTITY();", conn))
            {
                AddParams(cmd, a);
                return (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public void Update(AgentDTO a)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                @"UPDATE Agent SET AgentName=@n,Address=@ad,Phone=@ph,Email=@e
                  WHERE AgentID=@id", conn))
            {
                AddParams(cmd, a);
                cmd.Parameters.AddWithValue("@id", a.AgentID);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("DELETE FROM Agent WHERE AgentID=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>Agents who purchased a specific item.</summary>
        public List<AgentPurchaseDTO> GetAgentsByItem(int itemID)
        {
            var list = new List<AgentPurchaseDTO>();
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                @"SELECT a.AgentID, a.AgentName, a.Address,
                    COUNT(DISTINCT o.OrderID)          AS OrderCount,
                    SUM(od.Quantity * od.UnitAmount)   AS TotalSpent
                  FROM Agent a
                  JOIN [Order]     o  ON a.AgentID  = o.AgentID
                  JOIN OrderDetail od ON o.OrderID   = od.OrderID
                  WHERE od.ItemID = @iid
                  GROUP BY a.AgentID, a.AgentName, a.Address
                  ORDER BY TotalSpent DESC", conn))
            {
                cmd.Parameters.AddWithValue("@iid", itemID);
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(new AgentPurchaseDTO
                        {
                            AgentID    = (int)   r["AgentID"],
                            AgentName  = (string) r["AgentName"],
                            Address    = (string) r["Address"],
                            OrderCount = (int)   r["OrderCount"],
                            TotalSpent = (decimal)r["TotalSpent"]
                        });
            }
            return list;
        }

        //
        public List<AgentPurchaseDTO> GetAgentPurchaseSummary()
        {
            var list = new List<AgentPurchaseDTO>();
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                @"SELECT a.AgentID, a.AgentName, a.Address,
                    COUNT(DISTINCT o.OrderID)          AS OrderCount,
                    ISNULL(SUM(o.TotalAmount),0)       AS TotalSpent
                  FROM Agent a
                  LEFT JOIN [Order] o ON a.AgentID = o.AgentID
                  GROUP BY a.AgentID, a.AgentName, a.Address
                  ORDER BY TotalSpent DESC", conn))
            using (var r = cmd.ExecuteReader())
                while (r.Read())
                    list.Add(new AgentPurchaseDTO
                    {
                        AgentID    = (int)   r["AgentID"],
                        AgentName  = (string) r["AgentName"],
                        Address    = (string) r["Address"],
                        OrderCount = (int)   r["OrderCount"],
                        TotalSpent = (decimal)r["TotalSpent"]
                    });
            return list;
        }

        private void AddParams(SqlCommand cmd, AgentDTO a)
        {
            cmd.Parameters.AddWithValue("@n",  a.AgentName);
            cmd.Parameters.AddWithValue("@ad", a.Address);
            cmd.Parameters.AddWithValue("@ph", (object)a.Phone ?? System.DBNull.Value);
            cmd.Parameters.AddWithValue("@e",  (object)a.Email ?? System.DBNull.Value);
        }

        private AgentDTO Map(SqlDataReader r) => new AgentDTO
        {
            AgentID   = (int)   r["AgentID"],
            AgentName = (string)r["AgentName"],
            Address   = (string)r["Address"],
            Phone     = r["Phone"] as string,
            Email     = r["Email"] as string
        };
    }
}
