// ============================================================
//  UI/ItemForm.cs  –  Full CRUD for Items
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using WinformApp.BLL;
using WinformApp.DTO;

namespace WinformApp.UI
{
    public class ItemForm : Form
    {
        // Left: grid
        private DataGridView dgv;
        private TextBox      txtSearch;
        private Button       btnSearch, btnNew;

        // Right: input panel
        private Panel      pnlInput;
        private TextBox    txtItemName, txtSize, txtUnit, txtUnitPrice, txtStock, txtDesc;
        private Label      lblFormTitle;
        private Button     btnSave, btnDelete, btnClear;

        private readonly ItemBLL _bll = new ItemBLL();
        private ItemDTO _current = null;

        public ItemForm()
        {
            InitializeComponent();
            LoadGrid();
        }

        private void InitializeComponent()
        {
            this.Text          = "Item Management";
            this.Size          = new Size(900, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor     = Color.FromArgb(245, 247, 250);

            // ── Search bar
            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.White,
                                     Padding = new Padding(8) };
            txtSearch = new TextBox { Location = new Point(8, 10), Size = new Size(300, 26),
                                      PlaceholderText = "Search item name…" };
            btnSearch = new Button { Text = "Search", Location = new Point(318, 8), Size = new Size(80, 28),
                                     BackColor = Color.FromArgb(0,102,204), ForeColor = Color.White,
                                     FlatStyle = FlatStyle.Flat };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += (s,e) => LoadGrid(txtSearch.Text.Trim());

            btnNew = new Button { Text = "+ New", Location = new Point(415, 8), Size = new Size(80, 28),
                                  BackColor = Color.FromArgb(60,179,113), ForeColor = Color.White,
                                  FlatStyle = FlatStyle.Flat };
            btnNew.FlatAppearance.BorderSize = 0;
            btnNew.Click += (s,e) => ClearForm(true);
            pnlTop.Controls.AddRange(new Control[] { txtSearch, btnSearch, btnNew });

            // ── DataGridView
            dgv = new DataGridView
            {
                Location        = new Point(8, 48),
                Size            = new Size(555, 450),
                ReadOnly        = true,
                SelectionMode   = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows    = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor       = Color.White,
                BorderStyle           = BorderStyle.None
            };
            dgv.SelectionChanged += Dgv_SelectionChanged;

            // ── Right input panel
            pnlInput = new Panel
            {
                Location  = new Point(575, 48),
                Size      = new Size(300, 450),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblFormTitle = new Label { Text = "Item Details", Font = new Font("Segoe UI", 11, FontStyle.Bold),
                                       Location = new Point(10, 10), AutoSize = true,
                                       ForeColor = Color.FromArgb(0,102,204) };

            int y = 45;
            void AddField(string label, ref TextBox tb, bool multiline = false)
            {
                pnlInput.Controls.Add(new Label { Text = label, Location = new Point(10, y), AutoSize = true });
                tb = new TextBox { Location = new Point(10, y + 20), Size = new Size(278, multiline ? 50 : 24),
                                   Multiline = multiline, Font = new Font("Segoe UI", 9) };
                pnlInput.Controls.Add(tb);
                y += multiline ? 80 : 50;
            }

            AddField("Item Name *",  ref txtItemName);
            AddField("Size",         ref txtSize);
            AddField("Unit",         ref txtUnit);
            AddField("Unit Price",   ref txtUnitPrice);
            AddField("Stock",        ref txtStock);
            AddField("Description",  ref txtDesc, true);

            btnSave = MakeBtn("💾 Save",   Color.FromArgb(0,102,204), new Point(10, y));   btnSave.Click   += BtnSave_Click;
            btnDelete = MakeBtn("🗑 Delete", Color.FromArgb(200,0,0),  new Point(115, y)); btnDelete.Click += BtnDelete_Click;
            btnClear  = MakeBtn("✖ Clear",  Color.Gray,                new Point(220, y)); btnClear.Click  += (s,e) => ClearForm(false);

            pnlInput.Controls.AddRange(new Control[] { lblFormTitle, btnSave, btnDelete, btnClear });

            this.Controls.AddRange(new Control[] { pnlTop, dgv, pnlInput });
        }

        private Button MakeBtn(string text, Color color, Point loc)
        {
            var b = new Button { Text = text, Location = loc, Size = new Size(95, 30),
                                 BackColor = color, ForeColor = Color.White,
                                 FlatStyle = FlatStyle.Flat };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        private void LoadGrid(string search = "")
        {
            var items = _bll.GetAll();
            if (!string.IsNullOrEmpty(search))
                items = items.FindAll(i => i.ItemName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);

            dgv.DataSource = null;
            dgv.DataSource = items;

            // Hide irrelevant columns
            foreach (DataGridViewColumn col in dgv.Columns)
                col.Visible = col.Name != "Description";

            dgv.Columns["ItemID"].HeaderText    = "ID";
            dgv.Columns["ItemName"].HeaderText  = "Name";
            dgv.Columns["UnitPrice"].HeaderText = "Unit Price";
            dgv.Columns["ItemID"].Width = 40;
        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;
            _current = dgv.SelectedRows[0].DataBoundItem as ItemDTO;
            if (_current == null) return;

            txtItemName.Text  = _current.ItemName;
            txtSize.Text      = _current.Size;
            txtUnit.Text      = _current.Unit;
            txtUnitPrice.Text = _current.UnitPrice.ToString("N0");
            txtStock.Text     = _current.Stock.ToString();
            txtDesc.Text      = _current.Description;
            btnDelete.Enabled = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var item = new ItemDTO
            {
                ItemID      = _current?.ItemID ?? 0,
                ItemName    = txtItemName.Text.Trim(),
                Size        = txtSize.Text.Trim(),
                Unit        = txtUnit.Text.Trim(),
                Description = txtDesc.Text.Trim()
            };
            if (!decimal.TryParse(txtUnitPrice.Text.Replace(",",""), out decimal price) || price < 0)
            { MessageBox.Show("Invalid unit price."); return; }
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            { MessageBox.Show("Invalid stock value."); return; }
            item.UnitPrice = price;
            item.Stock     = stock;

            var (ok, msg) = _bll.Save(item);
            MessageBox.Show(msg);
            if (ok) { LoadGrid(); ClearForm(false); }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_current == null) return;
            if (MessageBox.Show($"Delete '{_current.ItemName}'?", "Confirm",
                    MessageBoxButtons.YesNo) == DialogResult.No) return;
            var (ok, msg) = _bll.Delete(_current.ItemID);
            MessageBox.Show(msg);
            if (ok) { LoadGrid(); ClearForm(false); }
        }

        private void ClearForm(bool isNew)
        {
            _current = null;
            txtItemName.Clear(); txtSize.Clear(); txtUnit.Clear();
            txtUnitPrice.Clear(); txtStock.Clear(); txtDesc.Clear();
            btnDelete.Enabled = !isNew;
            if (isNew) lblFormTitle.Text = "New Item";
            else       lblFormTitle.Text = "Item Details";
        }
    }
}
