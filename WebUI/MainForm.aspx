<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="WebUI.MainForm" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2 id="hdr">Main</h2>
            <asp:Button ID="btnUser" runat="server" Text="User" OnClick="btnUser_Click" />
            <br /><br />
            <a href="UserWForm.aspx">Manage Users</a><br />
            <a href="ItemWForm.aspx">Manage Items</a><br />
            <a href="AgentWForm.aspx">Manage Agents</a><br />
            <a href="Order1WForm.aspx">Manage Orders</a><br />
            <a href="OrderDetailWForm.aspx">Manage Order Details</a><br />
            <br />
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
        </div>
    </form>
</body>
</html>