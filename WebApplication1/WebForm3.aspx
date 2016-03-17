﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="WebApplication1.WebForm3" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

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
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="396px" NextPrevFormat="ShortMonth" Width="488px">
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
            <DayStyle BackColor="#CCCCCC" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
            <TodayDayStyle BackColor="#999999" ForeColor="White" />
        </asp:Calendar> </td>
                <td> Sign up for session:<br />
                    <asp:Panel ID="Panel1" runat="server" Width="485px">
                        <table style="width:100%;">
                            <tr>
                                <td class="auto-style1">Trainer</td>
                                <td>
                                    <asp:TextBox ID="TextBox3" runat="server" ReadOnly="True" ToolTip="Slected Trainer for Session"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Client</td>
                                <td>
                                    <asp:TextBox ID="TextBox4" runat="server" ReadOnly="True" ToolTip="Name of Client for Session"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Selected Date</td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" ToolTip="The desired date for the session."></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Start Time</td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server" ToolTip="The time the session will start.">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">End Time</td>
                                <td>
                                    <asp:DropDownList ID="DropDownList2" runat="server" ToolTip="The time the session will end.">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Location</td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server" ToolTip="The address where the session will take place." Width="295px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Number of People</td>
                                <td>
                                    <asp:DropDownList ID="DropDownList3" runat="server" ToolTip="Number of people attending the session. Additional rates apply.">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Type of Session</td>
                                <td>
                                    <asp:DropDownList ID="DropDownList4" runat="server" ToolTip="The type of workout session. Additonal rates apply.">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style1">
                                    <asp:Button ID="Button1" runat="server" Text="Submit" />
                                </td>
                                <td>
                                    <asp:Label ID="ErrorLbl" runat="server" ForeColor="Red" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
        </asp:Panel>

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
