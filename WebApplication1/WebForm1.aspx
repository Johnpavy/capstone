﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<!DOCTYPE html>

    <head>
        <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&libraries=places"></script>
        <script src="maps.js"></script>

    </head>
    <body>

        <input type="text" id="my-address" style="width: 448px">
        <button id="getCords" onClick="codeAddress();">getLat&Long</button> 
        <form id="WebForm1" runat="server">
        <asp:HiddenField ID="lat" runat="server" />
        <asp:HiddenField ID="lng" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

        </form>
    </body>
</html>

