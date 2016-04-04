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
    <script src="JS/JavaScript.js"></script>
    <link href="CSS/default.css" rel="stylesheet" />
  <style>
    body {background-color: #231F1E;}
    html, body, #container { height: 100%; }
    textarea{resize:none; background-color: #FFFFFF;}
    hr{background-color: #FFFFFF;}
    
.centered-form{
	margin-top: 60px;
}

.centered-form .panel{
	background: rgba(255, 255, 255, 0.8);
	box-shadow: rgba(0, 0, 0, 0.3) 20px 20px 20px;
}
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

    .LoginButton
    {
        padding-bottom: 5px;
        width: 50%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
    }

    .TrainerSignupButton
    {
        background-color: rgb(255,255,255);
        padding-bottom: 5px;
        width: 50%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
    }

    .ClientSignupButton
    {
        background-color: rgb(0, 141, 183);
        padding-bottom: 5px;
        width: 50%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
    }

    .AboutButton
    {
        background-color: rgb(255,255,255);
        padding-bottom: 5px;
        width: 50%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
    }
    .TrainerStartupButton
    {
        background-color: rgb(255,255,255);
        padding-bottom: 5px;
        width: 20%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
    }
    .LinkButton1
    {
        background-color: rgb(0, 141, 183);
    }
  </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="FitnessNetworkImageContainer">
            <img class="FitnessNetworkImage" src="Pictures/MobileFitnessNetworkPic.jpg" alt="Mountain View">
        </div>
        <div class="LoginButtonContainer text-center">
            <asp:LinkButton ID="login" Class="LoginButton btn btn-primary btn-lg btn-block" runat="server" OnClick="login_Click">Login</asp:LinkButton>
            <asp:LinkButton ID="TrainerSignup" Class="TrainerSignupButton btn btn-secondary btn-lg btn-block" runat="server" OnClick="signup_Click" >Trainer Sign Up</asp:LinkButton> 
             <asp:LinkButton ID="ClientSignup" Class="ClientSignupButton btn btn-primary btn-lg btn-block" runat="server" OnClick="ClientSignup_Click" >Client Sign Up</asp:LinkButton>
             <asp:LinkButton ID="about" Class="AboutButton btn btn-secondary btn-lg btn-block" runat="server" OnClick="about_Click">How It Works</asp:LinkButton>
            </div>
            
          <div class="container">
        <div class="row centered-form">
        <div class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4">
        	<div class="panel panel-default">
        		<div class="panel-heading">
			    		<h3 class="panel-title">Register as a trainer</h3>
			 			</div>
			 			<div class="panel-body">

			    			<div class="row">
			    				<div class="col-xs-6 col-sm-6 col-md-6">
			    					<div class="form-group">
			                <input type="text" name="FName" id="first_name" class="form-control input-sm" placeholder="First Name"/>
			    					</div>
			    				</div>
			    				<div class="col-xs-6 col-sm-6 col-md-6">
			    					<div class="form-group">
			    						<input type="text" name="LName" id="last_name" class="form-control input-sm" placeholder="Last Name"/>
			    					</div>
			    				</div>
			    			</div>

			    			<div class="form-group">
			    				<input type="email" name="email" id="email" class="form-control input-sm" placeholder="Email Address">
			    			</div>

			    			<div class="row">
			    				<div class="col-xs-6 col-sm-6 col-md-6">
			    					<div class="form-group">
			    						<input type="password" name="password" id="password" class="form-control input-sm" placeholder="Password">
			    					</div>
			    				</div>
			    				<div class="col-xs-6 col-sm-6 col-md-6">
			    					<div class="form-group">
			    						<input type="password" name="Cpassword" id="password_confirmation" class="form-control input-sm" placeholder="Confirm Password">
			    					</div>
			    				</div>
			    			</div>
			    			
			    			<asp:LinkButton ID = "LinkButton1" Class="btn btn-info btn-block" runat="server" OnClick="startup_Click" >Get Started</asp:LinkButton>
			    		

			    	</div>
                  <asp:Label ID="ErrorLabel" runat="server" Text="Label" Visible="False"></asp:Label>
	    		</div>
    		</div>
    	</div>
    </div>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
    </form>
</body>
</html>
