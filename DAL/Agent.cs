using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.DAL
{
    public class Agent
    {
        public int AgentID { get; set; }
        public string AgentName { get; set; }
        public string Address { get; set; }
        public override string ToString() => AgentName;
    }
}
