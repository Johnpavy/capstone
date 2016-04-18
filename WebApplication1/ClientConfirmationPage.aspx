<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientConfirmationPage.aspx.cs" Inherits="WebApplication1.ClientConfirmationPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1><asp:Literal ID="ltMessage" runat="server" />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [UserActivation]"></asp:SqlDataSource>
        </h1>
    </div>
    </form>
</body>
</html>
