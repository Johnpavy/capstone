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
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        TrainerObject Tobj = new TrainerObject();

        protected void Page_Load(object sender, EventArgs e)
        { 

            HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("Expires", "0");

            if (Session["TrainerInfo"] == null)
            {
                //Forces a redirect to splash page if this page is loaded without a session state.
                Response.Redirect("Default.aspx");
            }
            else
            {
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

        }

        protected void BookTrainer_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainerScheduler.aspx");
        }

        protected void ChangeAccountSetting_Click(object sender, EventArgs e)
        {
            Session["TrainerInfo"] = Tobj;
            Response.Redirect("AccountSettings.aspx");
        }
        

        protected void ComfirmUpdateBioButton2_Click(object sender, EventArgs e)
        {
            string newBio = TempTextBox2.Text;

            if(newBio.Length < 2000)
            {
                Tobj.Bio = newBio;
                Session["TrainerInfo"] = Tobj;

                SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;

                cmd.CommandText = "UPDATE [MFNTrainerTable] SET Trainer_Bio = @bio where Trainer_Id = @id";

                cmd.Parameters.AddWithValue("@id", Tobj.TrainerId);
                cmd.Parameters.AddWithValue("@bio", newBio);

                try
                {
                    db.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    BioFailLbl.Text = "We failed horribly!";
                    BioFailLbl.Visible = true;
                }
                finally
                {
                    db.Close();
                }

                Response.Redirect("WebForm2.aspx");

            }
            else
            {
                    //do nothing
            }

        }

        //depricated
        protected void BioTextBox_TextChanged(object sender, EventArgs e)
        {
                //does nothing
        }

        protected void ComfirmUpdateRatesButton_Click(object sender, EventArgs e)
        {
            string newIndividualRate = NewIndividualRateTxtBox.Text;
            string newAdditonalRate = NewAdditionalPersonRateTxtBox.Text;
            Regex rgx = new Regex("[0-9]?[0-9]?(\\.[0-9][0-9]?)?");

            if (newIndividualRate == "" || !rgx.IsMatch(newIndividualRate))
            {
                newIndividualRate = IndividualRatesTxtBox.Text;

            }
           
            if(newAdditonalRate == "" || !rgx.IsMatch(newIndividualRate))
            {
                newAdditonalRate = AdditionalPersonRateTxtBox.Text;
            }

            Tobj.IndividualRate = newIndividualRate;
            Tobj.AdditionalPersonRate = newAdditonalRate;
            Session["TrainerInfo"] = Tobj;

            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            cmd.CommandText = "UPDATE [MFNTrainerTable] SET Trainer_IndividualRate = @indRate, Trainer_AdditionalPersonRate = @addPerson WHERE Trainer_Id = @id";

            cmd.Parameters.AddWithValue("@id", Tobj.TrainerId);
            cmd.Parameters.AddWithValue("@indRate", newIndividualRate);
            cmd.Parameters.AddWithValue("@addPerson", newAdditonalRate);


            try
            {
                db.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //Response.Write(@"<script language='javascript'>alert('Error Removing from Database!');</script>");
            }
            finally
            {
                db.Close();
            }

            Response.Redirect("WebForm2.aspx");

        }
    }
}