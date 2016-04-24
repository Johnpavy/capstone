<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Oops.aspx.cs" Inherits="WebApplication1.WebForm8" %>

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
        <title>Oops</title>
</head>
<body>
    <div class="container">
    <form id="form1" runat="server">
        <div class="col-lg-12 well">
            <div class="form-group">
                <h1 class="well center-block">An Error Has Occurred</h1>
                <asp:Image ID="Image1" runat="server" src="Pictures/AdminPic.jpg" Height="430px" Width="481px" CssClass="center-block" />
                <br />
                <h3 text-align: center>An unexpected error has occurred on our website. The web administrator has been notified. Please click the button below.</h3>
                <br />
                <asp:Button ID="Button1" Class="btn btn-lg btn-inf center-block" runat="server" OnClick="Button1_Click" Text="Go Home" />
            </div>
        </div>
    </form>
    </div>
</body>
</html>
