using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebApplication1
{
    public partial class UploadProfilePic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            // Save the uploaded file to an "MFNRoot\ProfilePic\" directory
            // that already exists in the file system of the 
            // currently executing ASP.NET application.  
            // Creating an "MFNRoot\ProfilePic\" directory isolates uploaded 
            // files in a separate directory. This helps prevent
            // users from overwriting existing application files by
            // uploading files with names like "Web.config".
            string saveDir = @"MFNRoot\ProfilePic\";

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
                    if ((extension == ".jpg") || (extension == ".png") && fileSize < maxFileSize)
                    {

                        // Append the name of the file to upload to the path.
                        string savePath = appPath + saveDir +
                        Server.HtmlEncode(FileUpload1.FileName);
                        Label1.Text = savePath;
                        // Call the SaveAs method to save the 
                        // uploaded file to the specified path.
                        // This example does not perform all
                        // the necessary error checking.               
                        // If a file with the same name
                        // already exists in the specified path,  
                        // the uploaded file overwrites it.
                        FileUpload1.SaveAs(savePath);

                        // Notify the user that their file was successfully uploaded.
                        UploadStatusLabel.Text = "Your file was uploaded successfully to: ";
                    }
                    else
                    {
                        // Notify the user why their file was not uploaded.
                        UploadStatusLabel.Text = "Your file must be a .jpg or .png and be smaller than 3MB";
                    }

                }
                else
                {
                    // Notify the user that a file was not uploaded.
                    UploadStatusLabel.Text = "You did not specify a file to upload.";
                }
            }
            catch
            {
                UploadStatusLabel.Text = "Invalid upload.";
            }
        }
    }
}