<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainerScheduler.aspx.cs" Inherits="WebApplication1.TrainerScheduler" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Welcome to MFN</title>
  <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="JS/JavaScript.js"></script>
    <link href="CSS/default.css" rel="stylesheet" />
  <style>
    body {background-color: #231F1E;}
    html, body, #container { height: 100%; }
    textarea{resize:none; background-color: #FFFFFF;}
    hr{background-color: #FFFFFF;}
    
.centered-form{
	margin-top: 60px;
}

.centered-form .panel{
	background: rgba(255, 255, 255, 0.8);
	box-shadow: rgba(0, 0, 0, 0.3) 20px 20px 20px;
}
    label, p, button, span, footer
    {
        font-family: OptimaSegoe, 'Segoe UI', Candara, Calibri, Arial, sans-serif;
        font-variant: normal;
        font-weight: 500;
        font-size: 16px;
    }
        
    .FitnessNetworkImage
    {
        width: 100%;
        display: block;
        margin-left: auto;
        margin-right: auto;
    }
    .LoginButton
    {
        color: #FFFFFF;
        background-color: rgb(0, 141, 183);
        border-color: rgb(40, 94, 142);
    }

    .LoginButton
    {
        padding-bottom: 5px;
        width: 50%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
    }

    .LoginButton2
    {
        padding-bottom: 5px;
        width: 15%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
        background-color: rgb(0, 141, 183);
        width: 15%;
        float: right;
    }

    .TrainerSignupButton
    {
        background-color: rgb(255,255,255);
        padding-bottom: 5px;
        width: 50%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
    }

    .ClientSignupButton
    {
        background-color: rgb(0, 141, 183);
        padding-bottom: 5px;
        width: 50%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
    }

    .AboutButton
    {
        background-color: rgb(255,255,255);
        padding-bottom: 5px;
        width: 50%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
    }
    .TrainerStartupButton
    {
        background-color: rgb(255,255,255);
        padding-bottom: 5px;
        width: 20%;
        max-width: 960px;
        margin-right: auto;
        margin-left: auto;
    }
    .LinkButton1
    {
        background-color: rgb(0, 141, 183);
    }
    .LoginInformationPannel
    {
        width: 100%;
        display: block;
        float: right;
    }

    .UsernameInput
    {
        width: 20%;
        float: right;
    }
    .PasswordInput
    {
        width: 20%;
        float: right;
    }

    .ErrorLbl
    {
        float: right;
    }

    .TrainerToggle
    {
        width: 25%;
        float: right;
    }



  </style>
</head>
<body>
    <form id="form1" runat="server">
       <div class="row centered-form">
           <div class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4">
            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="20pt" ForeColor="Black" Height="500px" NextPrevFormat="ShortMonth" Width="100%" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender= "Calendar1_DayRender">
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                <DayStyle BackColor="#CCCCCC" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                <TodayDayStyle BackColor="#999999" ForeColor="White" />
                </asp:Calendar>

                <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server"></DayPilot:DayPilotScheduler>
            </div>
        </div>

        <div id ="Panel1" class="row centered-form">
             <div class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4">
        	        <div class="panel panel-default">
                           <div class="panel-heading">
			    		        <h3 class="panel-title">Select</h3>
			 			        </div>
			 			        <div class="panel-body">
			    		            <asp:LinkButton ID = "ManageSession" Class="btn btn-info btn-block" runat="server">Manage Session</asp:LinkButton>
                                    <asp:LinkButton ID = "ManageBlackedOutTimes" Class="btn btn-info btn-block" runat="server">Manage Blacked Out Times</asp:LinkButton>
			    	            </div>
                          <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
	    		        </div>
                    </div>
                     </div>

        <div id ="Panel2" class="row centered-form">
        <div class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4">
        	<div class="panel panel-default">
                    <div class="panel-heading">
			    		<h3 class="panel-title">Confirm Client Session</h3>
			 			</div>
			 			<div class="panel-body">
                             Selected Date: <asp:TextBox ID="DateTextBox" runat="server" width = "50%" ReadOnly="True"></asp:TextBox><br />
                             Selected Client: <asp:DropDownList ID="AppointmentsDropbx" runat="server"></asp:DropDownList><br />
                             <asp:TextBox ID="SummaryTextBox" runat="server" TextMode="MultiLine" width = "100%" ReadOnly="True" ></asp:TextBox><br />
                             <table style="width: 100%;">
                                 <tr>
                                     <td><asp:LinkButton ID = "ConfirmAppointment" Class="btn btn-info btn-block" runat="server">Confirm Appointment</asp:LinkButton></td>
                                     <td><asp:LinkButton ID = "LinkButton1" Class="btn btn-info btn-block" runat="server">Decline Appointment</asp:LinkButton></td>
                                 </tr>
                                 <tr>
                                     <td><asp:LinkButton ID = "RescheduleAppointment" Class="btn btn-info btn-block" runat="server">Reschedule Appointment</asp:LinkButton></td>
                                     <td> <asp:LinkButton ID = "CancelAppointmentManagement" Class="btn btn-info btn-block" runat="server">Cancel Appointment Management</asp:LinkButton></td>
                                 </tr>
                             </table>
			    		

			    	    </div>
                    <asp:Label ID="ErrorLabel" runat="server" Text="Label" Visible="False"></asp:Label>
	    		</div>
            </div>
            </div>

             <div id ="Panel3" class="row centered-form">
             <div class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4">
        	        <div class="panel panel-default">
                           <div class="panel-heading">
			    		        <h3 class="panel-title">Manage Blocked Out Dates</h3>
			 			        </div>
			 			        <div class="panel-body">
                                     Selected Date: <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" Width="50%"></asp:TextBox><br />
                                     Start Time: <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList><br />
                                     End Time:   <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList><br />
                                     <table style="width: 100%;">
                                        <tr>
                                            <td><asp:LinkButton ID = "LinkButton2" Class="btn btn-info btn-block" runat="server">Block Out Slected Times</asp:LinkButton></td>
                                            <td><asp:LinkButton ID = "LinkButton3" Class="btn btn-info btn-block" runat="server">Block Out Entire Day Times</asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td><asp:LinkButton ID = "LinkButton4" Class="btn btn-info btn-block" runat="server">Reopen Selected Times</asp:LinkButton></td>
                                            <td> <asp:LinkButton ID = "LinkButton5" Class="btn btn-info btn-block" runat="server">Reopen Entire Day</asp:LinkButton></td>
                                        </tr>
                                    </table>
			    			    </div>
			    	</div>
                          <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
	    		    <br />
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNUserTable]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNCalendarTable]"></asp:SqlDataSource>
	    		    </div>
              </div>
    </form>
</body>
</html>
