<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadProfilePic.aspx.cs" Inherits="WebApplication1.UploadProfilePic" %>

<!DOCTYPE html>
<!-- THIS PAGE WAS MADE FOR TESTING THE FILE UPLOADER -->
<!-- This Page is Deprecated and No Longer needs to be within the project-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h4>Select a file to upload:</h4>

        <asp:FileUpload id="FileUpload1"                 
            runat="server">
        </asp:FileUpload>

        <br/><br/>

        <asp:Button id="UploadBtn" 
            Text="Upload file"
            OnClick="UploadBtn_Click"
            runat="server">
        </asp:Button>    

        <hr />

        <asp:Label id="UploadStatusLabel"
            runat="server">
        </asp:Label>     
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
