using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Net;
namespace WebApplication1
{
    public partial class TrainerScheduler : System.Web.UI.Page
    {
        TrainerObject Tobj = new TrainerObject();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TrainerInfo"] == null)
            {
                //Forces a redirect to splash page if this page is loaded without a session state.
                Response.Redirect("Default.aspx");
            }
            else
            {
                Tobj.CopyTrainerObject((TrainerObject)Session["TrainerInfo"]);
                Session["TrainerId"] = Tobj.TrainerId;
                UserNameLbl.Text = Tobj.FirstName + " " + Tobj.LastName;

                //changes default profile pic to user uploaded one
                if (Tobj.ImagePath != "")
                {
                    ProfilePic.Attributes["src"] = Tobj.ImagePath;
                }
            }

            string startDate = Calendar1.TodaysDate.ToShortDateString();

            if (Session["SelectedDate"] == null)
            {
                BlockedOutSelctedDateTxtBox.Text = startDate;
                DateTextBox.Text = startDate;
            }
            else
            {
                BlockedOutSelctedDateTxtBox.Text = (string) Session["SelectedDate"];
                DateTextBox.Text = (string) Session["SelectedDate"];
            }

            //Calendar1.SelectedDate = Calendar1.TodaysDate;

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Session["SelectedDate"] = Calendar1.SelectedDate.ToShortDateString();
            BlockedOutSelctedDateTxtBox.Text = Calendar1.SelectedDate.ToShortDateString();
            DateTextBox.Text = Calendar1.SelectedDate.ToShortDateString();

        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        { 
            //If Date is listed on date blocked table, then mark as slate gray
            //If Date is completly blocked, then mark gray
            if(e.Day.Date == Convert.ToDateTime("4/12/2016"))
            {
                e.Cell.BackColor = System.Drawing.Color.SlateGray;
            }

            if (e.Day.Date == Convert.ToDateTime("4/13/2016"))
            {
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }
        }

        protected void BackToProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm2.aspx");
        }

