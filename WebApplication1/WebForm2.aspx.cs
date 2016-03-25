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
            if(Session["TrainerInfo"] == null)
            {
                Tobj.FirstName = "Failed";
                Tobj.LastName = "Pass";
            }
            else
            {
                Tobj = (TrainerObject)Session["TrainerInfo"];
            }
            Label1.Text = Tobj.FirstName + " " + Tobj.LastName;
        }
    }
}