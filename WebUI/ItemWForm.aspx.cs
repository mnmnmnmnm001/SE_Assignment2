using System;
using System.Web.UI;
using BLL;
using WindowsFormsApp.DAL;

namespace WebUI
{
    public partial class ItemWForm : Page
    {
        private readonly ItemS _s = new ItemS();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gv.DataSource = _s.GetAll();
            gv.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtPrice.Text, out decimal price))
            {
                var it = new Item
                {
                    ItemName = txtName.Text,
                    Price = price
                };
                _s.Add(it);
                Response.Write("Item added successfully!");
                BindGrid();
            }
            else
            {
                Response.Write("Invalid price.");
            }
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                var it = _s.GetByID(id);
                if (it != null)
                {
                    txtId.Text = it.ItemID.ToString();
                    txtName.Text = it.ItemName;
                    txtPrice.Text = it.Price.ToString();
                }
                else
                {
                    Response.Write("Item not found!");
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id) && decimal.TryParse(txtPrice.Text, out decimal price))
            {
                var it = new Item
                {
                    ItemID = id,
                    ItemName = txtName.Text,
                    Price = price
                };
                _s.Update(it);
                Response.Write("Item updated successfully!");
                BindGrid();
            }
            else
            {
                Response.Write("Invalid input for update.");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                _s.Delete(id);
                Response.Write("Item deleted successfully!");
                BindGrid();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}
