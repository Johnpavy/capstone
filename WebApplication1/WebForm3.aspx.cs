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
            bool isTrainer = false;
            if(isTrainer)
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
            }
            else
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
            }

        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            //Nothing for now
        }

        protected void ConfirmBtnTrainer_Click(object sender, EventArgs e)
        {
            Panel2.Visible = true;
            //Nothing for now
        }

        protected void DeclineBtnTrainer_Click(object sender, EventArgs e)
        {
            Panel2.Visible = true;
            //Nothing for now
        }
    }
}