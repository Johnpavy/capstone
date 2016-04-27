using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class PaymentConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string field1 = (string)(Session["first_name"]);
            string field2 = (string)(Session["address"]);
            string field3 = (string)(Session["city"]);
            string field4 = (string)(Session["postal_code"]);
            string field5 = (string)(Session["card_type"]);
            //string field6 = (string)(Session["card_number"]);
            string field7 = (string)(Session["currency"]);
            string field8 = (string)(Session["total"]);
     
            // var frm1 = new CheckOut();
            // frm1.ShowDialog(this); // make sure this instance of Form1 is visible
            // Label1.Text = frm1.MyValue;

            Label1.Text = field1;
            Label2.Text = field2;
            Label3.Text = field3;
            Label4.Text = field4;
            Label5.Text = field5;
            //Label6.Text = field6;
            Label7.Text = field7;
            Label8.Text = field8;

            int calendarID = Int32.Parse(Session["CalendarID"].ToString());

            //add Section that marks the database
            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            cmd.CommandText = "UPDATE [MFNCalendarTable] SET Calendar_PaidByClient = @paid  WHERE Calendar_Id = @id";
            cmd.Parameters.AddWithValue("@paid", true);
            cmd.Parameters.AddWithValue("@id", calendarID);

            try
            {
                db.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Response.Write(@"<script language='javascript'>alert('Error Loading Events');</script>");
            }
            finally
            {
                db.Close();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }


    }
}