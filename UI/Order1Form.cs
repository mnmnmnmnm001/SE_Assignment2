using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp.DAL;

namespace UI
{
    public partial class Order1Form : Form
    {
        private Order1? _current;

        public Order1Form()
        {
            InitializeComponent();
        }

        private void LoadGrid()
        {
            var items = (new OrderRepository()).GetAll() ?? new List<Order1>();
            dgv.DataSource = null;
            dgv.DataSource = items;

            if (dgv != null && dgv.Columns != null)
            {
                var colId = dgv.Columns["OrderID"];
                if (colId != null)
                {
                    colId.HeaderText = "ID";
                    colId.Width = 40;
                }
                var colDate = dgv.Columns["OrderDate"];
                if (colDate != null) colDate.HeaderText = "OrderDate";

                var colAgent = dgv.Columns["AgentID"];
                if (colAgent != null) colAgent.HeaderText = "AgentID";
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
            _current = row.DataBoundItem as Order1;
            if (_current == null) return;

            txtOrderID.Text = _current.OrderID + "";
            txtOrderDate.Text = _current.OrderDate.ToString("yyyy-MM-dd");
            txtAgentID.Text = _current.AgentID + "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(txtOrderDate.Text, out DateTime dt))
            {
                MessageBox.Show("Invalid date format. Use yyyy-MM-dd.");
                return;
            }
            if (!int.TryParse(txtAgentID.Text, out int aid))
            {
                MessageBox.Show("Invalid AgentID.");
                return;
            }

            var o = new Order1
            {
                OrderID = _current?.OrderID ?? 0,
                OrderDate = dt,
                AgentID = aid
            };
            (new OrderRepository()).Add(o);
            LoadGrid();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (_current == null) return;
            if (MessageBox.Show($"Delete Order '{_current.OrderID}'?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No) return;
            (new OrderRepository()).Delete(_current.OrderID);
            LoadGrid();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }
    }
}
