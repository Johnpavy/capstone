<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainerSignup.aspx.cs" Inherits="WebApplication1.ClientAccountaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Trainer Sign Up</title>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="JS/JavaScript.js"></script>
    <link href="CSS/TrainerSignup.css" rel="stylesheet" />
  
</head>
<body>
    <form id="form1" runat="server">
<div class="container">
    <h1 class="well">Trainer Registration</h1>
	<div class="col-lg-12 well">
	        <div class="row">
			</div>
					<div class="col-sm-12">
			
						<div class="form-group">
							<label>Street Address</label>
							<textarea placeholder="Enter Street Address" rows="3" class="form-control" name ="Street"></textarea>
						</div>	
						<div class="row">
							<div class="col-sm-4 form-group">
								<label>City</label>
								<input type="text" placeholder="Enter City Name" class="form-control" name="City">
							</div>	
							<div class="col-sm-4 form-group">
								<label>State</label>
								<input type="text" placeholder="Enter State Name" class="form-control" name="State">
							</div>	
							<div class="col-sm-4 form-group">
								<label>Zip</label>
								<input type="text" placeholder="Enter Zip Code" class="form-control">
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
                                       <asp:ListItem Text="Select Inches" Value="0"></asp:ListItem>
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
								<input type="text" placeholder="Enter Weight in pounds" class="form-control" name="weight"/>
							</div>	
						</div>	
                       </div>					
					<div class="col-sm-4 form-group">
						<label>Phone Number</label>
						<input type="text" placeholder="Enter Phone Number" class="form-control" name="pnumber"/>
					</div>		
                    <div class="col-sm-4 form-group">
						<label>Gender</label>
                        <div class="dropdown">
                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select Gender" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>

                            </asp:DropDownList>
                        </div>
					</div>	
                    <div class="col-sm-4 form-group">
						<label>Training Specialty</label>
                        <div class="dropdown">
                            <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select Specialty" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Personal Training" Value="Personal Training"></asp:ListItem>
                                <asp:ListItem Text="Yoga" Value="Yoga"></asp:ListItem>
                                <asp:ListItem Text="Both" Value="Personal Traning and Yoga"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
					</div>	
					<div class="form-group">

						<label>Biography</label>
						<textarea placeholder="Enter Bio" rows="3" class="form-control" name ="Bio"></textarea>
                        <div class="CreateAccountContainer text-center">
                            <br />
					        <asp:LinkButton ID="button" Class="btn btn-lg btn-inf" runat="server" OnClick="button_Click">Create Account</asp:LinkButton>									
					        <br />
					        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                        </div>
				    </div>
	</div>
      <br />

        &nbsp&nbsp
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" DeleteCommand="DELETE FROM [MFNTrainerLocTable] WHERE [TrainerLoc_Id] = @original_TrainerLoc_Id AND (([Trainer_Id] = @original_Trainer_Id) OR ([Trainer_Id] IS NULL AND @original_Trainer_Id IS NULL)) AND (([TrainerLoc_Lat] = @original_TrainerLoc_Lat) OR ([TrainerLoc_Lat] IS NULL AND @original_TrainerLoc_Lat IS NULL)) AND (([TrainerLoc_Long] = @original_TrainerLoc_Long) OR ([TrainerLoc_Long] IS NULL AND @original_TrainerLoc_Long IS NULL)) AND (([TrainerLoc_StreetAddress] = @original_TrainerLoc_StreetAddress) OR ([TrainerLoc_StreetAddress] IS NULL AND @original_TrainerLoc_StreetAddress IS NULL)) AND (([TrainerLoc_Description] = @original_TrainerLoc_Description) OR ([TrainerLoc_Description] IS NULL AND @original_TrainerLoc_Description IS NULL)) AND (([TrainerLoc_Prefered] = @original_TrainerLoc_Prefered) OR ([TrainerLoc_Prefered] IS NULL AND @original_TrainerLoc_Prefered IS NULL))" InsertCommand="INSERT INTO [MFNTrainerLocTable] ([TrainerLoc_Id], [Trainer_Id], [TrainerLoc_Lat], [TrainerLoc_Long], [TrainerLoc_StreetAddress], [TrainerLoc_Description], [TrainerLoc_Prefered]) VALUES (@TrainerLoc_Id, @Trainer_Id, @TrainerLoc_Lat, @TrainerLoc_Long, @TrainerLoc_StreetAddress, @TrainerLoc_Description, @TrainerLoc_Prefered)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [MFNTrainerLocTable]" UpdateCommand="UPDATE [MFNTrainerLocTable] SET [Trainer_Id] = @Trainer_Id, [TrainerLoc_Lat] = @TrainerLoc_Lat, [TrainerLoc_Long] = @TrainerLoc_Long, [TrainerLoc_StreetAddress] = @TrainerLoc_StreetAddress, [TrainerLoc_Description] = @TrainerLoc_Description, [TrainerLoc_Prefered] = @TrainerLoc_Prefered WHERE [TrainerLoc_Id] = @original_TrainerLoc_Id AND (([Trainer_Id] = @original_Trainer_Id) OR ([Trainer_Id] IS NULL AND @original_Trainer_Id IS NULL)) AND (([TrainerLoc_Lat] = @original_TrainerLoc_Lat) OR ([TrainerLoc_Lat] IS NULL AND @original_TrainerLoc_Lat IS NULL)) AND (([TrainerLoc_Long] = @original_TrainerLoc_Long) OR ([TrainerLoc_Long] IS NULL AND @original_TrainerLoc_Long IS NULL)) AND (([TrainerLoc_StreetAddress] = @original_TrainerLoc_StreetAddress) OR ([TrainerLoc_StreetAddress] IS NULL AND @original_TrainerLoc_StreetAddress IS NULL)) AND (([TrainerLoc_Description] = @original_TrainerLoc_Description) OR ([TrainerLoc_Description] IS NULL AND @original_TrainerLoc_Description IS NULL)) AND (([TrainerLoc_Prefered] = @original_TrainerLoc_Prefered) OR ([TrainerLoc_Prefered] IS NULL AND @original_TrainerLoc_Prefered IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_TrainerLoc_Id" Type="Int32" />
                <asp:Parameter Name="original_Trainer_Id" Type="Int32" />
                <asp:Parameter Name="original_TrainerLoc_Lat" Type="Decimal" />
                <asp:Parameter Name="original_TrainerLoc_Long" Type="Decimal" />
                <asp:Parameter Name="original_TrainerLoc_StreetAddress" Type="String" />
                <asp:Parameter Name="original_TrainerLoc_Description" Type="String" />
                <asp:Parameter Name="original_TrainerLoc_Prefered" Type="Boolean" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="TrainerLoc_Id" Type="Int32" />
                <asp:Parameter Name="Trainer_Id" Type="Int32" />
                <asp:Parameter Name="TrainerLoc_Lat" Type="Decimal" />
                <asp:Parameter Name="TrainerLoc_Long" Type="Decimal" />
                <asp:Parameter Name="TrainerLoc_StreetAddress" Type="String" />
                <asp:Parameter Name="TrainerLoc_Description" Type="String" />
                <asp:Parameter Name="TrainerLoc_Prefered" Type="Boolean" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Trainer_Id" Type="Int32" />
                <asp:Parameter Name="TrainerLoc_Lat" Type="Decimal" />
                <asp:Parameter Name="TrainerLoc_Long" Type="Decimal" />
                <asp:Parameter Name="TrainerLoc_StreetAddress" Type="String" />
                <asp:Parameter Name="TrainerLoc_Description" Type="String" />
                <asp:Parameter Name="TrainerLoc_Prefered" Type="Boolean" />
                <asp:Parameter Name="original_TrainerLoc_Id" Type="Int32" />
                <asp:Parameter Name="original_Trainer_Id" Type="Int32" />
                <asp:Parameter Name="original_TrainerLoc_Lat" Type="Decimal" />
                <asp:Parameter Name="original_TrainerLoc_Long" Type="Decimal" />
                <asp:Parameter Name="original_TrainerLoc_StreetAddress" Type="String" />
                <asp:Parameter Name="original_TrainerLoc_Description" Type="String" />
                <asp:Parameter Name="original_TrainerLoc_Prefered" Type="Boolean" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
      </form>
        </body>
</html>
