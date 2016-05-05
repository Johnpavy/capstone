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
        TrainerObject Tobj = new TrainerObject(); //create trainer object

        protected void Page_Load(object sender, EventArgs e)
        {
            //needs to be added to every page in the page load to prevent back on logout.
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
                Tobj.CopyTrainerObject((TrainerObject)Session["TrainerInfo"]); //get a copy of the trainer session object
                //populate these values with the trainer's data
                BioTextBox.Text = Tobj.Bio; //for the trainer bio
                TrainerTypesLbl.Text = Tobj.Speciality; //for the trainer types/specialty
                UserNameLbl.Text = Tobj.FirstName + " " + Tobj.LastName + " "; //for the trianers firat and last name

                //section to add client rates
                if (Tobj.AdditionalPersonRate == null || Tobj.IndividualRate == null)
                {
                    //populate these values with zero if they have not been set
                    IndividualRatesLbl.Text = "0.00";
                    AdditionalPersonRateLbl.Text = "0.00";
                    MaxNumberPeopleLbl.Text = "0";
                }
                  else
                {
                    //populate these values with the trainer's data
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
            //redirect after clicking book trainer button
            Response.Redirect("TrainerScheduler.aspx");
        }

        protected void ChangeAccountSetting_Click(object sender, EventArgs e)
        {
            Session["TrainerInfo"] = Tobj;
            Response.Redirect("AccountSettings.aspx");
        }
        

        protected void ComfirmUpdateBioButton2_Click(object sender, EventArgs e)
        {
            //this is for the update button within the model
            string newBio = TempTextBox2.Text;
            //limit to 2000 characters
            if(newBio.Length < 2000)
            {
                //place all of this info into the trainer session object 
                Tobj.Bio = newBio;
                Session["TrainerInfo"] = Tobj;
                //update the database with this new info
                SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;
                //SQL command
                cmd.CommandText = "UPDATE [MFNTrainerTable] SET Trainer_Bio = @bio where Trainer_Id = @id";
                //SQL parameters with newly added values
                cmd.Parameters.AddWithValue("@id", Tobj.TrainerId);
                cmd.Parameters.AddWithValue("@bio", newBio);

                try
                {
                    //attempt to write to the database
                    db.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    //error
                    BioFailLbl.Text = "We failed horribly!";
                    BioFailLbl.Visible = true;
                }
                finally
                {
                    //finish, and close the database connection
                    db.Close();
                }
                //refresh the page
                Response.Redirect("WebForm2.aspx");

            }
            else
            {
                    //do nothing
            }

        }

        protected void ComfirmUpdateTrainType_Click(object sender, EventArgs e)
        {
            //this is for the training specialty/type model update button
            string trainType = TrainerSpecialtyDrop.Text;

            if (trainType == "Select") //allow the traineing type not to be changed if updated without selecting a new type
            {
                trainType = TrainerTypesLbl.Text;
            }

            //place all of this info into the trainer session object 
            Tobj.Speciality = trainType;
            Session["TrainerInfo"] = Tobj;
            //update the database with this new info
            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;
            //SQL command
            cmd.CommandText = "UPDATE [MFNTrainerTable] SET Trainer_Specialty = @specialty WHERE Trainer_Id = @id";
            //SQL parameters with newly added values
            cmd.Parameters.AddWithValue("@id", Tobj.TrainerId);
            cmd.Parameters.AddWithValue("@specialty", trainType);

            try
            {
                //attempt to write to the database
                db.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //error
                //Response.Write(@"<script language='javascript'>alert('Error Removing from Database!');</script>");
            }
            finally
            {
                //finish, and close the database
                db.Close();
            }
            //refresh the trainer profile page
            Response.Redirect("WebForm2.aspx");
        }

        //depricated
        protected void BioTextBox_TextChanged(object sender, EventArgs e)
        {
                //does nothing
        }

        protected void ComfirmUpdateRatesButton_Click(object sender, EventArgs e)
        {
            //this is for the trainers rates model update button
            string newIndividualRate = NewIndividualRateTxtBox.Text; //how much it cost for an individual's session
            string newAdditonalRate = NewAdditionalPersonRateTxtBox.Text; //how much it costs for additional participants
            string newMaxPeople = MaxNumberPeopleDrop.Text; //the max amount of participants allowed by the trainer, capped at 10
           
            Regex rgx = new Regex("[0-9]?[0-9]?(\\.[0-9][0-9]?)?"); //regex for price input

            if (newIndividualRate == "" || !rgx.IsMatch(newIndividualRate)) //error handling for input
            {
                newIndividualRate = IndividualRatesLbl.Text;
            }
           
            if(newAdditonalRate == "" || !rgx.IsMatch(newIndividualRate))//error handling for input
            {
                newAdditonalRate = AdditionalPersonRateLbl.Text;
            }

            if (newMaxPeople == "Select") //allow the max amount of people not to be changed if updated without selecting a new number
            {
                newMaxPeople = MaxNumberPeopleLbl.Text;
            }

            //place all of this info into the trainer session object            
            Tobj.IndividualRate = newIndividualRate;
            Tobj.AdditionalPersonRate = newAdditonalRate;
            Tobj.MaxNumPeople = newMaxPeople;
            Session["TrainerInfo"] = Tobj;

            //update the database with this new info
            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;
            //SQL command
            cmd.CommandText = "UPDATE [MFNTrainerTable] SET Trainer_MaxPeople = @maxPeople, Trainer_IndividualRate = @indRate, Trainer_AdditionalPersonRate = @addPerson WHERE Trainer_Id = @id";
            //SQL parameters with newly added values
            cmd.Parameters.AddWithValue("@id", Tobj.TrainerId);
            cmd.Parameters.AddWithValue("@indRate", newIndividualRate);
            cmd.Parameters.AddWithValue("@addPerson", newAdditonalRate);
            cmd.Parameters.AddWithValue("@maxPeople", newMaxPeople);

            try
            {
                //attempt to write to the database
                db.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //error
                //Response.Write(@"<script language='javascript'>alert('Error Removing from Database!');</script>");
            }
            finally
            {
                //finish, and close the database
                db.Close();
            }

            //refresh the trainers profile page
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
                        // refresh the profile page, FOR SOME REASON THIS DOES NOT ALWAYS WORK, SOMETIMES YOU HAVE TO REFRESH MANUALLY!
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
                //something went horribly wrong
                myStringVariable = "Invalid Upload.";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
            }

            //Response.Redirect(Request.RawUrl);
        }
    }
}