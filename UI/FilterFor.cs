// ============================================================
//  UI/FilterForm.cs  –  Filter: best items, items by agent,
//                        agents who purchased an item
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using WinformApp.BLL;
using WinformApp.DTO;

namespace WinformApp.UI
{
    public class FilterForm : Form
    {
        private TabControl tabs;

        // Tab 1 – Best selling items
        private NumericUpDown nudTop;
        private Button        btnBestItems;
        private DataGridView  dgvBestItems;

        // Tab 2 – Items purchased by a specific agent
        private ComboBox     cboAgentFilter;
        private Button       btnItemsByAgent;
        private DataGridView dgvItemsByAgent;

        // Tab 3 – Agents who purchased a specific item
        private ComboBox     cboItemFilter;
        private Button       btnAgentsByItem;
        private DataGridView dgvAgentsByItem;

        // Tab 4 – Agent purchase summary
        private Button       btnAgentSummary;
        private DataGridView dgvAgentSummary;

        private readonly ItemBLL  _itemBLL  = new ItemBLL();
        private readonly AgentBLL _agentBLL = new AgentBLL();

        public FilterForm()
        {
            InitializeComponent();
            PopulateCombos();
        }

        private void InitializeComponent()
        {
            this.Text          = "Filter & Reports";
            this.Size          = new Size(850, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor     = Color.FromArgb(245, 247, 250);

            tabs = new TabControl { Dock = DockStyle.Fill };

            // ── Tab 1: Best Selling Items ─────────────────────
            var tab1 = new TabPage("🏆 Best Items");
            nudTop = new NumericUpDown { Location = new Point(140, 12), Size = new Size(60, 24), Minimum = 1, Maximum = 100, Value = 10 };
            tab1.Controls.Add(new Label { Text = "Show Top N items:", Location = new Point(10, 15), AutoSize = true });
            tab1.Controls.Add(nudTop);
            btnBestItems = StyledBtn("Show", Color.FromArgb(0,102,204), new Point(215, 10), 80);
            btnBestItems.Click += (s,e) => LoadBestItems();
            tab1.Controls.Add(btnBestItems);

            dgvBestItems = MakeGrid(new Point(10, 50), new Size(800, 450));
            tab1.Controls.Add(dgvBestItems);

            // ── Tab 2: Items by Agent ─────────────────────────
            var tab2 = new TabPage("🤝 Items by Agent");
            cboAgentFilter = new ComboBox { Location = new Point(110, 12), Size = new Size(260, 24),
                                            DropDownStyle = ComboBoxStyle.DropDownList };
            tab2.Controls.Add(new Label { Text = "Select Agent:", Location = new Point(10, 15), AutoSize = true });
            tab2.Controls.Add(cboAgentFilter);
            btnItemsByAgent = StyledBtn("Show", Color.FromArgb(60,179,113), new Point(385, 10), 80);
            btnItemsByAgent.Click += (s,e) => LoadItemsByAgent();
            tab2.Controls.Add(btnItemsByAgent);
            dgvItemsByAgent = MakeGrid(new Point(10, 50), new Size(800, 450));
            tab2.Controls.Add(dgvItemsByAgent);

            // ── Tab 3: Agents by Item ─────────────────────────
            var tab3 = new TabPage("🛒 Agents by Item");
            cboItemFilter = new ComboBox { Location = new Point(100, 12), Size = new Size(300, 24),
                                           DropDownStyle = ComboBoxStyle.DropDownList };
            tab3.Controls.Add(new Label { Text = "Select Item:", Location = new Point(10, 15), AutoSize = true });
            tab3.Controls.Add(cboItemFilter);
            btnAgentsByItem = StyledBtn("Show", Color.FromArgb(255,140,0), new Point(415, 10), 80);
            btnAgentsByItem.Click += (s,e) => LoadAgentsByItem();
            tab3.Controls.Add(btnAgentsByItem);
            dgvAgentsByItem = MakeGrid(new Point(10, 50), new Size(800, 450));
            tab3.Controls.Add(dgvAgentsByItem);

            // ── Tab 4: Agent Purchase Summary ────────────────
            var tab4 = new TabPage("📊 Agent Summary");
            btnAgentSummary = StyledBtn("Refresh", Color.FromArgb(138,43,226), new Point(10, 10), 100);
            btnAgentSummary.Click += (s,e) => LoadAgentSummary();
            tab4.Controls.Add(btnAgentSummary);
            dgvAgentSummary = MakeGrid(new Point(10, 50), new Size(800, 450));
            tab4.Controls.Add(dgvAgentSummary);

            tabs.TabPages.AddRange(new[] { tab1, tab2, tab3, tab4 });
            this.Controls.Add(tabs);
        }

        private void PopulateCombos()
        {
            cboAgentFilter.DataSource    = _agentBLL.GetAll();
            cboAgentFilter.DisplayMember = "AgentName";
            cboAgentFilter.ValueMember   = "AgentID";

            cboItemFilter.DataSource    = _itemBLL.GetAll();
            cboItemFilter.DisplayMember = "ItemName";
            cboItemFilter.ValueMember   = "ItemID";
        }

        private void LoadBestItems()
        {
            var data = _itemBLL.GetBestItems((int)nudTop.Value);
            dgvBestItems.DataSource = null;
            dgvBestItems.DataSource = data;
            FormatGrid(dgvBestItems);
            if (dgvBestItems.Columns.Contains("TotalRevenue"))
                dgvBestItems.Columns["TotalRevenue"].DefaultCellStyle.Format = "N0";
        }

        private void LoadItemsByAgent()
        {
            if (cboAgentFilter.SelectedValue == null) return;
            var data = _itemBLL.GetItemsByAgent((int)cboAgentFilter.SelectedValue);
            dgvItemsByAgent.DataSource = null;
            dgvItemsByAgent.DataSource = data;
            FormatGrid(dgvItemsByAgent);
        }

        private void LoadAgentsByItem()
        {
            if (cboItemFilter.SelectedValue == null) return;
            var data = _agentBLL.GetAgentsByItem((int)cboItemFilter.SelectedValue);
            dgvAgentsByItem.DataSource = null;
            dgvAgentsByItem.DataSource = data;
            FormatGrid(dgvAgentsByItem);
        }

        private void LoadAgentSummary()
        {
            var data = _agentBLL.GetPurchaseSummary();
            dgvAgentSummary.DataSource = null;
            dgvAgentSummary.DataSource = data;
            FormatGrid(dgvAgentSummary);
            if (dgvAgentSummary.Columns.Contains("TotalSpent"))
                dgvAgentSummary.Columns["TotalSpent"].DefaultCellStyle.Format = "N0";
        }

        private void FormatGrid(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // hide ID columns
            foreach (DataGridViewColumn c in dgv.Columns)
                if (c.Name.EndsWith("ID")) c.Visible = false;
        }

        private DataGridView MakeGrid(Point loc, Size size) =>
            new DataGridView
            {
                Location        = loc, Size = size,
                ReadOnly        = true,
                SelectionMode   = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White, BorderStyle = BorderStyle.None
            };

        private Button StyledBtn(string text, Color color, Point loc, int width)
        {
            var b = new Button { Text = text, Location = loc, Size = new Size(width, 28),
                                 BackColor = color, ForeColor = Color.White,
                                 FlatStyle = FlatStyle.Flat };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }
    }
}
