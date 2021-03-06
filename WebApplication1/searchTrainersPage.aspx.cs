﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace WebApplication1
{
    public partial class searchTrainersPage : System.Web.UI.Page
    {
        int trainerID;
        String imagePath, fName, lName, rateIndividual, rateGroup, equipment;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve session variable from selection from the client profile page
            bool isNameSearch = (bool)Session["NameSearch"];
            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int x = 0;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            if (!isNameSearch)
            {

                String selection = (String)Session["Selection"];
               


                try
                {
                    cmd.CommandText = "Select * FROM [MFNTrainerTable] WHERE Trainer_Specialty = @selection AND Trainer_VerifiedCert = @verified";

                    db.Open();

                    cmd.Parameters.AddWithValue("@selection", selection);
                    cmd.Parameters.AddWithValue("@verified", true);
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {

                        trainerID = Int32.Parse(sdr["Trainer_Id"].ToString());
                        imagePath = sdr["Trainer_Image"].ToString();
                        fName = sdr["Trainer_FirstName"].ToString();
                        lName = sdr["Trainer_LastName"].ToString();

                        rateIndividual = sdr["Trainer_IndividualRate"].ToString();
                        rateGroup = sdr["Trainer_AdditionalPersonRate"].ToString();
                        equipment = sdr["Trainer_Equipment"].ToString();

                        CreateDiv("div" + x);

                    }
                    if (!sdr.HasRows)
                    {
                        ErrorLbl.Visible = true;
                        ErrorLbl.Text = "No trainers found in your area.";
                    }
                }
                catch
                {
                    ErrorLbl.Visible = true;
                    ErrorLbl.Text = "Error while reading from Database";
                }



            }
            else
            {
                String trainerToSearchFor = "%" + (String)Session["TrainerName"] + "%";

                try
                {
                    cmd.CommandText = "Select * FROM [MFNTrainerTable] WHERE Trainer_FirstName like @trainerName OR Trainer_LastName like @trainerName OR Trainer_FirstName + ' ' +Trainer_LastName + ' ' like @trainerName AND Trainer_VerifiedCert = @verified";

                    db.Open();

                    cmd.Parameters.AddWithValue("@trainerName", trainerToSearchFor);
                    cmd.Parameters.AddWithValue("@verified", true);
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {

                        trainerID = Int32.Parse(sdr["Trainer_Id"].ToString());
                        imagePath = sdr["Trainer_Image"].ToString();
                        fName = sdr["Trainer_FirstName"].ToString();
                        lName = sdr["Trainer_LastName"].ToString();

                        rateIndividual = sdr["Trainer_IndividualRate"].ToString();
                        rateGroup = sdr["Trainer_AdditionalPersonRate"].ToString();
                        equipment = sdr["Trainer_Equipment"].ToString();

                        CreateDiv("div" + x);

                    }
                    if(!sdr.HasRows)
                    {
                        ErrorLbl.Visible = true;
                        ErrorLbl.Text = "No trainers found in your area with that name.";
                    }

                }
                catch
                {
                    ErrorLbl.Visible = true;
                    ErrorLbl.Text = "Error while reading from Database";
                }
            }
           

        }
        private void CreateDiv(string divId)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("id", divId);
            div.Attributes.Add("runat", "server");
            div.Attributes.Add("class", "row centered-form");
            imagePath = imagePath + "ProfilePic.jpg";
            //this line is an absolute nightmare,but it should work!
            div.InnerHtml = "<div class=\"row centered-form\" runat=\"server\"><div class=\"col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4\"><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">Local Trainer</h3></div><div class=\"panel-body\"><img src=\"" + imagePath + "\" class=\"UserPicture img-circle img - responsive\" style=\"width: 50px; height: 50px; \">" + " " + fName + " " + lName + "<br />Equipment: " + equipment +"<br /> Individual Rate: $" + rateIndividual + "/hr <br />Group Rate: $" + rateGroup + "/hr <br /> <a href='" + Request.Url.AbsoluteUri.Replace("searchTrainersPage.aspx", "TrainerViewOnly.aspx?TrainerID=" + trainerID) + "' ><br />Check out their profile</a></div></div></div></div>"; //not completed need button event to launch session!
            TrainerResults.Controls.Add(div);
        }

        // from http://stackoverflow.com/questions/13999187/distance-between-addresses not currently used but for the future. 
        // Starting out only Corpus Christi, TX so just checking to see if zip code is in that range 78401 - 78480 

        /*public decimal calcDistance(decimal latA, decimal longA, decimal latB, decimal longB)
        {

            double theDistance = (Math.Sin(DegreesToRadians(latA)) *
                    Math.Sin(DegreesToRadians(latB)) +
                    Math.Cos(DegreesToRadians(latA)) *
                    Math.Cos(DegreesToRadians(latB)) *
                    Math.Cos(DegreesToRadians(longA - longB)));

            return Convert.ToDecimal((RadiansToDegrees(Math.Acos(theDistance)))) * 69.09M * 1.6093M;
        }*/
    }
}