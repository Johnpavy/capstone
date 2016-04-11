using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class TrainerScheduler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string startDate = Calendar1.TodaysDate.ToShortDateString();
            TextBox1.Text = startDate;
            DateTextBox.Text = startDate;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

            TextBox1.Text = Calendar1.SelectedDate.ToShortDateString();
            DateTextBox.Text = Calendar1.SelectedDate.ToShortDateString();

        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        { 
            //If Date is listed on date blocked table, then mark as slate gray
            //If Date is completly blocked, then mark gray
            if(e.Day.Date == Convert.ToDateTime("4/12/2016"))
            {
                e.Cell.BackColor = System.Drawing.Color.SlateGray;
            }

            if (e.Day.Date == Convert.ToDateTime("4/13/2016"))
            {
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }
        }


    }
}