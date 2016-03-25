using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        TrainerObject Tobj = new TrainerObject();

        protected void Page_Load(object sender, EventArgs e)
        {
            //This block of code prevents caching. You cannot use the browser's foward and back
            //buttons to return to a page. Will be helpful with preventing double payment and double
            //database access.
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            if (Session["TrainerInfo"] == null)
            {
                //Forces a redirect to splash page if this page is loaded without a session state.
                Response.Redirect("Default.aspx");
            }
            else
            {
                Tobj = (TrainerObject)Session["TrainerInfo"];
                Session.Abandon();
            }
            //Label1.Text = Tobj.FirstName + " " + Tobj.LastName;
        }

    }
}