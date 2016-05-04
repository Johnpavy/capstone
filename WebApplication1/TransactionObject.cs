using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
This object contains information needed to calculate the cost of a session
The individualCost is the cost for 1 person per hour
The additonalPersonCost is the cost for each additonal person per hour
The numberAttending is the TOTAl number of people attending (individual + additional persons).
*/

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

        //ToString()
        //This Function is needed so that these values maybe passed via URl to the CheckoutPage
        // "|" is a safe delimiter in the URL.
        new public string ToString()
        {
            return this.IndividualCost + "|" + this.AdditionalPersonCost + "|" + this.NumberAttending;
        }
    }
}