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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var a = new UserRepository();
            var user = a.Login(txtName.Text.Trim(), txtPass.Text);
            if (user == null)
            {
                MessageBox.Show("Invalid credentials or account is locked.");
                txtPass.Focus();
                return;
            }
            var main = new UI.MainForm(user);
            main.Show();
            this.Hide();
            main.FormClosed += (s2, e2) => this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

    }
}
