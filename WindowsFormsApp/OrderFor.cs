// ============================================================
//  UI/OrderForm.cs  –  Master-Detail Order Form (One-to-Many)
// ============================================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinformApp.BLL;
using WinformApp.DTO;

namespace WinformApp.UI
{
    public class OrderForm : Form
    {
        // ── Order list (left)
        private DataGridView dgvOrders;
        private Button       btnNewOrder, btnDeleteOrder, btnPrintOrder;

        // ── Order header (top-right)
        private ComboBox    cboAgent;
        private DateTimePicker dtpOrderDate;
        private TextBox    txtNote;
        private Label      lblOrderID, lblTotal;

        // ── Order detail grid (bottom-right)
        private DataGridView         dgvDetails;
        private ComboBox             cboItem;
        private NumericUpDown        nudQty;
        private NumericUpDown        nudPrice;
        private Button               btnAddItem, btnRemoveItem, btnSaveOrder;

        // ── State
        private readonly OrderBLL _orderBLL = new OrderBLL();
        private readonly AgentBLL _agentBLL = new AgentBLL();
        private readonly ItemBLL  _itemBLL  = new ItemBLL();

        private OrderDTO             _currentOrder  = null;
        private List<OrderDetailDTO> _details        = new List<OrderDetailDTO>();

        public OrderForm()
        {
            InitializeComponent();
            LoadOrders();
            PopulateCombos();
        }

        private void InitializeComponent()
        {
            this.Text          = "Order Management";
            this.Size          = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor     = Color.FromArgb(245, 247, 250);

            // ── Left panel: order list ─────────────────────────
            var pnlLeft = new Panel
            {
                Location = new Point(5, 5), Size = new Size(340, 600),
                BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle
            };

            var lblOrders = new Label { Text = "📋 Orders", Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(8, 8), AutoSize = true, ForeColor = Color.FromArgb(255,140,0) };

            dgvOrders = new DataGridView
            {
                Location = new Point(5, 35), Size = new Size(328, 480),
                ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White, BorderStyle = BorderStyle.None
            };
            dgvOrders.SelectionChanged += DgvOrders_SelectionChanged;

            btnNewOrder    = MakeBtn("+ New Order",  Color.FromArgb(60,179,113), new Point(5,   520), 100);
            btnDeleteOrder = MakeBtn("🗑 Delete",     Color.FromArgb(200,0,0),   new Point(115, 520), 80);
            btnPrintOrder  = MakeBtn("🖨 Print",      Color.FromArgb(70,130,180),new Point(205, 520), 80);
            btnNewOrder.Click    += (s,e) => StartNewOrder();
            btnDeleteOrder.Click += BtnDeleteOrder_Click;
            btnPrintOrder.Click  += BtnPrintOrder_Click;

            pnlLeft.Controls.AddRange(new Control[]
            {
                lblOrders, dgvOrders, btnNewOrder, btnDeleteOrder, btnPrintOrder
            });

            // ── Right panel ─────────────────────────────────────
            var pnlRight = new Panel
            {
                Location = new Point(352, 5), Size = new Size(730, 600),
                BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle
            };

            // Header info
            lblOrderID = new Label { Text = "Order ID: (new)", Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(10, 10), AutoSize = true, ForeColor = Color.FromArgb(255,140,0) };

            pnlRight.Controls.Add(lblOrderID);
            pnlRight.Controls.Add(new Label { Text = "Agent:", Location = new Point(10, 45), AutoSize = true });
            cboAgent = new ComboBox { Location = new Point(80, 42), Size = new Size(220, 24),
                                      DropDownStyle = ComboBoxStyle.DropDownList };
            dtpOrderDate = new DateTimePicker { Location = new Point(340, 42), Size = new Size(150, 24), Format = DateTimePickerFormat.Short };
            pnlRight.Controls.Add(new Label { Text = "Date:", Location = new Point(310, 45), AutoSize = true });
            txtNote = new TextBox { Location = new Point(530, 42), Size = new Size(180, 24), PlaceholderText = "Note…" };
            pnlRight.Controls.Add(new Label { Text = "Note:", Location = new Point(500, 45), AutoSize = true });

            pnlRight.Controls.AddRange(new Control[] { cboAgent, dtpOrderDate, txtNote });

            // ── Detail table
            pnlRight.Controls.Add(new Label { Text = "Order Details", Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 80), AutoSize = true });

