<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="WebApplication1.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text=" Email Address:"></asp:Label>
&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Email" Width="223px"></asp:TextBox>
        <br />
        <asp:CheckBox ID="CheckBox1" runat="server" Text="Reset Trainer Account Password" />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Reset Password" />
&nbsp;
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Cancel" />
        <br />
        <asp:Label ID="ErrorLbl" runat="server" ForeColor="#FF3300" Text="Label" Visible="False"></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNUserTable]"></asp:SqlDataSource>
        <asp:Label ID="ErrorLabel" runat="server" Text="Label"></asp:Label>
    
    </div>
    </form>
</body>
</html>
