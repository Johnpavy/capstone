<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.WebForm5" %>

<!DOCTYPE html>
<html lang="en">
<head>
  <title>Bootstrap Example</title>
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
        
    .input-group-addon.primary {
    color: rgb(255, 255, 255);
    background-color: rgb(0, 141, 183);
    border-color: rgb(40, 94, 142);
    }
        
    .LoginLabel
    {
        color: rgb(0, 141, 183);
        font-size: 3.5vw;
    }
	
    .Username
    {
        padding-bottom: 5px;
        width: 70%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
    }
        
    .Password
    {
        padding-bottom: 35px;
        width: 70%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
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
	<div class="container col-xs-12 col-sm-12">
        
        <div class="FitnessNetworkImageContainer">
            <img class="FitnessNetworkImage" src="Pictures/MobileFitnessNetworkPic.jpg" alt="Mountain View">
        </div>
        
        <p class="LoginLabel text-center">LOGIN</p>
        
		<div class="Username input-group">
          <span class="input-group-addon primary" id="sizing-addon2">
            <span class="glyphicon glyphicon-user" aria-hidden="true"></span>
            </span>
          <input type="text" class="UsernameInput form-control" placeholder="Username" name="Name"  aria-describedby="sizing-addon2">
        </div>
        
        <div class="Password input-group">
          <span class="input-group-addon primary" id="sizing-addon2">
            <span class="glyphicon glyphicon-lock" aria-hidden="true"></span>
            </span>
          <input type="password" class="PasswordInput form-control" placeholder="Password" aria-describedby="sizing-addon2">
        </div>
        
        <div class="LoginButtonContainer text-center">
            <!-- <button type="Login" class="LoginButton btn btn-default">Log In</button> -->
            <asp:LinkButton ID="button" Class="btn btn-lg btn-inf" runat="server" OnClick="Login">Login</asp:LinkButton>
        </div>
	</div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
</form>  
</body>
</html>

