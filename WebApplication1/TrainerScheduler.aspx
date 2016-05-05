<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainerScheduler.aspx.cs" Inherits="WebApplication1.TrainerScheduler"  EnableViewState="true" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <link rel="shortcut icon" type="image/x-icon" href="/Pictures/favicon.ico"/>
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
    body > #container { height: auto; min-height: 100%; }
    textarea{resize:none; background-color: #FFFFFF;}
    hr{background-color: #FFFFFF;}
    label, p, button, span, footer
    {
        font-family: OptimaSegoe, 'Segoe UI', Candara, Calibri, Arial, sans-serif;
        font-variant: normal;
        font-weight: 500;
        font-size: 16px;
    }
    .navbar
    {
    color: rgb(255, 255, 255);
    background-color: rgb(0, 141, 183);
    border-color: rgb(40, 94, 142);
    }
    .white, .white a{color:#FFFFFF;}
    .NavDropdownMenu{color: #FFFFFF;}
    .NavDropdownMenu:hover
    {
        background-color: #008DB7;
    }
    .HeaderContainer { background-color: #231F1E; padding-bottom: 21px; padding-top: 8%;}
    .TrainerPicture {width: 25%; height: 25%; margin-left: auto;
    margin-right: auto; border-radius: 50%;}
    .TrainerHeaderInfo{padding-top: 1%; }
    .TrainerName {color: #FFFFFF; font-size: 4.5vw;}
    .TrainerRating {color: #FFFFFF; font-size: 3.5vw;}
    .BookButton {position: relative; top: 8px;}
    .BookColumn {padding-top: 4%;}
    .ButtonNavigationBar, .ButtonNavigationBarDropDown { width: 101%; background-color: #008DB7;}
    .ButtonResponsive, .ButtonResponsiveDropDown 
    {
        font-size: 2.5vw; 
        width: 20%; 
        background-color: #008DB7;
        border-color: #008DB7;
    }
	.ButtonResponsiveDropDown {padding-bottom:1px;}
    .ButtonResponsive:hover, ButtonResponsiveDropDown:hover{background-color: #FFFFFF; color: #231F1E;}
    .InfoContainer {padding-top: 1%;}
    .BiographyLabel, .TypesLabel, .RatesLabel, .LocationsLabel {font-size: 3.0vw; color: #FFFFFF; }
    .ReviewLabel {color: #FFFFFF; font-size: 3.5vw;}
    .ReviewHeader, .ReviewRatingGivenLabel, .ReviewRatingGiven , .ReviewBody{color: #FFFFFF; font-size: 2.5vw;}
    .InformationPanels
    {
        padding-top: 5px;
    }
    .panel-default
    {
        border-color: rgb(40, 94, 142);
    }
    .panel-default > .panel-heading
    {
    color: rgb(255, 255, 255);
    background-color: rgb(0, 141, 183);
    border-color: rgb(40, 94, 142);
    }
    footer 
    {
        clear: both;
        position: relative;
        z-index: 10;
        height: 3em;
        margin-top: -3em;
        background-color: #008DB7;
    }
        .FooterContent{color: #FFFFFF; font-size: 2.0vw;}
        .UserNameLbl {color: #FFFFFF; font-size: 4.5vw;}

     .LoginButton2
    {
        background-color: rgb(92, 184, 92);
        border: 2px solid;
        border-radius: 25px;
    }

    </style>
</head>
<body>
    <form id="form1" runat="server">
       <div class="row centered-form">
            <div class="content">  

                            <div class="content">  
                <div class="TopNavContainer col-xs-12 col-sm-12">
                    <nav class="navbar navbar-primary navbar-fixed-top">
                      <div class="container-fluid">
                        <!-- Brand and toggle get grouped for better mobile display -->
                        <div class="navbar-header">
                          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                          </button>
                          <a class="NavBrand navbar-brand glyphicon glyphicon-home white" href="WebForm2.aspx">
                            </a>
                        </div>

                        <!-- Collect the nav links, forms, and other content for toggling -->
                        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                          <ul class="nav navbar-nav navbar-right">
                              <li><asp:LinkButton ID = "LinkButton6" Class="LoginButton2 btn-info btn-block" runat="server" onclick="BackToProfile_Click">Back To Profile</asp:LinkButton></li>
                              <!--<li><button type="button" runat="server" class="BookButton btn btn-success" onclick="BookTrainer_Click">Manage Your Schedule</button></li>-->
                              <li class="dropdown">
                              <a href="#" class="NavDropdownMenu dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Menu <span class="caret"></span></a>
                              <ul class="dropdown-menu">
                                <li><a href="AccountSettings.aspx">Settings</a></li>
                                <li role="separator" class="divider"></li>
                                <li><a href="LogOut.aspx">Log Out</a></li>
                                <li role="separator" class="divider"></li>
                              </ul>
                            </li>
                          </ul>
                        </div><!-- /.navbar-collapse -->
                      </div><!-- /.container-fluid -->
                    </nav>
                </div>
                <div class="TopNavContainer col-xs-12 col-sm-12">
        <div class="HeaderContainer container-fluid">
            <div class="row">
                <div class="PictureColumn col-xs-12 col-sm-12 text-center">
                    <asp:Image id="ProfilePic" runat="server" src="Pictures/trainerPic.jpg" class="TrainerPicture img-circle img-responsive" alt="Trainer Picture" />
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="TrainerHeaderInfo text-center">
                    <p class="TrainerName"><asp:Label ID="UserNameLbl" CssClass="UserNameLbl" runat="server" Text="Label"></asp:Label>'s Schedule</p>
                </div>
            </div>
        </div>

           <div class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4">
            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="20pt" ForeColor="Black" Height="500px" NextPrevFormat="ShortMonth" Width="100%" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender= "Calendar1_DayRender">
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                <DayStyle BackColor="#F5FFFA" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#4682B4" ForeColor="White" />
                <TitleStyle BackColor="#058EBC" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                <TodayDayStyle BackColor="#058EBC" ForeColor="Black" />
                </asp:Calendar>

                <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server"></DayPilot:DayPilotScheduler>
            </div>
        </div>

        <div id ="OptionsDiv" class="row centered-form" runat="server">
             <div class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4">
        	        <div class="panel panel-default">
                           <div class="panel-heading">
			    		        <h3 class="panel-title">Select</h3>
			 			        </div>
			 			        <div class="panel-body">
			    		            <asp:LinkButton ID = "ManageSession" Class="btn btn-info btn-block" runat="server" OnClick="ManageSession_Click">Manage Session</asp:LinkButton>
                                    <asp:LinkButton ID = "ManageBlackedOutTimes" Class="btn btn-info btn-block" runat="server" OnClick="ManageBlackedOutTimes_Click">Manage Blacked Out Times</asp:LinkButton>
			    	            </div>
                          <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
	    		        </div>
                    </div>
                     </div>

        <div id ="ManageAppointmentDiv" class="row centered-form" runat="server" Visible = "false">
        <div class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4">
        	<div class="panel panel-default">
                    <div class="panel-heading">
			    		<h3 class="panel-title">Confirm Client Session</h3>
			 			</div>
			 			<div class="panel-body">
                            <!-- Selected Date: <asp:TextBox ID="DateTextBox" runat="server" width = "50%" ReadOnly="True"></asp:TextBox><br /> -->
                             Selected Client:<asp:DropDownList ID="ClientsDrpDown" runat="server"></asp:DropDownList>
                            <asp:LinkButton ID="SelectThisClientBtn"  Class="btn btn-info btn-block" runat="server" width="25%" OnClick="SelectThisClientBtn_Click">Select Client</asp:LinkButton>
                             <br />
                             <asp:TextBox ID="SummaryTextBox" runat="server" TextMode="MultiLine" width = "100%" height="150px" ReadOnly="True" ></asp:TextBox><br />
                             <table style="width: 100%;">
                                 <tr>
                                     <td><asp:LinkButton ID = "ConfirmAppointment" Class="btn btn-info btn-block" runat="server" OnClick="ConfirmAppointment_Click" Enabled="false">Confirm Appointment</asp:LinkButton></td>
                                     <td><asp:LinkButton ID = "DeclineAppointment" Class="btn btn-info btn-block" runat="server" OnClick="DeclineAppointment_Click" Enabled="false">Decline Appointment</asp:LinkButton></td>
                                 </tr>
                                 <tr>
                                     <td><asp:LinkButton ID = "RescheduleAppointment" Class="btn btn-info btn-block" runat="server" OnClick="RescheduleAppointment_Click" Enabled="false">Reschedule Appointment</asp:LinkButton></td>
                                     <td> <asp:LinkButton ID = "CancelAppointmentManagement" Class="btn btn-info btn-block" Style="background-color:green"  runat="server" OnClick="CancelAppointmentManagement_Click">Cancel Appointment Management</asp:LinkButton></td>
                                 </tr>
                             </table>
			    		

			    	    </div>
                    <asp:Label ID="ErrorLabel" runat="server" Text="Label" Visible="False"></asp:Label>
	    		</div>
            </div>
            </div>

             <div id ="ManageBlockedTimeDiv" class="row centered-form" runat="server" Visible = "false">
             <div class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4">
        	        <div class="panel panel-default">
                           <div class="panel-heading">
			    		        <h3 class="panel-title">Manage Blocked Out Dates</h3>
			 			        </div>
			 			        <div class="panel-body">
                                     Selected Date: <asp:TextBox ID="BlockedOutSelctedDateTxtBox" runat="server" ReadOnly="True" Width="50%"></asp:TextBox><br />
                                     Start Time: <asp:DropDownList ID="StartTimeDrpList" runat="server" Width="50%">
                                        <asp:ListItem>12:00 AM</asp:ListItem>
                                        <asp:ListItem>12:15 AM</asp:ListItem>
                                        <asp:ListItem>12: 30 AM</asp:ListItem>
                                        <asp:ListItem>12:45 AM</asp:ListItem>
                                        <asp:ListItem>1:00 AM</asp:ListItem>
                                        <asp:ListItem>1:15 AM</asp:ListItem>
                                        <asp:ListItem>1:30 AM</asp:ListItem>
                                        <asp:ListItem>1:45 AM</asp:ListItem>
                                        <asp:ListItem>2:00 AM</asp:ListItem>
                                        <asp:ListItem>2:15 AM</asp:ListItem>
                                        <asp:ListItem>2:30 AM</asp:ListItem>
                                        <asp:ListItem>2:45 AM</asp:ListItem>
                                        <asp:ListItem>3:00 AM</asp:ListItem>
                                        <asp:ListItem>3:15 AM</asp:ListItem>
                                        <asp:ListItem>3:30 AM</asp:ListItem>
                                        <asp:ListItem>3:45 AM</asp:ListItem>
                                        <asp:ListItem>4:00 AM</asp:ListItem>
                                        <asp:ListItem>4:15 AM</asp:ListItem>
                                        <asp:ListItem>4:30 AM</asp:ListItem>
                                        <asp:ListItem>4:45 AM</asp:ListItem>
                                        <asp:ListItem>5:00 AM</asp:ListItem>
                                        <asp:ListItem>5:15 AM</asp:ListItem>
                                        <asp:ListItem>5:30 AM</asp:ListItem>
                                        <asp:ListItem>5:45 AM</asp:ListItem>
                                        <asp:ListItem>6:00 AM</asp:ListItem>
                                        <asp:ListItem>6:15 AM</asp:ListItem>
                                        <asp:ListItem>6:30 AM</asp:ListItem>
                                        <asp:ListItem>6:45 AM</asp:ListItem>
                                        <asp:ListItem>7:00 AM</asp:ListItem>
                                        <asp:ListItem>7:15 AM</asp:ListItem>
                                        <asp:ListItem>7:30 AM</asp:ListItem>
                                        <asp:ListItem>7:45 AM</asp:ListItem>
                                        <asp:ListItem>8:00 AM</asp:ListItem>
                                        <asp:ListItem>8:15 AM</asp:ListItem>
                                        <asp:ListItem>8:30 AM</asp:ListItem>
                                        <asp:ListItem>8:45 AM</asp:ListItem>
                                        <asp:ListItem>9:00 AM</asp:ListItem>
                                        <asp:ListItem>9:15 AM</asp:ListItem>
                                        <asp:ListItem>9:30 AM</asp:ListItem>
                                        <asp:ListItem>9:45 AM</asp:ListItem>
                                        <asp:ListItem>10:00 AM</asp:ListItem>
                                        <asp:ListItem>10:15 AM</asp:ListItem>
                                        <asp:ListItem>10:30 AM</asp:ListItem>
                                        <asp:ListItem>10:45 AM</asp:ListItem>
                                        <asp:ListItem>11:00 AM</asp:ListItem>
                                        <asp:ListItem>11:15 AM</asp:ListItem>
                                        <asp:ListItem>11:30 AM</asp:ListItem>
                                        <asp:ListItem>11:45 AM</asp:ListItem>
                                        <asp:ListItem>12:00 PM</asp:ListItem>
                                        <asp:ListItem>12:15 PM</asp:ListItem>
                                        <asp:ListItem>12:30 PM</asp:ListItem>
                                        <asp:ListItem>12:45 PM</asp:ListItem>
                                        <asp:ListItem>1:00 PM</asp:ListItem>
                                        <asp:ListItem>1:15 PM</asp:ListItem>
                                        <asp:ListItem>1:30 PM</asp:ListItem>
                                        <asp:ListItem>1:45 PM</asp:ListItem>
                                        <asp:ListItem>2:00 PM</asp:ListItem>
                                        <asp:ListItem>2:15 PM</asp:ListItem>
                                        <asp:ListItem>2:30 PM</asp:ListItem>
                                        <asp:ListItem>2:45 PM</asp:ListItem>
                                        <asp:ListItem>3:00 PM</asp:ListItem>
                                        <asp:ListItem>3:15 PM</asp:ListItem>
                                        <asp:ListItem>3:30 PM</asp:ListItem>
                                        <asp:ListItem>3:45 PM</asp:ListItem>
                                        <asp:ListItem>4:00 PM</asp:ListItem>
                                        <asp:ListItem>4:15 PM</asp:ListItem>
                                        <asp:ListItem>4:30 PM</asp:ListItem>
                                        <asp:ListItem>4:45 PM</asp:ListItem>
                                        <asp:ListItem>5:00 PM</asp:ListItem>
                                        <asp:ListItem>5:15 PM</asp:ListItem>
                                        <asp:ListItem>5:30 PM</asp:ListItem>
                                        <asp:ListItem>5:45 PM</asp:ListItem>
                                        <asp:ListItem>6:00 PM</asp:ListItem>
                                        <asp:ListItem>6:15 PM</asp:ListItem>
                                        <asp:ListItem>6:30 PM</asp:ListItem>
                                        <asp:ListItem>6:45 PM</asp:ListItem>
                                        <asp:ListItem>7:00 PM</asp:ListItem>
                                        <asp:ListItem>7:15 PM</asp:ListItem>
                                        <asp:ListItem>7:30 PM</asp:ListItem>
                                        <asp:ListItem>7:45 PM</asp:ListItem>
                                        <asp:ListItem>8:00 PM</asp:ListItem>
                                        <asp:ListItem>8:15 PM</asp:ListItem>
                                        <asp:ListItem>8:30 PM</asp:ListItem>
                                        <asp:ListItem>8:45 PM</asp:ListItem>
                                        <asp:ListItem>9:00 PM</asp:ListItem>
                                        <asp:ListItem>9:15 PM</asp:ListItem>
                                        <asp:ListItem>9:30 PM</asp:ListItem>
                                        <asp:ListItem>9:45 PM</asp:ListItem>
                                        <asp:ListItem>10:00 PM</asp:ListItem>
                                        <asp:ListItem>10:15 PM</asp:ListItem>
                                        <asp:ListItem>10:30 PM</asp:ListItem>
                                        <asp:ListItem>10:45 PM</asp:ListItem>
                                        <asp:ListItem>11:00 PM</asp:ListItem>
                                        <asp:ListItem>11:15 PM</asp:ListItem>
                                        <asp:ListItem>11:30 PM</asp:ListItem>
                                        <asp:ListItem>11:45 PM</asp:ListItem>
                                    </asp:DropDownList><br />
                                     End Time:   <asp:DropDownList ID="EndTimeDrpList" runat="server" Width="50%">
                                        <asp:ListItem>12:00 AM</asp:ListItem>
                                        <asp:ListItem>12:15 AM</asp:ListItem>
                                        <asp:ListItem>12: 30 AM</asp:ListItem>
                                        <asp:ListItem>12:45 AM</asp:ListItem>
                                        <asp:ListItem>1:00 AM</asp:ListItem>
                                        <asp:ListItem>1:15 AM</asp:ListItem>
                                        <asp:ListItem>1:30 AM</asp:ListItem>
                                        <asp:ListItem>1:45 AM</asp:ListItem>
                                        <asp:ListItem>2:00 AM</asp:ListItem>
                                        <asp:ListItem>2:15 AM</asp:ListItem>
                                        <asp:ListItem>2:30 AM</asp:ListItem>
                                        <asp:ListItem>2:45 AM</asp:ListItem>
                                        <asp:ListItem>3:00 AM</asp:ListItem>
                                        <asp:ListItem>3:15 AM</asp:ListItem>
                                        <asp:ListItem>3:30 AM</asp:ListItem>
                                        <asp:ListItem>3:45 AM</asp:ListItem>
                                        <asp:ListItem>4:00 AM</asp:ListItem>
                                        <asp:ListItem>4:15 AM</asp:ListItem>
                                        <asp:ListItem>4:30 AM</asp:ListItem>
                                        <asp:ListItem>4:45 AM</asp:ListItem>
                                        <asp:ListItem>5:00 AM</asp:ListItem>
                                        <asp:ListItem>5:15 AM</asp:ListItem>
                                        <asp:ListItem>5:30 AM</asp:ListItem>
                                        <asp:ListItem>5:45 AM</asp:ListItem>
                                        <asp:ListItem>6:00 AM</asp:ListItem>
                                        <asp:ListItem>6:15 AM</asp:ListItem>
                                        <asp:ListItem>6:30 AM</asp:ListItem>
                                        <asp:ListItem>6:45 AM</asp:ListItem>
                                        <asp:ListItem>7:00 AM</asp:ListItem>
                                        <asp:ListItem>7:15 AM</asp:ListItem>
                                        <asp:ListItem>7:30 AM</asp:ListItem>
                                        <asp:ListItem>7:45 AM</asp:ListItem>
                                        <asp:ListItem>8:00 AM</asp:ListItem>
                                        <asp:ListItem>8:15 AM</asp:ListItem>
                                        <asp:ListItem>8:30 AM</asp:ListItem>
                                        <asp:ListItem>8:45 AM</asp:ListItem>
                                        <asp:ListItem>9:00 AM</asp:ListItem>
                                        <asp:ListItem>9:15 AM</asp:ListItem>
                                        <asp:ListItem>9:30 AM</asp:ListItem>
                                        <asp:ListItem>9:45 AM</asp:ListItem>
                                        <asp:ListItem>10:00 AM</asp:ListItem>
                                        <asp:ListItem>10:15 AM</asp:ListItem>
                                        <asp:ListItem>10:30 AM</asp:ListItem>
                                        <asp:ListItem>10:45 AM</asp:ListItem>
                                        <asp:ListItem>11:00 AM</asp:ListItem>
                                        <asp:ListItem>11:15 AM</asp:ListItem>
                                        <asp:ListItem>11:30 AM</asp:ListItem>
                                        <asp:ListItem>11:45 AM</asp:ListItem>
                                        <asp:ListItem>12:00 PM</asp:ListItem>
                                        <asp:ListItem>12:15 PM</asp:ListItem>
                                        <asp:ListItem>12:30 PM</asp:ListItem>
                                        <asp:ListItem>12:45 PM</asp:ListItem>
                                        <asp:ListItem>1:00 PM</asp:ListItem>
                                        <asp:ListItem>1:15 PM</asp:ListItem>
                                        <asp:ListItem>1:30 PM</asp:ListItem>
                                        <asp:ListItem>1:45 PM</asp:ListItem>
                                        <asp:ListItem>2:00 PM</asp:ListItem>
                                        <asp:ListItem>2:15 PM</asp:ListItem>
                                        <asp:ListItem>2:30 PM</asp:ListItem>
                                        <asp:ListItem>2:45 PM</asp:ListItem>
                                        <asp:ListItem>3:00 PM</asp:ListItem>
                                        <asp:ListItem>3:15 PM</asp:ListItem>
                                        <asp:ListItem>3:30 PM</asp:ListItem>
                                        <asp:ListItem>3:45 PM</asp:ListItem>
                                        <asp:ListItem>4:00 PM</asp:ListItem>
                                        <asp:ListItem>4:15 PM</asp:ListItem>
                                        <asp:ListItem>4:30 PM</asp:ListItem>
                                        <asp:ListItem>4:45 PM</asp:ListItem>
                                        <asp:ListItem>5:00 PM</asp:ListItem>
                                        <asp:ListItem>5:15 PM</asp:ListItem>
                                        <asp:ListItem>5:30 PM</asp:ListItem>
                                        <asp:ListItem>5:45 PM</asp:ListItem>
                                        <asp:ListItem>6:00 PM</asp:ListItem>
                                        <asp:ListItem>6:15 PM</asp:ListItem>
                                        <asp:ListItem>6:30 PM</asp:ListItem>
                                        <asp:ListItem>6:45 PM</asp:ListItem>
                                        <asp:ListItem>7:00 PM</asp:ListItem>
                                        <asp:ListItem>7:15 PM</asp:ListItem>
                                        <asp:ListItem>7:30 PM</asp:ListItem>
                                        <asp:ListItem>7:45 PM</asp:ListItem>
                                        <asp:ListItem>8:00 PM</asp:ListItem>
                                        <asp:ListItem>8:15 PM</asp:ListItem>
                                        <asp:ListItem>8:30 PM</asp:ListItem>
                                        <asp:ListItem>8:45 PM</asp:ListItem>
                                        <asp:ListItem>9:00 PM</asp:ListItem>
                                        <asp:ListItem>9:15 PM</asp:ListItem>
                                        <asp:ListItem>9:30 PM</asp:ListItem>
                                        <asp:ListItem>9:45 PM</asp:ListItem>
                                        <asp:ListItem>10:00 PM</asp:ListItem>
                                        <asp:ListItem>10:15 PM</asp:ListItem>
                                        <asp:ListItem>10:30 PM</asp:ListItem>
                                        <asp:ListItem>10:45 PM</asp:ListItem>
                                        <asp:ListItem>11:00 PM</asp:ListItem>
                                        <asp:ListItem>11:15 PM</asp:ListItem>
                                        <asp:ListItem>11:30 PM</asp:ListItem>
                                        <asp:ListItem>11:45 PM</asp:ListItem>
                                    </asp:DropDownList><br />
                                     <table style="width: 100%;">
                                        <tr>
                                            <td><asp:LinkButton ID = "BlockOutSelectedTimesBtn" Class="btn btn-info btn-block" runat="server" OnClick="BlockOutSelectedTimesBtn_Click">Block Out Slected Times</asp:LinkButton></td>
                                            <td><asp:LinkButton ID = "BlockOutEntireDayBtn" Class="btn btn-info btn-block" runat="server" OnClick="BlockOutEntireDayBtn_Click">Block Out Entire Day</asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td><asp:LinkButton ID = "ReopenSelectedTimesBtn" Class="btn btn-info btn-block" runat="server" OnClick="ReopenSelectedTimesBtn_Click">Reopen Selected Times</asp:LinkButton></td>
                                            <td> <asp:LinkButton ID = "ReopenEntireDayBtn" Class="btn btn-info btn-block" runat="server" OnClick="ReopenEntireDayBtn_Click">Reopen Entire Day</asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td> <asp:LinkButton ID = "CancelManageBlockedOutDate" Class="btn btn-info btn-block" Style="background-color:green" runat="server" OnClick="CancelManageBlockedOutDate_Click" >Cancel Manage Blocked Out Date</asp:LinkButton></td>
                                        </tr>
                                    </table>
			    			    </div>
			    	</div>
                          <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
	    		    <br />
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNUserTable]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNCalendarTable]"></asp:SqlDataSource>
	    		    <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT MFNCalendarTable.Calendar_Id, MFNCalendarTable.Trainer_Id, MFNCalendarTable.User_Id, MFNCalendarTable.Calendar_Date, MFNCalendarTable.Calendar_EventName, MFNCalendarTable.Calendar_StartTime, MFNCalendarTable.Calendar_EndTime, MFNCalendarTable.Calendar_Location, MFNCalendarTable.Calendar_ApprovedByTrainer, MFNCalendarTable.Calendar_PaidByClient, MFNCalendarTable.Calendar_CompletedSession, MFNCalendarTable.Calendar_NumberOfClients, MFNUserTable.User_FirstName, MFNUserTable.User_MiddleName, MFNUserTable.User_LastName FROM MFNCalendarTable INNER JOIN MFNUserTable ON MFNCalendarTable.User_Id = MFNUserTable.User_Id WHERE (MFNCalendarTable.Calendar_Date = @date) AND (MFNCalendarTable.Trainer_Id = @Tid)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DateTextBox" Name="date" PropertyName="Text" />
                            <asp:SessionParameter Name="Tid" SessionField="TrainerID" />
                        </SelectParameters>
                    </asp:SqlDataSource>
	    		    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNBlockedDatesTable]"></asp:SqlDataSource>
                    <br />
	    		    </div>
              </div>
        </div>
           </div>
    </form>
</body>
</html>
