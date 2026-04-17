using System;
using System.Collections.Generic;
using System.Text;
using WindowsFormsApp.DAL;
using System.Security.Cryptography;

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
        public string EncodePass(string password)
        { //SHA256 is a one-way hash, it cannot be decoded back to the original password.
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(bytes);
                // Convert hash bytes to a hex string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2")); // Lowercase hex
                }
                return builder.ToString();
            }
        }
    }
}