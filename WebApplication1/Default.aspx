<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Welcome to MFN</title>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
  <style>
    body {background-color: #231F1E;}
    html, body, #container { height: 100%; }
    textarea{resize:none; background-color: #FFFFFF;}
    hr{background-color: #FFFFFF;}
    label, p, button, span, footer
    {
        font-family: OptimaSegoe, 'Segoe UI', Candara, Calibri, Arial, sans-serif;
        font-variant: normal;
        font-weight: 500;
        font-size: 16px;
    }
        
    .FitnessNetworkImage
    {
        width: 100%;
        max-height: 584px;
        max-width: 1181px;
        display: block;
        margin-left: auto;
        margin-right: auto;
    }
    .LoginButton
    {
        color: #FFFFFF;
        background-color: rgb(0, 141, 183);
        border-color: rgb(40, 94, 142);
    }

  </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="FitnessNetworkImageContainer">
            <img class="FitnessNetworkImage" src="Pictures/MobileFitnessNetworkPic.jpg" alt="Mountain View">
        </div>
        <div class="LoginButtonContainer text-center">
            <asp:LinkButton ID="login" Class="btn btn-primary btn-lg btn-block" runat="server" OnClick="login_Click">Login</asp:LinkButton>
            <asp:LinkButton ID="TrainerSignup" Class="btn btn-secondary btn-lg btn-block" runat="server" OnClick="signup_Click" >Trainer Sign Up</asp:LinkButton> 
             <asp:LinkButton ID="ClientSignup" Class="btn btn-primary btn-lg btn-block" runat="server" OnClick="ClientSignup_Click" >Client Sign Up</asp:LinkButton>
             <asp:LinkButton ID="about" Class="btn btn-secondary btn-lg btn-block" runat="server" OnClick="about_Click">How It Works</asp:LinkButton>
          
        </div>
    </form>
</body>
</html>
