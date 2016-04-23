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
            String userID = Request.QueryString["UserID"];
            int userIDint = Int32.Parse(userID);
            if (!this.IsPostBack)
            {
                //SqlConnection trainerDb = new SqlConnection(SqlDataSource1.ConnectionString);
                if (Session["TrainerInfo"] == null)
                {

                    //Forces a redirect to splash page if this page is loaded without a session state.
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
                //  string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                String activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
                String UserID = Request.QueryString["UserID"];

                using (SqlConnection con = new SqlConnection(SqlDataSource1.ConnectionString))
                {
                    //using (SqlCommand cmd = new SqlCommand("SELECT COUNT (*) FROM UserActivation WHERE User_ActivationCode = @ActivationCode"))
                    //using (SqlCommand cmd = new SqlCommand("UPDATE MFNTrainerTable SET Trainer_ActivationCode = NULL WHERE Trainer_ActivationCode = @ActivationCode"))
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
                }
            }
        }
    }
}