using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class UserObject
    {
        private int userId;
        private string imagePath;
        private string email;
        private string phone;
        private string gender;
        private string dateOfBirth;
        private string firstName;
        private string middleName;
        private string lastName;
        private string homeAddress;
        private string equipment;
        private string trainingPref;
        private string bio;


        public UserObject()
        {
            userId = 0;
            imagePath = "";
            email = "";
            gender = "";
            dateOfBirth = "";
            firstName = "";
            middleName = "";
            lastName = "";
            phone = "";
            homeAddress = "";
            equipment = "";
            trainingPref = "";
            bio = "";

        }

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public string DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
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

        public string HomeAddress
        {
            get { return homeAddress; }
            set { homeAddress = value; }
        }

        public string Equipment
        {
            get { return equipment; }
            set { equipment = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string TrainingPref
        {
            get { return trainingPref; }
            set { trainingPref = value; }
        }

        public string Bio
        {
            get { return bio; }
            set { bio = value; }
        }

        public void CopyUserObject(UserObject objToCopy)
        {
            this.UserId = objToCopy.UserId;
            this.ImagePath = objToCopy.ImagePath;
            this.Email = objToCopy.Email;
            this.Gender = objToCopy.Gender;
            this.DateOfBirth = objToCopy.DateOfBirth;
            this.FirstName = objToCopy.FirstName;
            this.MiddleName = objToCopy.MiddleName;
            this.LastName = objToCopy.LastName;
            this.Phone = objToCopy.Phone;
            this.HomeAddress = objToCopy.HomeAddress;
            this.Equipment = objToCopy.Equipment;
            this.TrainingPref = objToCopy.TrainingPref;
            this.Bio = objToCopy.Bio;
        }
    }
}