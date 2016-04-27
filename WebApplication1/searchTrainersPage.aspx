<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="searchTrainersPage.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="WebApplication1.searchTrainersPage" %>

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
        <title>Search</title>
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
            <div class="col-lg-12 well">
                <div class="form-group">
                <h1 class="well center-block">Trainers in your area</h1>
                    <div id="TrainerResults" runat="server" class="centered-form" width="223px">
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
                    <asp:Label ID="ErrorLbl" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
