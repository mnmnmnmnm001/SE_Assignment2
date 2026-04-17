using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp.BLL;
using WindowsFormsApp.DAL;

namespace UI
{
    public partial class ItemForm : Form
    {
        private Item? _current;

        public ItemForm()
        {
            InitializeComponent();
        }
        private void LoadGrid(string search = "")
        {
            var items = (new ItemRepository()).GetAll() ?? new List<Item>();
            if (!string.IsNullOrEmpty(search))
                items = items.FindAll(i => i.ItemName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);

            dgv.DataSource = null;
            dgv.DataSource = items;

            if (dgv != null && dgv.Columns != null)
            {
                var colId = dgv.Columns["ItemID"];
                if (colId != null)
                {
                    colId.HeaderText = "ID";
                    colId.Width = 40;
                }

                var colName = dgv.Columns["ItemName"];
                if (colName != null) colName.HeaderText = "Name";

                var colSize = dgv.Columns["Size"];
                if (colSize != null) colSize.HeaderText = "Size";

                var colPrice = dgv.Columns["Price"];
                if (colPrice != null) colPrice.HeaderText = "Price";
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
            if (row == null)
            {
                _current = null;
                return;
            }

            _current = row.DataBoundItem as Item;
            if (_current == null) return;

            txtItemName.Text = _current.ItemName;
            txtSize.Text = _current.Size;
            txtItemID.Text = _current.ItemID + "";
            txtPrice.Text = _current.Price.ToString("N0");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var item = new Item
            {
                ItemID = _current?.ItemID ?? 0,
                ItemName = txtItemName.Text.Trim(),
                Size = txtSize.Text.Trim(),
            };
            if (!decimal.TryParse(txtPrice.Text.Replace(",", ""), out decimal price) || price < 0)
            { MessageBox.Show("Invalid unit price."); return; }
            item.Price = price;

            (new ItemRepository()).Add(item);
            LoadGrid();
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (_current == null) return;
            if (MessageBox.Show($"Delete '{_current.ItemName}'?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No) return;
            (new ItemRepository()).Delete(_current.ItemID);
            LoadGrid(); 
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }
    }
}
