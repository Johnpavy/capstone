<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovedSessionsPage.aspx.cs" Inherits="WebApplication1.ApprovedSessionsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="/Pictures/favicon.ico"/>
      <meta charset="utf-8"/>
      <meta name="viewport" content="width=device-width, initial-scale=1"/>
      <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css"/>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
      <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
        <script src="JS/JavaScript.js"></script>
        <link href="CSS/Checkout.css" rel="stylesheet" />
    <title>Approved Sessions</title>
</head>
<body>
    <div class="container">
    <form id="form1" runat="server">
        <div class="col-lg-12 well">
            <div class="form-group">
                <h1 class="well center-block">Test Approved Session</h1>
        
            <div id="SessionResults" runat="server" class="row centered-form">
            </div>
                <asp:Label ID="ErrorLbl" runat="server" Text="Label" Visible ="False"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNCalendarTable]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
            </div>
        </div>
    </form>
    </div>
</body>
</html>
