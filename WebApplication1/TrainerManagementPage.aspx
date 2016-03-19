<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainerManagementPage.aspx.cs" Inherits="WebApplication1.TrainerManagementPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 336px;
        }
        .auto-style2 {
            width: 249px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">Trainer Session Management Page</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Select Session to manage</td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Button ID="SelectBtn" runat="server" Text="Select" />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <asp:Panel ID="Panel1" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Confirm Session Completion" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td>
                    <br />
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="Panel2" runat="server">
            By clicking on&nbsp; &quot;Confirm Session&quot;, you confirm that your training session with
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            &nbsp;was completed on
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            &nbsp;at
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
            .
            <br />
            <br />
            <asp:CheckBox ID="CheckBox1" runat="server" Text="I have read the above statement and confirm that the training session has occured." />
            <table style="width:100%;">
                <tr>
                    <td class="auto-style2">
                        <asp:Button ID="Button2" runat="server" Text="Confirm Session" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="Button3" runat="server" Text="This Session Did Not Occur" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <br />
            <asp:Panel ID="Panel3" runat="server">
                You are about to report that your trainer,
                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                , has failed to conduct your training session that was scheduled for
                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                , at
                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                . Any conflict will be revewed by the Mobile Fitness Network Conflict Resolution Team.<br />
                <br />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="I have read the above statement and confirm that the training session has NOT occured." />
                <br />
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:Button ID="Button4" runat="server" Text="Confirm" />
                        </td>
                        <td>
                            <asp:Button ID="Button5" runat="server" Text="Back" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br />
            </asp:Panel>
        </asp:Panel>
        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
