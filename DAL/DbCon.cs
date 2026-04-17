using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;

namespace WindowsFormsApp.DAL
{
    public class DbCon : DbContext
    {
        public DbCon() : base("Name=Con") { }
        public DbSet<User> User { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<Order1> Order1 { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
    }
}