            dgvDetails = new DataGridView
            {
                Location = new Point(10, 105), Size = new Size(705, 350),
                AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White, BorderStyle = BorderStyle.None
            };
            SetupDetailGrid();

            // ── Add-item row
            pnlRight.Controls.Add(new Label { Text = "Item:", Location = new Point(10, 465), AutoSize = true });
            cboItem = new ComboBox { Location = new Point(55, 462), Size = new Size(250, 24), DropDownStyle = ComboBoxStyle.DropDownList };
            pnlRight.Controls.Add(new Label { Text = "Qty:", Location = new Point(315, 465), AutoSize = true });
            nudQty = new NumericUpDown { Location = new Point(345, 462), Size = new Size(65, 24), Minimum = 1, Maximum = 9999, Value = 1 };
            pnlRight.Controls.Add(new Label { Text = "Price:", Location = new Point(420, 465), AutoSize = true });
            nudPrice = new NumericUpDown { Location = new Point(460, 462), Size = new Size(120, 24), Minimum = 0,
                Maximum = 999999999, DecimalPlaces = 0, ThousandsSeparator = true };

            btnAddItem    = MakeBtn("+ Add",    Color.FromArgb(60,179,113), new Point(595, 460), 65);
            btnRemoveItem = MakeBtn("– Remove", Color.FromArgb(200,0,0),   new Point(10,  500), 90);
            btnSaveOrder  = MakeBtn("💾 Save Order", Color.FromArgb(0,102,204), new Point(560, 497), 140);

            lblTotal = new Label { Text = "Total: 0 ₫", Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(10, 530), AutoSize = true, ForeColor = Color.DarkGreen };

            btnAddItem.Click    += BtnAddItem_Click;
            btnRemoveItem.Click += BtnRemoveItem_Click;
            btnSaveOrder.Click  += BtnSaveOrder_Click;
            cboItem.SelectedIndexChanged += CboItem_SelectedIndexChanged;

            pnlRight.Controls.AddRange(new Control[]
            {
                dgvDetails, cboItem, nudQty, nudPrice,
                btnAddItem, btnRemoveItem, btnSaveOrder, lblTotal
            });

            this.Controls.AddRange(new Control[] { pnlLeft, pnlRight });
        }

