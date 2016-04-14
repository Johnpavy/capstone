using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
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
                //bio.InnerHtml = Tobj.Bio;
                specialty.InnerHtml = Tobj.Speciality;
                UserNameLbl.Text = Tobj.FirstName + " " + Tobj.LastName + " ";
                //changes default profile pic to user uploaded one
                if (Tobj.ImagePath != "")
                {
                    ProfilePic.Attributes["src"] = Tobj.ImagePath;
                }
            }

        }

        protected void BookTrainer_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainerScheduler.aspx");
        }

        protected void ChangeAccountSetting_Click(object sender, EventArgs e)
        {
            Session["TrainerInfo"] = Tobj;
            Response.Redirect("AccountSettings.aspx");
        }

        protected void ComfirmUpdateBioButton1_Click(object sender, EventArgs e)
        {
            if(BioTextBox.ReadOnly == true)
            {
                BioTextBox.ReadOnly = false;
                ConfirmBioUpdate.Text = "Save";
                CancelBioUpdate.Visible = true;
            }
            else
            {
                BioTextBox.ReadOnly = true;
                Tobj.Bio = BioTextBox.Text;
                Session["TrainerInfo"] = Tobj;
                ConfirmBioUpdate.Text = "Update";
                CancelBioUpdate.Visible = false;
            }
        }

        protected void CancelUpdateBioButton1_Click(object sender, EventArgs e)
        {
            BioTextBox.Text = Tobj.Bio;
            CancelBioUpdate.Visible = false;
            BioTextBox.ReadOnly = true;
            ConfirmBioUpdate.Text = "Update";

        }

    }
}