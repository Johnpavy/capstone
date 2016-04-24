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

//For Hasing Passwords
using System.Security.Cryptography;
using System.Web.Security;

namespace WebApplication1
{
    public partial class AdminControl : System.Web.UI.Page
    {
        AdminObject Aobj = new AdminObject();

        protected void Page_Load(object sender, EventArgs e)
        {

            //needs to be added to every page in the page load to prevent back on logout.
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();


            if (Session["AdminInfo"] == null)
            {
                Response.Redirect("AdminLogin.Aspx");
            }
            else
            {
                
                Aobj = (AdminObject)Session["AdminInfo"];
                UserNameLbl.Text = Aobj.FirstName + " " + Aobj.LastName;
                Session["AdminId"] = Aobj.AdminId;
            }

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            
            String firstName = first_name.Text;
            String lastName = last_name.Text;
            String email = Email.Text;
            String password = CreateTempPassword();
            string message = string.Empty;

            SqlConnection db = new SqlConnection(SqlDataSource2.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            string salt = CreateSalt(125);
            string hashedPassword = CreatePasswordHash(password, salt);

            cmd.CommandText = "INSERT INTO MFNAdminTable (Admin_Email, Admin_FirstName, Admin_LastName, Admin_PasswordHash, Admin_Salt) VALUES (@email, @fname, @lName, @password, @salt)";
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@fName", firstName);
            cmd.Parameters.AddWithValue("@lName", lastName);
            cmd.Parameters.AddWithValue("@password", hashedPassword);
            cmd.Parameters.AddWithValue("@salt", salt);

            try
            {
                db.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //error message
            }
            finally
            {
                db.Close();
            }

            //send email with  temporary account login.
            SendActivationEmail(email, firstName + " " + lastName, password);

        }


        private void SendActivationEmail(string email, string name, string password)
        {

            using (MailMessage mm = new MailMessage("MobileFitnessNetwork@gmail.com", email))
            {
                mm.Subject = "Mobile Fitness Network Admin Account Creation";
                string body = "Hello " + name + ",";
                body += "<br /><br />Here is your temporary login password: ";
                body += password;
                body += "<br/><br/> Please click the following link to login.";



                // for live web app hosted on azure, uncomment this and comment the local host line 
                // body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("http://mobilefitnessnetwork.azurewebsites.net", "http://mobilefitnessnetwork.azurewebsites.net/AdminLogin.aspx") + "'>Click here to login.</a>";

                // for local host comment this and uncomment link generator above
                body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("AdminControl.aspx", "AdminLogin.aspx") + "'>Click here to login.</a>";
 
                body += "<br /><br />Thanks";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                // the smtp host below will only work for gmail. 
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                // The function below takes in the email address account that will be used and the associated password
                NetworkCredential NetworkCred = new NetworkCredential("MobileFitnessNetwork@gmail.com", "6tfc^TFC");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }



        private static string CreateTempPassword()
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[10];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);

        }

        private static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            return hashedPwd;
        }

        protected void ResetPassword_Click(object sender, EventArgs e)
        {
            string password = Password.Text;
            string cpassword = Cpassword.Text;
            string salt = CreateSalt(125);

            if(password == cpassword)
            {
                SqlConnection db = new SqlConnection(SqlDataSource2.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;

                string hashedPassword = CreatePasswordHash(password, salt);

                cmd.CommandText = "UPDATE [MFNAdminTable] SET Admin_PasswordHash = @password, Admin_Salt = @salt WHERE Admin_Id = @id";
                cmd.Parameters.AddWithValue("@id", Aobj.AdminId);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@salt", salt);

                try
                {
                    db.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    //error message
                }
                finally
                {
                    db.Close();
                }

            }
            else
            {
                ErrorLbl3.Text = "Passwords Do Not Match";
                ErrorLbl3.Visible = true;
            }




        }

        protected void AdminTableBtn_Click(object sender, EventArgs e)
        {
            AdminControlOptions.Visible = false;
            AdminTableView.Visible = true;
        }

        protected void AdminRequestBtn_Click(object sender, EventArgs e)
        {
            AdminControlOptions.Visible = false;
            AdminSignupPanel.Visible = true;
        }

        protected void PasswordChangeBtn_Click(object sender, EventArgs e)
        {
            AdminControlOptions.Visible = false;
            ChangeYourPassword.Visible = true;
        }

        protected void AproveTrainerBtn_Click(object sender, EventArgs e)
        {
            AdminControlOptions.Visible = false;
            ApproveTrainerDiv.Visible = true;
        }

        protected void CloseAdminTableView_Click(object sender, EventArgs e)
        {
            AdminControlOptions.Visible = true;
            AdminTableView.Visible = false;
        }

        protected void CancelInvite_Click(object sender, EventArgs e)
        {
            AdminControlOptions.Visible = true;
            AdminSignupPanel.Visible = false;
        }

        protected void CancelUpdate_Click(object sender, EventArgs e)
        {
            AdminControlOptions.Visible = true;
            ChangeYourPassword.Visible = false;
        }

        protected void CloseApproveTrainerDiv_Click(object sender, EventArgs e)
        {
            AdminControlOptions.Visible = true;
            ApproveTrainerDiv.Visible = false;
        }
    }
}