<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentConfirmation.aspx.cs" Inherits="WebApplication1.PaymentConfirmation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <div>

    <h2>Payment Confirmed</h2>

        Detailed Payment Information:<br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Address"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="City"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Zip"></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server" Text="CardType"></asp:Label>
        <br />
        <!--
        <asp:Label ID="Label6" runat="server" Text="CardNumber"></asp:Label>
        <br />
        -->
        <asp:Label ID="Label7" runat="server" Text="Currency"></asp:Label>
        <asp:Label ID="Label8" runat="server" Text="Total"></asp:Label>

    </div>

    </form>
</body>
</html>
