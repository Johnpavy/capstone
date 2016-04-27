using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Api;
using PayPal.Sample;

namespace WebApplication1
{
    public partial class CheckOut : System.Web.UI.Page
    {
        Transaction TranObj = new Transaction();
        TrainerObject Tobj = new TrainerObject();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("Expires", "0");
            
            if (Session["TrainerInfo"] == null)
            {
                //Forces a redirect to splash page if this page is loaded without a session state.
                Response.Redirect("Default.aspx");
            }
            else
            {
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
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //TranObj.IndividaulPersonCost;
            
            string SampleTotal = "50.00";
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

                Response.Redirect("PaymentConfirmation.aspx");

            }
            catch
            {
                Response.Write("<script>alert('" + "Invalid Infromation, please try again." + "')</script>");
            }

            // ^ Ignore workflow code segment
            #region Track Workflow
            //  this.flow.RecordResponse(createdPayment);
            #endregion

            // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        }

    }
}