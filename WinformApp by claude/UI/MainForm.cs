// ============================================================
//  UI/MainForm.cs
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using WinformApp.DTO;

namespace WinformApp.UI
{
    public class MainForm : Form
    {
        private readonly UserDTO _user;

        public MainForm(UserDTO user)
        {
            _user = user;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text          = $"Order Management System  —  Welcome, {_user.UserName}";
            this.Size          = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor     = Color.FromArgb(245, 247, 250);

            // ── Header panel
            var pnlHeader = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 60,
                BackColor = Color.FromArgb(0, 102, 204)
            };
            var lblHeader = new Label
            {
                Text      = "📦  Order Management System",
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize  = true,
                Location  = new Point(20, 15)
            };
            pnlHeader.Controls.Add(lblHeader);

            // ── Button table
            var pnl = new TableLayoutPanel
            {
                Location    = new Point(80, 100),
                Size        = new Size(540, 280),
                ColumnCount = 3,
                RowCount    = 2,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };
            pnl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            pnl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            pnl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            pnl.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            pnl.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            pnl.Controls.Add(MakeNavButton("🛒  Items",          Color.FromArgb( 70,130,180), () => new ItemForm().ShowDialog()),        0, 0);
            pnl.Controls.Add(MakeNavButton("🤝  Agents",         Color.FromArgb( 60,179,113), () => new AgentForm().ShowDialog()),       1, 0);
            pnl.Controls.Add(MakeNavButton("📋  Orders",         Color.FromArgb(255,140,  0), () => new OrderForm().ShowDialog()),       2, 0);
            pnl.Controls.Add(MakeNavButton("📊  Filter/Reports", Color.FromArgb(138, 43,226), () => new FilterForm().ShowDialog()),      0, 1);
            pnl.Controls.Add(MakeNavButton("👤  Users",          Color.FromArgb( 99, 99, 99), () => MessageBox.Show("User management coming soon.")), 1, 1);
            pnl.Controls.Add(MakeNavButton("🚪  Logout",         Color.FromArgb(200,  0,  0), Logout),                                  2, 1);

            // ── Status bar
            var statusBar = new StatusStrip();
            statusBar.Items.Add(new ToolStripStatusLabel(
                $"Logged in as: {_user.UserName}  |  {DateTime.Now:dd/MM/yyyy HH:mm}"));

            this.Controls.AddRange(new Control[] { pnlHeader, pnl, statusBar });
        }

        private Button MakeNavButton(string text, Color color, Action onClick)
        {
            var btn = new Button
            {
                Text      = text,
                Dock      = DockStyle.Fill,
                Margin    = new Padding(8),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor    = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => onClick();
            return btn;
        }

        private void Logout()
        {
            var result = MessageBox.Show("Log out?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) this.Close();
        }
    }
}
