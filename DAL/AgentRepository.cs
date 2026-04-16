using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.DAL
{
    public class AgentRepository
    {
        private readonly DbCon _con;
        public AgentRepository()
        {
            _con = new DbCon();
        }
        public void Add(Agent a)
        {
            _con.Agent.Add(new Agent
            {
                AgentName = a.AgentName,
                Address   = a.Address
            });
            _con.SaveChanges();
        }
        public List<Agent> GetAll()
        {
            return _con.Agent.ToList();
        }

        public Agent? GetByID(int id)
        {
            return _con.Agent.FirstOrDefault(c => c.AgentID == id);
        }


        public void Update(Agent a)
        {
            var b = _con.Agent.FirstOrDefault(c => c.AgentID == a.AgentID);
            if (b != null)
            {
                b.AgentName = a.AgentName;
                b.Address = a.Address;
                _con.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var a = _con.Agent.FirstOrDefault(c => c.AgentID == id);
            if (a != null)
            {
                _con.Agent.Remove(a);
                _con.SaveChanges();
            }
        }
        /*
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
        }*/

    }
}
