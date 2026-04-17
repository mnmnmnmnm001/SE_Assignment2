using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp.DAL;

namespace WindowsFormsApp.DAL
{
    public class OrderRepository
    {
        private readonly DbCon _con;
        public OrderRepository()
        {
            _con = new DbCon();
        }
        public void Add(Order1 a)
        {
            _con.Order.Add(new Order1
            {
                OrderID = a.OrderID,
                OrderDate = a.OrderDate,
                AgentID = a.AgentID
            });
            _con.SaveChanges();
        }
        public List<Order1> GetAll()
        {
            return _con.Order.ToList();
        }

        public Order1? GetByID(int id)
        {
            return _con.Order.FirstOrDefault(c => c.OrderID == id);
        }


        public void Update(Order1 a)
        {
            var b = _con.Order.FirstOrDefault(c => c.OrderID == a.OrderID);
            if (b != null)
            {
                b.OrderID = a.OrderID;
                b.OrderDate = a.OrderDate;
                b.AgentID = a.AgentID;
                _con.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var a = _con.Order.FirstOrDefault(c => c.OrderID == id);
            if (a != null)
            {
                _con.Order.Remove(a);
                _con.SaveChanges();
            }
        }
    }
}
