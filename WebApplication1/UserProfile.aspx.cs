using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class UserProfile : System.Web.UI.Page
    {
        UserObject Uobj = new UserObject();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] == null)
            {
                Label1.Text = "NO SESSION STATE";
            }
            else
            {
                Uobj.CopyUserObject((UserObject)Session["UserInfo"]);

                Label1.Text = Uobj.UserId + " " + Uobj.FirstName + " " + Uobj.LastName;
            }
        }
    }
}