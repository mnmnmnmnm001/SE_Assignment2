using System;
using System.Collections.Generic;
using BLL;
using DAL;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI
{
    public partial class UserWForm : Page
    {
        private readonly UserS _r = new UserS();
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
            var a = new User
            {
                UserName = txtName.Text,
                email = txtEmail.Text,
                password = _r.EncodePass(txtPass.Text),
                Lock = CheckLock.Checked
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
                txtId.Text = a.UserID.ToString();
                txtName.Text = a.UserName;
                txtEmail.Text = a.email;
                CheckLock.Checked = a.Lock;
            }
            else
            {
                Response.Write("404 Not found!");
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var a = new User
            {
                UserID = int.Parse(txtId.Text),
                UserName = txtName.Text,
                email = txtEmail.Text,
                password = _r.EncodePass(txtPass.Text),
                Lock = CheckLock.Checked
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
