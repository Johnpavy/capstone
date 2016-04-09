using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class AdminControl : System.Web.UI.Page
    {
        AdminObject Aobj = new AdminObject();

        protected void Page_Load(object sender, EventArgs e)
        {
            Aobj = (AdminObject)Session["AdminInfo"];
            Label1.Text = Aobj.FirstName + " " + Aobj.LastName;
        }
    }
}