using System;
using System.Web.UI;
using BLL;
using WindowsFormsApp.DAL;

namespace WebUI
{
    public partial class Order1WForm : Page
    {
        private readonly OrderS _s = new OrderS();

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
            if (DateTime.TryParse(txtDate.Text, out DateTime dt) && int.TryParse(txtAgentId.Text, out int agentId))
            {
                var o = new Order1
                {
                    OrderDate = dt,
                    AgentID = agentId
                };
                _s.Add(o);
                Response.Write("Order added successfully!");
                BindGrid();
            }
            else
            {
                Response.Write("Invalid date or agent id.");
            }
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                var o = _s.GetByID(id);
                if (o != null)
                {
                    txtId.Text = o.OrderID.ToString();
                    txtDate.Text = o.OrderDate.ToString("yyyy-MM-dd");
                    txtAgentId.Text = o.AgentID.ToString();
                }
                else
                {
                    Response.Write("Order not found!");
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id) && DateTime.TryParse(txtDate.Text, out DateTime dt) && int.TryParse(txtAgentId.Text, out int agentId))
            {
                var o = new Order1
                {
                    OrderID = id,
                    OrderDate = dt,
                    AgentID = agentId
                };
                _s.Update(o);
                Response.Write("Order updated successfully!");
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
                Response.Write("Order deleted successfully!");
                BindGrid();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}
