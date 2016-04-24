using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session["TrainerInfo"] = null;
            Session["AdminInfo"] = null;
            Session["UserInfo"] = null;
            Response.Redirect("Default.aspx");
        }
    }
}