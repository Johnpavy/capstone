using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
        }

        protected void login_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void signup_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainerSignup.aspx");
        }

        protected void about_Click(object sender, EventArgs e)
        {

        }

        protected void ClientSignup_Click(object sender, EventArgs e)
        {

        }
    }
}