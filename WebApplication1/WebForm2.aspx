<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication1.WebForm2" %>

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
    .HeaderContainer { background-color: #231F1E; padding-bottom: 21px; padding-top: 2%;}
    .TrainerPicture {width: 100%; height: 100%;}
    .TrainerHeaderInfo{padding-top: 1%; }
    .TrainerName {color: #FFFFFF; font-size: 4.5vw;}
    .TrainerRating {color: #FFFFFF; font-size: 3.5vw;}
    .BookButton {float: right;}
    .BookColumn {padding-top: 4%;}
    .ButtonNavigationBar { width: 101%; background-color: #165469;}
    .ButtonResponsive 
    {
        font-size: 2.5vw; 
        width: 20%; 
        background-color: #165469;
        border-color: #165469;
    }
    .ButtonResponsive:hover{background-color: #FFFFFF; color: #231F1E;}
    .InfoContainer {padding-top: 1%;}
    .BiographyLabel, .TypesLabel, .RatesLabel, .LocationsLabel {font-size: 3.0vw; color: #FFFFFF; }
    .ReviewLabel {color: #FFFFFF; font-size: 3.5vw;}
    .ReviewHeader, .ReviewRatingGivenLabel, .ReviewRatingGiven , .ReviewBody{color: #FFFFFF; font-size: 2.5vw;}
    footer 
    {
        clear: both;
        position: relative;
        z-index: 10;
        height: 3em;
        margin-top: -3em;
        background-color: #165469;
    }
        .FooterContent{color: #FFFFFF; font-size: 2.0vw;}
    </style>
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="content">
        <div class="HeaderContainer container-fluid">
            <div class="row">
                <div class="PictureColumn col-xs-4 col-sm-3 text-center">
                    <img src="Pictures/trainerPic.jpg" class="TrainerPicture img-rounded img-responsive" alt="Trainer Picture">
                </div>
                <div class="TrainerHeaderInfo col-xs-4 col-sm-6 text-center">
                    <p class="TrainerName">Trainer Joe</p>
                    <p class="TrainerRating">Overall Rating: </p>
                </div>
                <div class="BookColumn col-xs-4 col-sm-2">
                    <button type="button" class="BookButton btn btn-success">Book</button>
                </div>
            </div>
        </div>
        <div class="ButtonNavigationBar btn-group">
            <button type="button" class="ButtonResponsive btn btn-primary">Navi</button>
            <button type="button" class="ButtonResponsive btn btn-primary">Navi</button>
            <button type="button" class="ButtonResponsive btn btn-primary">Navi</button>
            <button type="button" class="ButtonResponsive btn btn-primary">Navi</button>
            <button type="button" class="ButtonResponsive btn btn-primary">Navi</button>
        </div>
        <div class="InfoContainer container-fluid">
            <div class="row">
                <div class="TrainerBiography col-xs-6 col-sm-6 form-group">
                      <label class="BiographyLabel" for="Bio">Biography:</label>
                      <textarea class="BiographyBody form-control" rows="2" id="Bio"readonly></textarea>
                </div>
                <div class="TrainerTypes col-xs-6 col-sm-6 form-group">
                      <label class="TypesLabel" for="Bio">Training Types:</label>
                      <textarea class="TypesBody form-control" rows="2" id="Bio" readonly></textarea>
                </div>
            </div>
        </div>
        <div class="TrainingInformationContainer container-fluid">
            <div class="row">
                <div class="TrainerRates col-xs-12 col-sm-12 form-group">
                      <label class="RatesLabel" for="Bio">Rates:</label>
                      <textarea class="BiographyBody form-control" rows="1" id="Bio"readonly></textarea>
                </div>
                <div class="TrainerLocations col-xs-12 col-sm-12 form-group">
                      <label class="LocationsLabel" for="Bio">Favorite Locations:</label>
                      <textarea class="TypesBody form-control" rows="2" id="Bio"readonly></textarea>
                </div>
            </div>
        </div>
        <div class="ReviewsContainer col-xs-12 col-sm-12">
            <p class="ReviewLabel">Reviews:</p>
            <hr>
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
        <p class="FooterContent">
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </p>
    </footer>
    </form>
</body>
</html>

