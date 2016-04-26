<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminControl.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="WebApplication1.AdminControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link rel="shortcut icon" type="image/x-icon" href="/Pictures/favicon.ico"/>
  <title>Admin Control</title>
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
            <!--An Admin cannot delete themselves or another super admin from this section!-->
              <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" 
                  SelectCommand="SELECT [Admin_Id], [Admin_Email], [Admin_FirstName], [Admin_MiddleName], [Admin_LastName], [Admin_Picture] FROM [MFNAdminTable] WHERE (([Admin_Id] &lt;&gt; @Admin_Id) AND ([Admin_isSuperAdmin] = @Admin_isSuperAdmin))" OldValuesParameterFormatString="original_{0}">
                  <SelectParameters>
                      <asp:SessionParameter Name="Admin_Id" SessionField="AdminId" Type="Int32" />
                      <asp:Parameter DefaultValue="False" Name="Admin_isSuperAdmin" Type="Boolean" />
                  </SelectParameters>
              </asp:SqlDataSource>

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
                          <a class="NavBrand navbar-brand glyphicon glyphicon-home white" href="AdminControl.aspx">
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
                                <asp:LinkButton ID="searchBtn" runat="server" CssClass="btn btn-primary" > <span aria-hidden="true" class="glyphicon glyphicon-search"></span></asp:LinkButton>				
                                <asp:Label ID="ErrorLbl" runat="server" Text="*Error" ForeColor="#FF3300" Visible="False"></asp:Label>
                            </ul>
                          <ul class="nav navbar-nav navbar-right">


                              
                              <li class="dropdown">
                              <a href="#" class="NavDropdownMenu dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Menu <span class="caret"></span></a>
                              <ul class="dropdown-menu">
                                <li role="separator" class="divider"></li>
                                <li><a href="LogOut.aspx">Log Out</a></li>
                                  <!--Remove Next Line after testing! -->
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
                            <asp:Image id="ProfilePic" runat="server" src="Pictures/AdminPic.jpg" class="TrainerPicture img-circle img-responsive" alt="Trainer Picture" />
                            <br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="TrainerHeaderInfo text-center">
                            <p class="AdminName"><asp:Label ID="UserNameLbl" CssClass="UserNameLbl" runat="server" Text="Label"></asp:Label></p>
                        </div>
                    </div>
                </div>

           <div id="AdminControlOptions" runat="server" class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4" visible="true">
        	<div class="panel panel-default">
        		<div class="panel-heading">
			    		<h3 class="panel-title">Admin Options</h3>
			 			</div>
			 			<div class="panel-body">
                            <ul id="OptionsBar">
                                <li><asp:LinkButton ID="AdminTableBtn" Class="btn btn-primary btn-lg btn-block" runat="server" OnClick="AdminTableBtn_Click">View Admin Table</asp:LinkButton></li>
                                <li><asp:LinkButton ID="AdminRequestBtn" Class="btn btn-primary btn-lg btn-block" runat="server" OnClick="AdminRequestBtn_Click">Send Out Admin Invitation</asp:LinkButton></li>
                                <li><asp:LinkButton ID="ViewAllTablesBtn" Class="btn btn-primary btn-lg btn-block" runat="server">View All Tables</asp:LinkButton></li>
                                <li><asp:LinkButton ID="PasswordChangeBtn" Class="btn btn-primary btn-lg btn-block" runat="server" OnClick="PasswordChangeBtn_Click">Change Your Password</asp:LinkButton></li>
                                <li><asp:LinkButton ID="AproveTrainerBtn" Class="btn btn-primary btn-lg btn-block" runat="server" OnClick="AproveTrainerBtn_Click">Approve Trainers</asp:LinkButton></li>
                            </ul>
			    	    </div>
	    		</div>
               </div>


                
           <div id="AdminTableView" runat="server"  class="col-xs-12 col-sm-8 col-md-12 col-sm-offset-2 col-md-offset-4" style="float:right" visible="false">
        	<div class="panel panel-default">
        		<div class="panel-heading">
			    		<h3 class="panel-title">Admin Table</h3>
			 			</div>
			 			<div class="panel-body">
                             <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" DataKeyNames="Admin_Id" DataSourceID="SqlDataSource1">
                                 <Columns>
                                     <asp:BoundField DataField="Admin_Id" HeaderText="Admin_Id" InsertVisible="False" ReadOnly="True" SortExpression="Admin_Id" />
                                     <asp:BoundField DataField="Admin_Email" HeaderText="Admin_Email" SortExpression="Admin_Email" />
                                     <asp:BoundField DataField="Admin_FirstName" HeaderText="Admin_FirstName" SortExpression="Admin_FirstName" />
                                     <asp:BoundField DataField="Admin_MiddleName" HeaderText="Admin_MiddleName" SortExpression="Admin_MiddleName" />
                                     <asp:BoundField DataField="Admin_LastName" HeaderText="Admin_LastName" SortExpression="Admin_LastName" />
                                     <asp:BoundField DataField="Admin_Picture" HeaderText="Admin_Picture" SortExpression="Admin_Picture" />
                                 </Columns>
                             </asp:GridView>
                             <asp:LinkButton ID="CloseAdminTableView" runat="server" OnClick="CloseAdminTableView_Click">Close</asp:LinkButton>
			    	    </div>
	    		</div>
               </div>

            <div id="AdminSignupPanel" runat="server" class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4" visible ="false">
        	<div class="panel panel-default">
        		<div class="panel-heading">
			    		<h3 class="panel-title">Send Out Registration For Admin</h3>
			 			</div>
			 			<div class="panel-body">

			    			<div class="row">
			    				<div class="col-xs-6 col-sm-6 col-md-6">
			    					<div class="form-group">
                                        <asp:TextBox  name="cFName" id="first_name" class="form-control input-sm" placeholder="First Name" runat="server"/>
			    					</div>
			    				</div>
			    				<div class="col-xs-6 col-sm-6 col-md-6">
			    					<div class="form-group">
                                        <asp:TextBox  name="cLName" id="last_name" class="form-control input-sm" placeholder="Last Name" runat="server"/>
			    					</div>
			    				</div>
			    			</div>

			    			<div class="form-group">
                                <asp:TextBox  name="CEmail" id="Email" class="form-control input-sm" placeholder="Email Address" runat="server"/>
			    			</div>

			    			
			    			<asp:LinkButton ID = "LinkButton3" Class="btn btn-info btn-block" runat="server" OnClick="LinkButton3_Click" >Send Invite</asp:LinkButton><br />
                             <asp:LinkButton ID="CancelInvite" runat="server" OnClick="CancelInvite_Click">Cancel</asp:LinkButton>
			    		

			    	</div>
                  <asp:Label ID="ErrorLabel2" runat="server" Text="Label" Visible="False"></asp:Label>
	    		</div>
    		</div>

            <div id="ChangeYourPassword" runat="server" class="col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4" visible ="false">
        	<div class="panel panel-default">
        		<div class="panel-heading">
			    		<h3 class="panel-title">Change Your Password</h3>
			 			</div>
			 			<div class="panel-body">
                             <label>New Password</label><asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox> <br />
                             <label>Comfirm Password</label><asp:TextBox ID="Cpassword" runat="server" TextMode="Password"></asp:TextBox><br />
                             <asp:LinkButton ID="ResetPassword" runat="server" OnClick="ResetPassword_Click">Change Password</asp:LinkButton><br />
                             <asp:LinkButton ID="CancelUpdate" runat="server" OnClick="CancelUpdate_Click">Cancel</asp:LinkButton>
                             <asp:Label ID="ErrorLbl3" runat="server" Text="Label" Visible ="false" ForeColor="Red"></asp:Label>
			    	    </div>
	    		</div>
               </div>

            <div id="ApproveTrainerDiv" runat="server" class="col-xs-12 col-sm-8 col-md-12 col-sm-offset-2 col-md-offset-4" style="float:right" visible ="false">
        	<div class="panel panel-default">
        		<div class="panel-heading">
			    		<h3 class="panel-title">Approve Trainers</h3>
			 			</div>
			 			<div class="panel-body">
                             <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Trainer_Id" DataSourceID="SqlDataSource3">
                                 <Columns>
                                     <asp:CommandField ShowEditButton="True" />
                                     <asp:BoundField DataField="Trainer_Id" HeaderText="Trainer_Id" InsertVisible="False" ReadOnly="True" SortExpression="Trainer_Id" />
                                     <asp:BoundField DataField="Trainer_Email" HeaderText="Trainer_Email" SortExpression="Trainer_Email" />
                                     <asp:BoundField DataField="Trainer_DateofBirth" HeaderText="Trainer_DateofBirth" SortExpression="Trainer_DateofBirth" />
                                     <asp:BoundField DataField="Trainer_FirstName" HeaderText="Trainer_FirstName" SortExpression="Trainer_FirstName" />
                                     <asp:BoundField DataField="Trainer_MiddleName" HeaderText="Trainer_MiddleName" SortExpression="Trainer_MiddleName" />
                                     <asp:BoundField DataField="Trainer_LastName" HeaderText="Trainer_LastName" SortExpression="Trainer_LastName" />
                                     <asp:CheckBoxField DataField="Trainer_VerifiedCert" HeaderText="Trainer_VerifiedCert" SortExpression="Trainer_VerifiedCert" />
                                     <asp:BoundField DataField="Trainer_Specialty" HeaderText="Trainer_Specialty" SortExpression="Trainer_Specialty" />
                                 </Columns>
                             </asp:GridView>
			    	         <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" DeleteCommand="DELETE FROM [MFNTrainerTable] WHERE [Trainer_Id] = @original_Trainer_Id AND (([Trainer_Email] = @original_Trainer_Email) OR ([Trainer_Email] IS NULL AND @original_Trainer_Email IS NULL)) AND (([Trainer_DateofBirth] = @original_Trainer_DateofBirth) OR ([Trainer_DateofBirth] IS NULL AND @original_Trainer_DateofBirth IS NULL)) AND (([Trainer_FirstName] = @original_Trainer_FirstName) OR ([Trainer_FirstName] IS NULL AND @original_Trainer_FirstName IS NULL)) AND (([Trainer_MiddleName] = @original_Trainer_MiddleName) OR ([Trainer_MiddleName] IS NULL AND @original_Trainer_MiddleName IS NULL)) AND (([Trainer_LastName] = @original_Trainer_LastName) OR ([Trainer_LastName] IS NULL AND @original_Trainer_LastName IS NULL)) AND [Trainer_VerifiedCert] = @original_Trainer_VerifiedCert AND (([Trainer_Specialty] = @original_Trainer_Specialty) OR ([Trainer_Specialty] IS NULL AND @original_Trainer_Specialty IS NULL))" InsertCommand="INSERT INTO [MFNTrainerTable] ([Trainer_Email], [Trainer_DateofBirth], [Trainer_FirstName], [Trainer_MiddleName], [Trainer_LastName], [Trainer_VerifiedCert], [Trainer_Specialty]) VALUES (@Trainer_Email, @Trainer_DateofBirth, @Trainer_FirstName, @Trainer_MiddleName, @Trainer_LastName, @Trainer_VerifiedCert, @Trainer_Specialty)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [Trainer_Id], [Trainer_Email], [Trainer_DateofBirth], [Trainer_FirstName], [Trainer_MiddleName], [Trainer_LastName], [Trainer_VerifiedCert], [Trainer_Specialty] FROM [MFNTrainerTable]" UpdateCommand="UPDATE [MFNTrainerTable] SET [Trainer_Email] = @Trainer_Email, [Trainer_DateofBirth] = @Trainer_DateofBirth, [Trainer_FirstName] = @Trainer_FirstName, [Trainer_MiddleName] = @Trainer_MiddleName, [Trainer_LastName] = @Trainer_LastName, [Trainer_VerifiedCert] = @Trainer_VerifiedCert, [Trainer_Specialty] = @Trainer_Specialty WHERE [Trainer_Id] = @original_Trainer_Id AND (([Trainer_Email] = @original_Trainer_Email) OR ([Trainer_Email] IS NULL AND @original_Trainer_Email IS NULL)) AND (([Trainer_DateofBirth] = @original_Trainer_DateofBirth) OR ([Trainer_DateofBirth] IS NULL AND @original_Trainer_DateofBirth IS NULL)) AND (([Trainer_FirstName] = @original_Trainer_FirstName) OR ([Trainer_FirstName] IS NULL AND @original_Trainer_FirstName IS NULL)) AND (([Trainer_MiddleName] = @original_Trainer_MiddleName) OR ([Trainer_MiddleName] IS NULL AND @original_Trainer_MiddleName IS NULL)) AND (([Trainer_LastName] = @original_Trainer_LastName) OR ([Trainer_LastName] IS NULL AND @original_Trainer_LastName IS NULL)) AND [Trainer_VerifiedCert] = @original_Trainer_VerifiedCert AND (([Trainer_Specialty] = @original_Trainer_Specialty) OR ([Trainer_Specialty] IS NULL AND @original_Trainer_Specialty IS NULL))">
                                 <DeleteParameters>
                                     <asp:Parameter Name="original_Trainer_Id" Type="Int32" />
                                     <asp:Parameter Name="original_Trainer_Email" Type="String" />
                                     <asp:Parameter DbType="Date" Name="original_Trainer_DateofBirth" />
                                     <asp:Parameter Name="original_Trainer_FirstName" Type="String" />
                                     <asp:Parameter Name="original_Trainer_MiddleName" Type="String" />
                                     <asp:Parameter Name="original_Trainer_LastName" Type="String" />
                                     <asp:Parameter Name="original_Trainer_VerifiedCert" Type="Boolean" />
                                     <asp:Parameter Name="original_Trainer_Specialty" Type="String" />
                                 </DeleteParameters>
                                 <InsertParameters>
                                     <asp:Parameter Name="Trainer_Email" Type="String" />
                                     <asp:Parameter DbType="Date" Name="Trainer_DateofBirth" />
                                     <asp:Parameter Name="Trainer_FirstName" Type="String" />
                                     <asp:Parameter Name="Trainer_MiddleName" Type="String" />
                                     <asp:Parameter Name="Trainer_LastName" Type="String" />
                                     <asp:Parameter Name="Trainer_VerifiedCert" Type="Boolean" />
                                     <asp:Parameter Name="Trainer_Specialty" Type="String" />
                                 </InsertParameters>
                                 <UpdateParameters>
                                     <asp:Parameter Name="Trainer_Email" Type="String" />
                                     <asp:Parameter DbType="Date" Name="Trainer_DateofBirth" />
                                     <asp:Parameter Name="Trainer_FirstName" Type="String" />
                                     <asp:Parameter Name="Trainer_MiddleName" Type="String" />
                                     <asp:Parameter Name="Trainer_LastName" Type="String" />
                                     <asp:Parameter Name="Trainer_VerifiedCert" Type="Boolean" />
                                     <asp:Parameter Name="Trainer_Specialty" Type="String" />
                                     <asp:Parameter Name="original_Trainer_Id" Type="Int32" />
                                     <asp:Parameter Name="original_Trainer_Email" Type="String" />
                                     <asp:Parameter DbType="Date" Name="original_Trainer_DateofBirth" />
                                     <asp:Parameter Name="original_Trainer_FirstName" Type="String" />
                                     <asp:Parameter Name="original_Trainer_MiddleName" Type="String" />
                                     <asp:Parameter Name="original_Trainer_LastName" Type="String" />
                                     <asp:Parameter Name="original_Trainer_VerifiedCert" Type="Boolean" />
                                     <asp:Parameter Name="original_Trainer_Specialty" Type="String" />
                                 </UpdateParameters>
                             </asp:SqlDataSource>
			    	         <br />
                             <asp:LinkButton ID="CloseApproveTrainerDiv" runat="server" OnClick="CloseApproveTrainerDiv_Click">Close</asp:LinkButton>
			    	    </div>
	    		</div>
               </div>

            </div>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNAdminTable]"></asp:SqlDataSource>
          </form>
        </body>
</html>
