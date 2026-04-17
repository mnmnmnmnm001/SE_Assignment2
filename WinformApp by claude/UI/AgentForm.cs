// ============================================================
//  UI/AgentForm.cs  –  Full CRUD for Agents
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using WinformApp.BLL;
using WinformApp.DTO;

namespace WinformApp.UI
{
    public class AgentForm : Form
    {
        private DataGridView dgv;
        private TextBox      txtSearch;
        private Button       btnSearch, btnNew;

        private Panel   pnlInput;
        private TextBox txtAgentName, txtAddress, txtPhone, txtEmail;
        private Label   lblFormTitle;
        private Button  btnSave, btnDelete, btnClear;

        private readonly AgentBLL _bll = new AgentBLL();
        private AgentDTO _current = null;

        public AgentForm()
        {
            InitializeComponent();
            LoadGrid();
        }

        private void InitializeComponent()
        {
            this.Text          = "Agent Management";
            this.Size          = new Size(900, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor     = Color.FromArgb(245, 247, 250);

            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 45,
                                     BackColor = Color.White, Padding = new Padding(8) };
            txtSearch = new TextBox { Location = new Point(8, 10), Size = new Size(280, 26),
                                      PlaceholderText = "Search agent name…" };
            btnSearch = StyledBtn("Search", Color.FromArgb(0,102,204), new Point(298, 8), 80);
            btnSearch.Click += (s,e) => LoadGrid(txtSearch.Text.Trim());
            btnNew = StyledBtn("+ New", Color.FromArgb(60,179,113), new Point(390, 8), 80);
            btnNew.Click += (s,e) => ClearForm();
            pnlTop.Controls.AddRange(new Control[] { txtSearch, btnSearch, btnNew });

            dgv = new DataGridView
            {
                Location = new Point(8, 48), Size = new Size(555, 420),
                ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White, BorderStyle = BorderStyle.None
            };
            dgv.SelectionChanged += Dgv_SelectionChanged;

            pnlInput = new Panel
            {
                Location = new Point(575, 48), Size = new Size(300, 420),
                BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle
            };

            lblFormTitle = new Label { Text = "Agent Details",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(10, 10), AutoSize = true,
                ForeColor = Color.FromArgb(60,179,113) };

            int y = 45;
            void AddField(string label, ref TextBox tb)
            {
                pnlInput.Controls.Add(new Label { Text = label, Location = new Point(10, y), AutoSize = true });
                tb = new TextBox { Location = new Point(10, y+20), Size = new Size(278, 24),
                                   Font = new Font("Segoe UI", 9) };
                pnlInput.Controls.Add(tb);
                y += 52;
            }

            AddField("Agent Name *", ref txtAgentName);
            AddField("Address *",    ref txtAddress);
            AddField("Phone",        ref txtPhone);
            AddField("Email",        ref txtEmail);

            var btnS = StyledBtn("💾 Save",    Color.FromArgb(0,102,204), new Point(10,  y), 90); btnS.Click   += BtnSave_Click;
            var btnD = StyledBtn("🗑 Delete",  Color.FromArgb(200,0,0),   new Point(110, y), 90); btnD.Click   += BtnDelete_Click;
            var btnC = StyledBtn("✖ Clear",   Color.Gray,                 new Point(210, y), 80); btnC.Click   += (s,e) => ClearForm();
            btnDelete = btnD;

            pnlInput.Controls.AddRange(new Control[] { lblFormTitle, btnS, btnD, btnC });
            this.Controls.AddRange(new Control[] { pnlTop, dgv, pnlInput });
        }

        private Button StyledBtn(string text, Color color, Point loc, int width)
        {
            var b = new Button { Text = text, Location = loc, Size = new Size(width, 30),
                                 BackColor = color, ForeColor = Color.White,
                                 FlatStyle = FlatStyle.Flat };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        private Button btnDelete; // ref kept for Enabled toggle

        private void LoadGrid(string search = "")
        {
            var list = _bll.GetAll();
            if (!string.IsNullOrEmpty(search))
                list = list.FindAll(a => a.AgentName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            dgv.DataSource = null;
            dgv.DataSource = list;
            dgv.Columns["AgentID"].HeaderText = "ID";
            dgv.Columns["AgentID"].Width = 40;
        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;
            _current = dgv.SelectedRows[0].DataBoundItem as AgentDTO;
            if (_current == null) return;
            txtAgentName.Text = _current.AgentName;
            txtAddress.Text   = _current.Address;
            txtPhone.Text     = _current.Phone;
            txtEmail.Text     = _current.Email;
            btnDelete.Enabled = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var agent = new AgentDTO
            {
                AgentID   = _current?.AgentID ?? 0,
                AgentName = txtAgentName.Text.Trim(),
                Address   = txtAddress.Text.Trim(),
                Phone     = txtPhone.Text.Trim(),
                Email     = txtEmail.Text.Trim()
            };
            var (ok, msg) = _bll.Save(agent);
            MessageBox.Show(msg);
            if (ok) { LoadGrid(); ClearForm(); }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_current == null) return;
            if (MessageBox.Show($"Delete '{_current.AgentName}'?", "Confirm",
                    MessageBoxButtons.YesNo) == DialogResult.No) return;
            var (ok, msg) = _bll.Delete(_current.AgentID);
            MessageBox.Show(msg);
            if (ok) { LoadGrid(); ClearForm(); }
        }

        private void ClearForm()
        {
            _current = null;
            txtAgentName.Clear(); txtAddress.Clear();
            txtPhone.Clear(); txtEmail.Clear();
            lblFormTitle.Text = "Agent Details";
            btnDelete.Enabled = false;
        }
    }
}
