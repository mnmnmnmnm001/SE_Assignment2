using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp.DAL;

namespace UI
{
    public partial class AgentForm : Form
    {
        private Agent? _current;

        public AgentForm()
        {
            InitializeComponent();
        }

        private void LoadGrid()
        {
            var items = (new AgentRepository()).GetAll() ?? new List<Agent>();
            dgv.DataSource = null;
            dgv.DataSource = items;

            if (dgv != null && dgv.Columns != null)
            {
                var colId = dgv.Columns["AgentID"];
                if (colId != null)
                {
                    colId.HeaderText = "ID";
                    colId.Width = 40;
                }
                var colName = dgv.Columns["AgentName"];
                if (colName != null) colName.HeaderText = "AgentName";

                var colAddress = dgv.Columns["Address"];
                if (colAddress != null) colAddress.HeaderText = "Address";
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
            _current = row.DataBoundItem as Agent;
            if (_current == null) return;

            txtAgentID.Text = _current.AgentID + "";
            txtAgentName.Text = _current.AgentName;
            txtAddress.Text = _current.Address;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var a = new Agent
            {
                AgentID = _current?.AgentID ?? 0,
                AgentName = txtAgentName.Text.Trim(),
                Address = txtAddress.Text.Trim()
            };
            (new AgentRepository()).Add(a);
            LoadGrid();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (_current == null) return;
            if (MessageBox.Show($"Delete '{_current.AgentName}'?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No) return;
            (new AgentRepository()).Delete(_current.AgentID);
            LoadGrid();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }
    }
}
