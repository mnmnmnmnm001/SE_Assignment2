using System;
using System.Web.UI;
using BLL;
using DAL;

namespace WebUI
{
    public partial class OrderDetailWForm : Page
    {
        private readonly OrderDetailS _r = new OrderDetailS();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            var a = _r.GetAll();
            gv.DataSource = a;
            gv.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var a = new OrderDetail
            {
                ID = txtId.Text,
                OrderID = txtOrderID.Text,
                ItemID = txtItemID.Text,
                Quantity = txtQuantity.Text,
                UnitAmount = txtUnitAmount.Text
            };
            _r.Add(a);
            Response.Write("Obj Added successfully!");
            BindGrid();
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            var a = _r.GetByID(int.Parse(txtId.Text));
            if (a != null)
            {
                txtId.Text = a.ID;
                txtOrderID.Text = a.OrderID;
                txtItemID.Text = a.ItemID;
                txtQuantity.Text = a.Quantity;
                txtUnitAmount.Text = a.UnitAmount;
            }
            else
            {
                Response.Write("404 Not found!");
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var a = new OrderDetail
            {
                ID = int.Parse(txtId.Text),
                OrderID = txtOrderID.Text,
                ItemID = txtItemID.Text,
                Quantity = txtQuantity.Text,
                UnitAmount = txtUnitAmount.Text
            };
            _r.UpdateProduct(a);
            Response.Write("Obj Updated successfully!");
            BindGrid();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            _r.Delete(id);
            Response.Write("Obj Deleted successfully!");
            BindGrid();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}
