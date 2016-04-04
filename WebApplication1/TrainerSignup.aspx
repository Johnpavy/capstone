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
							<label>Address</label>
							<textarea placeholder="Enter Address Here.." rows="3" class="form-control" name ="Street"></textarea>
						</div>	
						<div class="row">
							<div class="col-sm-4 form-group">
								<label>City</label>
								<input type="text" placeholder="Enter City Name Here.." class="form-control" name="City">
							</div>	
							<div class="col-sm-4 form-group">
								<label>State</label>
								<input type="text" placeholder="Enter State Name Here.." class="form-control" name="State">
							</div>	
							<div class="col-sm-4 form-group">
								<label>Zip</label>
								<input type="text" placeholder="Enter Zip Code Here.." class="form-control">
							</div>		
						</div>
						<div class="row">
							<div class="col-sm-4 form-group">
								<label>Height</label>
								<div class="input-group-btn">
                                     <div class="input-group-btn">
                                <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">Feet
                                <span class="caret"></span>
                                </button>
                                 <ul id="feet" class="dropdown-menu">
                                        <li><a href="#">4</a></li>
                                        <li><a href="#">5</a></li>
                                        <li><a href="#">6</a></li>
                                        <li><a href="#">7</a></li>
                                   </ul>
                                    </div>
                                 </div>
                                </div>
                            <div class="col-sm-4 form-group">
                                <label></label>
                                <div class="dropdown">
                                <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">Inches
                                <span class="caret"></span></button>
                                 <ul id="inches" class="dropdown-menu">
                                        <li><a href="#">1</a></li>
                                        <li><a href="#">2</a></li>
                                        <li><a href="#">3</a></li>
                                        <li><a href="#">4</a></li>
                                        <li><a href="#">5</a></li>
                                        <li><a href="#">6</a></li>
                                        <li><a href="#">7</a></li>
                                        <li><a href="#">8</a></li>
                                        <li><a href="#">9</a></li>
                                        <li><a href="#">10</a></li>
                                        <li><a href="#">11</a></li>
                                        <li><a href="#">12</a></li>
                                   </ul>
                                 </div>	
                               </div>	
							<div class="col-sm-4 form-group">
								<label>Weight</label>
								<input type="text" placeholder="Enter Weight in pounds here.." class="form-control"/>
							</div>	
						</div>	
                       </div>					
					<div class="form-group">
						<label>Phone Number</label>
						<input type="text" placeholder="Enter Phone Number Here.." class="form-control"/>
					</div>		
					<div class="form-group">
						<label>Biography</label>
						<input type="text" placeholder="Enter a bio Here.." class="form-control" name ="bio"/>
                    <div class="CreateAccountContainer text-center">
					  <asp:LinkButton ID="button" Class="btn btn-lg btn-inf" runat="server" OnClick="button_Click">Create Account</asp:LinkButton>									
					    <br />
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
