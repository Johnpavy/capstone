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
        String imagePath, fName, lName, rate;
        protected void Page_Load(object sender, EventArgs e)
        {
            String selection = (String)Session["Selection"];
            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int x = 0;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;
           

            try
            {
                cmd.CommandText = "Select * FROM [MFNTrainerTable] WHERE Trainer_Specialty = @selection";
                
                db.Open();

                cmd.Parameters.AddWithValue("@selection", selection);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                   
                   trainerID = Int32.Parse(sdr["Trainer_Id"].ToString());
                   imagePath = sdr["Trainer_Image"].ToString();
                   fName = sdr["Trainer_FirstName"].ToString();
                   lName = sdr["Trainer_LastName"].ToString();
                   rate = sdr["Trainer_IndividualRate"].ToString();
                   CreateDiv("div" + x);
                   
                }

            }
            catch
            {
                ErrorLbl.Visible = true;
                ErrorLbl.Text = "Error while reading from Database";
            }

        }
        private void CreateDiv(string divId)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("id", divId);
            div.Attributes.Add("runat", "server");
            div.Attributes.Add("class", "row centered-form");
            //this line is an absolute nightmare,but it should work!
            div.InnerHtml = "<div class=\"row centered-form\" runat=\"server\"><div class=\"col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4\"><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">Local Trainer</h3></div><div class=\"panel-body\"><img src=\"" + imagePath + "\" class=\"UserPicture img-circle img - responsive\" style=\"width: 50px; height: 50px; \">" + fName + " " + lName + " is in your area <a href='" + Request.Url.AbsoluteUri.Replace("searchTrainersPage.aspx", "TrainerViewOnly.aspx?TrainerID=" + trainerID) + "' >Check out their profile</a></div></div></div></div>"; //not completed need button event to launch session!
            TrainerResults.Controls.Add(div);
        }
    }
}