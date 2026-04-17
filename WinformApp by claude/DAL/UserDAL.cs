// ============================================================
//  DAL/UserDAL.cs
// ============================================================
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WinformApp.DTO;

namespace WinformApp.DAL
{
    public class UserDAL
    {
        public UserDTO Login(string username, string hashedPassword)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                "SELECT * FROM Users WHERE UserName=@u AND Password=@p AND IsLocked=0", conn))
            {
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", hashedPassword);
                using (var r = cmd.ExecuteReader())
                    return r.Read() ? MapUser(r) : null;
            }
        }

        public List<UserDTO> GetAll()
        {
            var list = new List<UserDTO>();
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand("SELECT * FROM Users ORDER BY UserName", conn))
            using (var r = cmd.ExecuteReader())
                while (r.Read()) list.Add(MapUser(r));
            return list;
        }

        public int Insert(UserDTO u)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                "INSERT INTO Users(UserName,Email,Password,IsLocked) VALUES(@un,@e,@p,@l); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@un", u.UserName);
                cmd.Parameters.AddWithValue("@e",  u.Email);
                cmd.Parameters.AddWithValue("@p",  u.Password);
                cmd.Parameters.AddWithValue("@l",  u.IsLocked);
                return (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public void ToggleLock(int userID, bool locked)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(
                "UPDATE Users SET IsLocked=@l WHERE UserID=@id", conn))
            {
                cmd.Parameters.AddWithValue("@l",  locked);
                cmd.Parameters.AddWithValue("@id", userID);
                cmd.ExecuteNonQuery();
            }
        }

        private UserDTO MapUser(SqlDataReader r) => new UserDTO
        {
            UserID   = (int)  r["UserID"],
            UserName = (string)r["UserName"],
            Email    = (string)r["Email"],
            Password = (string)r["Password"],
            IsLocked = (bool)  r["IsLocked"]
        };
    }
}
