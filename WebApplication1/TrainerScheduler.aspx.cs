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
        UserObject Uobj = new UserObject();
        int blockedFullDaysCount = 0;
        string[] blockedFullDays = new string[1000];
        int blockedParitalDaysCount = 0;
        string[] blockedPartialDays = new string[1000];

        string address, start, end, numPeople, date;


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

                if (Session["UserInfo2"] != null)
                {
                    Uobj = (UserObject)Session["UserInfo2"];
                }


                if(Session["address"] != null)
                {
                    address = Session["address"].ToString();
                    start = Session["start"].ToString();
                    end = Session["end"].ToString();
                    numPeople = Session["numPeople"].ToString();
                    date = Session["date"].ToString();
                }


                //changes default profile pic to user uploaded one
                if (Tobj.ImagePath != "")
                {
                    ProfilePic.Attributes["src"] = Tobj.ImagePath + "ProfilePic.jpg";
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

            if(Session["SummaryTextBox"] == null)
            {
                SummaryTextBox.Text = "";
            }
            else
            {
                SummaryTextBox.Text = Session["SummaryTextBox"].ToString();
            }

            //Begin Loading Calendar with Blocked Dates Data

            SqlConnection db = new SqlConnection(SqlDataSource5.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            cmd.CommandText = "SELECT * FROM [MFNBlockedDatesTable] WHERE Trainer_Id = @id ORDER BY BlockedDate_Date";
            cmd.Parameters.AddWithValue("@id", Tobj.TrainerId);

            try
            {
                db.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while(sdr.Read())
                {
                    DateTime dt = Convert.ToDateTime(sdr["BlockedDate_Date"].ToString());
                    string temp = dt.ToShortDateString();

                    //reads bit type from sql db
                    bool isDateFullyBlocked = sdr.GetBoolean(sdr.GetOrdinal("BlockedDate_IsFullDay"));

                    if (isDateFullyBlocked)
                    {
                        blockedFullDays[blockedFullDaysCount] = temp;
                        blockedFullDaysCount++;
                    }
                    else
                    {
                        blockedPartialDays[blockedParitalDaysCount] = temp;
                        blockedParitalDaysCount++;
                    }
                }
            }
            catch
            {
                Response.Write(@"<script language='javascript'>alert('Error Loading Blocked Dates');</script>");
            }
            finally
            {
                db.Close();
            }

            if(!IsPostBack)
            {
                SqlConnection db3 = new SqlConnection(SqlDataSource3.ConnectionString);
                SqlCommand cmd3 = new SqlCommand();
                cmd3.CommandType = System.Data.CommandType.Text;
                cmd3.Connection = db3;

                ClientsDrpDown.Items.Clear();
                ListItem l = new ListItem("---Select---", "", true);
                ClientsDrpDown.Items.Add(l);

                cmd3.CommandText = "SELECT * FROM [MFNCalendarTable] WHERE Trainer_Id = @Tid AND Calendar_ApprovedByTrainer = @app";
                cmd3.Parameters.AddWithValue("@Tid", Tobj.TrainerId);
                cmd3.Parameters.AddWithValue("@app", false);

                try
                {
                    db3.Open();
                    SqlDataReader sdr = cmd3.ExecuteReader();

                    while (sdr.Read())
                    {
                        string name = sdr["Calendar_EventName"].ToString();
                        string calendarId = sdr["Calendar_Id"].ToString();
                        l = new ListItem(name, calendarId, true);
                        ClientsDrpDown.Items.Add(l);
                    }
                }
                catch
                {
                    Response.Write(@"<script language='javascript'>alert('Error Loading Events');</script>");
                }
                finally
                {
                    db3.Close();
                }
            }


        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Session["SelectedDate"] = Calendar1.SelectedDate.ToShortDateString();
            BlockedOutSelctedDateTxtBox.Text = Calendar1.SelectedDate.ToShortDateString();
            DateTextBox.Text = Calendar1.SelectedDate.ToShortDateString();
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        { 

            //PartialDay Color
            for (int x = 0; x < blockedParitalDaysCount; x++)
            {
                if (e.Day.Date == Convert.ToDateTime(blockedPartialDays[x]))
                {
                    e.Cell.BackColor = System.Drawing.Color.Silver;
                }
            }

            //FullDay Color
            for (int x = 0; x < blockedFullDaysCount; x++)
            {
                if (e.Day.Date == Convert.ToDateTime(blockedFullDays[x]))
                {
                    e.Cell.BackColor = System.Drawing.Color.DimGray;
                }
            }


        }

        protected void BackToProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm2.aspx");
        }


        protected void SelectThisClientBtn_Click(object sender, EventArgs e)
        {
            ConfirmAppointment.Enabled = true;
            DeclineAppointment.Enabled = true;
            RescheduleAppointment.Enabled = true;
            int userID = -1;
            SqlConnection db3 = new SqlConnection(SqlDataSource3.ConnectionString);
            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandType = System.Data.CommandType.Text;
            cmd3.Connection = db3;
            cmd3.CommandText = "SELECT * FROM [MFNCalendarTable] WHERE Calendar_Id = @id";
            cmd3.Parameters.AddWithValue("@id", ClientsDrpDown.SelectedValue);
            try
            {
                db3.Open();
                SqlDataReader sdr = cmd3.ExecuteReader();
                SummaryTextBox.Text = "";
                while (sdr.Read())
                {
                    start = convertMilitartTimetoAMPM(sdr["Calendar_StartTime"].ToString());
                    SummaryTextBox.Text += "Start Time: " + start+ "\n";
                    Session["start"] = start;
                    end = convertMilitartTimetoAMPM(sdr["Calendar_EndTime"].ToString());
                    Session["end"] = end;
                    SummaryTextBox.Text += "End Time: " + end + "\n";
                    numPeople = sdr["Calendar_NumberOfClients"].ToString();
                    Session["numPeople"] = numPeople;
                    SummaryTextBox.Text += "Total Number of People: "+ numPeople + "\n";
                    address = sdr["Calendar_Location"].ToString();
                    SummaryTextBox.Text += "Address: " + address + "\n";
                    Session["address"] = address;
                    userID = Int32.Parse(sdr["User_Id"].ToString());
                    SummaryTextBox.Text += "ID: " + userID + "\n";


                    DateTime dt = Convert.ToDateTime(sdr["Calendar_Date"].ToString());
                    date = dt.ToShortDateString();
                    Session["date"] = date;
                }
            }
            catch
            {
                SummaryTextBox.Text = ClientsDrpDown.SelectedValue + "error reading from database";
            }
            finally
            {
                db3.Close();
            }


            SqlConnection db4 = new SqlConnection(SqlDataSource2.ConnectionString);
            SqlCommand cmd4 = new SqlCommand();
            cmd4.CommandType = System.Data.CommandType.Text;
            cmd4.Connection = db4;
            cmd4.CommandText = "SELECT * FROM [MFNUserTable] WHERE User_Id = @Uid";
            cmd4.Parameters.AddWithValue("@Uid", userID);
            try
            {
                db4.Open();
                
                SqlDataReader sdr2 = cmd4.ExecuteReader();
                while (sdr2.Read())
                {

                    Uobj.FirstName = sdr2["User_FirstName"].ToString();
                    Uobj.LastName = sdr2["User_LastName"].ToString();
                    Uobj.Email = sdr2["User_Email"].ToString();

                    Session["UserInfo2"] = Uobj; 
                }
               
            }
            catch
            {
                SummaryTextBox.Text += "FAIL";
            }
            finally
            {
                db4.Close();
            }

        }




        //Time Range Validator
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
                case "10:30 AM":
                    startValue = 44;
                    break;
                case "10:45 AM":
                    startValue = 45;
                    break;
                case "11:00 AM":
                    startValue = 46;
                    break;
                case "11:15 AM":
                    startValue = 47;
                    break;
                case "11:30 AM":
                    startValue = 48;
                    break;
                case "11:45 AM":
                    startValue = 49;
                    break;
                case "12:00 PM":
                    startValue = 50;
                    break;
                case "12:15 PM":
                    startValue = 51;
                    break;
                case "12:30 PM":
                    startValue = 52;
                    break;
                case "12:45 PM":
                    startValue = 53;
                    break;
                case "1:00 PM":
                    startValue = 54;
                    break;
                case "1:15 PM":
                    startValue = 55;
                    break;
                case "1:30 PM":
                    startValue = 56;
                    break;
                case "1:45 PM":
                    startValue = 57;
                    break;
                case "2:00 PM":
                    startValue = 58;
                    break;
                case "2:15 PM":
                    startValue = 59;
                    break;
                case "2:30 PM":
                    startValue = 60;
                    break;
                case "2:45 PM":
                    startValue = 61;
                    break;
                case "3:00 PM":
                    startValue = 62;
                    break;
                case "3:15 PM":
                    startValue = 63;
                    break;
                case "3:30 PM":
                    startValue = 64;
                    break;
                case "3:45 PM":
                    startValue = 65;
                    break;
                case "4:00 PM":
                    startValue = 66;
                    break;
                case "4:15 PM":
                    startValue = 67;
                    break;
                case "4:30 PM":
                    startValue = 68;
                    break;
                case "4:45 PM":
                    startValue = 69;
                    break;
                case "5:00 PM":
                    startValue = 70;
                    break;
                case "5:15 PM":
                    startValue = 71;
                    break;
                case "5:30 PM":
                    startValue = 72;
                    break;
                case "5:45 PM":
                    startValue = 73;
                    break;
                case "6:00 PM":
                    startValue = 74;
                    break;
                case "6:15 PM":
                    startValue = 75;
                    break;
                case "6:30 PM":
                    startValue = 76;
                    break;
                case "6:45 PM":
                    startValue = 77;
                    break;
                case "7:00 PM":
                    startValue = 78;
                    break;
                case "7:15 PM":
                    startValue = 79;
                    break;
                case "7:30 PM":
                    startValue = 80;
                    break;
                case "7:45 PM":
                    startValue = 81;
                    break;
                case "8:00 PM":
                    startValue = 82;
                    break;
                case "8:15 PM":
                    startValue = 83;
                    break;
                case "8:30 PM":
                    startValue = 84;
                    break;
                case "8:45 PM":
                    startValue = 85;
                    break;
                case "9:00 PM":
                    startValue = 86;
                    break;
                case "9:15 PM":
                    startValue = 87;
                    break;
                case "9:30 PM":
                    startValue = 88;
                    break;
                case "9:45 PM":
                    startValue = 89;
                    break;
                case "10:00 PM":
                    startValue = 90;
                    break;
                case "10:15 PM":
                    startValue = 91;
                    break;
                case "10:30 PM":
                    startValue = 92;
                    break;
                case "10:45 PM":
                    startValue = 93;
                    break;
                case "11:00 PM":
                    startValue = 94;
                    break;
                case "11:15 PM":
                    startValue = 95;
                    break;
                case "11:30 PM":
                    startValue = 96;
                    break;
                case "11:45 PM":
                    startValue = 97;
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
                case "10:30 AM":
                    endValue = 44;
                    break;
                case "10:45 AM":
                    endValue = 45;
                    break;
                case "11:00 AM":
                    endValue = 46;
                    break;
                case "11:15 AM":
                    endValue = 47;
                    break;
                case "11:30 AM":
                    endValue = 48;
                    break;
                case "11:45 AM":
                    endValue = 49;
                    break;
                case "12:00 PM":
                    endValue = 50;
                    break;
                case "12:15 PM":
                    endValue = 51;
                    break;
                case "12:30 PM":
                    endValue = 52;
                    break;
                case "12:45 PM":
                    endValue = 53;
                    break;
                case "1:00 PM":
                    endValue = 54;
                    break;
                case "1:15 PM":
                    endValue = 55;
                    break;
                case "1:30 PM":
                    endValue = 56;
                    break;
                case "1:45 PM":
                    endValue = 57;
                    break;
                case "2:00 PM":
                    endValue = 58;
                    break;
                case "2:15 PM":
                    endValue = 59;
                    break;
                case "2:30 PM":
                    endValue = 60;
                    break;
                case "2:45 PM":
                    endValue = 61;
                    break;
                case "3:00 PM":
                    endValue = 62;
                    break;
                case "3:15 PM":
                    endValue = 63;
                    break;
                case "3:30 PM":
                    endValue = 64;
                    break;
                case "3:45 PM":
                    endValue = 65;
                    break;
                case "4:00 PM":
                    endValue = 66;
                    break;
                case "4:15 PM":
                    endValue = 67;
                    break;
                case "4:30 PM":
                    endValue = 68;
                    break;
                case "4:45 PM":
                    endValue = 69;
                    break;
                case "5:00 PM":
                    endValue = 70;
                    break;
                case "5:15 PM":
                    endValue = 71;
                    break;
                case "5:30 PM":
                    endValue = 72;
                    break;
                case "5:45 PM":
                    endValue = 73;
                    break;
                case "6:00 PM":
                    endValue = 74;
                    break;
                case "6:15 PM":
                    endValue = 75;
                    break;
                case "6:30 PM":
                    endValue = 76;
                    break;
                case "6:45 PM":
                    endValue = 77;
                    break;
                case "7:00 PM":
                    endValue = 78;
                    break;
                case "7:15 PM":
                    endValue = 79;
                    break;
                case "7:30 PM":
                    endValue = 80;
                    break;
                case "7:45 PM":
                    endValue = 81;
                    break;
                case "8:00 PM":
                    endValue = 82;
                    break;
                case "8:15 PM":
                    endValue = 83;
                    break;
                case "8:30 PM":
                    endValue = 84;
                    break;
                case "8:45 PM":
                    endValue = 85;
                    break;
                case "9:00 PM":
                    endValue = 86;
                    break;
                case "9:15 PM":
                    endValue = 87;
                    break;
                case "9:30 PM":
                    endValue = 88;
                    break;
                case "9:45 PM":
                    endValue = 89;
                    break;
                case "10:00 PM":
                    endValue = 90;
                    break;
                case "10:15 PM":
                    endValue = 91;
                    break;
                case "10:30 PM":
                    endValue = 92;
                    break;
                case "10:45 PM":
                    endValue = 93;
                    break;
                case "11:00 PM":
                    endValue = 94;
                    break;
                case "11:15 PM":
                    endValue = 95;
                    break;
                case "11:30 PM":
                    endValue = 96;
                    break;
                case "11:45 PM":
                    endValue = 97;
                    break;
                default:
                    endValue = 9999;
                    break;
            }

            if (startValue < endValue && (endValue - startValue == 4))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //BlockOutSelectedTimes
        protected void BlockOutSelectedTimesBtn_Click(object sender, EventArgs e)
        {
            int count = 0;


            if (GetValidTime(StartTimeDrpList.Text, EndTimeDrpList.Text))
            {

                SqlConnection db = new SqlConnection(SqlDataSource5.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = db;

                cmd.CommandText = "SELECT COUNT(*) FROM [MFNBlockedDatesTable] WHERE Trainer_Id = @id AND BlockedDate_Date = @date AND BlockedDate_IsFullDay = @fullDay AND BlockedDate_StartTime = @startTime AND BlockedDate_EndTime = @endTime";
                cmd.Parameters.AddWithValue("@id", Tobj.TrainerId);
                cmd.Parameters.AddWithValue("@date", BlockedOutSelctedDateTxtBox.Text);
                cmd.Parameters.AddWithValue("@startTime", StartTimeDrpList.Text);
                cmd.Parameters.AddWithValue("@endTime", EndTimeDrpList.Text);
                cmd.Parameters.AddWithValue("@fullDay", false);

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

                if(count < 0)
                {
                    Response.Write(@"<script language='javascript'>alert('Error Writing to Database!');</script>");
                }
                else if(count > 0)
                {
                    Response.Write(@"<script language='javascript'>alert('This entry already exists!');</script>");
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
                    cmd2.Parameters.AddWithValue("@startTime", StartTimeDrpList.Text);
                    cmd2.Parameters.AddWithValue("@endTime", EndTimeDrpList.Text);
                    cmd2.Parameters.AddWithValue("@fullDay", false);


                    try
                    {
                        db2.Open();
                        cmd2.ExecuteNonQuery();
                        Response.Write(@"<script language='javascript'>alert('" + BlockedOutSelctedDateTxtBox.Text + " from " + StartTimeDrpList.Text + "to" + EndTimeDrpList.Text + "has been selected as unavaliable.');</script>");
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
            else
            {
                Response.Write(@"<script language='javascript'>alert('Invalid Time Range!\n" + StartTimeDrpList.Text + "to" + EndTimeDrpList.Text + "');</script>");
            }

        }

        //BlockOutFullDay
        protected void BlockOutEntireDayBtn_Click(object sender, EventArgs e)
        {
            bool SelectedFullDate = false;
            int count = 0;

            SqlConnection db = new SqlConnection(SqlDataSource5.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            cmd.CommandText = "SELECT COUNT(*) FROM [MFNBlockedDatesTable] WHERE Trainer_Id = @id AND BlockedDate_Date = @date AND BlockedDate_IsFullDay = @fullDay";
            cmd.Parameters.AddWithValue("@id", Tobj.TrainerId);
            cmd.Parameters.AddWithValue("@date", BlockedOutSelctedDateTxtBox.Text);
            cmd.Parameters.AddWithValue("@fullDay", true);

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
                SelectedFullDate = true; //to prevent altering the db.
            }
            else if (count > 0)
            {
                SelectedFullDate = true;
            }


            if (SelectedFullDate)
            {
                if (count != -1)
                {
                   Response.Write(@"<script language='javascript'>alert('" + BlockedOutSelctedDateTxtBox.Text + " has already been selected as unavaliable!');</script>");
                }
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
                    Response.Write(@"<script language='javascript'>alert('" + BlockedOutSelctedDateTxtBox.Text + " has been marked as unavaliable.');</script>");
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

        //Reopen Entire Day
        protected void ReopenEntireDayBtn_Click(object sender, EventArgs e)
        {

            SqlConnection db2 = new SqlConnection(SqlDataSource5.ConnectionString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Connection = db2;

            cmd2.CommandText = "DELETE FROM [MFNBlockedDatesTable] WHERE Trainer_Id = @id AND BlockedDate_Date = @date AND BlockedDate_IsFullDay = @fullDay";
            cmd2.Parameters.AddWithValue("@date", BlockedOutSelctedDateTxtBox.Text);
            cmd2.Parameters.AddWithValue("@id", Tobj.TrainerId);
            cmd2.Parameters.AddWithValue("@startTime", "12:00 AM");
            cmd2.Parameters.AddWithValue("@endTime", "11:45 PM");
            cmd2.Parameters.AddWithValue("@fullDay", true);

            try
            {
                db2.Open();
                cmd2.ExecuteNonQuery();
                Response.Write(@"<script language='javascript'>alert('" + BlockedOutSelctedDateTxtBox.Text + " has been reopened.');</script>");
            }
            catch
            {
                Response.Write(@"<script language='javascript'>alert('Error Removing from Database!');</script>");
            }
            finally
            {
                db2.Close();
            }

        }

        //Reopen Selected Time
        protected void ReopenSelectedTimesBtn_Click(object sender, EventArgs e)
        {
            if (GetValidTime(StartTimeDrpList.Text, EndTimeDrpList.Text))
            {
                SqlConnection db2 = new SqlConnection(SqlDataSource5.ConnectionString);
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd2.Connection = db2;

                cmd2.CommandText = "DELETE FROM [MFNBlockedDatesTable] WHERE Trainer_Id = @id AND BlockedDate_Date = @date AND BlockedDate_IsFullDay = @fullDay AND BlockedDate_StartTime = @startTime AND BlockedDate_EndTime = @endTime";
                cmd2.Parameters.AddWithValue("@date", BlockedOutSelctedDateTxtBox.Text);
                cmd2.Parameters.AddWithValue("@id", Tobj.TrainerId);
                cmd2.Parameters.AddWithValue("@startTime", StartTimeDrpList.Text);
                cmd2.Parameters.AddWithValue("@endTime", EndTimeDrpList.Text);
                cmd2.Parameters.AddWithValue("@fullDay", false);



                try
                {
                    db2.Open();
                    cmd2.ExecuteNonQuery();
                    Response.Write(@"<script language='javascript'>alert('" + BlockedOutSelctedDateTxtBox.Text + " from " + StartTimeDrpList.Text + "to" + EndTimeDrpList.Text + "has been reopened.');</script>");
                }
                catch
                {
                    Response.Write(@"<script language='javascript'>alert('Error Removing from Database!');</script>");
                }
                finally
                {
                    db2.Close();
                }
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Invalid Time Range!\n" + StartTimeDrpList.Text + " to " + EndTimeDrpList.Text + "');</script>");
            }

        }

        protected void ManageSession_Click(object sender, EventArgs e)
        {
            OptionsDiv.Visible = false;
            ManageAppointmentDiv.Visible = true;

        }

        protected void ManageBlackedOutTimes_Click(object sender, EventArgs e)
        {
            OptionsDiv.Visible = false;
            ManageBlockedTimeDiv.Visible = true;
        }

        protected void CancelAppointmentManagement_Click(object sender, EventArgs e)
        {
            ConfirmAppointment.Enabled = false;
            DeclineAppointment.Enabled = false;
            RescheduleAppointment.Enabled = false;
            OptionsDiv.Visible = true;
            ManageAppointmentDiv.Visible = false;
        }

        protected void DeclineAppointment_Click(object sender, EventArgs e)
        {


            ConfirmAppointment.Enabled = false;
            DeclineAppointment.Enabled = false;
            RescheduleAppointment.Enabled = false;
            SqlConnection db3 = new SqlConnection(SqlDataSource3.ConnectionString);
            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandType = System.Data.CommandType.Text;
            cmd3.Connection = db3;
            cmd3.CommandText = "DELETE FROM [MFNCalendarTable] WHERE Calendar_Id = @id";
            cmd3.Parameters.AddWithValue("@id", ClientsDrpDown.SelectedValue);
            SendDeclineEmail(Uobj.FirstName, Uobj.LastName, address, date, start, end, numPeople);


            try
            {
                db3.Open();
                cmd3.ExecuteNonQuery();
                Response.Write(@"<script language='javascript'>alert('Successfuly Declined Client.');</script>");
            }
            catch
            {
                Response.Write(@"<script language='javascript'>alert('Error Removing Event');</script>");
            }
            finally
            {
                db3.Close();
            }


            SqlConnection db = new SqlConnection(SqlDataSource3.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            ClientsDrpDown.Items.Clear();
            ListItem l = new ListItem("---Select---", "", true);
            ClientsDrpDown.Items.Add(l);

            cmd.CommandText = "SELECT * FROM [MFNCalendarTable] WHERE Trainer_Id = @Tid AND Calendar_ApprovedByTrainer = @app";
            cmd.Parameters.AddWithValue("@Tid", Tobj.TrainerId);
            cmd.Parameters.AddWithValue("@app", false);

            try
            {
                db.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    string name = sdr["Calendar_EventName"].ToString();
                    string calendarId = sdr["Calendar_Id"].ToString();
                    l = new ListItem(name, calendarId, true);
                    ClientsDrpDown.Items.Add(l);
                }
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

        protected void RescheduleAppointment_Click(object sender, EventArgs e)
        {
            ConfirmAppointment.Enabled = false;
            DeclineAppointment.Enabled = false;
            RescheduleAppointment.Enabled = false;
            SqlConnection db3 = new SqlConnection(SqlDataSource3.ConnectionString);
            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandType = System.Data.CommandType.Text;
            cmd3.Connection = db3;
            cmd3.CommandText = "DELETE FROM [MFNCalendarTable] WHERE Calendar_Id = @id";
            cmd3.Parameters.AddWithValue("@id", ClientsDrpDown.SelectedValue);
            SendRescheduleEmail(Uobj.FirstName, Uobj.LastName, address, date, start, end, numPeople);


            try
            {
                db3.Open();
                cmd3.ExecuteNonQuery();
                Response.Write(@"<script language='javascript'>alert('Successfuly Requested a Reschedule for Client.');</script>");
            }
            catch
            {
                Response.Write(@"<script language='javascript'>alert('Error Removing Event');</script>");
            }
            finally
            {
                db3.Close();
            }


            SqlConnection db = new SqlConnection(SqlDataSource3.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            ClientsDrpDown.Items.Clear();
            ListItem l = new ListItem("---Select---", "", true);
            ClientsDrpDown.Items.Add(l);

            cmd.CommandText = "SELECT * FROM [MFNCalendarTable] WHERE Trainer_Id = @Tid AND Calendar_ApprovedByTrainer = @app";
            cmd.Parameters.AddWithValue("@Tid", Tobj.TrainerId);
            cmd.Parameters.AddWithValue("@app", false);

            try
            {
                db.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    string name = sdr["Calendar_EventName"].ToString();
                    string calendarId = sdr["Calendar_Id"].ToString();
                    l = new ListItem(name, calendarId, true);
                    ClientsDrpDown.Items.Add(l);
                }
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

        protected void CancelManageBlockedOutDate_Click(object sender, EventArgs e)
        {
            OptionsDiv.Visible = true;
            ManageBlockedTimeDiv.Visible = false;
        }

        protected void ConfirmAppointment_Click(object sender, EventArgs e)
        {

            ConfirmAppointment.Enabled = false;
            DeclineAppointment.Enabled = false;
            RescheduleAppointment.Enabled = false;


            SqlConnection db3 = new SqlConnection(SqlDataSource3.ConnectionString);
            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandType = System.Data.CommandType.Text;
            cmd3.Connection = db3;
            cmd3.CommandText = "UPDATE [MFNCalendarTable] SET Calendar_ApprovedByTrainer = @app WHERE Calendar_Id = @id";
            cmd3.Parameters.AddWithValue("@id", ClientsDrpDown.SelectedValue);
            cmd3.Parameters.AddWithValue("@app", true);

            try
            {
                db3.Open();
                cmd3.ExecuteNonQuery();
                Response.Write(@"<script language='javascript'>alert('Successfuly Accepted Client.');</script>");
                SendComfrimEmail(Uobj.FirstName, Uobj.LastName, address, date, start, end, numPeople);
            }
            catch
            {

            }
            finally
            {
                db3.Close();
            }



            SqlConnection db = new SqlConnection(SqlDataSource3.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            ClientsDrpDown.Items.Clear();
            ListItem l = new ListItem("---Select---", "", true);
            ClientsDrpDown.Items.Add(l);

            cmd.CommandText = "SELECT * FROM [MFNCalendarTable] WHERE Trainer_Id = @Tid AND Calendar_ApprovedByTrainer = @app";
            cmd.Parameters.AddWithValue("@Tid", Tobj.TrainerId);
            cmd.Parameters.AddWithValue("@app", false);

            try
            {
                db.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    string name = sdr["Calendar_EventName"].ToString();
                    string calendarId = sdr["Calendar_Id"].ToString();
                    l = new ListItem(name, calendarId, true);
                    ClientsDrpDown.Items.Add(l);
                }
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

        private string convertMilitartTimetoAMPM(string time)
        {
            string newTime = DateTime.Parse(time).ToString(@"hh\:mm\:ss tt");
            return newTime;
        }

        private void SendComfrimEmail(string first_name, string last_name, string address, string date, string startTime, string endTime, string numberOfPeople)
        {
            Uobj = (UserObject)Session["UserInfo2"];

            // String firstName = Request.Form["FName"];
            // String email = Request.Form["email"];
            string userEmail = Uobj.Email;
            DateTime localDate = DateTime.Now;
            //Response.Write("<script>alert('" + email + "')</script>");

            using (MailMessage mm = new MailMessage("MobileFitnessNetwork@gmail.com", userEmail))
            {
                mm.Subject = "Training Session Request Approved";
                string body = "Hello " + Uobj.FirstName + " " + Uobj.LastName + ",";


                body += "<br /><br />Your Session has been approved by " + Tobj.FirstName + " " +Tobj.LastName;
                body += "<br/><br/>---Session Details ---";
                body += "<br/>Client Name: " + first_name + " " + last_name;
                body += "<br/>Date: " + date;
                body += "<br/>Start Time: " + startTime;
                body += "<br/>End Time: " + endTime;
                body += "<br/>Location: " +address;
                body += "<br/>Number of People: " + numberOfPeople;
                body += "<br/><br/>Your session is ready for purchase.";
                body += "<br/><br/>Thank you";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                // the smtp host below will only work for gmail. 
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                // The function below takes in the email address account that will be used and the associated password
                NetworkCredential NetworkCred = new NetworkCredential("MobileFitnessNetwork@gmail.com", "6tfc^TFC");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }

        private void SendDeclineEmail(string first_name, string last_name, string address, string date, string startTime, string endTime, string numberOfPeople)
        {
            Uobj = (UserObject)Session["UserInfo2"];

            // String firstName = Request.Form["FName"];
            // String email = Request.Form["email"];
            string userEmail = Uobj.Email;
            DateTime localDate = DateTime.Now;
            //Response.Write("<script>alert('" + email + "')</script>");

            using (MailMessage mm = new MailMessage("MobileFitnessNetwork@gmail.com", userEmail))
            {
                mm.Subject = "Training Session Request Declined";
                string body = "Hello " + Uobj.FirstName + " " + Uobj.LastName + ",";


                body += "<br /><br />Your Session has been DECLINED by " + Tobj.FirstName + " " + Tobj.LastName;
                body += "<br/><br/>---Declined Session Details ---";
                body += "<br/>Client Name: " + first_name + " " + last_name;
                body += "<br/>Date: " + date;
                body += "<br/>Start Time: " + startTime;
                body += "<br/>End Time: " + endTime;
                body += "<br/>Location: " + address;
                body += "<br/>Number of People: " + numberOfPeople;
                body += "<br/><br/>No further action is needed on your part.";
                body += "<br/><br/>Thank you";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                // the smtp host below will only work for gmail. 
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                // The function below takes in the email address account that will be used and the associated password
                NetworkCredential NetworkCred = new NetworkCredential("MobileFitnessNetwork@gmail.com", "6tfc^TFC");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }

        private void SendRescheduleEmail(string first_name, string last_name, string address, string date, string startTime, string endTime, string numberOfPeople)
        {
            Uobj = (UserObject)Session["UserInfo2"];

            // String firstName = Request.Form["FName"];
            // String email = Request.Form["email"];
            string userEmail = Uobj.Email;
            DateTime localDate = DateTime.Now;
            //Response.Write("<script>alert('" + email + "')</script>");

            using (MailMessage mm = new MailMessage("MobileFitnessNetwork@gmail.com", userEmail))
            {
                mm.Subject = "Training Session Request Needs To Be Rescheduled";
                string body = "Hello " + Uobj.FirstName + " " + Uobj.LastName + ",";


                body += "<br /><br />A request to reschedule your session has been sent by " + Tobj.FirstName + " " + Tobj.LastName;
                body += "<br/><br/>---Reschedule Session Details ---";
                body += "<br/>Client Name: " + first_name + " " + last_name;
                body += "<br/>Date: " + date;
                body += "<br/>Start Time: " + startTime;
                body += "<br/>End Time: " + endTime;
                body += "<br/>Location: " + address;
                body += "<br/>Number of People: " + numberOfPeople;
                body += "<br/><br/>To reschedule your appoint, please head back to "+ Tobj.FirstName + " " + Tobj.LastName+ "'s profile and request a new appointment.";
                body += "<br/><br/>Thank you";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                // the smtp host below will only work for gmail. 
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                // The function below takes in the email address account that will be used and the associated password
                NetworkCredential NetworkCred = new NetworkCredential("MobileFitnessNetwork@gmail.com", "6tfc^TFC");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
    }
}