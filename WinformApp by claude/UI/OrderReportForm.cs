// ============================================================
//  UI/OrderReportForm.cs  –  Print preview / report for an order
// ============================================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using WinformApp.BLL;
using WinformApp.DTO;

namespace WinformApp.UI
{
    public class OrderReportForm : Form
    {
        private readonly int      _orderID;
        private OrderDTO          _order;
        private List<OrderDetailDTO> _details;

        private RichTextBox rtb;
        private Button      btnPrint, btnClose;

        private readonly OrderBLL _bll = new OrderBLL();

        public OrderReportForm(int orderID)
        {
            _orderID = orderID;
            InitializeComponent();
            LoadData();
            RenderReport();
        }

        private void InitializeComponent()
        {
            this.Text          = $"Order Report – #{_orderID}";
            this.Size          = new Size(620, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor     = Color.White;

            rtb = new RichTextBox
            {
                Location   = new Point(10, 10),
                Size       = new Size(585, 600),
                ReadOnly   = true,
                BorderStyle = BorderStyle.FixedSingle,
                Font       = new Font("Courier New", 10),
                BackColor  = Color.White
            };

            btnPrint = new Button
            {
                Text = "🖨  Print",  Location = new Point(10, 620), Size = new Size(120, 35),
                BackColor = Color.FromArgb(0,102,204), ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Click += BtnPrint_Click;

            btnClose = new Button
            {
                Text = "Close", Location = new Point(480, 620), Size = new Size(110, 35),
                BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s,e) => this.Close();

            this.Controls.AddRange(new Control[] { rtb, btnPrint, btnClose });
        }

        private void LoadData()
        {
            _order   = _bll.GetByID(_orderID);
            _details = _bll.GetDetails(_orderID);
        }

        private void RenderReport()
        {
            if (_order == null) { rtb.Text = "Order not found."; return; }
            var sb = new System.Text.StringBuilder();
            string line = new string('─', 60);

            sb.AppendLine("        ORDER MANAGEMENT SYSTEM");
            sb.AppendLine("             ORDER RECEIPT");
            sb.AppendLine(line);
            sb.AppendLine($"  Order No : #{_order.OrderID}");
            sb.AppendLine($"  Date     : {_order.OrderDate:dd/MM/yyyy}");
            sb.AppendLine($"  Agent    : {_order.AgentName}");
            if (!string.IsNullOrEmpty(_order.Note))
            sb.AppendLine($"  Note     : {_order.Note}");
            sb.AppendLine(line);
            sb.AppendLine($"  {"Item",-28} {"Qty",5} {"Price",12} {"Total",12}");
            sb.AppendLine(line);

            foreach (var d in _details)
            {
                sb.AppendLine($"  {d.ItemName,-28} {d.Quantity,5} {d.UnitAmount,12:N0} {d.LineTotal,12:N0}");
            }

            sb.AppendLine(line);
            decimal grandTotal = _details.Sum(d => d.LineTotal);
            sb.AppendLine($"  {"GRAND TOTAL",48} {grandTotal,12:N0}");
            sb.AppendLine(line);
            sb.AppendLine();
            sb.AppendLine("  Thank you for your business!");
            sb.AppendLine($"  Printed: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");

            rtb.Text = sb.ToString();
        }

        // ── Printing ──────────────────────────────────────────────

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            var pd = new PrintDocument();
            pd.PrintPage += (s, ev) => PrintPage(ev);

            var dialog = new PrintDialog { Document = pd };
            if (dialog.ShowDialog() == DialogResult.OK)
                pd.Print();
        }

        private void PrintPage(PrintPageEventArgs e)
        {
            var font     = new Font("Courier New", 10);
            var boldFont = new Font("Courier New", 11, FontStyle.Bold);
            var brush    = Brushes.Black;

            float x = 50, y = 60, lineH = font.GetHeight(e.Graphics) + 2;

            void PrintLine(string text, bool bold = false)
            {
                e.Graphics.DrawString(text, bold ? boldFont : font, brush, x, y);
                y += lineH;
            }

            string sep = new string('─', 60);

            PrintLine("        ORDER MANAGEMENT SYSTEM", true);
            PrintLine("             ORDER RECEIPT", true);
            PrintLine(sep);
            PrintLine($"Order No : #{_order.OrderID}");
            PrintLine($"Date     : {_order.OrderDate:dd/MM/yyyy}");
            PrintLine($"Agent    : {_order.AgentName}");
            PrintLine(sep);
            PrintLine($"{"Item",-30} {"Qty",4} {"Price",12} {"Total",12}");
            PrintLine(sep);

            foreach (var d in _details)
                PrintLine($"{d.ItemName,-30} {d.Quantity,4} {d.UnitAmount,12:N0} {d.LineTotal,12:N0}");

            PrintLine(sep);
            decimal total = _details.Sum(d => d.LineTotal);
            PrintLine($"{"GRAND TOTAL",50} {total,12:N0}", true);
            PrintLine(sep);
            PrintLine($"\nThank you!   Printed: {DateTime.Now:dd/MM/yyyy HH:mm}");
        }
    }
}
