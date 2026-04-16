using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp.DAL
{
    public class UserRepository
    {
        private readonly DbCon _con;
        public UserRepository()
        {
            _con = new DbCon();
        }
        public User Login(string username, string hashedPassword)
        {
            var userobj = _con.User.FirstOrDefault(c => c.UserName == username);
            if (userobj != null)
            {
                if (hashedPassword == userobj.password)
                {
                    return userobj;
                }
                else
                {
                    
                    userobj = null;
                }
            }
            return userobj;
        }

        public void Add(User a)
        {
            _con.User.Add(new User
            {
                UserID = a.UserID,
                UserName = a.UserName,
                email = a.email,
                password = a.password,
                Lock = a.Lock
            });
            _con.SaveChanges();
        }
        public List<User> GetAll()
        {
            return _con.User.ToList();
        }

        public User? GetByID(int id)
        {
            return _con.User.FirstOrDefault(c => c.UserID == id);
        }


        public void Update(User a)
        {
            var b = _con.User.FirstOrDefault(c => c.UserID == a.UserID);
            if (b != null)
            {
                b.UserID = a.UserID;
                b.UserName = a.UserName;
                b.email = a.email;
                b.password = a.password;
                b.Lock = a.Lock;
                _con.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var a = _con.User.FirstOrDefault(c => c.UserID == id);
            if (a != null)
            {
                _con.User.Remove(a);
                _con.SaveChanges();
            }
        }

        public void ToggleLock(int userID, bool locked)
        {
            var a = _con.User.FirstOrDefault(c => c.UserID == userID);
            if (a != null)
            {
                a.Lock = locked;
                _con.SaveChanges();
            }
        }
        /*
        private UserDTO MapUser(SqlDataReader r) => new UserDTO
        {
            UserID   = (int)  r["UserID"],
            UserName = (string)r["UserName"],
            Email    = (string)r["Email"],
            Password = (string)r["Password"],
            IsLocked = (bool)  r["IsLocked"]
        };*/
    }
}
