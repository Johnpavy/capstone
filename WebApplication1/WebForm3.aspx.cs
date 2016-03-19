using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm3 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isTrainer = true;
            if(isTrainer)
            {
                ClientPanel.Visible = false;
                TrainerControlPannel.Visible = true;
            }
            else
            {
                ClientPanel.Visible = true;
                TrainerControlPannel.Visible = false;
            }

        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            ClientPanel.Visible = true;
            //Nothing for now
        }

        protected void ConfirmBtnTrainer_Click(object sender, EventArgs e)
        {
            ConfirmSessionPanel.Visible = true;
            //Nothing for now
        }

        protected void DeclineBtnTrainer_Click(object sender, EventArgs e)
        {
            ConfirmSessionPanel.Visible = true;
            //Nothing for now
        }

        protected void ConfirmClientApptBtn_Click(object sender, EventArgs e)
        {
            TrainerControlPannel.Visible = false;
            ConfirmSessionPanel.Visible = true;
        }

        protected void ManageTimeBtn_Click(object sender, EventArgs e)
        {
            TrainerControlPannel.Visible = false;
            BlockTimesPanel.Visible = true;
        }
    }
}