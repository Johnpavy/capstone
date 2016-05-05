<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="WebApplication1.WebForm3" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<!-- This Page is Deprecated and No Longer needs to be within the project-->

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 172px;
        }
        .auto-style2 {
            width: 772px;
        }
        .auto-style4 {
            width: 212px;
        }
        .auto-style5 {
            width: 237px;
        }
        .auto-style6 {
            width: 237px;
            height: 21px;
        }
        .auto-style7 {
            height: 21px;
        }
        .auto-style8 {
            width: 172px;
            height: 26px;
        }
        .auto-style9 {
            height: 26px;
        }
        .auto-style10 {
            width: 128px;
        }
        .auto-style11 {
            width: 237px;
            height: 29px;
        }
        .auto-style12 {
            height: 29px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--<DayPilot:DayPilotCalendar ID="DayPilotCalendar1" runat="server" BackColor="#FFFFD5" BorderColor="#000000" BusinessBeginsHour="0" BusinessEndsHour="24" CssClassPrefix="calendar_default" DayFontFamily="Tahoma" DayFontSize="10pt" Days="7" DurationBarColor="Blue" EventBackColor="#FFFFFF" EventBorderColor="#000000" EventClickHandling="Disabled" EventFontFamily="Tahoma" EventFontSize="8pt" EventHoverColor="#DCDCDC" HourBorderColor="#EAD098" HourFontFamily="Tahoma" HourFontSize="16pt" HourHalfBorderColor="#F3E4B1" HourNameBackColor="#ECE9D8" HourNameBorderColor="#ACA899" HoverColor="#FFED95" NonBusinessBackColor="#FFF4BC" ScrollPositionHour="9" StartDate="2016-03-15" />
        -->
        <table style="width:100%;">
            <tr>
                <td class="auto-style2">
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="396px" NextPrevFormat="ShortMonth" Width="488px" ToolTip="Select the day of the week to see avaliable appointment times. ">
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
            <DayStyle BackColor="#CCCCCC" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
            <TodayDayStyle BackColor="#999999" ForeColor="White" />
        </asp:Calendar> </td>
                <td> &nbsp;<asp:Panel ID="ClientPanel" runat="server" Width="485px">
                        <table style="width:100%;">
                            <tr>
                                <td class="auto-style8">Sign Up for Session</td>
                                <td class="auto-style9">
                                    </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Trainer</td>
                                <td>
                                    <asp:TextBox ID="TrainerNameTxtBox" runat="server" ReadOnly="True" ToolTip="Slected Trainer for Session"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Client</td>
                                <td>
                                    <asp:TextBox ID="ClientNameTxtBox" runat="server" ReadOnly="True" ToolTip="Name of Client for Session"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Selected Date</td>
                                <td>
                                    <asp:TextBox ID="SelectedDateTxtBox" runat="server" ReadOnly="True" ToolTip="The desired date for the session."></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Start Time</td>
                                <td>
                                    <asp:DropDownList ID="StartTimeDrpDwn" runat="server" ToolTip="The time the session will start.">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">End Time</td>
                                <td>
                                    <asp:DropDownList ID="EndTimeDrpDwn" runat="server" ToolTip="The time the session will end.">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Location</td>
                                <td>
                                    <asp:TextBox ID="AddresTxtBox" runat="server" ToolTip="The address where the session will take place." Width="295px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Number of People</td>
                                <td>
                                    <asp:DropDownList ID="NumberOfPeopleDrpDwn" runat="server" ToolTip="Number of people attending the session. Additional rates apply.">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Type of Session</td>
                                <td>
                                    <asp:DropDownList ID="TypeOfSessionDrpDwn" runat="server" ToolTip="The type of workout session. Additonal rates apply.">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style1">
                                    <asp:Button ID="SubmitBtn" runat="server" Text="Submit" OnClick="SubmitBtn_Click" />
                                </td>
                                <td>
                                    <asp:Label ID="ErrorLbl" runat="server" ForeColor="Red" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
        </asp:Panel>

                    <br />
                    <asp:Panel ID="TrainerControlPannel" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td class="auto-style10">Select:</td>
                                <td>
                                    <asp:Button ID="ConfirmClientApptBtn" runat="server" OnClick="ConfirmClientApptBtn_Click" Text="Confirm Client Appointment" Width="231px" />
                                </td>
                                <td>
                                    <asp:Button ID="ManageTimeBtn" runat="server" OnClick="ManageTimeBtn_Click" Text="Manage Time Avaliable" Width="195px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style10">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style10">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="BlockTimesPanel" runat="server" Visible="False">
                        <table style="width:100%;">
                            <tr>
                                <td class="auto-style5">Block Out Times and Dates</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style11">Selcted Date</td>
                                <td class="auto-style12">
                                    <asp:TextBox ID="SelectedDateTxtBxTrainer0" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style12"></td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Start Time:</td>
                                <td>
                                    <asp:DropDownList ID="StartTimeDrpDwn0" runat="server" ToolTip="The time the session will start.">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style5">End Time:</td>
                                <td>
                                    <asp:DropDownList ID="EndTimeDrpDwn0" runat="server" ToolTip="The time the session will end.">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style6"></td>
                                <td class="auto-style7"></td>
                                <td class="auto-style7"></td>
                            </tr>
                            <tr>
                                <td class="auto-style5">
                                    <asp:Button ID="Button3" runat="server" Text="Block Out Slected Times" />
                                </td>
                                <td>
                                    <asp:Button ID="Button5" runat="server" Text="Block Out Entire Day" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style5">
                                    <asp:Button ID="Button4" runat="server" Text="Reopen Slected Times" Width="266px" />
                                </td>
                                <td>
                                    <asp:Button ID="Button6" runat="server" Text="Reopen Entire Day" Width="234px" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />

                    <br />
                    <asp:Panel ID="ConfirmSessionPanel" runat="server" Visible="False">
                        <table style="width:100%;">
                            <tr>
                                <td class="auto-style4">Confirm Client Session</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Selected Date</td>
                                <td>
                                    <asp:TextBox ID="SelectedDateTxtBxTrainer" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Select Your Client</td>
                                <td>
                                    <asp:DropDownList ID="ClientDrpDwnTrainer" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">View Client Details</td>
                                <td>
                                    <asp:TextBox ID="ClientDetailsTxtBox" runat="server" Height="109px" TextMode="MultiLine" Width="235px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Button ID="ConfirmBtnTrainer" runat="server" OnClick="ConfirmBtnTrainer_Click" Text="Comfirm Appointment" Width="269px" />
                                </td>
                                <td>
                                    <asp:Button ID="DeclineBtnTrainer" runat="server" OnClick="DeclineBtnTrainer_Click" Text="Decline Appointment" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Button ID="Button1" runat="server" Text="Reschedule Appointment" />
                                </td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="Cancel Appointment" Width="226px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />

                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server">
                    </DayPilot:DayPilotScheduler>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>

        <br />
    </div>
    </form>
</body>
</html>
