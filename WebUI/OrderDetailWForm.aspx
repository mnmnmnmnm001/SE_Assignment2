<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailWForm.aspx.cs" Inherits="WebUI.OrderDetailWForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Detail Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Order Detail Management</h2>
            <asp:Label ID="lblId" runat="server" Text="Detail ID:"></asp:Label>
            <asp:TextBox ID="txtId" runat="server"></asp:TextBox><br/>
            <asp:Label ID="lblOrderID" runat="server" Text="Order ID:"></asp:Label>
            <asp:TextBox ID="txtOrderID" runat="server"></asp:TextBox><br/>
            <asp:Label ID="lblItemID" runat="server" Text="Item ID:"></asp:Label>
            <asp:TextBox ID="txtItemID" runat="server"></asp:TextBox><br/>
            <asp:Label ID="lblQuantity" runat="server" Text="Quantity:"></asp:Label>
            <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox><br/>
            <asp:Label ID="lblUnitAmount" runat="server" Text="UnitAmount:"></asp:Label>
            <asp:TextBox ID="txtUnitAmount" runat="server"></asp:TextBox><br/>
            <asp:Button ID="btnAdd" runat="server" Text="Add" /><br/>
            <asp:Button ID="btnGet" runat="server" Text="Get" /><br/>
            <asp:Button ID="btnUpdate" runat="server" Text="Update"/><br/>
            <asp:Button ID="btnDelete" runat="server" Text="Delete"/><br/>
            <br /><br />
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="true"></asp:GridView>
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>