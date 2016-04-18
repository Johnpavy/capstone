using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Net;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class ClientAccountaspx : System.Web.UI.Page
    {
        TrainerObject Tobj = new TrainerObject();
        protected void Page_Load(object sender, EventArgs e)
        {
           /* if(!IsPostBack)
            {
                if (Session["TrainerInfo"] == null)
                {
                    //Forces a redirect to splash page if this page is loaded without a session state.
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    Tobj = (TrainerObject)Session["TrainerInfo"];
               
                }
            }
        */

        }

        protected void button_Click(object sender, EventArgs e)
        {
            
            Adress adrs = new Adress();
            String trainerAddress = Request.Form["Street"] + " " + Request.Form["City"] + " " + Request.Form["State"];
            String bio = Request.Form["bio"];
            String gender = DropDownList3.SelectedValue;
            String pnumber = Request.Form["pnumber"];
            String height = DropDownList1.SelectedValue + "'" + DropDownList2.SelectedValue + "\"";
            String weight = Request.Form["weight"];
            String specialty = DropDownList5.SelectedValue;
            adrs.Address = trainerAddress;
            adrs.GeoCode();
            int trainerID = (int)Session["trainerID"];
            String dBLat = adrs.Latitude;
            String dBLng = adrs.Longitude;
            // Check to make sure all fields are filled out
            if(trainerAddress.Equals("") || bio.Equals("") || gender.Equals("0") || pnumber.Equals("") || height.Equals("0'0\"") || specialty.Equals("0"))
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "All fields required";
                Label1.Visible = true;
            }

            else
            {
                // add info to session object
                Tobj.CopyTrainerObject((TrainerObject)Session["TrainerInfo"]);
                Tobj.Speciality = specialty;
                Tobj.Bio = bio;
                Session["TrainerInfo"] = Tobj;
                SqlConnection trainerLocDB = new SqlConnection(SqlDataSource1.ConnectionString);
                SqlConnection trainerDB = new SqlConnection(SqlDataSource2.ConnectionString);
                // sql command to insert address lat and long into the location table
                SqlCommand cmd = new SqlCommand();
                // sql command to save address, bio, height and weight in trainer table
                SqlCommand cmd2 = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into MFNTrainerLocTable ([TrainerLoc_Lat], [TrainerLoc_Long], [TrainerLoc_StreetAddress], [Trainer_Id], [TrainerLoc_Description]) values (@lat, @lng, @home, @id, @description)";
                cmd2.CommandText = "UPDATE MFNTrainerTable SET Trainer_HomeAddress = @home, Trainer_Bio = @bio, Trainer_Gender = @gender, Trainer_Phone = @phone, Trainer_Specialty = @specialty, Trainer_Weight = @weight, Trainer_Height = @height WHERE Trainer_Id = @id"; 
                // add values to location table
                cmd.Parameters.AddWithValue("@lat", dBLat);
                cmd.Parameters.AddWithValue("@lng", dBLng);
                cmd.Parameters.AddWithValue("@home", trainerAddress);
                cmd.Parameters.AddWithValue("@id", trainerID);
                cmd.Parameters.AddWithValue("@description", "Home");
                //add values to trainer table

                cmd2.Parameters.AddWithValue("@home", trainerAddress);
                cmd2.Parameters.AddWithValue("@bio", bio);
                cmd2.Parameters.AddWithValue("@gender", gender);
                cmd2.Parameters.AddWithValue("@id", trainerID);
                cmd2.Parameters.AddWithValue("@phone", pnumber);
                cmd2.Parameters.AddWithValue("@specialty", specialty);
                cmd2.Parameters.AddWithValue("@weight", weight);
                cmd2.Parameters.AddWithValue("@height", height);


                cmd.Connection = trainerLocDB;
                cmd2.Connection = trainerDB;
                // Write to the trainer location table
                try
                {
                    trainerLocDB.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Visible = true;
                    Label1.Text = "Unable to write to the location database";

                }
                finally
                {
                    trainerLocDB.Close();
                    
                }
                // write to the trainer table
                try
                {
                    trainerDB.Open()
;                   cmd2.ExecuteNonQuery();
                    Response.Redirect("Webform2.aspx");

                }
                catch
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Visible = true;
                    Label1.Text = "Unable to write to the trainer database";

                }
                finally
                {
                    trainerLocDB.Close();

                }

            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    // Credit to  http://www.codeproject.com/Tips/650139/Finding-Co-ordinates-of-an-Address-in-ASP-NET-Csha for lat and long retrieval
    public class Adress
    {
        public Adress()
        { 
        }
        //Properties
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }

        //The Geocoding here i.e getting the latt/long of address
        public void GeoCode()
        {
            //to Read the Stream
            StreamReader sr = null;
            
            //The Google Maps API Either return JSON or XML. We are using XML Here
            //Saving the url of the Google API 
            string url = String.Format("http://maps.googleapis.com/maps/api/geocode/xml?address=" + 
            this.Address + "&sensor=false");
            
            //to Send the request to Web Client 
            WebClient wc = new WebClient();
            try
            {
                sr = new StreamReader(wc.OpenRead(url));
            }
            catch (Exception ex)
            {
                throw new Exception("The Error Occured"+ ex.Message);
            }

            try
            {                
                XmlTextReader xmlReader = new XmlTextReader(sr);
                bool latread = false;
                bool longread = false;

                while (xmlReader.Read())
                {
                    xmlReader.MoveToElement();
                        switch (xmlReader.Name)
                        {
                            case "lat":

                                if (!latread)
                                {
                                    xmlReader.Read();
                                    this.Latitude = xmlReader.Value.ToString();
                                    latread = true;
                                    
                                }
                                break;
                            case "lng":
                                if (!longread)
                                {
                                    xmlReader.Read();
                                    this.Longitude = xmlReader.Value.ToString();
                                    longread = true;
                                }
                            
                                break;
                        }   
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An Error Occured"+ ex.Message);
            }
        }
    }
}