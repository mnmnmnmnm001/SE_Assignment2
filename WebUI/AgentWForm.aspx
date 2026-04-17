<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentWForm.aspx.cs" Inherits="WebUI.AgentWForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agent Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Agent Management</h2>
            <asp:Label ID="lblId" runat="server" Text="ID:"></asp:Label>
            <asp:TextBox ID="txtId" runat="server"></asp:TextBox><br/>
            <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br/>
            <asp:Label ID="lblPhone" runat="server" Text="Phone:"></asp:Label>
            <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox><br/>
            <asp:Button ID="btnAdd" runat="server" Text="Add" /><br />
            <asp:Button ID="btnGet" runat="server" Text="Get" /><br />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" /><br />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" /><br />
            <br /><br />
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="true"></asp:GridView>
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>