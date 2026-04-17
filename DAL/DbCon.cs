using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;

namespace WindowsFormsApp.DAL
{
    public class DbCon : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<Order1> Order1 { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        // Use the options-based constructor required by EF Core
        public DbCon(DbContextOptions<DbCon> options) : base(options) { }


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
