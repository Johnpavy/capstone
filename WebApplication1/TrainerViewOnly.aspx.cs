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
        TrainerObject Tobj = new TrainerObject();
        protected void Page_Load(object sender, EventArgs e)
        {
            String trainerID = Request.QueryString["TrainerID"];
            HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("Expires", "0");
            if (!String.IsNullOrEmpty(trainerID))
            {

                int trainerIDint = Int32.Parse(trainerID);
                SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;
                try
                {

                    cmd.CommandText = "Select * FROM [MFNTrainerTable] WHERE Trainer_Id = @trainerID";
                    cmd.Parameters.AddWithValue("@trainerID", trainerIDint);
                    db.Open();

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
                        Tobj.Speciality = sdr["Trainer_Specialty"].ToString();

                    }

                    Session["TrainerInfo"] = Tobj;

                }
                catch
                {
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
                IndividualRatesTxtBox.Text = "0.00";
                AdditionalPersonRateTxtBox.Text = "0.00";
            }
            else
            {
                IndividualRatesTxtBox.Text = Tobj.IndividualRate;
                AdditionalPersonRateTxtBox.Text = Tobj.AdditionalPersonRate;
            }

            //changes default profile pic to user uploaded one
            if (Tobj.ImagePath != "")
            {
                ProfilePic.Attributes["src"] = Tobj.ImagePath;
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