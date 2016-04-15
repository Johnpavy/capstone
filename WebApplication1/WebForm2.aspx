﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<!DOCTYPE html>
<html lang="en">
<head>
  <title>Bootstrap Example</title>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
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
                          <a class="NavBrand navbar-brand glyphicon glyphicon-home white" href="#">
                            </a>
                        </div>

                        <!-- Collect the nav links, forms, and other content for toggling -->
                        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                          <form class="navbar-form navbar-left" role="search">
                            <div class="form-group">
                              <input type="text" class="form-control" placeholder="Search">
                            </div>
                            <button type="submit" class="btn btn-default glyphicon glyphicon-search"></button>
                          </form>
                          <ul class="nav navbar-nav navbar-right">
                              <li><asp:LinkButton ID = "ManageSession" Class="LoginButton2 btn-info btn-block" runat="server" onclick="BookTrainer_Click">Manage Your Schedule</asp:LinkButton></li>
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
        
                <div class="HeaderContainer container-fluid">
                    <div class="row">
                        <div class="PictureColumn col-xs-12 col-sm-12 text-center">
                            <asp:Image id="ProfilePic" runat="server" src="Pictures/trainerPic.jpg" class="TrainerPicture img-circle img-responsive" alt="Trainer Picture" />
                            <br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="TrainerHeaderInfo text-center">
                            <p class="TrainerName"><asp:Label ID="UserNameLbl" CssClass="UserNameLbl" runat="server" Text="Label"></asp:Label></p>
                            <p class="TrainerRating">Overall Rating: </p>
                        </div>
                    </div>
                </div>
        
                <div class="InformationPanels panel-group" id="accordion">
                    <div class="panel panel-default">
                      <div class="panel-heading">
                        <h4 class="panel-title">
                          <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">Biography</a>
                        </h4>
                          <!-- Trigger the modal with a button -->
                          <button type="button" class="btn2 btn-info btn-lg" data-toggle="modal" data-target="#myModal" runat="server">Open Bio Modal</button>
                      </div>
                      <div id="collapse1" class="panel-collapse collapse in">
                        <div class="panel-body" id ="bio" runat="server">
                            <asp:TextBox ID="BioTextBox" runat="server" width ="75%" Height="50%" ReadOnly="true" BorderStyle="None" TextMode="MultiLine"></asp:TextBox>
                            <asp:Label ID="Fish" runat="server" Text="Label"></asp:Label>
                        </div>
                      </div>
                    </div>
                    <div class="panel panel-default">
                      <div class="panel-heading">
                        <h4 class="panel-title">
                          <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Training Types</a>
                        </h4>
                      </div>
                      <div id="collapse2" class="panel-collapse collapse">
                        <div class="panel-body" id ="specialty" runat="server">Test Text.</div>
                      </div>
                    </div>
                    <div class="panel panel-default">
                      <div class="panel-heading">
                        <h4 class="panel-title">
                          <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">Rates</a>
                        </h4>
                      </div>
                      <div id="collapse3" class="panel-collapse collapse">
                        <div class="panel-body">Test Text.</div>
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
                    <p class="ReviewLabel">Reviews:</p>
                    <hr>
			        <br>
			
                    <div class="row">
                        <div class="ReviewHeaderContainer col-xs-12 col-sm-12 form-group">
                            <p class="ReviewHeader text-center">
                                Bob
                            </p>
                            <p class="ReviewRatingGivenLabel text-center">
                                    Rating Given: 
                                <span class="ReviewRatingGiven">
                                    5/5
                                </span>
                            </p>
                        </div>
                        <div class="ReviewBodyContainer col-xs-12 col-sm-12 form-group">
                            <p class="ReviewBody text-center">
                                This is a test review! This is a test review!  This is a test review!  This is a test review!  This is a test review!  This is a test review!  This is a test review!  This is a test review!  This is a test review!  This is a test review!  This is a test review!  
                            </p>
                            <hr>
                        </div>
                    </div>
                    <div class="row">
                        <div class="ReviewHeaderContainer col-xs-12 col-sm-12 form-group">
                            <p class="ReviewHeader text-center">
                                Emily
                            </p>
                            <p class="ReviewRatingGivenLabel text-center">
                                    Rating Given: 
                                <span class="ReviewRatingGiven">
                                    5/5
                                </span>
                            </p>
                        </div>
                        <div class="ReviewBodyContainer col-xs-12 col-sm-12 form-group">
                            <p class="ReviewBody text-center">
                                This is a test review! This is a test review!  This is a test review!  This is a test review!  This is a test review!  This is a test review!  This is a test review!  This is a test review!  This is a test review!  This is a test review!  This is a test review!  
                            </p>
                            <hr>
                        </div>
                    </div>
			
                </div>
            </div>
            <footer class="FooterContainer col-xs-12 col-sm-12 text-center">
                <p class="FooterContent">TEST FOOTER</p>
            </footer>
            <!-- Bio Modal -->
            <div id="myModal" class="modal fade" role="dialog" runat="server" >
              <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Edit Bio </h4>
                  </div>
                  <div class="modal-body">
                    <p>Enter your Biography:</p>
                      <asp:TextBox ID="TempTextBox2" runat="server" Width="100%" Height="50%"  BorderStyle="Solid" TextMode="MultiLine"></asp:TextBox>
                  </div>
                  <div class="modal-footer">
                    <asp:LinkButton ID="TempUpdate" cssclass="btn btn-default" runat ="server" onclick="ComfirmUpdateBioButton2_Click">Update</asp:LinkButton>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                  </div>
                </div>
              </div>
            </div>
            </div>

            </ContentTemplate>
            </asp:UpdatePanel>
          </form>
        </body>
</html>




