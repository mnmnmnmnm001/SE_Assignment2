using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;

namespace WebUI
{
    public partial class About : Page
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
            password = txtPass.Text
            //Lock = txtEmail.Text //i don't know how to put a checkbox for lock
        };
        _r.Add(a);
        Response.Write("User added successfully!");
        BindGrid();
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        int id = int.Parse(txtId.Text);
        var a = _r.GetByID(id);
        if (a != null)
        {
            txtID.Text = a.UserID;
            txtName.Text = a.UserName;
            txtEmail.Text = a.email;
            txtPass.Text = a.password;
            //txtPass.Text = a.Lock;
        }
        else
        {
            Response.Write("Product not found!");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        var a = new User
        {
            UserID = int.Parse(txtId.Text),
            UserName = txtName.Text,
            email = txtEmail.Text,
            pass = txtPass.Text,
            //Lock = 
        };
        _r.UpdateProduct(a);
        Response.Write("User updated successfully!");
        BindGrid();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(txtId.Text);
        _r.Delete(id);
        Response.Write("User deleted successfully!");
        BindGrid();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MainForm.aspx");
    }
}
}
