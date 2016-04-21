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
    public partial class searchTrainersPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String selection = (String)Session["Selection"];
            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;
            int trainerID;
            String imagePath, fName, lName, rate;

            try
            {
                cmd.CommandText = "Select * FROM [MFNTrainerTable] WHERE Trainer_Specialty = @selection";
                
                db.Open();

                cmd.Parameters.AddWithValue("@selection", selection);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                   trainerID = Int32.Parse(sdr["Trainer_Id"].ToString());
                   imagePath = sdr["Trainer_Image"].ToString();
                   fName = sdr["Trainer_FirstName"].ToString();
                   lName = sdr["Trainer_LastName"].ToString();
                   rate = sdr["Trainer_IndividualRate"].ToString();

                }

            }
            catch
            {
                ErrorLbl.Visible = true;
                ErrorLbl.Text = "Error while reading from Database";
            }

        }
    }
}