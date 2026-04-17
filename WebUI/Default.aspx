<%@ Page Language="C#" AutoEventWireup="true"CodeBehind="Default.aspx.cs" Inherits="WebUI.CustomerWForm" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Login</title>
</head>
<body>
    <form id="Form1" runat="server">
        <div>
            <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br/>
            <asp:Label ID="lblPass" runat="server" Text="Email:"></asp:Label>
            <asp:TextBox ID="txtPass" runat="server"></asp:TextBox><br />
            <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click"/>
            <br/><asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"/><br/>
        </div>
    </form>
</body>
</html>