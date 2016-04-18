<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordResetComfirm.aspx.cs" Inherits="WebApplication1.PasswordResetComfirm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    New Password:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox><br />
    Confirm Password:   <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox><br />
        <asp:Button ID="Button1" runat="server" Text="Reset Password" />
    </div>
    </form>
</body>
</html>