        private void SetupDetailGrid()
        {
            dgvDetails.Columns.Clear();
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Item",       DataPropertyName = "ItemName",   ReadOnly = true, FillWeight = 40 });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Qty",        DataPropertyName = "Quantity",   ReadOnly = true, FillWeight = 15 });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Unit Price", DataPropertyName = "UnitAmount", ReadOnly = true, FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Line Total", DataPropertyName = "LineTotal",  ReadOnly = true, FillWeight = 25,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
        }

        // ── Data loading ─────────────────────────────────────────

        private void LoadOrders()
        {
            var orders = _orderBLL.GetAll();
            dgvOrders.DataSource = null;
            dgvOrders.DataSource = orders;
            if (dgvOrders.Columns.Count == 0) return;
            foreach (DataGridViewColumn c in dgvOrders.Columns)
                c.Visible = c.Name is "OrderID" or "OrderDate" or "AgentName" or "TotalAmount";
            dgvOrders.Columns["OrderID"].Width = 40;
            dgvOrders.Columns["TotalAmount"].DefaultCellStyle.Format = "N0";
        }

        private void PopulateCombos()
        {
            cboAgent.DataSource    = _agentBLL.GetAll();
            cboAgent.DisplayMember = "AgentName";
            cboAgent.ValueMember   = "AgentID";

            cboItem.DataSource    = _itemBLL.GetAll();
            cboItem.DisplayMember = "ItemName";
            cboItem.ValueMember   = "ItemID";
        }

        private void RefreshDetails()
        {
            dgvDetails.DataSource = null;
            dgvDetails.DataSource = _details;
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            decimal total = _details.Sum(d => d.LineTotal);
            lblTotal.Text = $"Total: {total:N0} ₫";
        }

        // ── Event handlers ────────────────────────────────────────

        private void DgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0) return;
            _currentOrder = dgvOrders.SelectedRows[0].DataBoundItem as OrderDTO;
            if (_currentOrder == null) return;

            lblOrderID.Text   = $"Order ID: {_currentOrder.OrderID}";
            dtpOrderDate.Value = _currentOrder.OrderDate;
            txtNote.Text       = _currentOrder.Note;

            // Select agent in combo
            foreach (AgentDTO a in cboAgent.Items)
                if (a.AgentID == _currentOrder.AgentID) { cboAgent.SelectedItem = a; break; }

            _details = _orderBLL.GetDetails(_currentOrder.OrderID);
            RefreshDetails();
        }

        private void StartNewOrder()
        {
            _currentOrder      = null;
            _details           = new List<OrderDetailDTO>();
            lblOrderID.Text    = "Order ID: (new)";
            dtpOrderDate.Value = DateTime.Today;
            txtNote.Clear();
            if (cboAgent.Items.Count > 0) cboAgent.SelectedIndex = 0;
            RefreshDetails();
        }

        private void CboItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboItem.SelectedItem is ItemDTO selected)
                nudPrice.Value = (decimal)selected.UnitPrice;
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            if (cboItem.SelectedItem == null) return;
            var item = (ItemDTO)cboItem.SelectedItem;
            // Merge if already in list
            var existing = _details.FirstOrDefault(d => d.ItemID == item.ItemID);
            if (existing != null)
                existing.Quantity += (int)nudQty.Value;
            else
                _details.Add(new OrderDetailDTO
                {
                    ItemID     = item.ItemID,
                    ItemName   = item.ItemName,
                    Quantity   = (int)nudQty.Value,
                    UnitAmount = nudPrice.Value
                });
            RefreshDetails();
            nudQty.Value = 1;
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvDetails.SelectedRows.Count == 0) return;
            var d = dgvDetails.SelectedRows[0].DataBoundItem as OrderDetailDTO;
            if (d != null) { _details.Remove(d); RefreshDetails(); }
        }

        private void BtnSaveOrder_Click(object sender, EventArgs e)
        {
            var order = new OrderDTO
            {
                OrderID   = _currentOrder?.OrderID ?? 0,
                AgentID   = (int)cboAgent.SelectedValue,
                OrderDate = dtpOrderDate.Value,
                Note      = txtNote.Text.Trim()
            };
            var (ok, msg, newID) = _orderBLL.Save(order, _details);
            MessageBox.Show(msg);
            if (ok)
            {
                LoadOrders();
                if (newID > 0)
                {
                    _currentOrder = _orderBLL.GetByID(newID);
                    lblOrderID.Text = $"Order ID: {newID}";
                }
            }
        }

        private void BtnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (_currentOrder == null) { MessageBox.Show("Select an order first."); return; }
            if (MessageBox.Show($"Delete Order #{_currentOrder.OrderID}?", "Confirm",
                    MessageBoxButtons.YesNo) == DialogResult.No) return;
            var (ok, msg) = _orderBLL.Delete(_currentOrder.OrderID);
            MessageBox.Show(msg);
            if (ok) { StartNewOrder(); LoadOrders(); }
        }

        private void BtnPrintOrder_Click(object sender, EventArgs e)
        {
            if (_currentOrder == null) { MessageBox.Show("Select an order first."); return; }
            var report = new OrderReportForm(_currentOrder.OrderID);
            report.ShowDialog();
        }

        private Button MakeBtn(string text, Color color, Point loc, int width)
        {
            var b = new Button { Text = text, Location = loc, Size = new Size(width, 28),
                                 BackColor = color, ForeColor = Color.White,
                                 FlatStyle = FlatStyle.Flat };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }
    }
}
