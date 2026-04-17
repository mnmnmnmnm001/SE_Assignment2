<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order1WForm.aspx.cs" Inherits="WebUI.Order1WForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Order Management</h2>
            <asp:Label ID="lblId" runat="server" Text="Order ID:"></asp:Label>
            <asp:TextBox ID="txtId" runat="server"></asp:TextBox><br/>
            <asp:Label ID="lblDate" runat="server" Text="Date:"></asp:Label>
            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox><br/>
            <asp:Label ID="lblAgentId" runat="server" Text="Agent ID:"></asp:Label>
            <asp:TextBox ID="txtAgentId" runat="server"></asp:TextBox><br/>
            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" /><br />
            <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" /><br />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" /><br />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" /><br />
            <br /><br />
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="true"></asp:GridView>
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>