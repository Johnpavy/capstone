using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    
    public partial class WebForm1 : System.Web.UI.Page
    {
        UserObject Uobj = new UserObject();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"] == null)
            {
                //Forces a redirect to splash page if this page is loaded without a session state.
                Response.Redirect("Default.aspx");
            }
            else
            {
                Uobj.CopyUserObject((UserObject)Session["UserInfo"]);

                preferences.InnerHtml = Uobj.TrainingPref;
                equipment.InnerHtml = Uobj.Equipment;
                UserNameLbl.Text = Uobj.FirstName + " " + Uobj.LastName + " ";

            }
        }
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            if(DropDownList1.SelectedValue == "")
            {
                ErrorLbl.Visible = true;
                ErrorLbl.Text = "You must select a type of training";
            }
            else
            {
                Session["Selection"] = DropDownList1.SelectedValue;
                Response.Redirect("searchTrainersPage.aspx");
            }

        }

    }
}