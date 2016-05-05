<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientProfile.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <link rel="shortcut icon" type="image/x-icon" href="/Pictures/favicon.ico"/>
  <title>Client Profile</title>
  <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    
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
    .SearchList{
        padding-top: 7px;
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
              <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>

                 <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                      <asp:PostBackTrigger ControlID="UploadPic" />
                    </Triggers>
                  <ContentTemplate>
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
                          <a class="NavBrand navbar-brand glyphicon glyphicon-home white" href="ClientProfile.aspx">
                            </a>
                        </div>

                        <!-- Collect the nav links, forms, and other content for toggling -->
                        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                            <ul class="SearchList nav navbar-nav navbar-left">
                                <li>
                                    <div class="form-group">
                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="---Trainer Type---" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Personal Training" Value="Personal Training"></asp:ListItem>
                                            <asp:ListItem Text="Yoga" Value="Yoga"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </li>
                                <asp:LinkButton ID="searchBtn" runat="server" CssClass="btn btn-primary" OnClick ="SearchBtn_Click"> <span aria-hidden="true" class="glyphicon glyphicon-search"></span></asp:LinkButton>				
                                <asp:Label ID="ErrorLbl" runat="server" Text="*Error" ForeColor="#FF3300" Visible="False"></asp:Label>
                            </ul>
                          <ul class="nav navbar-nav navbar-right">


                              <!--<li><button type="button" runat="server" class="BookButton btn btn-success" onclick="BookTrainer_Click">Manage Your Schedule</button></li>-->
                              <li class="dropdown">
                              <a href="#" class="NavDropdownMenu dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Menu <span class="caret"></span></a>
                              <ul class="dropdown-menu">
                                <li><a href="AccountSettings.aspx">Settings</a></li>
                                <li role="separator" class="divider"></li>
                                <li><a href="ApprovedSessionsPage.aspx">Check My Approved Sessions</a></li>
                                <li role="separator" class="divider"></li>
                                <li><a href="LogOut.aspx">Log Out</a></li>
                                <li role="separator" class="divider"></li>
                              </ul>
                            </li>
                          </ul>
                        </div><!-- /.navbar-collapse -->
                          <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Width="10%"></asp:TextBox>
                          <asp:Button ID="Button1" runat="server" Text="Search by Name" CssClass ="btn btn-primary" OnClick="Button1_Click"/>
                          <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                      </div><!-- /.container-fluid -->
                    </nav>
                </div>
        
                <div class="HeaderContainer container-fluid">
                    <div class="row">
                        <div class="PictureColumn col-xs-12 col-sm-12 text-center">
                            <asp:Image id="ProfilePic" runat="server" src="Pictures/UserPicture.jpg" class="TrainerPicture img-circle img-responsive" alt="User Picture" data-toggle="modal" data-target="#PicUploadModal" />
                            <br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="TrainerHeaderInfo text-center">
                            <p class="TrainerName"><asp:Label ID="UserNameLbl" CssClass="UserNameLbl" runat="server" Text="Label"></asp:Label></p>
                        </div>
                    </div>
                </div>
        
                <div class="InformationPanels panel-group" id="accordion">
                    <div class="panel panel-default">
                      <div class="panel-heading">
                        <h4 class="panel-title">
                          <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Training Preferences</a>
                        </h4>
                      </div>
                      <div id="collapse2" class="panel-collapse collapse">
                        <div class="panel-body" id ="preferences" runat="server">
                             <asp:TextBox ID="PrefTextBox" runat="server" width ="75%" Height="250px" ReadOnly="true" BorderStyle="None" TextMode="MultiLine" MaxLength="4000"></asp:TextBox>
                            <!-- Trigger the modal with a button -->
                          <button type="button" class="btn btn-info btn-sm" data-toggle="modal" data-target="#myModal" style="float:right;">Edit Training Prefrences</button>
                            <asp:Label ID="PrefFailLbl" runat="server" Text="Label" Visible="False"></asp:Label>
                        </div>
                      </div>
                    </div>
                    <div class="panel panel-default">
                      <div class="panel-heading">
                        <h4 class="panel-title">
                          <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">Available Equipment</a>
                        </h4>
                      </div>
                      <div id="collapse3" class="panel-collapse collapse">
                        <div class="panel-body" id="equipment" runat="server">
                            <asp:TextBox ID="AvaEquipTxt" runat="server" width ="75%" Height="250px" ReadOnly="true" BorderStyle="None" TextMode="MultiLine" MaxLength="4000"></asp:TextBox>
                            <!-- Trigger the modal with a button -->
                          <button type="button" class="btn btn-info btn-sm" data-toggle="modal" data-target="#myModal2" style="float:right;">Edit Available Equipment</button>
                            <asp:Label ID="AvEqFailLbl" runat="server" Text="Label" Visible="False"></asp:Label>
                        </div>
                      </div>
                    </div>
                    <div class="panel panel-default">
                      <div class="panel-heading">
                        <h4 class="panel-title">
                          <a data-toggle="collapse" data-parent="#accordion" href="#collapse4">Favorite Locations</a>
                        </h4>
                      </div>
                      <div id="collapse4" class="panel-collapse collapse">
                        <div class="panel-body">Test Text.</div>
                      </div>
                    </div>
                </div>
                <div class="ReviewsContainer col-xs-12 col-sm-12">
                    <br>
                    <div class="row">
                    </div>
			
                </div>
            </div>
            <footer class="FooterContainer col-xs-12 col-sm-12 text-center">
                <p class="FooterContent">TEST FOOTER</p>
            </footer>

            <!-- Trainer Prefrences Modal -->
            <div id="myModal" class="modal fade" role="dialog" runat="server" >
              <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Edit Trainer Prefrences </h4>
                  </div>
                  <div class="modal-body">
                    <p>Enter your Trainer Prefrences:<br />
                         (2000 character limit)</p>
                      <asp:TextBox ID="TrainerPrefTxt" runat="server" Width="100%" Height="250px"  BorderStyle="Solid" TextMode="MultiLine" MaxLength="4000"></asp:TextBox>
                  </div>
                  <div class="modal-footer">
                    <asp:LinkButton ID="TempUpdate" cssclass="btn btn-default" runat ="server" onclick="ComfirmUpdateTrainerPrefButton2_Click">Update</asp:LinkButton>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                  </div>
                </div>
              </div>
            </div>

            <!-- Available Equipment Modal -->
            <div id="myModal2" class="modal fade" role="dialog" runat="server" >
              <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Edit Available Equipment </h4>
                  </div>
                  <div class="modal-body">
                    <p>Enter your Available Equipment:<br />
                         (2000 character limit)</p>
                      <asp:TextBox ID="AvailableEquipTxt" runat="server" Width="100%" Height="250px"  BorderStyle="Solid" TextMode="MultiLine" MaxLength="4000"></asp:TextBox>
                  </div>
                  <div class="modal-footer">
                    <asp:LinkButton ID="LinkButton1" cssclass="btn btn-default" runat ="server" onclick="ComfirmUpdateAvaEquipButton2_Click">Update</asp:LinkButton>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                  </div>
                </div>
              </div>
            </div>

            <!--UploadPicture Modal -->
            <div id="PicUploadModal" class="modal fade" role="dialog" runat="server" >
              <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Upload Profile Picture</h4>
                  </div>
                  <div class="modal-body">
                        <p>Choose a Picture to Upload: </p>
                        <asp:FileUpload id="FileUpload1" cssclass="btn btn-default" runat="server" />
                  </div>
                  <div class="modal-footer">
                      <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                      <asp:Button id="UploadPic" cssclass="btn btn-default" Text="Upload file" OnClick="UploadPic_Click" runat="server"></asp:Button> 
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="ComfirmUpdateRatesButton_Click">Close</button>
                  </div>
                </div>
              </div>
            </div>

            </ContentTemplate>
            </asp:UpdatePanel>
          </form>
        </body>
</html>
