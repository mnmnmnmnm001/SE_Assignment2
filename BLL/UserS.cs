using System;
using System.Collections.Generic;
using System.Text;
using WindowsFormsApp.DAL;

namespace BLL
{
    public class UserS
    {
        private readonly UserRepository _re;

        public UserS()
        {
            _re = new UserRepository();
        }

        public void Add(User a)
        {
            _re.Add(a);
        }

        public User GetByID(int id)
        {
            return _re.GetByID(id);
        }
        public List<User> GetAll()
        {
            return _re.GetAll();
        }

        public void Update(User a)
        {
            _re.Update(a);
        }

        public void Delete(int id)
        {
            _re.Delete(id);
        }
    }
}