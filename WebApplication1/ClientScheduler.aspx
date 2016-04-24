<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientScheduler.aspx.cs" Inherits="WebApplication1.WebForm6" EnableViewState="true"%>

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
    .UserPicture {width: 25%; height: 25%; margin-left: auto;
    margin-right: auto; border-radius: 50%;}
    .UserHeaderInfo{padding-top: 1%; }
    .UserName {color: #FFFFFF; font-size: 4.5vw;}
    .UserRating {color: #FFFFFF; font-size: 3.5vw;}
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
                              <li><asp:LinkButton ID = "LinkButton6" Class="LoginButton2 btn-info btn-block" runat="server" OnClick="BackToProfile_Click">Back To Profile</asp:LinkButton></li>
                              <!--<li><button type="button" runat="server" class="BookButton btn btn-success" onclick="BookUser_Click">Manage Your Schedule</button></li>-->
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
                                <asp:Image id="ProfilePic" runat="server" src="Pictures/TrainerPic.jpg" class="UserPicture img-circle img-responsive" alt="User Picture" />
                                <br />
                            </div>
                        </div>
                        <div class="row">
                            <div class="UserHeaderInfo text-center">
                                <p class="UserName"><asp:Label ID="UserNameLbl" CssClass="UserNameLbl" runat="server" Text="Label"></asp:Label>'s Schedule</p>
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
                           </div>

                    <div id ="RequestSessionDiv" class="row centered-form" runat="server">
                        <div class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4">
        	                <div class="panel panel-default">
                           <div class="panel-heading">
			    		        <h3 class="panel-title">Select</h3>
			 			        </div>
			 			        <div class="panel-body">
			    						Selected Date: <asp:TextBox ID="SelectedDateTxtBox" runat="server" ReadOnly="true"></asp:TextBox><br />
                                        Event Summary: <asp:TextBox ID="EventSummaryTxtBox" runat="server" TextMode="MultiLine" Height="84px" Width="186px"></asp:TextBox><br />
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
                                        <asp:ListItem>10:30 AM</asp:ListItem>
                                        <asp:ListItem>10:15 AM</asp:ListItem>
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
                                        Location:  <asp:DropDownList ID="LocationDrpDown" runat="server"></asp:DropDownList><!-- How to get address stuff? --><br />
                                        Number of People Attending (Including yourself) <asp:DropDownList ID="NumberInAttendance" runat="server"></asp:DropDownList><br />
                                        <asp:LinkButton ID = "RequestAppointmentBtn" Class="btn btn-info btn-block" runat="server" OnClick="RequestAppointmentBtn_Click">Request Appointment</asp:LinkButton>
			    	            </div>
	    		        </div>
                    </div>
                     </div>

                        <div id ="YourComfirmedSessions" class="row centered-form" runat="server">

                     </div>
                </div>
                </div>
            </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNBlockedDatesTable]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNCalendarTable]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNUserLocTable]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerLocTable]"></asp:SqlDataSource>
    </form>
</body>
</html>
