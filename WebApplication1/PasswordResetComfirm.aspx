<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordResetComfirm.aspx.cs" Inherits="WebApplication1.PasswordResetComfirm" %>

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
        <title>Reset Password Confirm</title>
</head>
<body>
    <div class="container">
    <form id="form1" runat="server">
        <div class="col-lg-12 well">
            <div class="form-group">
                <h1 class="well center-block">Reset Your Password</h1>
                <label>New Password: </label>
            
                <asp:TextBox ID="TextBox1" class="form-control" Width="223px" runat="server" TextMode="Password"></asp:TextBox><br />
                <label>Confirm Password: </label>
            
                <asp:TextBox ID="TextBox2" class="form-control" runat="server" TextMode="Password" Width="223px"></asp:TextBox><br />
                <asp:Button ID="Button1" Class="btn btn-lg btn-inf center-block" runat="server" Text="Reset Password" OnClick="Button1_Click" />
                <h1><asp:Literal ID="ltMessage" runat="server" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT [Trainer_PasswordSalt], [Trainer_PasswordHash], [Trainer_Id], [Trainer_Email] FROM [MFNTrainerTable]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNUserTable]"></asp:SqlDataSource>
                </h1>
                <p>
                    <asp:Label ID="ErrorLabel2" runat="server" Text="" Visible="False"></asp:Label>
                </p>
             </div>
        </div>
    </form>
    </div>
</body>
</html>
