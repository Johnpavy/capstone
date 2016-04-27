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
using System.IO;

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
                TrainerTypesLbl.Text = Tobj.Speciality;
                UserNameLbl.Text = Tobj.FirstName + " " + Tobj.LastName + " ";

                //section to add client rates
                if (Tobj.AdditionalPersonRate == null || Tobj.IndividualRate == null)
                {
                    IndividualRatesLbl.Text = "0.00";
                    AdditionalPersonRateLbl.Text = "0.00";
                    MaxNumberPeopleLbl.Text = "0";
                }
                  else
                {
                    IndividualRatesLbl.Text = Tobj.IndividualRate;
                    AdditionalPersonRateLbl.Text = Tobj.AdditionalPersonRate;
                    MaxNumberPeopleLbl.Text = Tobj.MaxNumPeople;
                }

                //changes default profile pic to user uploaded one
                if (Tobj.ImagePath != "")
                {
                    ProfilePic.Attributes["src"] = Tobj.ImagePath + "ProfilePic.jpg";
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

        protected void ComfirmUpdateTrainType_Click(object sender, EventArgs e)
        {
            string trainType = TrainerSpecialtyDrop.Text;

            if (trainType == "Select")
            {
                trainType = TrainerTypesLbl.Text;
            }

            Tobj.Speciality = trainType;
            Session["TrainerInfo"] = Tobj;

            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            cmd.CommandText = "UPDATE [MFNTrainerTable] SET Trainer_Specialty = @specialty WHERE Trainer_Id = @id";

            cmd.Parameters.AddWithValue("@id", Tobj.TrainerId);
            cmd.Parameters.AddWithValue("@specialty", trainType);

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

        //depricated
        protected void BioTextBox_TextChanged(object sender, EventArgs e)
        {
                //does nothing
        }

        protected void ComfirmUpdateRatesButton_Click(object sender, EventArgs e)
        {
            string newIndividualRate = NewIndividualRateTxtBox.Text;
            string newAdditonalRate = NewAdditionalPersonRateTxtBox.Text;
            string newMaxPeople = MaxNumberPeopleDrop.Text;
           
            Regex rgx = new Regex("[0-9]?[0-9]?(\\.[0-9][0-9]?)?");

            if (newIndividualRate == "" || !rgx.IsMatch(newIndividualRate))
            {
                newIndividualRate = IndividualRatesLbl.Text;
            }
           
            if(newAdditonalRate == "" || !rgx.IsMatch(newIndividualRate))
            {
                newAdditonalRate = AdditionalPersonRateLbl.Text;
            }

            if (newMaxPeople == "Select")
            {
                newMaxPeople = MaxNumberPeopleLbl.Text;
            }
            
            Tobj.IndividualRate = newIndividualRate;
            Tobj.AdditionalPersonRate = newAdditonalRate;
            Tobj.MaxNumPeople = newMaxPeople;
            Session["TrainerInfo"] = Tobj;

            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            cmd.CommandText = "UPDATE [MFNTrainerTable] SET Trainer_MaxPeople = @maxPeople, Trainer_IndividualRate = @indRate, Trainer_AdditionalPersonRate = @addPerson WHERE Trainer_Id = @id";

            cmd.Parameters.AddWithValue("@id", Tobj.TrainerId);
            cmd.Parameters.AddWithValue("@indRate", newIndividualRate);
            cmd.Parameters.AddWithValue("@addPerson", newAdditonalRate);
            cmd.Parameters.AddWithValue("@maxPeople", newMaxPeople);

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

        protected void UploadPic_Click(object sender, EventArgs e)
        {
            string myStringVariable = "";

            // Save the uploaded file to an "MFNRoot\ProfilePic\" directory
            // that already exists in the file system of the 
            // currently executing ASP.NET application.  
            // Creating an "MFNRoot\ProfilePic\" directory isolates uploaded 
            // files in a separate directory. This helps prevent
            // users from overwriting existing application files by
            // uploading files with names like "Web.config".
            string saveDir = Tobj.ImagePath;

            // Get the physical file system path for the currently
            // executing application.
            string appPath = Request.PhysicalApplicationPath;

            try
            {
                // Before attempting to save the file, verify
                // that the FileUpload control contains a file.
                if (FileUpload1.HasFile)
                {
                    // Get the name of the file to upload.
                    string fileName = Server.HtmlEncode(FileUpload1.FileName);

                    // Get the extension of the uploaded file.
                    string extension = System.IO.Path.GetExtension(fileName);

                    // Get the size in bytes of the file to upload.
                    int fileSize = FileUpload1.PostedFile.ContentLength;

                    // Set the max file size in bytes of the file to upload.
                    int maxFileSize = 3000000; //3MB

                    // Allow only files with .jpg or .png extensions
                    // to be uploaded.
                    if ((extension == ".jpg") && fileSize < maxFileSize)
                    {
                        // Append the name of the file to upload to the path. You can change the default file name here, change "priflePic".
                        string savePath = appPath + saveDir + "ProfilePic" + extension;
                        
                        // Call the SaveAs method to save the 
                        // uploaded file to the specified path.
                        // This example does not perform all
                        // the necessary error checking.               
                        // If a file with the same name
                        // already exists in the specified path,  
                        // the uploaded file overwrites it.
                        FileUpload1.SaveAs(savePath);

                        // Notify the user that their file was successfully uploaded.
                        // myStringVariable = "Your file was uploaded successfully to: " + savePath;
                        // ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
                         Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        // Notify the user why their file was not uploaded.
                        myStringVariable = "Your file must be a .jpg and be smaller than 3MB";
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
                       
                    }

                }
                else
                {
                    // Notify the user that a file was not uploaded.
                    myStringVariable = "You did not specify a file to upload.";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
                }
            }
            catch
            {
                myStringVariable = "Invalid Upload.";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
            }

            //Response.Redirect(Request.RawUrl);
        }
    }
}