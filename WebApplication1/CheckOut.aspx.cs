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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
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
                    total = "1.50",
                    details = new Details()
                    {
                        shipping = "0.50",
                        subtotal = "0.50",
                        tax = "0.50"
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
                            price = "0.50",
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

        //class Data
        //{
        //    public string FirstName
        //    {
        //        get { return first_name.Text; }
        //    }
        //}
        
    }
}