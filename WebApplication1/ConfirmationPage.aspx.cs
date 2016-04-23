using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class ConfirmationPage : System.Web.UI.Page
    {
        TrainerObject Tobj = new TrainerObject();

        protected void Page_Load(object sender, EventArgs e)
        {
            // get the user id from url
            String userID = Request.QueryString["UserID"];
            // turn it into an int
            int userIDint = Int32.Parse(userID);

            if (!this.IsPostBack)
            {
                // if session object is null, try to populate it from database with name and email
                if (Session["TrainerInfo"] == null)
                {

                    SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = db;

                    try
                    {
                        cmd.CommandText = "Select * FROM [MFNTrainerTable] WHERE Trainer_Id = @Id";
                        cmd.Parameters.AddWithValue("@Id", userIDint);
                        db.Open();

                        SqlDataReader sdr = cmd.ExecuteReader();

                        while (sdr.Read())
                        {
                            Tobj.FirstName = sdr["Trainer_FirstName"].ToString();
                            Tobj.LastName = sdr["Trainer_LastName"].ToString();
                            Tobj.Email = sdr["Trainer_Email"].ToString();

                        }

                        Session["TrainerInfo"] = Tobj;


                    }
                    catch
                    {
                        Response.Redirect("Default.aspx");
                    }

                }
                // Get activation code from url
                String activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();

                using (SqlConnection con = new SqlConnection(SqlDataSource1.ConnectionString))
                {
                    //using (SqlCommand cmd = new SqlCommand("SELECT COUNT (*) FROM UserActivation WHERE User_ActivationCode = @ActivationCode"))
                    //using (SqlCommand cmd = new SqlCommand("UPDATE MFNTrainerTable SET Trainer_ActivationCode = NULL WHERE Trainer_ActivationCode = @ActivationCode"))
                    // Delete the entry from the activation code table
                    using (SqlCommand cmd = new SqlCommand("UPDATE MFNTrainerTable SET Trainer_EmailVerified = @verified WHERE Trainer_Id = @Id"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Id", userIDint);
                            cmd.Parameters.AddWithValue("@verified", "True");
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM UserActivation WHERE User_ActivationCode = @ActivationCode"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
                            cmd.Connection = con;
                            con.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            con.Close();
                            if (rowsAffected == 1)
                            {
                                ltMessage.Text = "Activation successful.";
                                Response.Redirect("TrainerSignup.aspx");
                            }
                            else
                            {
                                ltMessage.Text = "Invalid Activation code.";
                            }
                        }
                    }
                    // update trainer table to reflect that email has been confirmed

                }
            }
        }
    }
}