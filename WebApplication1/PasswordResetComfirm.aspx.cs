using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

//For Hasing Passwords
using System.Security.Cryptography;
using System.Web.Security;

namespace WebApplication1
{
    public partial class PasswordResetComfirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
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
            //hash the password
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            return hashedPwd;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String password = TextBox1.Text;
            String CPassword = TextBox2.Text;
            string message = string.Empty;
            //user and trainer database connections
            SqlConnection trainerDB = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlConnection clientDB = new SqlConnection(SqlDataSource2.ConnectionString);

            if (CPassword.Equals(""))
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
                trainerDB.Close();
            }
            else if (!password.Any(c => char.IsUpper(c))) //checks if string does not contain uppercase letter
            {
                ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                ErrorLabel2.Text = "Passwords must contain at least one capital letter.";
                ErrorLabel2.Visible = true;
                trainerDB.Close();
            }
            else if (!password.Any(c => char.IsLower(c))) //checks if string does not contain uppercase letter
            {
                ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                ErrorLabel2.Text = "Passwords must contain at least one lowercase letter.";
                ErrorLabel2.Visible = true;
                trainerDB.Close();
            }
            else if (!password.Any(c => char.IsDigit(c))) //checks if string does not contain a digit
            {
                ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                ErrorLabel2.Text = "Passwords must contain at least one digit.";
                ErrorLabel2.Visible = true;
                trainerDB.Close();
            }
            else if (!password.Equals(CPassword)) //checks if passwords match
            {
                ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                ErrorLabel2.Text = "Passwords must match.";
                ErrorLabel2.Visible = true;
                trainerDB.Close();
            }
            else
            {
                trainerDB.Open();
                clientDB.Open();
                //create password hash and salt
                string salt = CreateSalt(125);
                string hashedPassword = CreatePasswordHash(password, salt);
                int count = -1;
                //used to identify user
                string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
                SqlConnection con = new SqlConnection(SqlDataSource1.ConnectionString); //trainer database
                SqlConnection con2 = new SqlConnection(SqlDataSource2.ConnectionString); //user database
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@email", activationCode);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Connection = con;
                con.Open();
                con2.Open();

                    //locate trainer email
                    cmd.CommandText = "Select COUNT(1) FROM [MFNTrainerTable] WHERE Trainer_Email = @email";
                    count = (int)cmd.ExecuteScalar();

                try
                {
                    if (count == 0) 
                    {
                        //if not found in trainer db, update user database
                        cmd.Connection = con2;
                        cmd.CommandText = "UPDATE [MFNUserTable] SET User_PasswordHash='" + hashedPassword + "', User_PasswordSalt='" + salt + "' WHERE User_Email = @email";
                        cmd.ExecuteScalar();
                        trainerDB.Close();
                    }
                    else if (count > 0)
                    {
                        //if found in trainer db, update trainer db
                        cmd.CommandText = "UPDATE [MFNTrainerTable] SET Trainer_PasswordHash='" + hashedPassword + "', Trainer_PasswordSalt='" + salt + "' WHERE Trainer_Email = @email";
                        cmd.ExecuteScalar();
                        clientDB.Close();
                    }
                    
                    //Was there a change to the database, if so redirect to main page
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    con2.Close();
                    if (rowsAffected == 1)
                    {
                        ltMessage.Text = "Reset successful.";
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        ltMessage.Text = "Invalid Input";
                    }
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