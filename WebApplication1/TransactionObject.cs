using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class TransactionObject
    {
        private string individualCost;
        private string additonalPersonCost;
        private string numberAttending;

        public TransactionObject()
        {
        individualCost ="0.00";
        additonalPersonCost = "0.00";
        numberAttending = "1";
        }

        public string IndividualCost
        {
            get { return individualCost; }
            set { individualCost = value; }
        }

        public string AdditionalPersonCost
        {
            get { return additonalPersonCost; }
            set { additonalPersonCost = value; }
        }

        public string NumberAttending
        {
            get { return numberAttending; }
            set { numberAttending = value; }
        }

        new public string ToString()
        {
            return this.IndividualCost + "|" + this.AdditionalPersonCost + "|" + this.NumberAttending;
        }
    }
}