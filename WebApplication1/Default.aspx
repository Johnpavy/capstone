﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="WebApplication1.WebForm4" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <link rel="shortcut icon" type="image/x-icon" href="/Pictures/favicon.ico"/>
  <title>Welcome to MFN</title>
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
    <form runat="server">
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

                            <input id="LoginEmailID" class="LoginEmail form-control" name="UserEmail" placeholder="Email Address" runat="server" TextMode="Email" />

                            <input type="password" id="LoginPasswordId" class="LoginPassword form-control" name="Password2" placeholder="Password" runat="server" TextMode="Password"/>

                        </div>
                        <div>
                               
                                <asp:Button ID="LoginButton" class="LoginButton btn btn-default" runat="server" onclick="login_Click" role="button" Text="LOG IN"></asp:Button>

                                <label class="IsAdminLabel">Trainer?</label>
                                <div class="onoffswitch">
                                    <asp:CheckBox type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" ID="CheckBox1" runat="server"  CssClass="TrainerToggle form-control" Text="Login as Trainer"/>
                                    <label class="onoffswitch-label" for="CheckBox1"></label>
                                    <asp:Label ID="ErrorLbl" runat="server" Text="*Error" ForeColor="#FF3300" Visible="False"></asp:Label>
                                </div>
                                 <br /><br /><a style="float:right; padding-right:5px" href ="ResetPassword.aspx">Reset Password</a>
                        </div>
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

              <div class="form-group">
                  <asp:TextBox runat="server" name="FName" type="text" class="RegisterFormTextBoxFirstName form-control" id="first_name" placeholder="First Name"></asp:TextBox>
              </div>
              <div class="form-group">
                <asp:TextBox runat="server" name="LName" type="text" class="RegisterFormTextBoxLastName form-control" Id="last_name" placeholder="Last Name"></asp:TextBox>
              </div>


                <div class="form-group">
                
                    <asp:TextBox name="Email" id="Email" class="RegisterFormTextBoxEmail form-control"  placeholder="Email Address" runat="server" TextMode="Email" />
              </div>

              <div class="form-group">
                <input type="password" class="RegisterFormTextBoxPassword form-control" id="password" placeholder="Password">
              </div>
                <div class="form-group">
                <input type="text" class="RegisterFormTextBoxConfirmPassword form-control" id="password_confirmation" placeholder="Confirm Password">
              </div>
                <asp:Label ID="ErrorLabel" runat="server" Text="Password:  length &gt;8, 1 upper, 1 lower, 1 number"></asp:Label>

            </div>
            <button type="submit" ID="LinkButton1" class="GetStartedButton btn btn-default" OnClick="startup_Click">GET STARTED</button>
        </div>
        
        <footer class="Footer">
            <a href="AdminLogin.aspx"><u>Login as Admin</u></a>
        </footer>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
        <br />
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNTrainerTable]"></asp:SqlDataSource>
        <br />
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Mobile Fitness Network DBConnectionString %>" SelectCommand="SELECT * FROM [MFNUserTable]"></asp:SqlDataSource>

    </div>
   </form>
</body>
</html>