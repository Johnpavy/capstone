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
    public partial class WebForm4 : System.Web.UI.Page
    {
        TrainerObject Tobj = new TrainerObject();
        UserObject Uobj = new UserObject();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // trainer login box checked, login clicked
        protected void login_Click(object sender, EventArgs e)
        {
            int count = 0;
            string UserName = "";
            UserName = Request.Form["UserEmail"];
            string Password = "";
            Password = Request.Form["Password2"].ToString();

            string salt = "";


            //login as trainer
            if (CheckBox1.Checked)
            {
                SqlConnection db = new SqlConnection(SqlDataSource2.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;

                //hash entered password

                try
                {
                    cmd.CommandText = "Select * FROM [MFNTrainerTable] WHERE Trainer_Email = '" + UserName + "'";
                    db.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        salt = sdr["Trainer_PasswordSalt"].ToString();
                    }

                }
                catch
                {
                    ErrorLbl.Visible = true;
                    ErrorLbl.Text = "Error while reading from Database";
                }
                finally
                {
                    db.Close();
                }



                string hashedPassword = CreatePasswordHash(Password, salt);

                cmd.CommandText = "Select COUNT(*) FROM [MFNTrainerTable] WHERE Trainer_Email = '" + UserName + "'COLLATE SQL_Latin1_General_CP1_CS_AS AND Trainer_PasswordHash = '" + hashedPassword + "' COLLATE SQL_Latin1_General_CP1_CS_AS";

                try
                {
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
                /*
                If the the trainer is verified and the database has succesfully placed data
                into trainer object.
                */
                if (count == -1)
                {
                    //Login fail
                    ErrorLbl.Visible = true;
                    ErrorLbl.Text = "Database is not connected!";
                }
                else if (count > 0)
                {

                    try
                    {
                        cmd.CommandText = "Select * FROM [MFNTrainerTable] WHERE Trainer_Email = '" + UserName + "'";
                        db.Open();

                        SqlDataReader sdr = cmd.ExecuteReader();

                        while (sdr.Read())
                        {
                            Tobj.TrainerId = Int32.Parse(sdr["Trainer_Id"].ToString());
                            Tobj.ImagePath = sdr["Trainer_Image"].ToString();
                            Tobj.FirstName = sdr["Trainer_FirstName"].ToString();
                            Tobj.LastName = sdr["Trainer_LastName"].ToString();
                            Tobj.Bio = sdr["Trainer_Bio"].ToString();
                            Tobj.Speciality = sdr["Trainer_Specialty"].ToString();
                        }

                        Session["TrainerInfo"] = Tobj;
                        Response.Redirect("WebForm2.aspx");

                    }
                    catch
                    {
                        ErrorLbl.Visible = true;
                        ErrorLbl.Text = "Error while reading from Database";
                    }

                }
                else
                {
                    //Login fail
                    ErrorLbl.Visible = true;
                    ErrorLbl.Text = "Invalid Email or Password";
                }
                //Response.Redirect("Login.aspx");
            }
            //login as user
            else
            {
                SqlConnection db = new SqlConnection(SqlDataSource3.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;
                cmd.CommandText = "Select COUNT(*) FROM [MFNUserTable] WHERE User_Email = '" + UserName + "'COLLATE SQL_Latin1_General_CP1_CS_AS AND User_PasswordHash = '" + Password + "' COLLATE SQL_Latin1_General_CP1_CS_AS";

                try
                {
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
                /*
                If the the trainer is verified and the database has succesfully placed data
                into trainer object.
                */
                if (count == -1)
                {
                    //Login fail
                    ErrorLbl.Visible = true;
                    ErrorLbl.Text = "Database is not connected!";
                }
                else if (count > 0)
                {

                    try
                    {
                        cmd.CommandText = "Select * FROM [MFNUserTable] WHERE User_Email = '" + UserName + "'";
                        db.Open();

                        SqlDataReader sdr = cmd.ExecuteReader();

                        while (sdr.Read())
                        {
                            Uobj.UserId = Int32.Parse(sdr["User_Id"].ToString());
                            Uobj.FirstName = sdr["User_FirstName"].ToString();
                            Uobj.LastName = sdr["User_LastName"].ToString();
                            Uobj.TrainingPref = sdr["User_TrainingPref"].ToString();
                            Uobj.Equipment = sdr["User_Equipment"].ToString();
                        }

                        Session["UserInfo"] = Uobj;
                        Response.Redirect("UserProfile.aspx");

                    }
                    catch
                    {
                        ErrorLbl.Visible = true;
                        ErrorLbl.Text = "Error while reading from Database";
                    }

                }
                else
                {
                    //Login fail
                    ErrorLbl.Visible = true;
                    ErrorLbl.Text = "Invalid Email or Password";
                }
            }
        }
        // deprecated, not used in newest iteration can probably be deleted
        protected void signup_Click(object sender, EventArgs e)
        {
            TrainerSignupPanel.Visible = true;
            //Response.Redirect("TrainerSignup.aspx");
        }

        protected void about_Click(object sender, EventArgs e)
        {

        }
        // deprecated, not used in newest iteration can probably be deleted
        protected void ClientSignup_Click(object sender, EventArgs e)
        {
            ClientSignupPanel.Visible = true;
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
            // If true, trainer confirmation email is sent if false, client email confirmation is sent
            bool isTrainer = true;

            bool userNameExists;
            SqlConnection trainerDb = new SqlConnection(SqlDataSource1.ConnectionString);

            if (firstName.Equals("") || lastName.Equals("") || email.Equals("") || password.Equals("") || CPassword.Equals(""))
            {
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Text = "All fields required.";
                ErrorLabel.Visible = true;
            }
            else if (password.Length < 8)
            {
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Text = "Passwords must be at least 8 characters long.";
                ErrorLabel.Visible = true;
                trainerDb.Close();
            }
            else if (!password.Any(c => char.IsUpper(c))) //checks if string does not contain uppercase letter
            {
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Text = "Passwords must contain at least one capital letter.";
                ErrorLabel.Visible = true;
                trainerDb.Close();
            }
            else if (!password.Any(c => char.IsLower(c))) //checks if string does not contain uppercase letter
            {
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Text = "Passwords must contain at least one lowercase letter.";
                ErrorLabel.Visible = true;
                trainerDb.Close();
            }
            else if (!password.Any(c => char.IsDigit(c))) //checks if string does not contain a digit
            {
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Text = "Passwords must contain at least one digit.";
                ErrorLabel.Visible = true;
                trainerDb.Close();
            }

            /*            else if(!password.Any(c => char.IsSymbol(c)))
                        {
                            ErrorLabel.ForeColor = System.Drawing.Color.Red;
                            ErrorLabel.Text = "Passwords must contain at least one special character.";
                            ErrorLabel.Visible = true;
                            trainerDb.Close();
                        } */
            else if (!password.Equals(CPassword))
            {
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Text = "Passwords must match.";
                ErrorLabel.Visible = true;
                trainerDb.Close();
            }
            else if (!IsValidEmail(email))
            {
                ErrorLabel.ForeColor = System.Drawing.Color.Red;
                ErrorLabel.Text = "Invalid E-mail address.";
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
                // if it exists, display error message
                if (userNameExists)
                {
                    ErrorLabel.ForeColor = System.Drawing.Color.Red;
                    ErrorLabel.Text = "Email address taken";
                    ErrorLabel.Visible = true;
                    trainerDb.Close();
                }
                else
                {

                    string salt = CreateSalt(125);
                    string hashedPassword = CreatePasswordHash(password, salt);



                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = System.Data.CommandType.Text;
                    // create sql command
                    cmd.CommandText = "INSERT INTO MFNTrainerTable (Trainer_Email, Trainer_FirstName, Trainer_LastName, Trainer_PasswordHash, Trainer_PasswordSalt) OUTPUT INSERTED.Trainer_Id values (@email, @fName, @lName, @password, @salt)";
                    // add values to sql table
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@fName", firstName);
                    cmd.Parameters.AddWithValue("@lName", lastName);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@salt", salt);
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

                        SendActivationEmail((int)Session["trainerID"], email, firstName, isTrainer);
                        message = "Activation email sent, please click the link in the email from us to finish registration.";

                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
                        TrainerSignupPanel.Visible = false;
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

        private void SendActivationEmail(int userId, string email, string name, Boolean isTrainer)
        {
           // String firstName = Request.Form["FName"];
           // String email = Request.Form["email"];
            string activationCode = Guid.NewGuid().ToString();
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = System.Data.CommandType.Text;
            //cmd2.CommandText= "UPDATE MFNTrainerTable SET Trainer_ActivationCode = @code WHERE Trainer_Id = @id";
            cmd2.CommandText = "INSERT INTO UserActivation ([Id],[User_ActivationCode]) VALUES(@UserId, @ActivationCode)";
            cmd2.Parameters.AddWithValue("@ActivationCode", activationCode);
            cmd2.Parameters.AddWithValue("@UserId", userId);
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


            using (MailMessage mm = new MailMessage("MobileFitnessNetwork@gmail.com", email))
            {
                mm.Subject = "Account Activation";
                string body = "Hello " + name + ",";
                body += "<br /><br />Please click the following link to activate your account";
                if (isTrainer)
                {
                    
                    // for live website, uncomment this and comment the local host
                    // body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("http://mobilefitnessnetwork.azurewebsites.net/default.aspx", "http://mobilefitnessnetwork.azurewebsites.net/ConfirmationPage?ActivationCode=" + activationCode) + "'>Click here to activate your account.</a>";

                    // for local host comment this and uncomment link generator above
                    body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("Default.aspx", "ConfirmationPage.aspx?ActivationCode=" + activationCode) + "'>Click here to activate your account.</a>";
                }
                else
                {
                    body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("Default.aspx", "ClientConfirmationPage.aspx?ActivationCode=" + activationCode) + "'>Click here to activate your account.</a>";
                }
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
        // If the client/user clicks startup
        protected void cstartup_Click(object sender, EventArgs e)
        {
            //something something dark side

            String firstName = Request.Form["cFName"];
            String lastName = Request.Form["cLName"];
            String email = Request.Form["cEmail"];
            String password = Request.Form["CLpassword"];
            String CPassword = Request.Form["CCpassword"];
            string message = string.Empty;
            // if false, client email verification is sent
            bool isTrainer = false;

            bool clientNameExists;

            SqlConnection clientDB = new SqlConnection(SqlDataSource3.ConnectionString);

            if (firstName.Equals("") || lastName.Equals("") || email.Equals("") || password.Equals("") || CPassword.Equals(""))
            {
                ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                ErrorLabel2.Text = "All fields required.";
                ErrorLabel2.Visible = true;
            }
            else if (password.Length < 8)
            {
                ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                ErrorLabel2.Text = "Passwords must be at least 8 characters long.";
                ErrorLabel2.Visible = true;
                clientDB.Close();
            }
            else if (!password.Any(c => char.IsUpper(c))) //checks if string does not contain uppercase letter
            {
                ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                ErrorLabel2.Text = "Passwords must contain at least one capital letter.";
                ErrorLabel2.Visible = true;
                clientDB.Close();
            }
            else if (!password.Any(c => char.IsLower(c))) //checks if string does not contain uppercase letter
            {
                ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                ErrorLabel2.Text = "Passwords must contain at least one lowercase letter.";
                ErrorLabel2.Visible = true;
                clientDB.Close();
            }
            else if (!password.Any(c => char.IsDigit(c))) //checks if string does not contain a digit
            {
                ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                ErrorLabel2.Text = "Passwords must contain at least one digit.";
                ErrorLabel2.Visible = true;
                clientDB.Close();
            }

            /*            else if(!password.Any(c => char.IsSymbol(c)))
                        {
                            ErrorLabel.ForeColor = System.Drawing.Color.Red;
                            ErrorLabel.Text = "Passwords must contain at least one special character.";
                            ErrorLabel.Visible = true;
                            trainerDb.Close();
                        } */
            else if (!password.Equals(CPassword))
            {
                ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                ErrorLabel2.Text = "Passwords must match.";
                ErrorLabel2.Visible = true;
                clientDB.Close();
            }
            else if (!IsValidEmail(email))
            {
                ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                ErrorLabel2.Text = "Invalid E-mail address.";
                ErrorLabel2.Visible = true;
                clientDB.Close();
            }
            else
            {

                clientDB.Open();
                // Check to see if email exists in the database
                
                using (SqlCommand checkCmd = new SqlCommand("select count(*) from MFNUserTable where User_Email = @email", clientDB))
                {
                    checkCmd.Parameters.AddWithValue("@email", email);
                    clientNameExists = (int)checkCmd.ExecuteScalar() > 0;
                }
                // if it exists, display error message
                if (clientNameExists)
                {
                    ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                    ErrorLabel2.Text = "Email address taken";
                    ErrorLabel2.Visible = true;
                    clientDB.Close();
                }
                else
                {

                    string salt = CreateSalt(125);
                    string hashedPassword = CreatePasswordHash(password, salt);



                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = System.Data.CommandType.Text;
                    // create sql command
                    cmd.CommandText = "INSERT INTO MFNUserTable (User_Email, User_FirstName, User_LastName, User_PasswordHash, User_PasswordSalt) OUTPUT INSERTED.User_Id values (@email, @fName, @lName, @password, @salt)";
                    // add values to sql table
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@fName", firstName);
                    cmd.Parameters.AddWithValue("@lName", lastName);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@salt", salt);
                    cmd.Connection = clientDB;
                    // try to execute query and save session object variables
                    try
                    {
                        int userID = (int)cmd.ExecuteScalar();
                        clientDB.Close();
                        Session["userID"] = userID;

                        Uobj.FirstName = firstName;
                        Uobj.LastName = lastName;
                        Uobj.Email = email;
                        Uobj.UserId = userID;
                        Session["UserInfo"] = Uobj;

                        SendActivationEmail((int)Session["userID"], email, firstName, isTrainer);
                        message = "Activation email sent, please click the link in the email from us to finish registration.";

                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
                        ClientSignupPanel.Visible = false;

                    }
                    catch
                    {

                        ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                        ErrorLabel2.Text = "Error writing to the database";
                        ErrorLabel2.Visible = true;

                    }



                }

            }
        }
    }
}