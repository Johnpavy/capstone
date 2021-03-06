﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentConfirmation.aspx.cs" Inherits="WebApplication1.PaymentConfirmation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="/Pictures/favicon.ico"/>
   <title>Payment Confirmation</title>
  <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="JS/JavaScript.js"></script>
    <link href="CSS/TrainerSignup.css" rel="stylesheet" />
</head>
<body>
        
        <div class="container">
            <h1 class="well">Payment Confirmation</h1>
        </div>

    <div class="container">
        <form id="form1" runat="server">

           <div class="col-lg-12 well">
            <h2 class="well">Detailed Payment Information:</h2>
            <label>Name: </label>
               <br />
               <asp:Label ID="Namelbl" runat="server" class="form-group" Text="Name"></asp:Label>
               <br />
            <br />
            <label>Address: </label>
               <br />
               <asp:Label ID="Addresslbl" runat="server" class="form-group" Text="Address"></asp:Label>
               <br />
            <br />
            <label>City: </label>
               <br />
               <asp:Label ID="Citylbl" runat="server" class="form-group" Text="City"></asp:Label>
               <br />
            <br />
            <label>Zip: </label>
               <br />
               <asp:Label ID="Ziplbl" runat="server" class="form-group" Text="Zip"></asp:Label>
               <br />
            <br />
            <label>Card Type: </label>
               <br />
               <asp:Label ID="Typelbl" runat="server" class="form-group" Text="CardType"></asp:Label>
               <br />
            <br />
               <label>Trainers Standard Rate: </label>
               <br />
               $<asp:Label ID="Ratelbl" runat="server" class="form-group" Text="StandardRate"></asp:Label>
               <br />
            <br />
               <label>Additional Persons Rate: </label>
               <br />
               $<asp:Label ID="AddPersonRatelbl" runat="server" class="form-group" Text="AdditionalPersons"></asp:Label>
               <br />
            <br />
               <label>Number of People Attending: </label>
               <br />
               <asp:Label ID="NumPeoplelbl" runat="server" class="form-group" Text="NumberPeople"></asp:Label>
               <br />
            <br />
            <label>SubTotal: </label>
               <br />
               $<asp:Label ID="SubTotallbl" runat="server" class="form-group" Text="CardType"></asp:Label>
               <br />
            <br />
            <label>Service Fee: </label>
               <br />
               $<asp:Label ID="ServiceFeelbl" runat="server" class="form-group" Text="ServiceFee"></asp:Label>
               <br />
            <br />
            <!--
            <asp:Label ID="CardNumlbl" runat="server" class="form-group" Text="CardNumber"></asp:Label>
            <br />
            -->
            <label>Total: </label>
               <br />
               <asp:Label ID="Currencylbl" runat="server" class="form-group" Text="Currency"></asp:Label> &nbsp;$<asp:Label ID="Totallbl" runat="server" class="form-group" Text="Total"></asp:Label>
            <br />
            <asp:Button ID="Button1" Class="btn btn-lg btn-inf center-block" runat="server" OnClick="Button1_Click" Text="Go Home" />
           </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNCalendarTable]"></asp:SqlDataSource>
        </form>
    </div>
</body>
</html>
