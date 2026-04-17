using System;
using System.Collections.Generic;
using System.Text;
using WindowsFormsApp.DAL;

namespace BLL
{
    public class OrderS
    {
        private readonly OrderRepository _re;

        public OrderS()
        {
            _re = new OrderRepository();
        }

        public void Add(Order1 a)
        {
            _re.Add(a);
        }

        public Order1 GetByID(int id)
        {
            return _re.GetByID(id);
        }
        public List<Order1> GetAll()
        {
            return _re.GetAll();
        }

        public void Update(Order1 a)
        {
            _re.Update(a);
        }

        public void Delete(int id)
        {
            _re.Delete(id);
        }
    }
}