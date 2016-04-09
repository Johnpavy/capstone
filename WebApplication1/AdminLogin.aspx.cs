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
    public partial class Admin : System.Web.UI.Page
    {
        AdminObject Aobj = new AdminObject();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login(object sender, EventArgs e)
        {
            int count = 0;

            string UserName = Request.Form["Name"];
            string Password = Request.Form["Password"].ToString();
            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;
            cmd.CommandText = "Select COUNT(*) FROM [MFNAdminTable] WHERE Admin_Email = '" + UserName + "'COLLATE SQL_Latin1_General_CP1_CS_AS AND Admin_PasswordHash = '" + Password + "' COLLATE SQL_Latin1_General_CP1_CS_AS";



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
            If the the Admin is verified and the database has succesfully placed data
            into Admin object.
            */
            if (count == -1)
            {
                //Login fail
                ErrorLbl.Visible = true;
                ErrorLbl.Text = "Dtabase is not connected!";
            }
            else if (count > 0)
            {

                try
                {
                    cmd.CommandText = "Select * FROM [MFNAdminTable] WHERE Admin_Email = '" + UserName + "'";
                    db.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        Aobj.AdminId = Int32.Parse(sdr["Admin_Id"].ToString());
                        Aobj.FirstName = sdr["Admin_FirstName"].ToString();
                        Aobj.LastName = sdr["Admin_LastName"].ToString();
                    }

                    Session["AdminInfo"] = Aobj;
                    Response.Redirect("AdminControl.aspx");

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
}