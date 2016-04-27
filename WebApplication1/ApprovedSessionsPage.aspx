<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovedSessionsPage.aspx.cs" Inherits="WebApplication1.ApprovedSessionsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approved Sessions</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Test Approved Session
        <div id="SessionResults" runat="server" class="row centered-form">
        </div>
        <asp:Label ID="ErrorLbl" runat="server" Text="Label" Visible ="False"></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNCalendarTable]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
