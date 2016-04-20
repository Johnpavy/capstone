using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace WebApplication1
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorLbl.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Page page = HttpContext.Current.Handler as Page;
            int count = 0;
            string Email = "";
            //bool isClient = false;

            Email = TextBox1.Text;

            if(CheckBox1.Checked)
            {
                SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;

                try
                {
                    cmd.CommandText = "Select COUNT(1) FROM [MFNTrainerTable] WHERE Trainer_Email = '" + Email + "'";
                    db.Open();

                    count = (int)cmd.ExecuteScalar();

                }
                catch
                {
                    count = -1;
                }
                finally
                {
                    db.Close();
                }

                if (count == -1)
                {
                    //Login fail
                    ErrorLbl.Visible = true;
                    ErrorLbl.Text = "Database is not connected!";
                }
                else if (count > 0)
                {
                    cmd.CommandText = "Select Trainer_Id FROM [MFNTrainerTable] WHERE Trainer_Email = '" + Email + "'";
                    db.Open();
                    int trainerID = (int)cmd.ExecuteScalar();
                    Session["trainerID"] = trainerID;

                    SendResetEmail((int)Session["trainerID"]);
                    string message = "Reset Password email sent. Please click the link in the email from us to finish password reset.";

                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + message + "');window.location='Default.aspx';", true);
                    db.Close();
                }
                else
                {
                    ErrorLbl.Visible = true;
                    ErrorLbl.Text = "No Registered Trainer Account.";

                }
            }
            //Client Account
            else
            {
                SqlConnection db = new SqlConnection(SqlDataSource2.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;

                try
                {
                    cmd.CommandText = "Select COUNT(1) FROM [MFNUserTable] WHERE User_Email = '" + Email + "'";
                    db.Open();

                    count = (int)cmd.ExecuteScalar();

                }
                catch
                {
                    count = -1;
                }
                finally
                {
                    db.Close();
                }

                if (count == -1)
                {
                    //Login fail
                    ErrorLbl.Visible = true;
                    ErrorLbl.Text = "Database is not connected!";
                }
                else if (count > 0)
                {
                    //isClient = true;
                    cmd.CommandText = "Select User_Id FROM [MFNUserTable] WHERE User_Email = '" + Email + "'";
                    db.Open();
                    int trainerID = (int)cmd.ExecuteScalar();
                    Session["userID"] = trainerID;

                    SendResetEmail((int)Session["userID"]);
                    string message = "Reset Password email sent. Please click the link in the email from us to finish password reset.";

                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + message + "');window.location='Default.aspx';", true);
                    db.Close();
                }
                else
                {
                    ErrorLbl.Visible = true;
                    ErrorLbl.Text = "No Registered Client Account.";

                }
            }
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        private void SendResetEmail(int userId)
        { 
            String email = TextBox1.Text;
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = System.Data.CommandType.Text;

            //if(isClient == true) { cmd2.CommandText = "Select User_ActivationCode FROM [MFNUserTable] WHERE User_Email = '" + email + "'"; } else {
            cmd2.CommandText = "Select Trainer_ActivationCode FROM [MFNTrainerTable] WHERE Trainer_Email = '" + email + "'";
            //}
            cmd2.Parameters.AddWithValue("@trainerID", userId);
            SqlConnection trainerDb2 = new SqlConnection(SqlDataSource1.ConnectionString);
            cmd2.Connection = trainerDb2;

            try
            {
                trainerDb2.Open();
                cmd2.ExecuteNonQuery();
                trainerDb2.Close();

                using (MailMessage mm = new MailMessage("MobileFitnessNetwork@gmail.com", email))
                {
                    mm.Subject = "Password Reset";
                    string body = "Hello, ";
                    body += "<br /><br />You may reset your password through this link: ";
                    body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("ResetPassword.aspx", "PasswordResetComfirm.aspx?ActivationCode=" + email) + "'>Click here to reset your password.</a>";
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

            catch
            {
                //ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Text = "Error sending email to user";
                ErrorLabel.Visible = true;
            }


            
        }
    }
}