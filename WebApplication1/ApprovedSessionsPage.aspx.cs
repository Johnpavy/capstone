using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ApprovedSessionsPage : System.Web.UI.Page
    {
        UserObject Uobj = new UserObject();
        TransactionObject TranObj = new TransactionObject();
        int trainerID, calendarID;
        string date, startTime, endTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UserInfo"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                Uobj = (UserObject)Session["UserInfo"];

                // Retrieve session variable from selection from the client profile page
                SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                int x = 0;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;


                try
                {
                    cmd.CommandText = "Select * FROM [MFNCalendarTable] WHERE User_Id = @Uid AND Calendar_ApprovedByTrainer = @app AND Calendar_PaidByClient = @payed";

                    db.Open();

                    cmd.Parameters.AddWithValue("@Uid", Uobj.UserId);
                    cmd.Parameters.AddWithValue("@app", true);
                    cmd.Parameters.AddWithValue("@payed", false);
                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        calendarID = Int32.Parse(sdr["Calendar_Id"].ToString());
                        trainerID = Int32.Parse(sdr["Trainer_Id"].ToString());
                        TranObj.NumberAttending = sdr["Calendar_NumberOfClients"].ToString();

                        DateTime dt = Convert.ToDateTime(sdr["Calendar_Date"].ToString());
                        date = dt.ToShortDateString();
                        startTime = convertMilitartTimetoAMPM(sdr["Calendar_StartTime"].ToString());
                        endTime = convertMilitartTimetoAMPM(sdr["Calendar_EndTime"].ToString());

                        CreateDiv("div" + x);

                    }

                }
                catch
                {
                    ErrorLbl.Visible = true;
                    ErrorLbl.Text = "Error while reading from Database1";
                }
            }
        }

        private void CreateDiv(string divId)
        {
            SqlConnection db = new SqlConnection(SqlDataSource2.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            int x = 0;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            string imagePath ="";
            string fName="";
            string lName="";
            try
            {
                cmd.CommandText = "Select * FROM [MFNTrainerTable] WHERE Trainer_Id = @Tid";

                db.Open();

                cmd.Parameters.AddWithValue("@Tid", trainerID);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {

                    imagePath = sdr["Trainer_Image"].ToString();
                    fName = sdr["Trainer_FirstName"].ToString();
                    lName = sdr["Trainer_LastName"].ToString();
                    TranObj.IndividualCost = sdr["Trainer_IndividualRate"].ToString();
                    TranObj.AdditionalPersonCost = sdr["Trainer_AdditionalPersonRate"].ToString();
                }

            }
            catch
            {
                ErrorLbl.Visible = true;
                ErrorLbl.Text = "Error while reading from Database2";
            }


            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("id", divId);
            div.Attributes.Add("runat", "server");
            div.Attributes.Add("class", "row centered-form");
            imagePath = imagePath + "ProfilePic.jpg";
            //this line is an absolute nightmare,but it should work!
            div.InnerHtml = "<div class=\"row centered-form\" runat=\"server\"><div class=\"col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4\"><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">Local Trainer</h3></div><div class=\"panel-body\"><img src=\"" + imagePath + "\" class=\"UserPicture img-circle img - responsive\" style=\"width: 50px; height: 50px; \">" + " " + fName + " " + lName + "   " + " has approved your session on " + date + " from " + startTime + " to " + endTime + " <a href='" + Request.Url.AbsoluteUri.Replace("ApprovedSessionsPage.aspx", "CheckOut.aspx?SessionTransactionInfo=" + TranObj.ToString()+"&CalendarID="+calendarID) + "' >Click Here To Pay</a></div></div></div></div>"; //not completed need button event to launch session!
            SessionResults.Controls.Add(div);
        }

        private string convertMilitartTimetoAMPM(string time)
        {
            string newTime = DateTime.Parse(time).ToString(@"hh\:mm\:ss tt");
            return newTime;
        }
    }
}