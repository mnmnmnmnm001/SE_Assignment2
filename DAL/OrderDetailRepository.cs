using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp.DAL
{
    public class OrderDetailRepository
    {
        private readonly DbCon _con;
        public OrderDetailRepository()
        {
            _con = new DbCon();
        }
        public void Add(OrderDetail a)
        {
            _con.OrderDetail.Add(new OrderDetail
            {
                ID = a.ID,
                OrderID = a.OrderID,
                ItemID = a.ItemID,
                Quantity = a.Quantity,
                UnitAmount = a.UnitAmount
            });
            _con.SaveChanges();
        }
        public List<OrderDetail> GetAll()
        {
            return _con.OrderDetail.ToList();
        }

        public OrderDetail? GetByID(int id)
        {
            return _con.OrderDetail.FirstOrDefault(c => c.ID == id);
        }
        public void Update(OrderDetail a)
        {
            var b = _con.OrderDetail.FirstOrDefault(c => c.ID == a.ID);
            if (b != null)
            {
                b.ID = a.ID;
                b.OrderID = a.OrderID;
                b.ItemID = a.ItemID;
                b.Quantity = a.Quantity;
                b.UnitAmount = a.UnitAmount;
                _con.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var a = _con.OrderDetail.FirstOrDefault(c => c.ID == id);
            if (a != null)
            {
                _con.OrderDetail.Remove(a);
                _con.SaveChanges();
            }
        }
       
    }
}
