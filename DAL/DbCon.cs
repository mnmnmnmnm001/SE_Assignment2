using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data.SqlClient;

namespace WindowsFormsApp.DAL
{
    public class DbCon : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbCon() : base("name=Conn"){ }

        /*
        private static readonly string _connStr =
            ConfigurationManager.ConnectionStrings["OrderDB"].ConnectionString;
        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_connStr);
            conn.Open();
            return conn;
        }*/
    }
}
