using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WebApplication1
{
    public partial class TrainerViewOnly : System.Web.UI.Page
    {
        TrainerObject Tobj = new TrainerObject(); //create trainer object
        protected void Page_Load(object sender, EventArgs e)
        {
            String trainerID = Request.QueryString["TrainerID"]; //get the trainer id
            HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("Expires", "0");
            if (!String.IsNullOrEmpty(trainerID)) //check if trainer ID is not NULL
            {

                int trainerIDint = Int32.Parse(trainerID); //parse the trainer id to type int
                //connect to the trainer database
                SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;
                try
                {
                    //get trainer info by trainer ID
                    cmd.CommandText = "Select * FROM [MFNTrainerTable] WHERE Trainer_Id = @trainerID";
                    cmd.Parameters.AddWithValue("@trainerID", trainerIDint);
                    db.Open();
                    //read in sql data
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        Tobj.TrainerId = Int32.Parse(sdr["Trainer_Id"].ToString());
                        Tobj.ImagePath = sdr["Trainer_Image"].ToString();
                        Tobj.FirstName = sdr["Trainer_FirstName"].ToString();
                        Tobj.LastName = sdr["Trainer_LastName"].ToString();
                        Tobj.Bio = sdr["Trainer_Bio"].ToString();
                        Tobj.IndividualRate = sdr["Trainer_IndividualRate"].ToString();
                        Tobj.AdditionalPersonRate = sdr["Trainer_AdditionalPersonRate"].ToString();
                        Tobj.MaxNumPeople = sdr["Trainer_MaxPeople"].ToString();
                        Tobj.Speciality = sdr["Trainer_Specialty"].ToString();

                    }
                    //add the trainer info to the session
                    Session["TrainerInfo"] = Tobj;

                }
                catch
                {
                    //forces a redirect to splash page if this page is loaded without complete trainer object info
                    Response.Redirect("Default.aspx");
                }
            }
            if (Session["TrainerInfo"] == null)
            {
                //Forces a redirect to splash page if this page is loaded without a session state.
                Response.Redirect("Default.aspx");
            }

            Tobj.CopyTrainerObject((TrainerObject)Session["TrainerInfo"]);
            BioTextBox.Text = Tobj.Bio;
            specialty.InnerHtml = Tobj.Speciality;
            UserNameLbl.Text = Tobj.FirstName + " " + Tobj.LastName + " ";

            //section to add client rates
            if (Tobj.AdditionalPersonRate == null || Tobj.IndividualRate == null)
            {
                //if rates have not been set, start them at 0
                IndividualRatesLbl.Text = "0.00";
                AdditionalPersonRateLbl.Text = "0.00";
                MaxNumberPeopleLbl.Text = "0";
            }
            else
            {
                //get the set rates and display them
                IndividualRatesLbl.Text = Tobj.IndividualRate;
                AdditionalPersonRateLbl.Text = Tobj.AdditionalPersonRate;
                MaxNumberPeopleLbl.Text = Tobj.MaxNumPeople;
            }

            //changes default profile pic to user uploaded one
            if (Tobj.ImagePath != "")
            {
                //get the trainers profile picture
                ProfilePic.Attributes["src"] = Tobj.ImagePath + "ProfilePic.jpg";
            }
        }
        /*
        protected void BookTrainer_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientScheduler.aspx");
        }
        */
    }
}