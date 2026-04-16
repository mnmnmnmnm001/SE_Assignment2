using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.DAL
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string email { get; set; }
        public string password { get; set; } // hashed
        public bool Lock { get; set; }
    }
}
