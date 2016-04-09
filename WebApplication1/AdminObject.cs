using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class AdminObject
    {
        private int adminId;
        private string email;
        private string firstName;
        private string middleName;
        private string lastName;


        public AdminObject()
        {
            adminId = 0;
            email = "";
            firstName = "";
            middleName = "";
            lastName = "";
        }

        public int AdminId
        {
            get { return adminId; }
            set { adminId = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }


        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
    }
}