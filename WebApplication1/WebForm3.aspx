<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="WebApplication1.WebForm3" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <DayPilot:DayPilotCalendar ID="DayPilotCalendar1" runat="server" BackColor="#FFFFD5" BorderColor="#000000" BusinessBeginsHour="0" BusinessEndsHour="24" CssClassPrefix="calendar_default" DayFontFamily="Tahoma" DayFontSize="10pt" Days="7" DurationBarColor="Blue" EventBackColor="#FFFFFF" EventBorderColor="#000000" EventClickHandling="Disabled" EventFontFamily="Tahoma" EventFontSize="8pt" EventHoverColor="#DCDCDC" HourBorderColor="#EAD098" HourFontFamily="Tahoma" HourFontSize="16pt" HourHalfBorderColor="#F3E4B1" HourNameBackColor="#ECE9D8" HourNameBorderColor="#ACA899" HoverColor="#FFED95" NonBusinessBackColor="#FFF4BC" ScrollPositionHour="9" StartDate="2016-03-15" />

    </div>
    </form>
</body>
</html>
