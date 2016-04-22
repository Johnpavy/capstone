<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordResetComfirm.aspx.cs" Inherits="WebApplication1.PasswordResetComfirm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="/Pictures/favicon.ico"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    New Password:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox><br />
    Confirm Password:   <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox><br />
        <asp:Button ID="Button1" runat="server" Text="Reset Password" OnClick="Button1_Click" />
        <h1><asp:Literal ID="ltMessage" runat="server" />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT [Trainer_PasswordSalt], [Trainer_PasswordHash], [Trainer_Id], [Trainer_Email] FROM [MFNTrainerTable]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNUserTable]"></asp:SqlDataSource>
        </h1>
        <p>
            <asp:Label ID="ErrorLabel2" runat="server" Text="" Visible="False"></asp:Label>
        </p>
    </div>
    </form>
</body>
</html>
