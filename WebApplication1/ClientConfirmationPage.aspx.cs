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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
               
                string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
                String UserID = Request.QueryString["UserID"];
                using (SqlConnection con = new SqlConnection(SqlDataSource1.ConnectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM UserActivation WHERE User_ActivationCode = @ActivationCode"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
                            cmd.Connection = con;
                            con.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            // cmd.CommandText = "DELETE FROM UserActivation WHERE User_ActivationCode = @ActivationCode";
                            // cmd.ExecuteNonQuery();
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