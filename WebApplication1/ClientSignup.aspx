﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientSignup.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="WebApplication1.ClientSignup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="/Pictures/favicon.ico"/>
    <title>Client Sign Up</title>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="JS/JavaScript.js"></script>
    <link href="CSS/TrainerSignup.css" rel="stylesheet" />
  
    <style type="text/css">
        .auto-style1 {
            font-weight: bold;
        }
    </style>
  
</head>
<body>
    <form id="form1" runat="server">
<div class="container">
    <h1 class="well">Client Registration</h1>
	<div class="col-lg-12 well">
	        <div class="row">
			</div>
					<div class="col-sm-12">
			
						<div class="form-group">
							<label>Street Address</label>
							<asp:TextBox ID="Street" runat="server" Height="35px" TextMode="MultiLine" Width="100%" CssClass ="form-control"></asp:TextBox>
						</div>	
						<div class="row">
							<div class="col-sm-4 form-group">
								<label>City</label>
								 <asp:TextBox ID="City" runat="server" Width="100%" CssClass ="form-control"></asp:TextBox>
							</div>	
							<div class="col-sm-4 form-group">
								<label>State</label>
								<asp:TextBox ID="State" runat="server" Width="100%" CssClass ="form-control"></asp:TextBox>
							</div>	
							<div class="col-sm-4 form-group">
								<label>Zip</label>
								<asp:TextBox ID="Zip" CssClass ="form-control" runat="server"></asp:TextBox>
							</div>		
						</div>
						<div class="row">
							<div class="col-sm-4 form-group">
								<label>Height Feet</label>
								<div class="dropdown">

                                      <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control">
                                       <asp:ListItem Text="Select Feet" Value="0"></asp:ListItem>
                                       <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                       <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                       <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                       <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                      </asp:DropDownList>
                                 </div>
                            </div>
                            <div class="col-sm-4 form-group">
                                <label>Inches</label>
                                <div class="dropdown">

                                    <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                       <asp:ListItem Text="Select Inches" Value=""></asp:ListItem>
                                       <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                       <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                       <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                       <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                       <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                       <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                       <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                       <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                       <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                       <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                       <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                       <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    </asp:DropDownList>
                                 </div>	
                               </div>	
							<div class="col-sm-4 form-group">
								<label>Weight Pounds</label>
								<asp:TextBox ID="Weight" runat="server" Width="100%" CssClass ="form-control"></asp:TextBox>
							</div>	
						</div>	
                       </div>					
					<div class="col-sm-4 form-group" style="left: 0px; top: 0px">
						<label>Phone Number</label>
						<asp:TextBox ID="Phone" runat="server" Width="100%" CssClass ="form-control"></asp:TextBox>
					</div>		
                    <div class="col-sm-4 form-group">
						<label>Gender</label>
                        <div class="dropdown">
                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                <asp:ListItem Text="Select Gender" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>

                            </asp:DropDownList>
                        </div>
					</div>	
                     <div class="col-sm-4 form-group">
						<label>Training Interest</label>
                        <div class="dropdown">
                            <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select one" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Personal Training" Value="Personal Training"></asp:ListItem>
                                <asp:ListItem Text="Yoga" Value="Yoga"></asp:ListItem>
                                <asp:ListItem Text="Both" Value="Personal Traning and Yoga"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
					</div>	
					<div class="form-group">

						<span class="auto-style1">Available Equipment</span>
						<asp:TextBox ID="Equipment" runat="server" TextMode="MultiLine" Width="100%" CssClass ="form-control"></asp:TextBox>
                        <div class="CreateAccountContainer text-center">
                            <br />
					        <asp:LinkButton ID="button" Class="btn btn-lg btn-inf" runat="server" OnClick="button_Click">Create Account</asp:LinkButton>									
					        <br />
					        <br />
					        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNUserLocTable]"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNUserTable]"></asp:SqlDataSource>
                        </div>
				    </div>
	</div>
      <br />

        &nbsp&nbsp
        </div>
       
      </form>
   </body>
</html>