        protected void PopulateSummaryTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection db = new SqlConnection(SqlDataSource3.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            cmd.CommandText = "SELECT * FROM [MFNCalendarTable] WHERE Calendar_Id = @id";
            cmd.Parameters.AddWithValue("@id", AppointmentsDropbx.Text);

            
            try
            {
                db.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                SummaryTextBox.Text = "";

                while(sdr.Read())
                {
                    SummaryTextBox.Text += "Event Date: ";

                    DateTime dt = Convert.ToDateTime(sdr["Calendar_Date"].ToString());

                    SummaryTextBox.Text += dt.ToShortDateString() + "\n";
                    SummaryTextBox.Text += "Summary: ";
                    SummaryTextBox.Text += sdr["Calendar_EventSummary"].ToString() + "\n";
                    SummaryTextBox.Text += "Start: ";
                    SummaryTextBox.Text += sdr["Calendar_StartTime"].ToString() + "\n";
                    SummaryTextBox.Text += "End: ";
                    SummaryTextBox.Text += sdr["Calendar_EndTime"].ToString() + "\n";
                    SummaryTextBox.Text += "\n";
                }


                if (SummaryTextBox.Text == "")
                {
                    SummaryTextBox.Text = "No Appointmetns Selected";
                }

            }
            catch
            {
                SummaryTextBox.Text = "An error occured displaying!";
            }
            finally
            {
                db.Close();
            }
        }
        protected bool GetValidTime(string startTime, string endTime)
        {
            int startValue, endValue;

            switch (startTime)
            {
                case "12:00 AM":
                    startValue = 1;
                    break;
                case "12:15 AM":
                    startValue = 2;
                    break;
                case "12: 30 AM":
                    startValue = 3;
                    break;
                case "12:45 AM":
                    startValue = 4;
                    break;
                case "1:00 AM":
                    startValue = 5;
                    break;
                case "1:15 AM":
                    startValue = 6;
                    break;
                case "1:30 AM":
                    startValue = 7;
                    break;
                case "1:45 AM":
                    startValue = 8;
                    break;
                case "2:00 AM":
                    startValue = 9;
                    break;
                case "2:15 AM":
                    startValue = 10;
                    break;
                case "2:30 AM":
                    startValue = 11;
                    break;
                case "2:45 AM":
                    startValue = 12;
                    break;
                case "3:00 AM":
                    startValue = 13;
                    break;
                case "3:15 AM":
                    startValue = 14;
                    break;
                case "3:30 AM":
                    startValue = 15;
                    break;
                case "3:45 AM":
                    startValue = 16;
                    break;
                case "4:00 AM":
                    startValue = 17;
                    break;
                case "4:15 AM":
                    startValue = 18;
                    break;
                case "4:30 AM":
                    startValue = 19;
                    break;
                case "4:45 AM":
                    startValue = 20;
                    break;
                case "5:00 AM":
                    startValue = 21;
                    break;
                case "5:15 AM":
                    startValue = 22;
                    break;
                case "5:30 AM":
                    startValue = 23;
                    break;
                case "5:45 AM":
                    startValue = 24;
                    break;
                case "6:00 AM":
                    startValue = 26;
                    break;
                case "6:15 AM":
                    startValue = 27;
                    break;
                case "6:30 AM":
                    startValue = 28;
                    break;
                case "6:45 AM":
                    startValue = 29;
                    break;
                case "7:00 AM":
                    startValue = 30;
                    break;
                case "7:15 AM":
                    startValue = 31;
                    break;
                case "7:30 AM":
                    startValue = 32;
                    break;
                case "7:45 AM":
                    startValue = 33;
                    break;
                case "8:00 AM":
                    startValue = 34;
                    break;
                case "8:15 AM":
                    startValue = 35;
                    break;
                case "8:30 AM":
                    startValue = 36;
                    break;
                case "8:45 AM":
                    startValue = 37;
                    break;
                case "9:00 AM":
                    startValue = 38;
                    break;
                case "9:15 AM":
                    startValue = 39;
                    break;
                case "9:30 AM":
                    startValue = 40;
                    break;
                case "9:45 AM":
                    startValue = 41;
                    break;
                case "10:00 AM":
                    startValue = 42;
                    break;
                case "10:15 AM":
                    startValue = 43;
                    break;
                case "10:45 AM":
                    startValue = 44;
                    break;
                case "11:00 AM":
                    startValue = 45;
                    break;
                case "11:15 AM":
                    startValue = 46;
                    break;
                case "11:30 AM":
                    startValue = 47;
                    break;
                case "11:45 AM":
                    startValue = 48;
                    break;
                case "12:00 PM":
                    startValue = 49;
                    break;
                case "12:15 PM":
                    startValue = 50;
                    break;
                case "12:30 PM":
                    startValue = 51;
                    break;
                case "12:45 PM":
                    startValue = 52;
                    break;
                case "1:00 PM":
                    startValue = 53;
                    break;
                case "1:15 PM":
                    startValue = 54;
                    break;
                case "1:30 PM":
                    startValue = 55;
                    break;
                case "1:45 PM":
                    startValue = 56;
                    break;
                case "2:00 PM":
                    startValue = 57;
                    break;
                case "2:15 PM":
                    startValue = 58;
                    break;
                case "2:30 PM":
                    startValue = 59;
                    break;
                case "2:45 PM":
                    startValue = 60;
                    break;
                case "3:00 PM":
                    startValue = 61;
                    break;
                case "3:15 PM":
                    startValue = 62;
                    break;
                case "3:30 PM":
                    startValue = 63;
                    break;
                case "3:45 PM":
                    startValue = 64;
                    break;
                case "4:00 PM":
                    startValue = 65;
                    break;
                case "4:15 PM":
                    startValue = 66;
                    break;
                case "4:30 PM":
                    startValue = 67;
                    break;
                case "4:45 PM":
                    startValue = 68;
                    break;
                case "5:00 PM":
                    startValue = 69;
                    break;
                case "5:15 PM":
                    startValue = 70;
                    break;
                case "5:30 PM":
                    startValue = 71;
                    break;
                case "5:45 PM":
                    startValue = 72;
                    break;
                case "6:00 PM":
                    startValue = 73;
                    break;
                case "6:15 PM":
                    startValue = 74;
                    break;
                case "6:30 PM":
                    startValue = 75;
                    break;
                case "6:45 PM":
                    startValue = 76;
                    break;
                case "7:00 PM":
                    startValue = 77;
                    break;
                case "7:15 PM":
                    startValue = 78;
                    break;
                case "7:30 PM":
                    startValue = 79;
                    break;
                case "7:45 PM":
                    startValue = 80;
                    break;
                case "8:00 PM":
                    startValue = 81;
                    break;
                case "8:15 PM":
                    startValue = 82;
                    break;
                case "8:30 PM":
                    startValue = 83;
                    break;
                case "8:45 PM":
                    startValue = 84;
                    break;
                case "9:00 PM":
                    startValue = 85;
                    break;
                case "9:15 PM":
                    startValue = 86;
                    break;
                case "9:30 PM":
                    startValue = 87;
                    break;
                case "9:45 PM":
                    startValue = 88;
                    break;
                case "10:00 PM":
                    startValue = 89;
                    break;
                case "10:15 PM":
                    startValue = 90;
                    break;
                case "10:30 PM":
                    startValue = 91;
                    break;
                case "10:45 PM":
                    startValue = 92;
                    break;
                case "11:00 PM":
                    startValue = 93;
                    break;
                case "11:15 PM":
                    startValue = 94;
                    break;
                case "11:30 PM":
                    startValue = 95;
                    break;
                case "11:45 PM":
                    startValue = 96;
                    break;
                default:
                    startValue = 9999;
                    break;
            }

            switch (endTime)
            {
                case "12:00 AM":
                    endValue = 1;
                    break;
                case "12:15 AM":
                    endValue = 2;
                    break;
                case "12: 30 AM":
                    endValue = 3;
                    break;
                case "12:45 AM":
                    endValue = 4;
                    break;
                case "1:00 AM":
                    endValue = 5;
                    break;
                case "1:15 AM":
                    endValue = 6;
                    break;
                case "1:30 AM":
                    endValue = 7;
                    break;
                case "1:45 AM":
                    endValue = 8;
                    break;
                case "2:00 AM":
                    endValue = 9;
                    break;
                case "2:15 AM":
                    endValue = 10;
                    break;
                case "2:30 AM":
                    endValue = 11;
                    break;
                case "2:45 AM":
                    endValue = 12;
                    break;
                case "3:00 AM":
                    endValue = 13;
                    break;
                case "3:15 AM":
                    endValue = 14;
                    break;
                case "3:30 AM":
                    endValue = 15;
                    break;
                case "3:45 AM":
                    endValue = 16;
                    break;
                case "4:00 AM":
                    endValue = 17;
                    break;
                case "4:15 AM":
                    endValue = 18;
                    break;
                case "4:30 AM":
                    endValue = 19;
                    break;
                case "4:45 AM":
                    endValue = 20;
                    break;
                case "5:00 AM":
                    endValue = 21;
                    break;
                case "5:15 AM":
                    endValue = 22;
                    break;
                case "5:30 AM":
                    endValue = 23;
                    break;
                case "5:45 AM":
                    endValue = 24;
                    break;
                case "6:00 AM":
                    endValue = 26;
                    break;
                case "6:15 AM":
                    endValue = 27;
                    break;
                case "6:30 AM":
                    endValue = 28;
                    break;
                case "6:45 AM":
                    endValue = 29;
                    break;
                case "7:00 AM":
                    endValue = 30;
                    break;
                case "7:15 AM":
                    endValue = 31;
                    break;
                case "7:30 AM":
                    endValue = 32;
                    break;
                case "7:45 AM":
                    endValue = 33;
                    break;
                case "8:00 AM":
                    endValue = 34;
                    break;
                case "8:15 AM":
                    endValue = 35;
                    break;
                case "8:30 AM":
                    endValue = 36;
                    break;
                case "8:45 AM":
                    endValue = 37;
                    break;
                case "9:00 AM":
                    endValue = 38;
                    break;
                case "9:15 AM":
                    endValue = 39;
                    break;
                case "9:30 AM":
                    endValue = 40;
                    break;
                case "9:45 AM":
                    endValue = 41;
                    break;
                case "10:00 AM":
                    endValue = 42;
                    break;
                case "10:15 AM":
                    endValue = 43;
                    break;
                case "10:45 AM":
                    endValue = 44;
                    break;
                case "11:00 AM":
                    endValue = 45;
                    break;
                case "11:15 AM":
                    endValue = 46;
                    break;
                case "11:30 AM":
                    endValue = 47;
                    break;
                case "11:45 AM":
                    endValue = 48;
                    break;
                case "12:00 PM":
                    endValue = 49;
                    break;
                case "12:15 PM":
                    endValue = 50;
                    break;
                case "12:30 PM":
                    endValue = 51;
                    break;
                case "12:45 PM":
                    endValue = 52;
                    break;
                case "1:00 PM":
                    endValue = 53;
                    break;
                case "1:15 PM":
                    endValue = 54;
                    break;
                case "1:30 PM":
                    endValue = 55;
                    break;
                case "1:45 PM":
                    endValue = 56;
                    break;
                case "2:00 PM":
                    endValue = 57;
                    break;
                case "2:15 PM":
                    endValue = 58;
                    break;
                case "2:30 PM":
                    endValue = 59;
                    break;
                case "2:45 PM":
                    endValue = 60;
                    break;
                case "3:00 PM":
                    endValue = 61;
                    break;
                case "3:15 PM":
                    endValue = 62;
                    break;
                case "3:30 PM":
                    endValue = 63;
                    break;
                case "3:45 PM":
                    endValue = 64;
                    break;
                case "4:00 PM":
                    endValue = 65;
                    break;
                case "4:15 PM":
                    endValue = 66;
                    break;
                case "4:30 PM":
                    endValue = 67;
                    break;
                case "4:45 PM":
                    endValue = 68;
                    break;
                case "5:00 PM":
                    endValue = 69;
                    break;
                case "5:15 PM":
                    endValue = 70;
                    break;
                case "5:30 PM":
                    endValue = 71;
                    break;
                case "5:45 PM":
                    endValue = 72;
                    break;
                case "6:00 PM":
                    endValue = 73;
                    break;
                case "6:15 PM":
                    endValue = 74;
                    break;
                case "6:30 PM":
                    endValue = 75;
                    break;
                case "6:45 PM":
                    endValue = 76;
                    break;
                case "7:00 PM":
                    endValue = 77;
                    break;
                case "7:15 PM":
                    endValue = 78;
                    break;
                case "7:30 PM":
                    endValue = 79;
                    break;
                case "7:45 PM":
                    endValue = 80;
                    break;
                case "8:00 PM":
                    endValue = 81;
                    break;
                case "8:15 PM":
                    endValue = 82;
                    break;
                case "8:30 PM":
                    endValue = 83;
                    break;
                case "8:45 PM":
                    endValue = 84;
                    break;
                case "9:00 PM":
                    endValue = 85;
                    break;
                case "9:15 PM":
                    endValue = 86;
                    break;
                case "9:30 PM":
                    endValue = 87;
                    break;
                case "9:45 PM":
                    endValue = 88;
                    break;
                case "10:00 PM":
                    endValue = 89;
                    break;
                case "10:15 PM":
                    endValue = 90;
                    break;
                case "10:30 PM":
                    endValue = 91;
                    break;
                case "10:45 PM":
                    endValue = 92;
                    break;
                case "11:00 PM":
                    endValue = 93;
                    break;
                case "11:15 PM":
                    endValue = 94;
                    break;
                case "11:30 PM":
                    endValue = 95;
                    break;
                case "11:45 PM":
                    endValue = 96;
                    break;
                default:
                    endValue = 9999;
                    break;
            }

            if (startValue < endValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //BlockOutSelectedTimes
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            SqlConnection db2 = new SqlConnection(SqlDataSource5.ConnectionString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Connection = db2;

            cmd2.CommandText = "INSERT INTO [MFNBlockedDatesTable] (BlockedDate_Date, Trainer_Id, BlockedDate_StartTime, BlockedDate_EndTime, BlockedDate_IsFullDay) VALUES (@date, @id, @startTime, @endTime, @fullDay)";
            cmd2.Parameters.AddWithValue("@date", BlockedOutSelctedDateTxtBox.Text);
            cmd2.Parameters.AddWithValue("@id", Tobj.TrainerId);
            cmd2.Parameters.AddWithValue("@startTime", "12:00 AM");
            cmd2.Parameters.AddWithValue("@endTime", "11:45 PM");
            cmd2.Parameters.AddWithValue("@fullDay", true);


            try
            {
                db2.Open();
                cmd2.ExecuteNonQuery();
                Response.Write(@"<script language='javascript'>alert('" + BlockedOutSelctedDateTxtBox.Text + " has been selected as unavaliable.');</script>");
            }
            catch
            {
                Response.Write(@"<script language='javascript'>alert('Error Writing to Database!');</script>");
            }
            finally
            {
                db2.Close();
            }

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            bool SelectedFullDate = false;
            int count = 0;

            SqlConnection db = new SqlConnection(SqlDataSource5.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            cmd.CommandText = "SELECT COUNT(*) FROM [MFNBlockedDatesTable] WHERE BlockedDate_Date = @date AND BlockedDate_IsFullDay = @fullDay";
            cmd.Parameters.AddWithValue("@date", BlockedOutSelctedDateTxtBox.Text);
            cmd.Parameters.AddWithValue("@fullDay", 1);

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

            if (count < 0)
            {
            Response.Write(@"<script language='javascript'>alert('Error Connecting to Database!');</script>");
            }
            else if (count > 0)
            {
                SelectedFullDate = false;
            }
            else
            {
            SelectedFullDate = true;
            }


            if (SelectedFullDate)
            {
                Response.Write(@"<script language='javascript'>alert('" + BlockedOutSelctedDateTxtBox.Text + " has already been selected as unavaliable!');</script>"); 
            }   
            else
            {
                SqlConnection db2 = new SqlConnection(SqlDataSource5.ConnectionString);
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd2.Connection = db2;

                cmd2.CommandText = "INSERT INTO [MFNBlockedDatesTable] (BlockedDate_Date, Trainer_Id, BlockedDate_StartTime, BlockedDate_EndTime, BlockedDate_IsFullDay) VALUES (@date, @id, @startTime, @endTime, @fullDay)";
                cmd2.Parameters.AddWithValue("@date", BlockedOutSelctedDateTxtBox.Text);
                cmd2.Parameters.AddWithValue("@id", Tobj.TrainerId);
                cmd2.Parameters.AddWithValue("@startTime", "12:00 AM");
                cmd2.Parameters.AddWithValue("@endTime", "11:45 PM");
                cmd2.Parameters.AddWithValue("@fullDay", true);

                try
                {
                    db2.Open();
                    cmd2.ExecuteNonQuery();
                    Response.Write(@"<script language='javascript'>alert('" + BlockedOutSelctedDateTxtBox.Text + " has been selected as unavaliable.');</script>");
                }
                catch
                {
                    Response.Write(@"<script language='javascript'>alert('Error Writing to Database!');</script>");
                }
                finally
                {
                    db2.Close();
                }
            }
            }
    }
}