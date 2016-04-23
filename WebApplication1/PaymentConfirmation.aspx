<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentConfirmation.aspx.cs" Inherits="WebApplication1.PaymentConfirmation" %>

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
               <asp:Label ID="Label1" runat="server" class="form-group" Text="Name"></asp:Label>
               <br />
            <br />
            <label>Address: </label>
               <br />
               <asp:Label ID="Label2" runat="server" class="form-group" Text="Address"></asp:Label>
               <br />
            <br />
            <label>City: </label>
               <br />
               <asp:Label ID="Label3" runat="server" class="form-group" Text="City"></asp:Label>
               <br />
            <br />
            <label>Zip: </label>
               <br />
               <asp:Label ID="Label4" runat="server" class="form-group" Text="Zip"></asp:Label>
               <br />
            <br />
            <label>Card Type: </label>
               <br />
               <asp:Label ID="Label5" runat="server" class="form-group" Text="CardType"></asp:Label>
               <br />
            <br />
            <!--
            <asp:Label ID="Label6" runat="server" class="form-group" Text="CardNumber"></asp:Label>
            <br />
            -->
            <label>Total: </label>
               <br />
               <asp:Label ID="Label7" runat="server" class="form-group" Text="Currency"></asp:Label> <asp:Label ID="Label8" runat="server" class="form-group" Text="Total"></asp:Label>
            <br />
           </div>

        </form>
    </div>
</body>
</html>
