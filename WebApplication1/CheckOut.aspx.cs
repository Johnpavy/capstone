using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Api;
using PayPal.Sample;
using System.Net.Mail;
using System.Net;

namespace WebApplication1
{
    public partial class CheckOut : System.Web.UI.Page
    {
        TransactionObject TranObj = new TransactionObject(); //create paypal transaction object
        TrainerObject Tobj = new TrainerObject(); //create trainer object
        UserObject Uobj = new UserObject(); //create user object
        String calendarID; //string for the calendar ID
        String transactionString; //string for the transaction string being pulled from the URL
        string[] transactionInfo = new string[3]; //string array in order to parse the pulled string from the URL
        string SampleTotal; //sample total is the total price after calculation

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("Expires", "0");

            calendarID = !string.IsNullOrEmpty(Request.QueryString["CalendarID"]) ? Request.QueryString["CalendarID"] : Guid.Empty.ToString(); //get the calendar ID from the URL
            transactionString = !string.IsNullOrEmpty(Request.QueryString["SessionTransactionInfo"]) ? Request.QueryString["SessionTransactionInfo"] : Guid.Empty.ToString(); 
            //Get the transaction info from the URL

            Session["CalendarID"] = calendarID; //create calendar Session based on calendar ID

            if (calendarID == null || calendarID == "" || transactionString == null || transactionString == "") //check to ensure something was passed when going to pay
            {
                //Forces a redirect to splash page if this page is loaded without a session state.
                Response.Redirect("Default.aspx");
            }
            else
            {
                //split the trainsaction string to parse pricing info
                transactionInfo = transactionString.Split('|'); //split function splits at the | symbol and end of file
                SampleTotal = calcPriceOfSession(transactionInfo); //calculate the sample total
                Uobj = (UserObject)Session["UserInfo"]; //get user info

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            // See [Configuration.cs](/Source/Configuration.html) to know more about APIContext.
            var apiContext = Configuration.GetAPIContext();

            // A transaction defines the contract of a payment - what is the payment for and who is fulfilling it. 
            var transaction = new Transaction()
            {
                amount = new Amount()
                {
                    currency = "USD", //set currency
                    total = SampleTotal, //sample total is sent to paypal as the total
                    details = new Details()
                    {
                        shipping = "0.00", //not using a shipping cost
                        subtotal = SampleTotal, //subtotal is precalculated
                        tax = "0.00" //not using a tax cost
                    }
                },

                description = "This is the payment transaction description.", //the user does not see this
                item_list = new ItemList()
                {
                    items = new List<Item>()
                    {
                        new Item()
                        {
                            name = "Session", //not relevant
                            currency = "USD",
                            price = SampleTotal, //price, total, and subtotal must all be equal
                            quantity = "1", //number of times charge, not relevant
                            sku = "sku" //not setting a sku
                        }
                    },
                    //shipping address is used to verify credit card info
                    shipping_address = new ShippingAddress
                    {
                        city = city.Text, //get the user input city
                        country_code = country_code.Text, //get the user input country code, only US for now
                        line1 = address.Text, //get the users address
                        postal_code = postal_code.Text, //get the users postal code
                        state = state.Text, //get the users state, only Texas for now
                        recipient_name = first_name.Text + " " + last_name.Text //get the users first and last name, combine them
                    }
                },
                invoice_number = Common.GetRandomInvoiceNumber()
            };

            // A resource representing a Payer that funds a payment.
            var payer = new Payer()
            {
                payment_method = "credit_card",
                funding_instruments = new List<FundingInstrument>()
                {
                    new FundingInstrument()
                    {
                        credit_card = new CreditCard()
                        {
                            billing_address = new Address()
                            {
                                city = city.Text, //get the user input city
                                country_code = country_code.Text, //get the user input country code, only US for now
                                line1 = address.Text, //get the users address
                                postal_code = postal_code.Text,  //get the users postal code
                                state = state.Text //get the users state, only Texas for now
                            },
                            cvv2 = cvv2.Text, //the cvv is used to verify the credit card number
                            expire_month = Int32.Parse(ex_month.Text), //the expiration month of the credit card
                            expire_year = Int32.Parse(ex_year.Text), // the expiration year of the credit card
                            first_name = first_name.Text, // the users first name
                            last_name = last_name.Text, // the users last name
                            number = card_number.Text, // the credit card number, using a fake number for testing
                            type = card_type.Text //credit card type (visa, mastercard, etc.), using viso for testing
                        }
                    }
                },
                payer_info = new PayerInfo
                {
                    //Not sure what this does
                    email = "test@email.com"
                }
            };

            // A Payment resource; create one using the above types and intent as `sale` or `authorize`
            var payment = new Payment()
            {
                intent = "sale",
                payer = payer, //payer resource
                transactions = new List<Transaction>() { transaction } //trainsaction details
            };

            // ^ Ignore workflow code segment
            #region Track Workflow
            // this.flow.AddNewRequest("Create credit card payment", payment);
            #endregion

            // Create a payment using a valid APIContext
            try
            {
                var createdPayment = payment.Create(apiContext);

                //add session payment info
                Session["first_name"] = first_name.Text + " " + last_name.Text;
                Session["address"] = address.Text;
                Session["city"] = city.Text;
                Session["postal_code"] = postal_code.Text;
                Session["card_type"] = card_type.Text;
                Session["card_number"] = card_number.Text;
                Session["currency"] = "USD";
                Session["total"] = SampleTotal;

                //send an email confirmation
                SendConfirmationEmail(first_name.Text, last_name.Text, address.Text, city.Text, postal_code.Text, card_type.Text, SampleTotal);
                //redirect user to the payment confirmation page
                Response.Redirect("PaymentConfirmation.aspx");

            }
            catch
            {
                //catch inalid information, and display an error
                Response.Write("<script>alert('" + "Invalid Infromation, please try again." + "')</script>");
            }

            // ^ Ignore workflow code segment
            //#region Track Workflow
            //  this.flow.RecordResponse(createdPayment);
            //#

            // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        }

        //send payment confirmation email
        private void SendConfirmationEmail(string first_name, string last_name, string address, string city, string postal_code, string card_type, string SampleTotal)
        {
            // String firstName = Request.Form["FName"];
            // String email = Request.Form["email"];
            string activationCode = Guid.NewGuid().ToString();
            string userEmail = Uobj.Email; //get the user Email
            string sessionDetails = (string)(Session["session_details"]); //get the training session details
            DateTime localDate = DateTime.Now; //get the date and time of the payment creation
            //Response.Write("<script>alert('" + email + "')</script>");

            using (MailMessage mm = new MailMessage("MobileFitnessNetwork@gmail.com", userEmail)) //send user email with web app gmail account
            {
                //create the subject of the email
                mm.Subject = "Training Session Payment Confirmation";
                //create the body of the email
                string body = "Hello " + first_name + ",";

                //add details to the body of the email
                body += "<br /><br />Your approved training session has been payed for.";
                body += "<br /><br /> ---Payment Details---";
                body += "<br /><br />" + "Transaction Date/Time: " + localDate + "<br />" + "Name: " + first_name + " " + last_name + "<br />" + "Address: " + address + "<br />" + "City: " + city + "<br />" + "Zip: " + postal_code + "<br />" +
                    "Card Type: " + card_type + "<br />" + "Total: " + SampleTotal;
                body += "<br /><br /> ---Session Details--- <br /><br />";
                body += sessionDetails;


                body += "<br /><br />Thank you";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(); //use smtp protocol
                // the smtp host below will only work for gmail. 
                smtp.Host = "smtp.gmail.com"; //use googles smtp server
                smtp.EnableSsl = true; //use SSL for security
                // The function below takes in the email address account that will be used and the associated password
                NetworkCredential NetworkCred = new NetworkCredential("MobileFitnessNetwork@gmail.com", "6tfc^TFC"); //credentials
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }

        string calcPriceOfSession(string[] info)
        {
            double standardRate = Double.Parse(info[0]); //get the first parsed portion of the payment info, this is the session rate
            double additonalPersonRate = Double.Parse(info[1]); //get the second parsed portion, this is the additional person rate
            int numberOfPeople = Int32.Parse(info[2]); //get the last, this is the number of people attending

            baseRateLbl.Text = "Base Rate: $" + standardRate.ToString("0.00"); //display the base rate value to the user
            NumberAttendingLbl.Text = "Number of People Attending: " + numberOfPeople.ToString(); //display the number of people attending to the user
            AdditionalRateLbl.Text = "Cost From Additional Persons: $" + (additonalPersonRate * (numberOfPeople - 1)).ToString("0.00"); //display the additional rate to the user

            double subTotal;

            subTotal = standardRate + (additonalPersonRate * (numberOfPeople - 1)); //get the sub total, the base rate + (the additional persons rate * (number of people attending - 1))
            subTotal.ToString("0.00"); //make subtotal a string, this is how paypal accepts values
            SubTotalLbl.Text = "Subtotal: $" + subTotal.ToString("0.00"); //display the subtotal to the user
            ServiceCostLbl.Text = "Service Fee: $" + (subTotal * 0.05).ToString("0.00"); //calculate a service fee. 5% of the subtotal
            double finalTotal = subTotal * 1.05; //add the service fee to the subtotal to get the final total
            TotalLbl.Text = "Final Total: $" + finalTotal.ToString("0.00"); //display the total

            //create session to be passed to the payment confirmation page
            Session["standard_rate"] = standardRate.ToString("0.00"); 
            Session["additional_person"] = (additonalPersonRate * (numberOfPeople - 1)).ToString("0.00");
            Session["number_people"] = numberOfPeople.ToString();
            Session["service_fee"] = (subTotal * 0.05).ToString("0.00");
            Session["sub_total"] = subTotal.ToString("0.00");

            //return the total to be sent with credit card info to paypal
            return finalTotal.ToString("0.00");

        }

    }
}