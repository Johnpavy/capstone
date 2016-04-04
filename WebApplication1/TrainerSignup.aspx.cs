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
    public partial class ClientAccountaspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        protected void button_Click(object sender, EventArgs e)
        {
            
            Adress adrs = new Adress();
            String street = Request.Form["Street"];
            String trainerAddress = Request.Form["Street"] + " " + Request.Form["City"] + " " + Request.Form["State"];
            adrs.Address = street;
            adrs.Address = trainerAddress;
            adrs.GeoCode();
            String dBLat = adrs.Latitude;
            String dBLng = adrs.Longitude;
            Session["Email"] = Request.Form["Email"];
            SqlConnection trainerLocDB = new SqlConnection(SqlDataSource1.ConnectionString);
            trainerLocDB.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into MFNTrainerLocTable ([TrainerLoc_Lat], [TrainerLoc_Long]) values (@lat, @lng)";
            // add values to sql table
            cmd.Parameters.AddWithValue("@lat", dBLat);
            cmd.Parameters.AddWithValue("@lng", dBLng);
            cmd.Connection = trainerLocDB;
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch
            {

                Label1.Visible = true;
                Label1.Text = "Unable to write to the database";
                
            }
            finally
            {
                trainerLocDB.Close();
                Response.Redirect("Webform2.aspx");
            }
            
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