using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using WindowsFormsApp.BLL;

namespace WindowsFormsApp
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

        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
