using System;
using System.Collections.Generic;
using System.Text;
using WindowsFormsApp.DAL;

namespace BLL
{
    public class OrderDetailS
    {
        private readonly OrderDetailRepository _re;

        public OrderDetailS()
        {
            _re = new OrderDetailRepository();
        }

        public void Add(OrderDetail a)
        {
            _re.Add(a);
        }

        public OrderDetail GetByID(int id)
        {
            return _re.GetByID(id);
        }
        public List<OrderDetail> GetAll()
        {
            return _re.GetAll();
        }

        public void Update(OrderDetail a)
        {
            _re.Update(a);
        }

        public void Delete(int id)
        {
            _re.Delete(id);
        }
    }
}