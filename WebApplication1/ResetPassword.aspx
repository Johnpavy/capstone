<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="WebApplication1.ResetPassword" %>

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
        <title>Reset Password</title>
</head>
<body>
    <div class="container">
    <form id="form1" runat="server">
        <div class="col-lg-12 well">
            <div class="form-group">
                <h1 class="well center-block">Reset Your Password</h1>
    
                <label>Email Address: </label>
        &nbsp;
                <asp:TextBox ID="TextBox1" class="form-control" runat="server" TextMode="Email" Width="223px"></asp:TextBox>
                <br />
                <asp:CheckBox ID="CheckBox1" runat="server" Text="Reset Trainer Account Password" />
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" Class="btn btn-lg btn-inf center-block" OnClick="Button1_Click" Text="Reset Password" />
        &nbsp;
                <asp:Button ID="Button2" runat="server" Class="btn btn-lg btn-inf center-block" OnClick="Button2_Click" Text="Cancel" />
                <br />
                <asp:Label ID="ErrorLbl" runat="server" ForeColor="#FF3300" Text="Label" Visible="False"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNUserTable]"></asp:SqlDataSource>
                <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
    
           </div>
        </div>
    </form>
    </div>
</body>
</html>
