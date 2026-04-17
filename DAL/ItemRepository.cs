using System.Collections.Generic;
using System.Data.SqlClient;

namespace WindowsFormsApp.DAL
{
    public class ItemRepository
    {
        private readonly DbCon _con;
        public ItemRepository()
        {
            _con = new DbCon();
        }
        public void Add(Item a)
        {
            _con.Item.Add(new Item
            {
                ItemName = a.ItemName,
                Size = a.Size,
                Price = a.Price
            });
            _con.SaveChanges();
        }
        public List<Item> GetAll()
        {
            return _con.Item.ToList();
        }

        public Item? GetByID(int id)
        {
            return _con.Item.FirstOrDefault(c => c.ItemID == id);
        }


        public void Update(Item a)
        {
            var b = _con.Item.FirstOrDefault(c => c.ItemID == a.ItemID);
            if (b != null)
            {
                b.ItemName = a.ItemName;
                b.Size = a.Size;
                b.Price = a.Price;
                _con.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var a = _con.Item.FirstOrDefault(c => c.ItemID == id);
            if (a != null)
            {
                _con.Item.Remove(a);
                _con.SaveChanges();
            }
        }
    }
}