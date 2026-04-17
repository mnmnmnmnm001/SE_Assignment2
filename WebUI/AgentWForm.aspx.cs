using System;
using System.Web.UI;
using BLL;
using WindowsFormsApp.DAL;

namespace WebUI
{
    public partial class AgentWForm : Page
    {
        private readonly AgentS _s = new AgentS();

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
            var ag = new Agent
            {
                AgentName = txtName.Text,
                Address = txtAddress.Text
            };
            _s.Add(ag);
            Response.Write("Agent added successfully!");
            BindGrid();
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                var ag = _s.GetByID(id);
                if (ag != null)
                {
                    txtId.Text = ag.AgentID.ToString();
                    txtName.Text = ag.AgentName;
                    txtAddress.Text = ag.Address;
                }
                else
                {
                    Response.Write("Agent not found!");
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                var ag = new Agent
                {
                    AgentID = id,
                    AgentName = txtName.Text,
                    Address = txtAddress.Text
                };
                _s.Update(ag);
                Response.Write("Agent updated successfully!");
                BindGrid();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                _s.Delete(id);
                Response.Write("Agent deleted successfully!");
                BindGrid();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}
