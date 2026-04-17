// ============================================================
//  DAL/DBConnection.cs  –  Database connection helper
// ============================================================
using System.Configuration;
using System.Data.SqlClient;

namespace WinformApp.DAL
{
    public static class DBConnection
    {
        private static readonly string _connStr =
            ConfigurationManager.ConnectionStrings["OrderDB"].ConnectionString;

        /// <summary>Returns an open SqlConnection. Caller must dispose it.</summary>
        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_connStr);
            conn.Open();
            return conn;
        }
    }
}
