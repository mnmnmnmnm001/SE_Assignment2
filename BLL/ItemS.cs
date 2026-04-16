using System;
using System.Collections.Generic;
using System.Text;
using WindowsFormsApp.DAL;

namespace BLL
{
    public class ItemS
    {
        private readonly ItemRepository _re;

        public ItemS()
        {
            _re = new ItemRepository();
        }

        public void Add(Item a)
        {
            _re.Add(a);
        }

        public Item GetByID(int id)
        {
            return _re.GetByID(id);
        }
        public List<Item> GetAll()
        {
            return _re.GetAll();
        }

        public void Update(Item a)
        {
            _re.Update(a);
        }

        public void Delete(int id)
        {
            _re.Delete(id);
        }
    }
}