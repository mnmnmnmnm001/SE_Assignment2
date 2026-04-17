using System;
using System.Web.UI;

namespace WebUI
{
    public partial class OrderDetailWForm : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainForm.aspx");
        }
    }
}
