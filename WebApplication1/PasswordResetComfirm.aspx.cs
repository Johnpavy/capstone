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
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            return hashedPwd;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String password = TextBox1.Text;
            String CPassword = TextBox2.Text;
            string message = string.Empty;

            SqlConnection clientDB = new SqlConnection(SqlDataSource1.ConnectionString);

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
            else if (!password.Equals(CPassword))
            {
                ErrorLabel2.ForeColor = System.Drawing.Color.Red;
                ErrorLabel2.Text = "Passwords must match.";
                ErrorLabel2.Visible = true;
                clientDB.Close();
            }
            else
            {
                clientDB.Open();

                string salt = CreateSalt(125);
                string hashedPassword = CreatePasswordHash(password, salt);

                string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
                using (SqlConnection con = new SqlConnection(SqlDataSource1.ConnectionString))
                {
                    using (SqlCommand comd = new SqlCommand("UPDATE [MFNTrainerTable] SET Trainer_PasswordHash='@password', Trainer_PasswordSalt='@salt' WHERE Trainer_Email = @Trainer_Email"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            comd.CommandType = CommandType.Text;
                            comd.Parameters.AddWithValue("@Trainer_Email", activationCode);
                            comd.Parameters.AddWithValue("@password", hashedPassword);
                            comd.Parameters.AddWithValue("@salt", salt);
                            comd.Connection = con;
                            con.Open();
                            comd.CommandText = "UPDATE [MFNTrainerTable] SET Trainer_PasswordHash='" + hashedPassword + "', Trainer_PasswordSalt='" + salt + "' WHERE Trainer_Email = @Trainer_Email";

                            try
                            { 
                                int rowsAffected = comd.ExecuteNonQuery();
                                con.Close();
                                if (rowsAffected == 1)
                                {
                                    ltMessage.Text = "Reset successful.";
                                    Response.Redirect("Default.aspx");
                                }
                                else
                                {
                                    ltMessage.Text = "Invalid Input.";
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
        }
    }
}