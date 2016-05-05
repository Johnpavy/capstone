<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminRegistration.aspx.cs" Inherits="WebApplication1.WebForm7" %>

<!DOCTYPE html>
<!-- This Page is Deprecated and No Longer needs to be within the project-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="/Pictures/favicon.ico"/>
   <title>Trainer Sign Up</title>
  <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="JS/JavaScript.js"></script>
    <link href="CSS/TrainerSignup.css" rel="stylesheet" />
  
</head>
<body>
    <form id="form1" runat="server">
<div class="container">
    <h1 class="well"> <asp:Label ID="AdminName" runat="server" Text="Admin"></asp:Label>'s Password Confirmation</h1>
	<div class="col-lg-12 well">
	        <div class="row">
			</div>
					<div class="col-sm-12">
			
						<div class="form-group">
							<label>Enter New Password</label>
						<div class="row">
                           <label>Password: </label><asp:TextBox ID="Password" runat="server"></asp:TextBox>
                        </div>

						<div class="row">
                           <label>Password Comfirm: </label> <asp:TextBox ID="PasswordComfirm" runat="server"></asp:TextBox>
                        </div>
                            <asp:LinkButton ID="SubmitPasswordBtn" runat="server" OnClick="SubmitPasswordBtn_Click">Submit</asp:LinkButton>
	                    </div>
	                    </div>




                    
        </div>
    </div>

      </form>
        </body>
</html>
