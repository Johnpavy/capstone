<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="searchTrainersPage.aspx.cs" Inherits="WebApplication1.searchTrainersPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Trainer search placeholder<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
        <asp:Label ID="ErrorLbl" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
        </div>
    </form>
</body>
</html>
