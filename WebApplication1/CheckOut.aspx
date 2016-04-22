<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="WebApplication1.CheckOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="/Pictures/favicon.ico"/>
    <title>CheckOut</title>
</head>
<body>
    <div class="jumbotron">
        <h1>CHECKOUT PAGE</h1>
    </div>
    <div>
        <h2>Pay With PayPal</h2>
            <form action="https://www.paypal.com/cgi-bin/webscr" method="post" target="_top">
            <input type="hidden" name="cmd" value="_s-xclick"/>
            <input type="hidden" name="hosted_button_id" value="ERL3AWGSFGP32"/>
            <input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_buynowCC_LG.gif" name="submit" alt="PayPal - The safer, easier way to pay online!"/>
            <img alt="" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1"/>
            </form>
    </div>

    <form id="form1" runat="server">

    <div class="row">

        <div class="col-md-3">
            <h2>Pay With Credit Card</h2>
             <p>Please Enter First Name: <asp:TextBox ID="first_name" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="first_name" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="first_name" runat="server" ErrorMessage="Invalid Input" ValidationExpression="[A-z -]{1,20}"></asp:RegularExpressionValidator>
            </p>

             <p>Please Enter Last Name: 
             <asp:TextBox ID="last_name" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="last_name" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="last_name" runat="server" ErrorMessage="Invalid Input" ValidationExpression="[A-z -]{1,20}"></asp:RegularExpressionValidator>
            </p>
            <!-- for some reason it will not except apostrophes, future development calls for a RegEx expression that accepts apostrophes in the first, last, city, and street names-->
             <p>Please Enter Country: 
             <asp:DropDownList ID="country_code" runat="server">
                 <asp:ListItem Value="US">United States</asp:ListItem>
             </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="country_code" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
            </p>

             <p>Please Enter Street Address:
             <asp:TextBox ID="address" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="address" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="address" runat="server" ErrorMessage="Invalid Input" ValidationExpression="[A-z0-9 .-]{1,30}"></asp:RegularExpressionValidator>
            </p>
            
             <p>Please Enter City: 
             <asp:TextBox ID="city" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="city" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="city" runat="server" ErrorMessage="Invalid Input" ValidationExpression="[A-z -]{1,30}"></asp:RegularExpressionValidator>
            </p>

             <p>Please Enter Zip Code:
             <asp:TextBox ID="postal_code" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="postal_code" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="postal_code" runat="server" ErrorMessage="Invalid Input" ValidationExpression="\d{5}"></asp:RegularExpressionValidator>
            </p>

             <p>Please Enter State: 
             <asp:DropDownList ID="state" runat="server">
                 <asp:ListItem Value="TX">Texas</asp:ListItem>
             </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="state" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
            </p>
        </div>
        

        <div class="col-md-4">

            <p>Please Enter Card Type: 
             <asp:DropDownList ID="card_type" runat="server">
                 <asp:ListItem Value="visa">Visa</asp:ListItem>
                 <asp:ListItem Value="mastercard">Master Card</asp:ListItem>
             </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="card_type" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator> 
            </p>
            <p>Creadit Card Number: 
             <asp:TextBox ID="card_number" runat="server" Text="4877274905927862"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="card_number" runat="server" ErrorMessage="Invalid Input" ValidationExpression="\d{16}"></asp:RegularExpressionValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="card_number" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator> 
            </p>
            <p>cvv: 
             <asp:TextBox ID="cvv2" runat="server" Text="874"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="cvv2" runat="server" ErrorMessage="Invalid Input" ValidationExpression="\d{3}"></asp:RegularExpressionValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cvv2" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>  
            </p>
            <p>Expiration Month (mm): 
             <asp:TextBox ID="ex_month" runat="server" Text="11"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="ex_month" runat="server" ErrorMessage="Invalid Input" ValidationExpression="\d{1,2}"></asp:RegularExpressionValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ex_month" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>        
            </p>    
            <p>Expiration Year (yyyy): 
             <asp:TextBox ID="ex_year" runat="server" Text="2018"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="ex_year" runat="server" ErrorMessage="Invalid Input" ValidationExpression="\d{4}"></asp:RegularExpressionValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ex_year" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>        
            </p>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />

        </div>

        <div class="col-md-5">
            <asp:Button ID="Button1" runat="server" Text="Confirm Payment" OnClick="Button1_Click" />
        </div>

    </div>
         </form>

</body>
</html>