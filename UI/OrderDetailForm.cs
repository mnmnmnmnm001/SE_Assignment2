using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp.DAL;

namespace UI
{
    public partial class OrderDetailForm : Form
    {
        private OrderDetail? _current;

        public OrderDetailForm()
        {
            InitializeComponent();
        }

        private void LoadGrid()
        {
            var items = (new OrderDetailRepository()).GetAll() ?? new List<OrderDetail>();
            dgv.DataSource = null;
            dgv.DataSource = items;

            if (dgv != null && dgv.Columns != null)
            {
                var colId = dgv.Columns["ID"];
                if (colId != null)
                {
                    colId.HeaderText = "ID";
                    colId.Width = 40;
                }
                var colOrder = dgv.Columns["OrderID"];
                if (colOrder != null) colOrder.HeaderText = "OrderID";
                var colItem = dgv.Columns["ItemID"];
                if (colItem != null) colItem.HeaderText = "ItemID";
                var colQty = dgv.Columns["Quantity"];
                if (colQty != null) colQty.HeaderText = "Quantity";
                var colAmt = dgv.Columns["UnitAmount"];
                if (colAmt != null) colAmt.HeaderText = "UnitAmount";
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
            _current = row.DataBoundItem as OrderDetail;
            if (_current == null) return;

            txtID.Text = _current.ID + "";
            txtOrderID.Text = _current.OrderID + "";
            txtItemID.Text = _current.ItemID + "";
            txtQuantity.Text = _current.Quantity + "";
            txtUnitAmount.Text = _current.UnitAmount.ToString("N0");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtOrderID.Text, out int oid)) { MessageBox.Show("Invalid OrderID"); return; }
            if (!int.TryParse(txtItemID.Text, out int iid)) { MessageBox.Show("Invalid ItemID"); return; }
            if (!int.TryParse(txtQuantity.Text, out int qty)) { MessageBox.Show("Invalid Quantity"); return; }
            if (!decimal.TryParse(txtUnitAmount.Text.Replace(",", ""), out decimal amt)) { MessageBox.Show("Invalid UnitAmount"); return; }

            var od = new OrderDetail
            {
                ID = _current?.ID ?? 0,
                OrderID = oid,
                ItemID = iid,
                Quantity = qty,
                UnitAmount = amt
            };
            (new OrderDetailRepository()).Add(od);
            LoadGrid();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (_current == null) return;
            if (MessageBox.Show($"Delete detail ID '{_current.ID}'?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No) return;
            (new OrderDetailRepository()).Delete(_current.ID);
            LoadGrid();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }
    }
}
