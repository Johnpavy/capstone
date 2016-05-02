<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="CheckOut.aspx.cs" Inherits="WebApplication1.CheckOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="/Pictures/favicon.ico"/>
  <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="JS/JavaScript.js"></script>
    <link href="CSS/Checkout.css" rel="stylesheet" />
    <title>CheckOut</title>
</head>
<body>
    
    <div class="container">

        <h1 class="well">Checkout Page</h1>

           <div class="col-lg-12 well">
                <h3 class="well">Transaction Details</h3>
               <asp:Label ID="baseRateLbl" runat="server" Text=""></asp:Label><br />
               <asp:Label ID="NumberAttendingLbl" runat="server" Text=""></asp:Label><br />
               <asp:Label ID="AdditionalRateLbl" runat="server" Text=""></asp:Label><br />
               <asp:Label ID="SubTotalLbl" runat="server" Text=""></asp:Label><br />
               <asp:Label ID="ServiceCostLbl" runat="server" Text=""></asp:Label><br />
               <asp:Label ID="TotalLbl" runat="server" Text=""></asp:Label>
            </div>
 
            <div class="col-lg-12 well">
                <h3 class="well">Pay With PayPal</h3>
                    <form class="text-center" action="https://www.paypal.com/cgi-bin/webscr" method="post" target="_top">
                    <input type="hidden" name="cmd" value="_s-xclick"/>
                    <input type="hidden" name="hosted_button_id" value="ERL3AWGSFGP32"/>
                    <input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_buynowCC_LG.gif" name="submit" alt="PayPal - The safer, easier way to pay online!"/>
                    <img alt="" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1"/>
                    </form>
            </div>
 
    
        <form id="form1" runat="server">

            <div class="col-lg-12 well">
                <h3 class="well">Pay With Credit Card</h3>
                
                <div class="form-group">
                <label>First Name: </label>
                <br />
                 <asp:TextBox ID="first_name" class="form-control" runat="server" placeholder="Enter First Name"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="first_name" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="first_name" runat="server" ErrorMessage="Invalid Input" ValidationExpression="[A-z -]{1,20}"></asp:RegularExpressionValidator>
                
                    <br />
                
                <label>Last Name: </label>
                <br />
                 <asp:TextBox ID="last_name" class="form-control" runat="server" placeholder="Enter Last Name"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="last_name" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="last_name" runat="server" ErrorMessage="Invalid Input" ValidationExpression="[A-z -]{1,20}"></asp:RegularExpressionValidator>

                    <br />

                <!-- for some reason it will not except apostrophes, future development calls for a RegEx expression that accepts apostrophes in the first, last, city, and street names-->
                <label>Country: </label>
                <br />
                 <asp:DropDownList ID="country_code" class="form-control" runat="server">
                     <asp:ListItem Value="US">United States</asp:ListItem>
                 </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="country_code" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>

                    <br />

                <label>Street Address: </label>
                <br />
                 <asp:TextBox ID="address" class="form-control" runat="server" placeholder="Enter Street Address"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="address" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="address" runat="server" ErrorMessage="Invalid Input" ValidationExpression="[A-z0-9 .-]{1,30}"></asp:RegularExpressionValidator>

                    <br />

                <label>City: </label>
                <br />
                 <asp:TextBox ID="city" class="form-control" runat="server" placeholder="Enter City Name"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="city" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="city" runat="server" ErrorMessage="Invalid Input" ValidationExpression="[A-z -]{1,30}"></asp:RegularExpressionValidator>

                    <br />

                 <label>Zip Code: </label>
                <br />
                 <asp:TextBox ID="postal_code" class="form-control" runat="server" placeholder="Enter Zip Code"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="postal_code" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="postal_code" runat="server" ErrorMessage="Invalid Input" ValidationExpression="\d{5}"></asp:RegularExpressionValidator>

                    <br />

                 <label>State: </label>
                <br />
                 <asp:DropDownList ID="state" class="form-control" runat="server">
                     <asp:ListItem Value="TX">Texas</asp:ListItem>
                 </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="state" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>

                    <br />

                <label>Card Type: </label>
                <br />
                 <asp:DropDownList ID="card_type" class="form-control" runat="server">
                     <asp:ListItem Value="visa">Visa</asp:ListItem>
                     <asp:ListItem Value="mastercard">Master Card</asp:ListItem>
                 </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="card_type" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator> 

                    <br />

                <label>Credit Card Number: </label>
                <br />
                 <asp:TextBox ID="card_number" class="form-control" runat="server" placeholder="Enter Credit Card Number" Text="4877274905927862"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="card_number" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator> 
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="card_number" runat="server" ErrorMessage="Invalid Input" ValidationExpression="\d{16}"></asp:RegularExpressionValidator>

                    <br />

                <label>CVV: </label>
                <br />
                 <asp:TextBox ID="cvv2" class="form-control" runat="server" placeholder="Enter Security Code" Text="874"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cvv2" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>  
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="cvv2" runat="server" ErrorMessage="Invalid Input" ValidationExpression="\d{3}"></asp:RegularExpressionValidator>

                    <br />

                <label>Expiration Month (mm): </label>
                <br />
                 <asp:TextBox ID="ex_month" class="form-control" runat="server" placeholder="Enter Expiration Month" Text="11"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ex_month" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="ex_month" runat="server" ErrorMessage="Invalid Input" ValidationExpression="\d{1,2}"></asp:RegularExpressionValidator>
                <br />
                <label>Expiration Year (yyyy): </label>
                <br />
                 <asp:TextBox ID="ex_year" class="form-control" runat="server" placeholder="Enter Expiration Year" Text="2018"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ex_year" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>        

                 <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="ex_year" runat="server" ErrorMessage="Invalid Input" ValidationExpression="\d{4}"></asp:RegularExpressionValidator>

                </div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />

           

            <div class="CreateAccountContainer text-center">
                <asp:Button ID="Button1" Class="btn btn-lg btn-inf" runat="server" Text="Confirm Payment" OnClick="Button1_Click" />
            </div>
       </div>
   </form>
  </div>
</body>
</html>