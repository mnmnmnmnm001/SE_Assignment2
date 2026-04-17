// ============================================================
//  UI/LoginForm.cs
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using WinformApp.BLL;
using WinformApp.DTO;

namespace WinformApp.UI
{
    public class LoginForm : Form
    {
        private TextBox    txtUsername, txtPassword;
        private Button     btnLogin, btnExit;
        private Label      lblTitle, lblUser, lblPass, lblStatus;
        private CheckBox   chkShow;

        private readonly UserBLL _bll = new UserBLL();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text            = "Order Management – Login";
            this.Size            = new Size(420, 320);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.BackColor       = Color.White;

            // ── Title
            lblTitle = new Label
            {
                Text      = "🔐  Order Management System",
                Font      = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                AutoSize  = true,
                Location  = new Point(30, 25)
            };

            // ── Username
            lblUser = new Label { Text = "Username:", Location = new Point(30, 80), AutoSize = true };
            txtUsername = new TextBox
            {
                Location = new Point(120, 77),
                Size     = new Size(240, 24),
                Font     = new Font("Segoe UI", 10)
            };

            // ── Password
            lblPass = new Label { Text = "Password:", Location = new Point(30, 120), AutoSize = true };
            txtPassword = new TextBox
            {
                Location      = new Point(120, 117),
                Size          = new Size(240, 24),
                Font          = new Font("Segoe UI", 10),
                PasswordChar  = '●'
            };

            // ── Show password
            chkShow = new CheckBox
            {
                Text     = "Show password",
                Location = new Point(120, 148),
                AutoSize = true
            };
            chkShow.CheckedChanged += (s, e) =>
                txtPassword.PasswordChar = chkShow.Checked ? '\0' : '●';

            // ── Status label
            lblStatus = new Label
            {
                Text      = "",
                ForeColor = Color.Red,
                Location  = new Point(30, 175),
                Size      = new Size(340, 20)
            };

            // ── Buttons
            btnLogin = new Button
            {
                Text      = "Login",
                Location  = new Point(120, 200),
                Size      = new Size(100, 35),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            btnExit = new Button
            {
                Text      = "Exit",
                Location  = new Point(235, 200),
                Size      = new Size(100, 35),
                BackColor = Color.FromArgb(200, 0, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 10)
            };
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.Click += (s, e) => Application.Exit();

            this.Controls.AddRange(new Control[]
            {
                lblTitle, lblUser, txtUsername,
                lblPass,  txtPassword, chkShow,
                lblStatus, btnLogin, btnExit
            });

            // Allow Enter key to login
            this.AcceptButton = btnLogin;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var user = _bll.Login(txtUsername.Text.Trim(), txtPassword.Text);
            if (user == null)
            {
                MessageBox.Show("Invalid credentials or account is locked.");
                txtPassword.Focus();
                return;
            }
            // Pass user to main form
            var main = new MainForm(user);
            main.Show();
            this.Hide();
            main.FormClosed += (s2, e2) => this.Show();
        }
    }
}
