<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="WebApplication1.CheckOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <div class="jumbotron">
        <h1>CHECKOUT PAGE</h1>
    </div>

    <div class="row">
       
        <!-- 
            city = "Johnstown",
            country_code = "US",
            line1 = "52 N Main ST",
            postal_code = "43210",
            state = "OH"
        },
            cvv2 = "874",
            expire_month = 11,
            expire_year = 2018,
            first_name = "Joe",
            last_name = "Shopper",
            number = "4877274905927862",
            type = "visa"
        -->

        <div class="col-md-3">
            <h2>Payment Information</h2>
             <p>Please Enter First Name: </p>
             <asp:TextBox ID="first_name" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="first_name" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>

             <p>Please Enter Last Name: </p>
             <asp:TextBox ID="last_name" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="last_name" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>

             <p>Please Enter Country: </p>
             <asp:DropDownList ID="country_code" runat="server">
                 <asp:ListItem Value="US">United States</asp:ListItem>
                 <asp:ListItem Value="CA">Canada</asp:ListItem>
             </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="country_code" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>

             <p>Please Enter Street Address: </p>
             <asp:TextBox ID="address" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="address" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>

             <p>Please Enter City: </p>
             <asp:TextBox ID="city" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="city" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>

             <p>Please Enter Zip Code: </p>
             <asp:TextBox ID="postal_code" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="postal_code" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="postal_code" runat="server" ErrorMessage="Only 5 digit Numbers allowed" ValidationExpression="\d{5}"></asp:RegularExpressionValidator>
             
             <p>Please Enter State: </p>
             <asp:TextBox ID="state" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="state" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
        </div>
        

        <div class="col-md-4">
            <p>Please Enter Card Type: </p>
             <asp:DropDownList ID="card_type" runat="server">
                 <asp:ListItem Value="visa">Visa</asp:ListItem>
                 <asp:ListItem Value="mastercard">Master Card</asp:ListItem>
             </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="card_type" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator> 

            <p>Creadit Card Number: </p>
             <asp:TextBox ID="card_number" runat="server" Text="4877274905927862"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="card_number" runat="server" ErrorMessage="Only 16 digit Numbers allowed" ValidationExpression="\d{16}"></asp:RegularExpressionValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="card_number" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator> 

            <p>cvv: </p>
             <asp:TextBox ID="cvv2" runat="server" Text="874"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="cvv2" runat="server" ErrorMessage="Only 3 digit Numbers allowed" ValidationExpression="\d{3}"></asp:RegularExpressionValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cvv2" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>  

            <p>Expiration Month (mm): </p>
             <asp:TextBox ID="ex_month" runat="server" Text="11"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="ex_month" runat="server" ErrorMessage="Only 1 or 2 digit Numbers allowed" ValidationExpression="\d{1,2}"></asp:RegularExpressionValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ex_month" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>        
                  
            <p>Expiration Year (yyyy): </p>
             <asp:TextBox ID="ex_year" runat="server" Text="2018"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="ex_year" runat="server" ErrorMessage="Only 4 digit Numbers allowed" ValidationExpression="\d{4}"></asp:RegularExpressionValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ex_year" runat="server" ErrorMessage="Required Field"></asp:RequiredFieldValidator>        
                  

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </div>

        <div class="col-md-5">
            <asp:Button ID="Button1" runat="server" Text="Confirm Payment" OnClick="Button1_Click" />
        </div>
        
    </div>
         </form>
</body>
</html>