﻿using System;
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
    public partial class ClientSignup : System.Web.UI.Page
    {
        UserObject Uobj = new UserObject();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void button_Click(object sender, EventArgs e)
        {
            Adress adrs = new Adress();
            String clientAddress = Request.Form["Street"] + " " + Request.Form["City"] + " " + Request.Form["State"];
            String equipment = Request.Form["Equipment"];
            String gender = DropDownList3.SelectedValue;
            String pnumber = Request.Form["pnumber"];
            String interests = DropDownList5.SelectedValue;

            adrs.Address = clientAddress;
            adrs.GeoCode();
            // retrieve session id variable
            int userID = (int)Session["userID"];
            String dBLat = adrs.Latitude;
            String dBLng = adrs.Longitude;
            // Check to make sure all fields are filled out
            if (clientAddress.Equals("") || equipment.Equals("") || gender.Equals("0") || pnumber.Equals(""))
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "All fields required";
                Label1.Visible = true;
            }

            else
            {

                SqlConnection userLocDB = new SqlConnection(SqlDataSource2.ConnectionString);
                SqlConnection userDB = new SqlConnection(SqlDataSource1.ConnectionString);
                // sql command to insert address lat and long into the location table
                SqlCommand cmd = new SqlCommand();
                // sql command to save address, equipment, height, goals and weight in user table
                SqlCommand cmd2 = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into MFNUserLocTable ([UserLoc_Lat], [UserLoc_Long], [UserLoc_StreetAddress], [User_Id], [UserLoc_Description]) values (@lat, @lng, @home, @id, @description)";
                cmd2.CommandText = "UPDATE MFNUserTable SET User_HomeAddress = @home, User_Equipment = @equipment, User_Gender = @gender, User_Phone = @phone, User_TrainingPref = @prefs WHERE User_Id = @id";
                // add values to location table
                cmd.Parameters.AddWithValue("@lat", dBLat);
                cmd.Parameters.AddWithValue("@lng", dBLng);
                cmd.Parameters.AddWithValue("@home", clientAddress);
                cmd.Parameters.AddWithValue("@id", userID);
                cmd.Parameters.AddWithValue("@description", "Home");
                //add values to trainer table

                cmd2.Parameters.AddWithValue("@home", clientAddress);
                cmd2.Parameters.AddWithValue("@gender", gender);
                cmd2.Parameters.AddWithValue("@id", userID);
                cmd2.Parameters.AddWithValue("@phone", pnumber);
                cmd2.Parameters.AddWithValue("@prefs", interests);
                cmd2.Parameters.AddWithValue("@equipment", equipment);

                cmd.Connection = userLocDB;
                cmd2.Connection = userDB;
                // Write to the client location table
                try
                {
                    userLocDB.Open();
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
                    userLocDB.Close();

                }
                // write to the trainer table
                try
                {
                    userDB.Open();
                    cmd2.ExecuteNonQuery();
                    Response.Redirect("ClientProfile.aspx");

                }
                catch
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Visible = true;
                    Label1.Text = "Unable to write to the client database";

                }
                finally
                {
                    userLocDB.Close();

                }

            }

        }
    }
    }
    public class ClientAdress
    {
        public ClientAdress()
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
                throw new Exception("The Error Occured" + ex.Message);
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
                throw new Exception("An Error Occured" + ex.Message);
            }
        }
}
    
    
