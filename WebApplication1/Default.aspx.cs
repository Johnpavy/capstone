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
            string message = string.Empty;

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
                        trainerDb.Close();
                        Session["trainerID"] = trainerID;
                        
                        Tobj.FirstName = firstName;
                        Tobj.LastName = lastName;
                        Tobj.Email = email;
                        Tobj.TrainerId = trainerID;
                        Session["TrainerInfo"] = Tobj;

                       // Response.Redirect("TrainerSignup.aspx");

                    }
                    catch
                    {

                        ErrorLabel.ForeColor = System.Drawing.Color.Red;
                        ErrorLabel.Text = "Error writing to the database";
                        ErrorLabel.Visible = true;

                    }

                    //       finally
                    //    {
                    // trainerDb.Close();
                    //    }

                    SendActivationEmail((int)Session["trainerID"]);
                    message = "Activation email sent, please click the link in the email from us to finish registration.";

                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);

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

        // From http://www.aspsnippets.com/Articles/Send-user-Confirmation-email-after-Registration-with-Activation-Link-in-ASPNET.aspx
        private void SendActivationEmail(int userId)
        {
            String firstName = Request.Form["FName"];
            String email = Request.Form["email"];
            string activationCode = Guid.NewGuid().ToString();
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = System.Data.CommandType.Text;
            //cmd2.CommandText= "UPDATE MFNTrainerTable SET Trainer_ActivationCode = @code WHERE Trainer_Id = @id";
            cmd2.CommandText = "INSERT INTO UserActivation ([Id],[User_ActivationCode]) VALUES(@UserId, @ActivationCode)";
            cmd2.Parameters.AddWithValue("@code", activationCode);
            cmd2.Parameters.AddWithValue("@id", userId);
            SqlConnection trainerDb2 = new SqlConnection(SqlDataSource1.ConnectionString);
            cmd2.Connection = trainerDb2;

            try
            {
                trainerDb2.Open();
                cmd2.ExecuteNonQuery();
                trainerDb2.Close();
            }

            catch
            {
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Text = "Error writing verification number to the database";
                ErrorLabel.Visible = true;
            }
            

            /* using (SqlConnection con = new SqlConnection(constr))
             {
                 using (SqlCommand cmd = new SqlCommand("INSERT INTO MFNTrainerTable VALUES(@UserId, @ActivationCode)"))
                 {
                     using (SqlDataAdapter sda = new SqlDataAdapter())
                     {
                         cmd.CommandType = CommandType.Text;
                         cmd.Parameters.AddWithValue("@UserId", userId);
                         cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
                         cmd.Connection = con;
                         con.Open();
                         cmd.ExecuteNonQuery();
                         con.Close();
                     }
                 }
             }*/
            using (MailMessage mm = new MailMessage("MobileFitnessNetwork@gmail.com", email))
            {
                mm.Subject = "Account Activation";
                string body = "Hello " + firstName + ",";
                body += "<br /><br />Please click the following link to activate your account";
                body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("Default.aspx", "ConfirmationPage.aspx?ActivationCode=" + activationCode) + "'>Click here to activate your account.</a>";
                body += "<br /><br />Thanks";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("MobileFitnessNetwork@gmail.com", "6tfc^TFC");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
    }
}