using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp.DAL;

namespace UI
{
    public partial class UserForm : Form
    {
        private User? _current;

        public UserForm()
        {
            InitializeComponent();
        }

        private void LoadGrid()
        {
            var items = (new UserRepository()).GetAll() ?? new List<User>();
            dgv.DataSource = null;
            dgv.DataSource = items;

            if (dgv != null && dgv.Columns != null)
            {
                var colId = dgv.Columns["UserID"];
                if (colId != null)
                {
                    colId.HeaderText = "ID";
                    colId.Width = 40;
                }
                var colName = dgv.Columns["UserName"];
                if (colName != null) colName.HeaderText = "UserName";

                var colEmail = dgv.Columns["email"];
                if (colEmail != null) colEmail.HeaderText = "Email";

                var colLock = dgv.Columns["Lock"];
                if (colLock != null) colLock.HeaderText = "Locked";
            }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv == null || dgv.SelectedRows == null || dgv.SelectedRows.Count == 0)
            {
                _current = null;
                return;
            }
            var row = dgv.SelectedRows[0];
            _current = row.DataBoundItem as User;
            if (_current == null) return;

            txtUserID.Text = _current.UserID + "";
            txtUserName.Text = _current.UserName;
            txtEmail.Text = _current.email;
            txtPassword.Text = _current.password;
            chkLock.Checked = _current.Lock;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User
            {
                UserID = _current?.UserID ?? 0,
                UserName = txtUserName.Text.Trim(),
                email = txtEmail.Text.Trim(),
                password = txtPassword.Text,
                Lock = chkLock.Checked
            };
            (new UserRepository()).Add(u);
            LoadGrid();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (_current == null) return;
            if (MessageBox.Show($"Delete '{_current.UserName}'?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No) return;
            (new UserRepository()).Delete(_current.UserID);
            LoadGrid();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }
    }
}
