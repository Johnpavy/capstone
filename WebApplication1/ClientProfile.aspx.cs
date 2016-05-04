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
    
    public partial class WebForm1 : System.Web.UI.Page
    {
        UserObject Uobj = new UserObject();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //needs to be added to every page in the page load to prevent back on logout.
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            if (Session["UserInfo"] == null)
            {
                //Forces a redirect to splash page if this page is loaded without a session state.
                Response.Redirect("Default.aspx");
            }
            else
            {
                Uobj.CopyUserObject((UserObject)Session["UserInfo"]);
                PrefTextBox.Text = Uobj.TrainingPref;
                AvaEquipTxt.Text = Uobj.Equipment;
                UserNameLbl.Text = Uobj.FirstName + " " + Uobj.LastName + " ";

                //changes default profile pic to user uploaded one
                if (Uobj.ImagePath != "")
                {
                    ProfilePic.Attributes["src"] = Uobj.ImagePath + "ProfilePic.jpg";
                }

            }

        }
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            if(DropDownList1.SelectedValue == "")
            {
                ErrorLbl.Visible = true;
                ErrorLbl.Text = "You must select a type of training";
            }
            else
            {
                Session["Selection"] = DropDownList1.SelectedValue;
                Session["NameSearch"] = false;
                Response.Redirect("searchTrainersPage.aspx");
                
            }

        }

        protected void ComfirmUpdateTrainerPrefButton2_Click(object sender, EventArgs e)
        {
            string newTrainPref = TrainerPrefTxt.Text;

            if (newTrainPref.Length < 2000)
            {
                Uobj.TrainingPref = newTrainPref;
                Session["UserInfo"] = Uobj;

                SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;

                cmd.CommandText = "UPDATE [MFNUserTable] SET User_TrainingPref = @trainPref where User_Id = @id";

                cmd.Parameters.AddWithValue("@id", Uobj.UserId);
                cmd.Parameters.AddWithValue("@trainPref", newTrainPref);

                try
                {
                    db.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    PrefFailLbl.Text = "We failed horribly!";
                    PrefFailLbl.Visible = true;
                }
                finally
                {
                    db.Close();
                }

                Response.Redirect(Request.RawUrl);

            }
            else
            {
                //do nothing
            }

        }

        protected void ComfirmUpdateAvaEquipButton2_Click(object sender, EventArgs e)
        {
            string newAvaEquip = AvailableEquipTxt.Text;

            if (newAvaEquip.Length < 2000)
            {
                Uobj.Equipment = newAvaEquip;
                Session["UserInfo"] = Uobj;

                SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;

                cmd.CommandText = "UPDATE [MFNUserTable] SET User_Equipment = @equipment where User_Id = @id";

                cmd.Parameters.AddWithValue("@id", Uobj.UserId);
                cmd.Parameters.AddWithValue("@equipment", newAvaEquip);

                try
                {
                    db.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    PrefFailLbl.Text = "We failed horribly!";
                    PrefFailLbl.Visible = true;
                }
                finally
                {
                    db.Close();
                }

                Response.Redirect(Request.RawUrl);

            }
            else
            {
                //do nothing
            }

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
            string saveDir = Uobj.ImagePath;

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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String name = TextBox1.Text;
            if(name.Equals(""))
            {
               
                Label1.Text = "Enter a name to search";
                Label1.Visible = true;
            
            }
            else
            {
                Session["TrainerName"] = name;
                Session["NameSearch"] = true;
                Response.Redirect("searchTrainersPage.aspx");
            }
            
        }
    }
}