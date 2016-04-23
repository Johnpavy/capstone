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
    public partial class ClientConfirmationPage : System.Web.UI.Page
    {
        UserObject Uobj = new UserObject();
        protected void Page_Load(object sender, EventArgs e)
        {
            String userID = Request.QueryString["UserID"];
            int userIDint = Int32.Parse(userID);
            if (!this.IsPostBack)
            {
                // if there is no session state, use the user id that is passed in the url to fill the info into a session object
                if (Session["UserInfo"] == null)
                {

                 
                    SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = db;

                    try
                    {
                        cmd.CommandText = "Select * FROM [MFNUserTable] WHERE User_Id = @Id";
                        cmd.Parameters.AddWithValue("@Id", userIDint);
                        db.Open();

                        SqlDataReader sdr = cmd.ExecuteReader();

                        while (sdr.Read())
                        {
                            Uobj.FirstName = sdr["User_FirstName"].ToString();
                            Uobj.LastName = sdr["User_LastName"].ToString();
                            Uobj.Email = sdr["User_Email"].ToString();

                        }

                        Session["UserInfo"] = Uobj;


                    }
                    catch
                    {
                        Response.Redirect("Default.aspx");
                    }

                }

                string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
                String UserID = Request.QueryString["UserID"];
                using (SqlConnection con = new SqlConnection(SqlDataSource1.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE MFNUserTable SET User_EmailVarified = @verified WHERE User_Id = @Id"))
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
                                Response.Redirect("ClientSignup.aspx");
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