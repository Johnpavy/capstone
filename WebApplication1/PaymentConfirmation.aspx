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
        <%@ PreviousPageType VirtualPath="~/CheckOut.aspx" %> 
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
