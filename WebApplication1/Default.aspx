<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.NewDefault" %>

<!DOCTYPE html>
<html lang="en">
<head>
  <title>Bootstrap Example</title>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    
    
    <script>
        var firstClick = 0;
        var trainerFlag = 0;
        var clientFlag = 0;
        
        $(function()
        {
             $(".HowItWorksButton").click(function()
             {
                $(".HowItWorksContainer").slideToggle();
                return false;
             }); 
        });
        $(function()
        {
             $(".TrainerRegisterPane").click(function()
             {
                if(firstClick === 0)
                {
                    document.getElementById("RegistrationLabelId").innerHTML = "TRAINER REGISTRATION";
                    $(".RegisterFormContainer").hide();
                    $(".RegisterFormContainer").slideToggle();
                    document.getElementById("RegisterFormTextBoxFirstNameId").focus();
                    clientFlag = 0;
                    trainerFlag = 1;
                    firstClick = 1;
                    //document.getElementById('IsClientRegisterCheckbox').checked = false;
                    //document.getElementById("HiddenField1").value = "trainer";
                    var myHidden = document.getElementById('<%= HiddenField1.ClientID %>');
                    myHidden.value = 'trainer';


                    return false;
                }
                else if(clientFlag === 1)
                {
                    document.getElementById("RegistrationLabelId").innerHTML = "TRAINER REGISTRATION";
                    $(".RegisterFormContainer").hide();
                    $(".RegisterFormContainer").slideToggle();
                    document.getElementById("RegisterFormTextBoxFirstNameId").focus();
                    clientFlag = 0;
                    trainerFlag = 1;
                    firstClick = 1;

                    //document.getElementById('IsClientRegisterCheckbox').checked = false;
                    //document.getElementById("HiddenField1").value = "trainer";
                    var myHidden = document.getElementById('<%= HiddenField1.ClientID %>');
                    myHidden.value = 'trainer';
                    return false;
                }
             });
        });
        $(function()
        {
             $(".ClientRegisterPane").click(function()
             {
                if(firstClick === 0)
                {   
                    document.getElementById("RegistrationLabelId").innerHTML = "CLIENT REGISTRATION";
                    $(".RegisterFormContainer").hide();
                    $(".RegisterFormContainer").slideToggle();
                    document.getElementById("RegisterFormTextBoxFirstNameId").focus();
                    clientFlag = 1;
                    trainerFlag = 0;
                    firstClick = 1;
                    //document.getElementById("IsClientRegisterCheckbox").checked = true;
                    //document.getElementById("HiddenField1").value = "client";
                    var myHidden = document.getElementById('<%= HiddenField1.ClientID %>');
                    myHidden.value = 'client';
                    return false;
                }
                else if(trainerFlag === 1)
                {
                    document.getElementById("RegistrationLabelId").innerHTML = "CLIENT REGISTRATION";
                    $(".RegisterFormContainer").hide();
                    $(".RegisterFormContainer").slideToggle();
                    document.getElementById("RegisterFormTextBoxFirstNameId").focus();
                    clientFlag = 1;
                    trainerFlag = 0;
                    firstClick = 1;

                   // document.getElementById("IsClientRegisterCheckbox").checked = true;
                    //document.getElementById("HiddenField1").value = "client";
                    var myHidden = document.getElementById('<%= HiddenField1.ClientID %>');
                    myHidden.value = 'client';

                    return false;
                }
             });
        });
    </script>

    
    <style>
        body{background-color: #393839;}
        .content{width: 100%;margin: 0 auto;}
        .HowItWorksContainer{background-color: #f1f1f2; display: none;padding-bottom: 2%;}
        .HIWInfoText{margin-left: auto; margin-right: auto;}
        .HowItWorksInfoContainer{padding-bottom: 2%; padding-top: 2%;}
        .HeaderContainer{background-color: #FFFFFF;padding-bottom: 1%;}
        .LoginContainer{float: right;}
        .FindATrainerSearch, .LoginButton{float:right;}
        .HeaderLogoContainer
        {
            width:106px; 
            height: 96px;
            padding-bottom: 3%; 
            padding-top: 1%;
            margin-left: auto; margin-right: auto;
        }
        .MFNContainer{background-color: #393839; padding-bottom: 5%; padding-top: 3%;}
        .MFNPicture{margin-left: auto; margin-right: auto;}
        .ClientRegisterPictureColumn, .TrainerRegisterPictureColumn{}
        .ClientRegisterPictureColumn{}
        .HowItWorksButton, .HowItWorksCloseButton{margin-left: auto; margin-right: auto;}
        .RegisterContainer{background-color: #f1f1f2;}
        .TrainerRegisterPane, .RegisterLogo{float: left;}
        .ClientRegisterPictureColumn, .TrainerRegisterPictureColumn {width: 50%; }
        .HowItWorksPictureColumnLeft, .HowItWorksPictureColumnRight {width: 50%;}
        .ClientRegisterPictureColumn{float: right;}
        .HowItWorksPictureColumnRight {float: right;}
        .RegisterFormContainer{padding-top: 2%; padding-bottom: 3%; display:none;}
        .HeaderComponentContainer{float: right; padding-bottom: 1%; padding-top: 25px; padding-right: 30px;}
        .LoginButton, .LoginEmail, .LoginPassword{
            display: inline-block;
            top: 30px;
            border: 2px solid #848484; 
            -webkit-border-radius: 30px; 
            -moz-border-radius: 30px; 
            border-radius: 30px; 
            outline:0; 
            height:35px; 
            width: 150px; 
            padding-left:10px; 
            padding-right:10px;
            border-color: #008cc1;
            color: #008cc1;
          }
        .LoginEmail{
            width: 225px !important;
        }
        .IsAdminLabel{color: #008cc1;}
        .HeaderComponentContainer > li{
            padding-left: 20px;
        }
        .FormInput{padding-bottom: 2%;}
        .RegisterNameForm, .RegisterEmailForm{
            padding-bottom: 1%;
        }
        .RegisterFormTextBoxFirstName, .RegisterFormTextBoxLastName, .RegisterFormTextBoxEmail, .RegisterFormTextBoxPassword, .RegisterFormTextBoxConfirmPassword
        {
            background-color : #393839;
            border: 4px solid #FFFFFF;
            border-radius: 0px;
            width: 300px;
            height: 40px;
            color: #008cc1;
        }
        .RegisterFormTextBoxEmail{width: 408px !important;}
        .GetStartedButton, .HowItWorksButton
        {
            background-color: #939598;
            border: none;
            border-radius: 0px;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19);
        }
        .Footer
        {
            width: 100%;
            height: 200px;
            background-color: #000000;
            position: relative;
            bottom: 0;
        }
        
        /* CSS FOR LOGIN TOGGLE BUTTON */
        .onoffswitch {
        position: relative; width: 60px;
        -webkit-user-select:none; -moz-user-select:none; -ms-user-select: none;
        }
        .onoffswitch-checkbox {
            display: none;
        }
        .onoffswitch-label {
            display: block; overflow: hidden; cursor: pointer;
            height: 36px; padding: 0; line-height: 36px;
            border: 2px solid #CCCCCC; border-radius: 36px;
            background-color: #FFFFFF;
            transition: background-color 0.3s ease-in;
        }
        .onoffswitch-label:before {
            content: "";
            display: block; width: 36px; margin: 0px;
            background: #FFFFFF;
            position: absolute; top: 0; bottom: 0;
            right: 22px;
            border: 2px solid #CCCCCC; border-radius: 36px;
            transition: all 0.3s ease-in 0s; 
        }
        .onoffswitch-checkbox:checked + .onoffswitch-label {
            background-color: #008cc1;
        }
        .onoffswitch-checkbox:checked + .onoffswitch-label, .onoffswitch-checkbox:checked + .onoffswitch-label:before {
           border-color: #008cc1;
        }
        .onoffswitch-checkbox:checked + .onoffswitch-label:before {
            right: 0px; 
        }
        .IsAdminLabel{padding: 10px;float:right;}
        .RegistrationLabel
        {
            font-size: 24px;
            color: #FFFFFF;
            text-align: center;
        }
    </style>
    
    
</head>
<body>
    <form runat ="server">

    <div class="content">
        
        
        <div class="HowItWorksContainer container-fluid text-center">
            <!--
            <div class="row row-no-gutter">
                <div class="HowItWorksPictureColumnLeft text-center">
                    <img src="Pictures/TrainerRegisterPane2.png" class="TrainerRegisterPane img-responsive" alt="MFN Logo">
                </div>
                <div class="HowItWorksPictureColumnRight text-center">
                    <img src="Pictures/ClientRegisterPane2.png" class="ClientRegisterPane img-responsive" alt="MFN Logo">
                </div>
            </div>
            -->
            <div class="HowItWorksInfoContainer row">
                <div class="HIWRightPictureColumn text-center">
                    <img src="MFNDesignSplashPage/MFNHowItWorksBanner.png" class="HIWInfoText img-responsive" alt="HowItWorks Info">
                </div>
            </div>
            <div class="row">
                <button type="submit" class="HowItWorksButton btn btn-default">CLOSE</button>
            </div>
        </div>
        
        <nav class="HeaderContainer" >
              <div class="container-fluid">
                <!-- Brand and toggle get grouped for better mobile display -->

                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="row row-no-gutter">
                    <div class="HeaderComponentContainer">
                        <div>
                            <asp:TextBox runat="server" name = "UserName" class="LoginEmail form-control" id="LoginEmailId" placeholder="Email"></asp:TextBox>

                            <asp:TextBox runat="server" name = "Password" class="LoginPassword form-control" id="LoginPasswordId" placeholder="Password" TextMode="Password"></asp:TextBox>
                        </div>
                        <div>
                                <asp:LinkButton runat="server" type="submit" OnClick="login_Click" class="LoginButton btn btn-default">LOG IN</asp:LinkButton>
                                <label class="IsAdminLabel">Trainer?</label>
                                <div class="onoffswitch">
                                    <input type="checkbox" runat="server" name="onoffswitch" class="onoffswitch-checkbox" id="myonoffswitch" checked>
                                    <label class="onoffswitch-label" for="myonoffswitch"></label>
                                </div>
                            <a href="ResetPassword.aspx">Forgot your Password?</a>
                        </div>
                        <asp:Label ID="ErrorLbl" runat="server" Text="Label" Visible="False"></asp:Label>
                    </div>
                    <div class="HeaderLogoContainer">
                        <img src="MFNDesignSplashPage/MFNLogoBlue.png" class="HeaderLogo img-responsive" alt="MFN Logo">
                    </div>
                      
                    </div>
              </div><!-- /.container-fluid -->
            </nav>

        <div class="MFNContainer container-fluid text-center">
            <div class="row">
                <div class="HeaderLogoPictureColumn col-xs-12 col-sm-12 text-center">
                    <img src="Pictures/MFNBannerNoBG.png" class="MFNPicture img-responsive" alt="MFN Logo">
                </div>
            </div>
            <button type="submit" class="HowItWorksButton btn btn-default">HOW IT WORKS</button>
        </div>
        
        <div class="RegisterContainer container-fluid text-center">
            <div class="row row-no-gutter">
                <div class="TrainerRegisterPictureColumn text-center">
                    <img src="MFNDesignSplashPage/MFNTrainerPanel.png" class="TrainerRegisterPane img-responsive" alt="MFN Logo">
                </div>
                <div class="ClientRegisterPictureColumn text-center">
                    <img src="MFNDesignSplashPage/MFNClientPanel.png" class="ClientRegisterPane img-responsive" alt="MFN Logo">
                </div>
            </div>
        </div>
        
        <div class="RegisterFormContainer text-center">
            
            <label class="RegistrationLabel text-center" id="RegistrationLabelId"></label>
            
            <div class="FormInput">
            <div class="RegisterNameForm form-inline">
              <div class="form-group">
                <asp:TextBox runat = "server" type="text" class="RegisterFormTextBoxFirstName form-control" Id="RegisterFormTextBoxFirstNameId" placeholder="First Name"> </asp:TextBox>
              </div>
              <div class="form-group">
                <asp:TextBox runat="server" type="text" class="RegisterFormTextBoxLastName form-control" Id = "RegisterFormTextBoxLastName" placeholder="Last Name"></asp:TextBox>
              </div>
            </div>
            <div class="RegisterEmailForm form-inline">
                <div class="form-group">
                <asp:TextBox runat="server" type="email" class="RegisterFormTextBoxEmail form-control" ID="emailBox" placeholder="Email"></asp:TextBox>
              </div>
            </div>
            <div class="RegisterPasswordForm form-inline">
              <div class="form-group">
                <asp:TextBox runat="server" type="password" class="RegisterFormTextBoxPassword form-control" Id = "passwordBox" placeholder="Password"></asp:TextBox>
              </div>
                <div class="form-group">
                <asp:TextBox runat="server" type="password" class="RegisterFormTextBoxConfirmPassword form-control" Id = "cPasswordBox" placeholder="Confirm Password"></asp:TextBox>

                </div>

                
            </div>

            </div>
            <asp:LinkButton runat="server" type="submit" OnClick="startup_Click" class="GetStartedButton btn btn-default">GET STARTED</asp:LinkButton>
            <br />
            <asp:Label ID="ErrorLabel" runat="server" Text="Label"></asp:Label>
            
        </div>
        
        <footer class="Footer">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
            <a href="AdminLogin.aspx">Admin Login</a>
            <input id="hiddenControl" type="hidden" runat="server" />
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNUserTable]"></asp:SqlDataSource>



            <asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged="HiddenField1_ValueChanged" />



        </footer>

</div>
</form>
</body>
</html>
