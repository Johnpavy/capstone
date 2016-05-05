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
            //get all of the payment session information and populate the appropriate string
            string first_name = (string)(Session["first_name"]);
            string address = (string)(Session["address"]);
            string city = (string)(Session["city"]);
            string zip = (string)(Session["postal_code"]);
            string type = (string)(Session["card_type"]);
            //string card_num = (string)(Session["card_number"]); //exclude credit card number
            string service_fee = (string)(Session["service_fee"]);
            string standard_rate = (string)(Session["standard_rate"]);
            string additional_person = (string)(Session["additional_person"]);
            string number_people = (string)(Session["number_people"]);
            string sub_total = (string)(Session["sub_total"]);
            string currency = (string)(Session["currency"]);
            string total = (string)(Session["total"]);

            //set the correct lable to the session value
            Namelbl.Text = first_name;
            Addresslbl.Text = address;
            Citylbl.Text = city;
            Ziplbl.Text = zip;
            Typelbl.Text = type;
            //CardNumlbl.Text = card_num;
            Ratelbl.Text = standard_rate;
            AddPersonRatelbl.Text = additional_person;
            NumPeoplelbl.Text = number_people;
            ServiceFeelbl.Text = service_fee;
            SubTotallbl.Text = sub_total;
            Currencylbl.Text = currency;
            Totallbl.Text = total;

            //get the calendar ID from the calendar session
            int calendarID = Int32.Parse(Session["CalendarID"].ToString());

            //add Section that marks the database
            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;
            // SQL querry
            cmd.CommandText = "UPDATE [MFNCalendarTable] SET Calendar_PaidByClient = @paid  WHERE Calendar_Id = @id";
            cmd.Parameters.AddWithValue("@paid", true);
            cmd.Parameters.AddWithValue("@id", calendarID);

            try
            {
                //open the connection and execute the querry
                db.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Response.Write(@"<script language='javascript'>alert('Error Loading Events');</script>");
            }
            finally
            {
                //close the connection to the database
                db.Close();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //return home button, redirects to the home page after a session has been paid for
            Response.Redirect("Default.aspx");
        }


    }
}