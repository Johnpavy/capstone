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
    public partial class WebForm5 : System.Web.UI.Page
    {
        TrainerObject Tobj = new TrainerObject();
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["TrainerInfo"] = null;
        }

        protected void Login(object sender, EventArgs e)
        {
            string UserName = Request.Form["Name"];
            string Password = Request.Form["Password"];
            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;
            cmd.CommandText = "Select COUNT(*) FROM [MFNTrainerTable] WHERE Trainer_Email = '"+ UserName +"' AND Trainer_PasswordHash = '"+Password +"'";



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
                //login fail
            }
            else
            {
                try
                {
                    cmd.CommandText = "Select * FROM [MFNTrainerTable] WHERE Trainer_Id = '0'";
                    db.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        Tobj.TrainerId = Int32.Parse(sdr["Trainer_Id"].ToString());
                        Tobj.FirstName = sdr["Trainer_FirstName"].ToString();
                        Tobj.LastName = sdr["Trainer_LastName"].ToString();
                    }

                    Session["TrainerInfo"] = Tobj;
                    Response.Redirect("WebForm2.aspx");

                }
                catch
                {

                }
            }
        }
    }
}