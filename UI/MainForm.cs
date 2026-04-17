using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.BLL;
using WindowsFormsApp.DAL;

namespace UI
{
    public partial class MainForm : Form
    {
        private readonly User _user;
        public MainForm(User user)
        {
            _user = user;
            InitializeComponent();
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Log out?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) this.Close();
        }

        private void itemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemForm f = new ItemForm();
            f.Show();
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Order1Form f = new Order1Form();
            f.Show();
        }

        private void agentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgentForm f = new AgentForm();
            f.Show();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            UserForm f = new UserForm();
            f.Show();
        }
    }
}
