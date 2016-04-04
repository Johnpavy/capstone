using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        TrainerObject Tobj = new TrainerObject();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void login_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void signup_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainerSignup.aspx");
        }

        protected void about_Click(object sender, EventArgs e)
        {

        }

        protected void ClientSignup_Click(object sender, EventArgs e)
        {

        }

        protected void getStarted_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainerSignup.aspx");
        }

        protected void startup_Click(object sender, EventArgs e)
        {
            String firstName = Request.Form["FName"];
            String lastName = Request.Form["LName"];
            String email = Request.Form["email"];
            String password = Request.Form["password"];
            String CPassword = Request.Form["Cpassword"];

            bool userNameExists;
            SqlConnection trainerDb = new SqlConnection(SqlDataSource1.ConnectionString);

            if (firstName.Equals("")|| lastName.Equals("")||email.Equals("")|| password.Equals("") || CPassword.Equals("")){
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Text = "All textboxes required";
                ErrorLabel.Visible = true;
            }
            else if(!password.Equals(CPassword))
            {
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Text = "Passwords must match";
                ErrorLabel.Visible = true;
                trainerDb.Close();
            }
            else if(!IsValidEmail(email))
            {
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Text = "Invalid E-mail address";
                ErrorLabel.Visible = true;
                trainerDb.Close();
            }
            else
            {

                trainerDb.Open();
                // Check to see if email exists in the database
                using (SqlCommand checkCmd = new SqlCommand("select count(*) from MFNTrainerTable where Trainer_Email = @email", trainerDb))
                {
                    checkCmd.Parameters.AddWithValue("@email", email);
                    userNameExists = (int)checkCmd.ExecuteScalar() > 0;
                }
                if(userNameExists)
                {
                    ErrorLabel.ForeColor = System.Drawing.Color.Red;
                    ErrorLabel.Text = "Email address taken";
                    ErrorLabel.Visible = true;
                    trainerDb.Close();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    // create sql command
                    cmd.CommandText = "insert into MFNTrainerTable (Trainer_Email, Trainer_FirstName, Trainer_LastName, Trainer_PasswordHash) OUTPUT INSERTED.Trainer_Id values (@email, @fName, @lName, @password)";
                    // add values to sql table
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@fName", firstName);
                    cmd.Parameters.AddWithValue("@lName", lastName);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Connection = trainerDb;
                    // try to execute query and save session object variables
                    try
                    {
                        int trainerID = (int)cmd.ExecuteScalar();
                        Session["trainerID"] = trainerID;

                   
                        Tobj.FirstName = firstName;
                        Tobj.LastName = lastName;
                        Tobj.Email = email;
                        Tobj.TrainerId = trainerID;
                        Session["TrainerInfo"] = Tobj;
                        Response.Redirect("TrainerSignup.aspx");

                    }
                    catch
                    {

                        ErrorLabel.ForeColor = System.Drawing.Color.Red;
                        ErrorLabel.Text = "Error writing to the database";
                        ErrorLabel.Visible = true;

                    }
                    finally
                    {
                        trainerDb.Close();
                    }
                  
                }
               
            }
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}