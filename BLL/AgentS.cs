using System;
using System.Collections.Generic;
using System.Text;
using WindowsFormsApp.DAL;

namespace BLL
{
    public class AgentS
    {
        private readonly AgentRepository _re;

        public AgentS()
        {
            _re = new AgentRepository();
        }

        public void Add(Agent a)
        {
            _re.Add(a);
        }

        public Agent GetByID(int id)
        {
            return _re.GetByID(id);
        }
        public List<Agent> GetAll()
        {
            return _re.GetAll();
        }

        public void Update(Agent a)
        {
            _re.Update(a);
        }

        public void Delete(int id)
        {
            _re.Delete(id);
        }
    }
}