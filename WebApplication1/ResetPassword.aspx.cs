using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;

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

            Email = TextBox1.Text;

            if(CheckBox1.Checked)
            {
                SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;

                try
                {
                    cmd.CommandText = "Select COUNT(*) FROM [MFNTrainerTable] WHERE Trainer_Email = '" + Email + "'";
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
                    string message = "Reset Password email sent. Please click the link in the email from us to finish password reset (NO EMAIL SENT).";

                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + message + "');window.location='Default.aspx';", true);
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
                    cmd.CommandText = "Select COUNT(*) FROM [MFNUserTable] WHERE User_Email = '" + Email + "'";
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
                    string message = "Reset Password email sent. Please click the link in the email from us to finish password reset (NO EMAIL SENT).";

                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + message + "');window.location='pagename.aspx';", true);
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
    }
}