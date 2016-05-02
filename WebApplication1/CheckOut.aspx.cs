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
        TransactionObject TranObj = new TransactionObject();
        TrainerObject Tobj = new TrainerObject();
        UserObject Uobj = new UserObject();
        String calendarID;
        String transactionString;
        string[] transactionInfo = new string[3];
        string SampleTotal;

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("Expires", "0");

            calendarID = !string.IsNullOrEmpty(Request.QueryString["CalendarID"]) ? Request.QueryString["CalendarID"] : Guid.Empty.ToString();
            transactionString = !string.IsNullOrEmpty(Request.QueryString["SessionTransactionInfo"]) ? Request.QueryString["SessionTransactionInfo"] : Guid.Empty.ToString();

            Session["CalendarID"] = calendarID;

            if (calendarID == null || calendarID == "" || transactionString == null || transactionString == "")
            {
                //Forces a redirect to splash page if this page is loaded without a session state.
                Response.Redirect("Default.aspx");
            }
            else
            {
                transactionInfo = transactionString.Split('|');
                SampleTotal = calcPriceOfSession(transactionInfo);
                Uobj = (UserObject)Session["UserInfo"];

                /*
                Tobj.CopyTrainerObject((TrainerObject)Session["TrainerInfo"]);

                //section to add client rates
                if (Tobj.AdditionalPersonRate == null || Tobj.IndividualRate == null)
                {
                    //  IndividualRatesTxtBox.Text = "0.00";
                    // AdditionalPersonRateTxtBox.Text = "0.00";
                }
                else
                {
                    // IndividualRatesTxtBox.Text = Tobj.IndividualRate;
                    // AdditionalPersonRateTxtBox.Text = Tobj.AdditionalPersonRate;
                }
                */
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
                    currency = "USD",
                    total = SampleTotal,
                    details = new Details()
                    {
                        shipping = "0.00",
                        subtotal = SampleTotal,
                        tax = "0.00"
                    }
                },

                description = "This is the payment transaction description.",
                item_list = new ItemList()
                {
                    items = new List<Item>()
                    {
                        new Item()
                        {
                            name = "Session",
                            currency = "USD",
                            price = SampleTotal,
                            quantity = "1",
                            sku = "sku"
                        }
                    },
                    shipping_address = new ShippingAddress
                    {
                        city = city.Text,
                        country_code = country_code.Text,
                        line1 = address.Text,
                        postal_code = postal_code.Text,
                        state = state.Text,
                        recipient_name = first_name.Text + " " + last_name.Text
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
                                city = city.Text,
                                country_code = country_code.Text,
                                line1 = address.Text,
                                postal_code = postal_code.Text,
                                state = state.Text
                            },
                            cvv2 = cvv2.Text,
                            expire_month = Int32.Parse(ex_month.Text),
                            expire_year = Int32.Parse(ex_year.Text),
                            first_name = first_name.Text,
                            last_name = last_name.Text,
                            number = card_number.Text,
                            type = card_type.Text
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
                payer = payer,
                transactions = new List<Transaction>() { transaction }
            };

            // ^ Ignore workflow code segment
            #region Track Workflow
            // this.flow.AddNewRequest("Create credit card payment", payment);
            #endregion

            // Create a payment using a valid APIContext
            try
            {
                var createdPayment = payment.Create(apiContext);

                Session["first_name"] = first_name.Text + " " + last_name.Text;
                Session["address"] = address.Text;
                Session["city"] = city.Text;
                Session["postal_code"] = postal_code.Text;
                Session["card_type"] = card_type.Text;
                Session["card_number"] = card_number.Text;
                Session["currency"] = "USD";
                Session["total"] = SampleTotal;

                SendConfirmationEmail(first_name.Text, last_name.Text, address.Text, city.Text, postal_code.Text, card_type.Text, SampleTotal);
                Response.Redirect("PaymentConfirmation.aspx");

            }
            catch
            {
                Response.Write("<script>alert('" + "Invalid Infromation, please try again." + "')</script>");
            }

            // ^ Ignore workflow code segment
            //#region Track Workflow
            //  this.flow.RecordResponse(createdPayment);
            //#

            // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        }

        private void SendConfirmationEmail(string first_name, string last_name, string address, string city, string postal_code, string card_type, string SampleTotal)
        {
            // String firstName = Request.Form["FName"];
            // String email = Request.Form["email"];
            string activationCode = Guid.NewGuid().ToString();
            string userEmail = Uobj.Email;
            string sessionDetails = (string)(Session["session_details"]);
            DateTime localDate = DateTime.Now;
            //Response.Write("<script>alert('" + email + "')</script>");

            using (MailMessage mm = new MailMessage("MobileFitnessNetwork@gmail.com", userEmail))
            {
                mm.Subject = "Training Session Payment Confirmation";
                string body = "Hello " + first_name + ",";

                
                body += "<br /><br />Your approved training session has been payed for.";
                body += "<br /><br /> ---Payment Details---";
                body += "<br /><br />" + "Transaction Date/Time: " + localDate + "<br />" + "Name: " + first_name + " " + last_name + "<br />" + "Address: " + address + "<br />" + "City: " + city + "<br />" + "Zip: " + postal_code + "<br />" + 
                    "Card Type: " + card_type + "<br />" + "Total: " + SampleTotal;
                body += "<br /><br /> ---Session Details--- <br /><br />";
                body += sessionDetails;

                body += "<br /><br />Thank you";
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

        string calcPriceOfSession(string[] info)
        {
            double standardRate = Double.Parse(info[0]);
            double additonalPersonRate = Double.Parse(info[1]);
            int numberOfPeople = Int32.Parse(info[2]);

            baseRateLbl.Text = "Base Rate: $" + standardRate.ToString("0.00");
            NumberAttendingLbl.Text = "Number of People Attending: " + numberOfPeople.ToString();
            AdditionalRateLbl.Text = "Cost From Additional Persons: $" + (additonalPersonRate * (numberOfPeople - 1)).ToString("0.00");

            double subTotal;

            subTotal = standardRate + (additonalPersonRate * (numberOfPeople - 1));
            subTotal.ToString("0.00");
            SubTotalLbl.Text = "Subtotal: $" + subTotal.ToString("0.00");
            ServiceCostLbl.Text = "Service Fee: $" + (subTotal * 0.05).ToString("0.00");
            double finalTotal = subTotal * 1.05;
            TotalLbl.Text = "Final Total: $" + finalTotal.ToString("0.00");

            Session["standard_rate"] = standardRate.ToString("0.00");
            Session["additional_person"] = (additonalPersonRate * (numberOfPeople - 1)).ToString("0.00");
            Session["number_people"] = numberOfPeople.ToString();
            Session["service_fee"] = (subTotal * 0.05).ToString("0.00");
            Session["sub_total"] = subTotal.ToString("0.00");

            return finalTotal.ToString("0.00");

        }

    }
}