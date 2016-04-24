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
using System.Web.UI.HtmlControls; //for dynamic divs

namespace WebApplication1
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        UserObject Uobj = new UserObject();
        TrainerObject Tobj = new TrainerObject();

        int blockedFullDaysCount = 0;
        string[] blockedFullDays = new string[1000];
        int blockedParitalDaysCount = 0;
        string[] blockedPartialDays = new string[1000];

        protected void Page_Load(object sender, EventArgs e)
        {
            //needs to be added to every page in the page load to prevent back on logout.
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            //This segment of code is to simulate entering this page with a particular trainer info
            //For now, this is a query to MFNTrainerTable to emulate this.
            //This will pull the stuff under trainer 86

            SqlConnection db = new SqlConnection(SqlDataSource1.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            cmd.CommandText = "Select * FROM [MFNTrainerTable] WHERE Trainer_Id = @id";
            cmd.Parameters.AddWithValue("@id", 86);
            db.Open();

            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                Tobj.TrainerId = Int32.Parse(sdr["Trainer_Id"].ToString());
                Tobj.ImagePath = sdr["Trainer_Image"].ToString();
                Tobj.FirstName = sdr["Trainer_FirstName"].ToString();
                Tobj.LastName = sdr["Trainer_LastName"].ToString();
                Tobj.Bio = sdr["Trainer_Bio"].ToString();
                Tobj.IndividualRate = sdr["Trainer_IndividualRate"].ToString();
                Tobj.AdditionalPersonRate = sdr["Trainer_AdditionalPersonRate"].ToString();
                Tobj.MaxNumPeople = sdr["Trainer_MaxPeople"].ToString();

            }

            Session["SelectedTrainer"] = Tobj;
            Session["TrainerId"] = Tobj.TrainerId;
            //End of emulation


            if (Session["UserInfo"] == null || Session["SelectedTrainer"] == null)
            {
                //Forces a redirect to splash page if this page is loaded without a session state.
                Response.Redirect("Default.aspx");
            }
            else
            {
                Uobj.CopyUserObject((UserObject)Session["UserInfo"]);
                Session["UserId"] = Uobj.UserId;

                Tobj.CopyTrainerObject((TrainerObject)Session["SelectedTrainer"]);

                UserNameLbl.Text = Tobj.FirstName + " " + Tobj.LastName;

                //changes default profile pic to user uploaded one
                if (Tobj.ImagePath != "")
                {
                    ProfilePic.Attributes["src"] = Tobj.ImagePath;
                }
                else
                {
                    Tobj.ImagePath = "Pictures/TrainerPic.jpg";
                }
            }

            string startDate = Calendar1.TodaysDate.ToShortDateString();

            if (Session["SelectedDate"] == null)
            {
                SelectedDateTxtBox.Text = startDate;
            }
            else
            {
                SelectedDateTxtBox.Text = (string)Session["SelectedDate"];
            }

            if (!IsPostBack)
            {
                //To populate the number of attendence dropdown list
                NumberInAttendance.Items.Clear(); //clear out existing stuff.

                for (int x = 0; x < Int32.Parse(Tobj.MaxNumPeople); x++)
                {
                    string number = (x + 1).ToString();
                    ListItem l = new ListItem(number, number, true);
                    NumberInAttendance.Items.Add(l);

                    //for dynamic div testing
                    // CreateDiv("div"+x);

                }

                LocationDrpDown.Items.Clear();

                //Add Trainer and User Loctions
                AddTrainerAndUserLocations();

            }





            SqlConnection db2 = new SqlConnection(SqlDataSource2.ConnectionString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Connection = db2;

            cmd2.CommandText = "SELECT * FROM [MFNBlockedDatesTable] WHERE Trainer_Id = @id ORDER BY BlockedDate_Date";
            cmd2.Parameters.AddWithValue("@id", Tobj.TrainerId);

            try
            {
                db2.Open();
                SqlDataReader sdr2 = cmd2.ExecuteReader();

                while (sdr2.Read())
                {
                    DateTime dt = Convert.ToDateTime(sdr2["BlockedDate_Date"].ToString());
                    string temp = dt.ToShortDateString();

                    //reads bit type from sql db
                    bool isDateFullyBlocked = sdr2.GetBoolean(sdr2.GetOrdinal("BlockedDate_IsFullDay"));

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

        }

        protected void BackToProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientProfile.aspx");
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Session["SelectedDate"] = Calendar1.SelectedDate.ToShortDateString();
            SelectedDateTxtBox.Text = Calendar1.SelectedDate.ToShortDateString();

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

        protected string convertAMPMTime(string time)
        {
            string timeValue;

            switch (time)
            {
                case "12:00 AM":
                    timeValue = "00:00:00";
                    break;
                case "12:15 AM":
                    timeValue = "00:15:00";
                    break;
                case "12: 30 AM":
                    timeValue = "00:30:00";
                    break;
                case "12:45 AM":
                    timeValue = "00:45:00";
                    break;
                case "1:00 AM":
                    timeValue = "01:00:00";
                    break;
                case "1:15 AM":
                    timeValue = "01:15:00";
                    break;
                case "1:30 AM":
                    timeValue = "01:30:00";
                    break;
                case "1:45 AM":
                    timeValue = "01:45:00";
                    break;
                case "2:00 AM":
                    timeValue = "02:00:00";
                    break;
                case "2:15 AM":
                    timeValue = "02:15:00";
                    break;
                case "2:30 AM":
                    timeValue = "02:30:00";
                    break;
                case "2:45 AM":
                    timeValue = "02:45:00";
                    break;
                case "3:00 AM":
                    timeValue = "03:00:00";
                    break;
                case "3:15 AM":
                    timeValue = "03:15:00";
                    break;
                case "3:30 AM":
                    timeValue = "03:30:00";
                    break;
                case "3:45 AM":
                    timeValue = "03:45:00";
                    break;
                case "4:00 AM":
                    timeValue = "04:00:00";
                    break;
                case "4:15 AM":
                    timeValue = "04:15:00";
                    break;
                case "4:30 AM":
                    timeValue = "04:30:00";
                    break;
                case "4:45 AM":
                    timeValue = "04:45:00";
                    break;
                case "5:00 AM":
                    timeValue = "05:00:00";
                    break;
                case "5:15 AM":
                    timeValue = "05:15:00";
                    break;
                case "5:30 AM":
                    timeValue = "05:30:00";
                    break;
                case "5:45 AM":
                    timeValue = "05:45:00";
                    break;
                case "6:00 AM":
                    timeValue = "06:00:00";
                    break;
                case "6:15 AM":
                    timeValue = "06:15:00";
                    break;
                case "6:30 AM":
                    timeValue = "06:30:00";
                    break;
                case "6:45 AM":
                    timeValue = "06:45:00";
                    break;
                case "7:00 AM":
                    timeValue = "07:00:00";
                    break;
                case "7:15 AM":
                    timeValue = "07:15:00";
                    break;
                case "7:30 AM":
                    timeValue = "07:30:00";
                    break;
                case "7:45 AM":
                    timeValue = "07:45:00";
                    break;
                case "8:00 AM":
                    timeValue = "08:00:00";
                    break;
                case "8:15 AM":
                    timeValue = "08:15:00";
                    break;
                case "8:30 AM":
                    timeValue = "08:30:00";
                    break;
                case "8:45 AM":
                    timeValue = "08:45:00";
                    break;
                case "9:00 AM":
                    timeValue = "09:00:00";
                    break;
                case "9:15 AM":
                    timeValue = "09:15:00";
                    break;
                case "9:30 AM":
                    timeValue = "09:30:00";
                    break;
                case "9:45 AM":
                    timeValue = "09:45:00";
                    break;
                case "10:00 AM":
                    timeValue = "10:00:00";
                    break;
                case "10:15 AM":
                    timeValue = "10:15:00";
                    break;
                case "10:30 AM":
                    timeValue = "10:30:00";
                    break;
                case "10:45 AM":
                    timeValue = "10:45:00";
                    break;
                case "11:00 AM":
                    timeValue = "11:00:00";
                    break;
                case "11:15 AM":
                    timeValue = "11:15:00";
                    break;
                case "11:30 AM":
                    timeValue = "11:30:00";
                    break;
                case "11:45 AM":
                    timeValue = "11:45:00";
                    break;
                case "12:00 PM":
                    timeValue = "12:00:00";
                    break;
                case "12:15 PM":
                    timeValue = "12:15:00";
                    break;
                case "12:30 PM":
                    timeValue = "12:30:00";
                    break;
                case "12:45 PM":
                    timeValue = "12:45:00";
                    break;
                case "1:00 PM":
                    timeValue = "13:00:00";
                    break;
                case "1:15 PM":
                    timeValue = "13:15:00";
                    break;
                case "1:30 PM":
                    timeValue = "13:30:00";
                    break;
                case "1:45 PM":
                    timeValue = "13:45:00";
                    break;
                case "2:00 PM":
                    timeValue = "14:00:00";
                    break;
                case "2:15 PM":
                    timeValue = "14:15:00";
                    break;
                case "2:30 PM":
                    timeValue = "14:30:00";
                    break;
                case "2:45 PM":
                    timeValue = "14:45:00";
                    break;
                case "3:00 PM":
                    timeValue = "15:00:00";
                    break;
                case "3:15 PM":
                    timeValue = "15:15:00";
                    break;
                case "3:30 PM":
                    timeValue = "15:30:00";
                    break;
                case "3:45 PM":
                    timeValue = "15:45:00";
                    break;
                case "4:00 PM":
                    timeValue = "16:00:00";
                    break;
                case "4:15 PM":
                    timeValue = "16:15:00";
                    break;
                case "4:30 PM":
                    timeValue = "16:30:00";
                    break;
                case "4:45 PM":
                    timeValue = "16:45:00";
                    break;
                case "5:00 PM":
                    timeValue = "17:00:00";
                    break;
                case "5:15 PM":
                    timeValue = "17:15:00";
                    break;
                case "5:30 PM":
                    timeValue = "17:30:00";
                    break;
                case "5:45 PM":
                    timeValue = "17:45:00";
                    break;
                case "6:00 PM":
                    timeValue = "18:00:00";
                    break;
                case "6:15 PM":
                    timeValue = "18:15:00";
                    break;
                case "6:30 PM":
                    timeValue = "18:30:00";
                    break;
                case "6:45 PM":
                    timeValue = "18:45:00";
                    break;
                case "7:00 PM":
                    timeValue = "19:00:00";
                    break;
                case "7:15 PM":
                    timeValue = "19:15:00";
                    break;
                case "7:30 PM":
                    timeValue = "19:30:00";
                    break;
                case "7:45 PM":
                    timeValue = "19:45:00";
                    break;
                case "8:00 PM":
                    timeValue = "20:00:00";
                    break;
                case "8:15 PM":
                    timeValue = "20:15:00";
                    break;
                case "8:30 PM":
                    timeValue = "20:30:00";
                    break;
                case "8:45 PM":
                    timeValue = "20:45:00";
                    break;
                case "9:00 PM":
                    timeValue = "21:00:00";
                    break;
                case "9:15 PM":
                    timeValue = "21:15:00";
                    break;
                case "9:30 PM":
                    timeValue = "21:30:00";
                    break;
                case "9:45 PM":
                    timeValue = "21:45:00";
                    break;
                case "10:00 PM":
                    timeValue = "22:00:00";
                    break;
                case "10:15 PM":
                    timeValue = "22:15:00";
                    break;
                case "10:30 PM":
                    timeValue = "22:30:00";
                    break;
                case "10:45 PM":
                    timeValue = "22:45:00";
                    break;
                case "11:00 PM":
                    timeValue = "23:00:00";
                    break;
                case "11:15 PM":
                    timeValue = "23:15:00";
                    break;
                case "11:30 PM":
                    timeValue = "23:30:00";
                    break;
                case "11:45 PM":
                    timeValue = "20:45:00";
                    break;
                default:
                    timeValue = "00:00:00";
                    break;
            }


            return timeValue;
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

        protected void RequestAppointmentBtn_Click(object sender, EventArgs e)
        {
            string startTime;
            string endTime;
            string SlectedNumberOfPeople = NumberInAttendance.Text;
            string address = LocationDrpDown.SelectedValue;
            int count = 0;

            //This needs to validate and append information to the db.
            if (GetValidTime(StartTimeDrpList.Text, EndTimeDrpList.Text) && address != "")
            {
                startTime = convertAMPMTime(StartTimeDrpList.Text);
                endTime = convertAMPMTime(EndTimeDrpList.Text);

                //This section checks to see if the Client attempts to add a session on a date that is fully  Blocked.
                //
                SqlConnection db3 = new SqlConnection(SqlDataSource5.ConnectionString);
                SqlCommand cmd3 = new SqlCommand();
                cmd3.CommandType = System.Data.CommandType.Text;
                cmd3.Connection = db3;
                cmd3.CommandText = "Select COUNT(*) FROM [MFNBlockedDatesTable] WHERE Trainer_Id = @Tid AND BlockedDate_Date = @date AND BlockedDate_IsFullDay = @fullDay";
                cmd3.Parameters.AddWithValue("@Tid", Tobj.TrainerId);
                cmd3.Parameters.AddWithValue("@date", SelectedDateTxtBox.Text);
                cmd3.Parameters.AddWithValue("@fullDay", true);

                try
                {
                    db3.Open();
                    count = (int)cmd3.ExecuteScalar();
                }
                catch
                {
                    count = -1;
                }
                finally
                {
                    db3.Close();
                }

                if(count > 0)
                {
                    Response.Write(@"<script language='javascript'>alert('" + UserNameLbl.Text + " has marked "+ SelectedDateTxtBox.Text + " as unavaliable!');</script>");
                }
                else if(count == -1)
                {
                    Response.Write(@"<script language='javascript'>alert('Error connecting to Database!');</script>");
                }
                else
                {

                    if(isTimeFreeOnPartiallyBlockedDate(startTime, endTime))
                    {
                        //This section checks to see if the Client has already subimmted a request for a session
                        //on the same date at the same time.

                        SqlConnection db = new SqlConnection(SqlDataSource3.ConnectionString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = db;

                        cmd.CommandText = "Select COUNT(*) FROM [MFNCalendarTable] WHERE Trainer_Id = @Tid AND User_Id = @Uid AND Calendar_Date = @date AND Calendar_StartTime = @startTime AND Calendar_EndTime = @endTime";
                        cmd.Parameters.AddWithValue("@Tid", Tobj.TrainerId);
                        cmd.Parameters.AddWithValue("@Uid", Uobj.UserId);
                        cmd.Parameters.AddWithValue("@date", SelectedDateTxtBox.Text);
                        cmd.Parameters.AddWithValue("@startTime", startTime);
                        cmd.Parameters.AddWithValue("@endTime", endTime);

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



                        if (count > 0)
                        {
                            Response.Write(@"<script language='javascript'>alert(' You have already made a reqest for a session on " + SelectedDateTxtBox.Text + " from " + StartTimeDrpList.Text + " to " + EndTimeDrpList.Text + " with" + UserNameLbl.Text + "!');</script>");
                        }
                        else if (count == -1)
                        {
                            Response.Write(@"<script language='javascript'>alert('Error connecting to Database!');</script>");
                        }
                        else
                        {

                            SqlConnection db2 = new SqlConnection(SqlDataSource3.ConnectionString);
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandType = System.Data.CommandType.Text;
                            cmd2.Connection = db2;

                            cmd2.CommandText = "INSERT INTO [MFNCalendarTable] (Trainer_Id, User_Id, Calendar_Date, Calendar_EventName, Calendar_StartTime, Calendar_EndTime, Calendar_NumberOfClients, Calendar_Location) VALUES (@Tid, @Uid, @date, @event, @startTime, @endTime, @number, @loc)";
                            cmd2.Parameters.AddWithValue("@Tid", Tobj.TrainerId);
                            cmd2.Parameters.AddWithValue("@Uid", Uobj.UserId);
                            cmd2.Parameters.AddWithValue("@date", SelectedDateTxtBox.Text);
                            cmd2.Parameters.AddWithValue("@event", EventSummaryTxtBox.Text);
                            cmd2.Parameters.AddWithValue("@startTime", startTime);
                            cmd2.Parameters.AddWithValue("@endTime", endTime);
                            cmd2.Parameters.AddWithValue("@number", SlectedNumberOfPeople);
                            cmd2.Parameters.AddWithValue("@loc", address);

                            try
                            {
                                db2.Open();
                                cmd2.ExecuteNonQuery();

                                //success message
                                if (SlectedNumberOfPeople == "1")
                                {
                                    Response.Write(@"<script language='javascript'>alert('Reqest for a session on " + SelectedDateTxtBox.Text + " from " + StartTimeDrpList.Text + " to " + EndTimeDrpList.Text + " for " + SlectedNumberOfPeople + " person has been sent to " + UserNameLbl.Text + "!');</script>");
                                }
                                else
                                {
                                    Response.Write(@"<script language='javascript'>alert('Reqest for a session on " + SelectedDateTxtBox.Text + " from " + StartTimeDrpList.Text + " to " + EndTimeDrpList.Text + " for " + SlectedNumberOfPeople + " people has been sent to " + UserNameLbl.Text + "!');</script>");
                                }
                            }
                            catch
                            {
                                Response.Write(@"<script language='javascript'>alert('Error Writing into Database!');</script>");
                            }
                            finally
                            {
                                db2.Close();
                            }
                        }

                    }
                    else
                    {
                        Response.Write(@"<script language='javascript'>alert('" + UserNameLbl.Text + " has marked this time slot as unavaliable!');</script>");
                    }

                }

            }
            else
            {
                if(address == "")
                {
                    Response.Write(@"<script language='javascript'>alert('Please Select a Location.');</script>");
                }
                else
                {
                    Response.Write(@"<script language='javascript'>alert('Invalid Time Range.');</script>");
                }
            }

          //Response.Redirect("ClientScheduler.aspx");
        }




        private void CreateDiv(string divId)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("id", divId);
            div.Attributes.Add("runat", "server");
            div.Attributes.Add("class", "row centered-form");
            //this line is an absolute nightmare,but it should work!
            div.InnerHtml = "<div class=\"row centered-form\" runat=\"server\"><div class=\"col-xs-12 col-sm-8 col-md-4 col-sm-offset-2 col-md-offset-4\"><div class=\"panel panel-default\"><div class=\"panel-heading\"><h3 class=\"panel-title\">Approved Session</h3></div><div class=\"panel-body\"><img src=\""+ Tobj.ImagePath+ "\" class=\"UserPicture img-circle img - responsive\" style=\"width: 50px; height: 50px; \">" + Tobj.FirstName + " " + Tobj.LastName + " has accepted your session! <a href=\"CheckOut.aspx\">Click Here to Pay!</a></div></div></div></div>"; //not completed need button event to launch session!
            YourComfirmedSessions.Controls.Add(div); 
        }

        private void AddTrainerAndUserLocations()
        {
            ListItem l = new ListItem("---User Addresses---", "", true);
            LocationDrpDown.Items.Add(l);

            SqlConnection db = new SqlConnection(SqlDataSource4.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            cmd.CommandText = "Select * FROM [MFNUserLocTable] WHERE User_Id = @id";
            cmd.Parameters.AddWithValue("@id", ((int)Session["UserId"]).ToString());
            db.Open();

            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                string address = sdr["UserLoc_StreetAddress"].ToString();

                l = new ListItem(address, address, true);
                LocationDrpDown.Items.Add(l);
            }
            db.Close();


            SqlConnection db2 = new SqlConnection(SqlDataSource5.ConnectionString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Connection = db2;

            l = new ListItem("---Trainer Addresses---", "", true);
            LocationDrpDown.Items.Add(l);


            cmd2.CommandText = "Select * FROM [MFNTrainerLocTable] WHERE Trainer_Id = @id";
            cmd2.Parameters.AddWithValue("@id", (int)Session["TrainerId"]);
            db2.Open();

            SqlDataReader sdr2 = cmd2.ExecuteReader();

            while (sdr2.Read())
            {
                string address = sdr2["TrainerLoc_StreetAddress"].ToString();

                l = new ListItem(address, address, true);
                LocationDrpDown.Items.Add(l);
            }

            db2.Close();

        }

        
        private bool isTimeFreeOnPartiallyBlockedDate(string ClientStrartTime, string ClientEndTime)
        {
            //returns false is time is blocked, true if time is avaliable

            //This section checks to see if the Client attempts to add a session on a date that is partially blocked.
            /*

            read from db for times.

            Four broad cases that need to be blocked
            case 1: starts before blocked time, but ends within blocked time ClientStart < TrainerStart < ClientEnd < TrainerEnd
            case 2: starts during blocked time, but ends outside blocked time TrainerStart < ClientStart < TrainerEnd < ClientEnd
            case 3: Within blocked time TrainerStart < ClientStart < ClientEnd < TrainerEnd
            case 4: Overlaps blocked time Client Start < TrainerStart < TrainerEnd < Client End

            simplified
            case 1: ClientEnd > TrainerStart and ClientEnd < Trainer End
            case 2: ClientStart > TrainerStart and ClientStart < TrainerEnd
            case 3: can be caught with case1 and case2 test
            case 4: ClientStart < TrainerStart and ClientEnd > TrainerEnd

            These cases are okay
            Client Starts and Ends before blocked time ClientStart < ClientEnd < TrainerStop < TrainerEnd
            Client Starts and Ends after blocked time TrainerStop < TrainerEnd < ClientStart < ClientEnd
            */

            int count = 0;
            string TrainerStartTime = "";
            string TrainerEndTime = "";

            SqlConnection db = new SqlConnection(SqlDataSource3.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = db;

            cmd.CommandText = "Select * FROM [MFNBlockedDateTable] WHERE Trainer_Id = @id AND BlockedDate_Date = @date";
            cmd.Parameters.AddWithValue("@id", Tobj.TrainerId);
            cmd.Parameters.AddWithValue("@date", SelectedDateTxtBox.Text);

            try
            {
                db.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    TrainerStartTime = sdr["BlockedDate_StartTime"].ToString();
                    TrainerEndTime = sdr["BlockedDate_EndTime"].ToString();
                }

            }
            catch
            {
                count = -1;
            }
            finally
            {
                db.Close();
            }

            if(count == -1)
            {
                return false;
            }
            else 
            {
                int clientStart = getTimeValue(ClientStrartTime);
                int clientEnd = getTimeValue(ClientEndTime);
                int trainerStart = getTimeValue(TrainerStartTime);
                int trainerEnd = getTimeValue(TrainerEndTime);

                if(clientEnd > trainerStart && clientEnd < trainerEnd)
                {
                    return false;
                }
                else if(clientStart > trainerStart && clientStart < trainerEnd)
                {
                    return false;
                }
                else if(clientStart < trainerStart && clientEnd > trainerEnd)
                {
                    return false;
                }
                else
                {
                    return true;
                }


            }



        }

        int getTimeValue(string time)
        {
            int value;

            switch (time)
            {
                case "12:00 AM":
                    value = 1;
                    break;
                case "12:15 AM":
                    value = 2;
                    break;
                case "12: 30 AM":
                    value = 3;
                    break;
                case "12:45 AM":
                    value = 4;
                    break;
                case "1:00 AM":
                    value = 5;
                    break;
                case "1:15 AM":
                    value = 6;
                    break;
                case "1:30 AM":
                    value = 7;
                    break;
                case "1:45 AM":
                    value = 8;
                    break;
                case "2:00 AM":
                    value = 9;
                    break;
                case "2:15 AM":
                    value = 10;
                    break;
                case "2:30 AM":
                    value = 11;
                    break;
                case "2:45 AM":
                    value = 12;
                    break;
                case "3:00 AM":
                    value = 13;
                    break;
                case "3:15 AM":
                    value = 14;
                    break;
                case "3:30 AM":
                    value = 15;
                    break;
                case "3:45 AM":
                    value = 16;
                    break;
                case "4:00 AM":
                    value = 17;
                    break;
                case "4:15 AM":
                    value = 18;
                    break;
                case "4:30 AM":
                    value = 19;
                    break;
                case "4:45 AM":
                    value = 20;
                    break;
                case "5:00 AM":
                    value = 21;
                    break;
                case "5:15 AM":
                    value = 22;
                    break;
                case "5:30 AM":
                    value = 23;
                    break;
                case "5:45 AM":
                    value = 24;
                    break;
                case "6:00 AM":
                    value = 26;
                    break;
                case "6:15 AM":
                    value = 27;
                    break;
                case "6:30 AM":
                    value = 28;
                    break;
                case "6:45 AM":
                    value = 29;
                    break;
                case "7:00 AM":
                    value = 30;
                    break;
                case "7:15 AM":
                    value = 31;
                    break;
                case "7:30 AM":
                    value = 32;
                    break;
                case "7:45 AM":
                    value = 33;
                    break;
                case "8:00 AM":
                    value = 34;
                    break;
                case "8:15 AM":
                    value = 35;
                    break;
                case "8:30 AM":
                    value = 36;
                    break;
                case "8:45 AM":
                    value = 37;
                    break;
                case "9:00 AM":
                    value = 38;
                    break;
                case "9:15 AM":
                    value = 39;
                    break;
                case "9:30 AM":
                    value = 40;
                    break;
                case "9:45 AM":
                    value = 41;
                    break;
                case "10:00 AM":
                    value = 42;
                    break;
                case "10:15 AM":
                    value = 43;
                    break;
                case "10:30 AM":
                    value = 44;
                    break;
                case "10:45 AM":
                    value = 45;
                    break;
                case "11:00 AM":
                    value = 46;
                    break;
                case "11:15 AM":
                    value = 47;
                    break;
                case "11:30 AM":
                    value = 48;
                    break;
                case "11:45 AM":
                    value = 49;
                    break;
                case "12:00 PM":
                    value = 50;
                    break;
                case "12:15 PM":
                    value = 51;
                    break;
                case "12:30 PM":
                    value = 52;
                    break;
                case "12:45 PM":
                    value = 53;
                    break;
                case "1:00 PM":
                    value = 54;
                    break;
                case "1:15 PM":
                    value = 55;
                    break;
                case "1:30 PM":
                    value = 56;
                    break;
                case "1:45 PM":
                    value = 57;
                    break;
                case "2:00 PM":
                    value = 58;
                    break;
                case "2:15 PM":
                    value = 59;
                    break;
                case "2:30 PM":
                    value = 60;
                    break;
                case "2:45 PM":
                    value = 61;
                    break;
                case "3:00 PM":
                    value = 62;
                    break;
                case "3:15 PM":
                    value = 63;
                    break;
                case "3:30 PM":
                    value = 64;
                    break;
                case "3:45 PM":
                    value = 65;
                    break;
                case "4:00 PM":
                    value = 66;
                    break;
                case "4:15 PM":
                    value = 67;
                    break;
                case "4:30 PM":
                    value = 68;
                    break;
                case "4:45 PM":
                    value = 69;
                    break;
                case "5:00 PM":
                    value = 70;
                    break;
                case "5:15 PM":
                    value = 71;
                    break;
                case "5:30 PM":
                    value = 72;
                    break;
                case "5:45 PM":
                    value = 73;
                    break;
                case "6:00 PM":
                    value = 74;
                    break;
                case "6:15 PM":
                    value = 75;
                    break;
                case "6:30 PM":
                    value = 76;
                    break;
                case "6:45 PM":
                    value = 77;
                    break;
                case "7:00 PM":
                    value = 78;
                    break;
                case "7:15 PM":
                    value = 79;
                    break;
                case "7:30 PM":
                    value = 80;
                    break;
                case "7:45 PM":
                    value = 81;
                    break;
                case "8:00 PM":
                    value = 82;
                    break;
                case "8:15 PM":
                    value = 83;
                    break;
                case "8:30 PM":
                    value = 84;
                    break;
                case "8:45 PM":
                    value = 85;
                    break;
                case "9:00 PM":
                    value = 86;
                    break;
                case "9:15 PM":
                    value = 87;
                    break;
                case "9:30 PM":
                    value = 88;
                    break;
                case "9:45 PM":
                    value = 89;
                    break;
                case "10:00 PM":
                    value = 90;
                    break;
                case "10:15 PM":
                    value = 91;
                    break;
                case "10:30 PM":
                    value = 92;
                    break;
                case "10:45 PM":
                    value = 93;
                    break;
                case "11:00 PM":
                    value = 94;
                    break;
                case "11:15 PM":
                    value = 95;
                    break;
                case "11:30 PM":
                    value = 96;
                    break;
                case "11:45 PM":
                    value = 97;
                    break;
                default:
                    value = 9999;
                    break;
            }

            return value;
        }

    }
}