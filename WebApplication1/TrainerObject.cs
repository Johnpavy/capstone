using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class TrainerObject
    {
        private int trainerId;
        private string imagePath;
        private string email;
        private string phone;
        private string gender;
        private string dateOfBirth;
        private string firstName;
        private string middleName;
        private string lastName;
        private string homeAddress;
        private string bio;
        private string speciality;
        private string individualRate;
        private string additionalPersonRate;


        public TrainerObject()
        {
            trainerId = 0;
            imagePath = "";
            email = "";
            gender = "";
            dateOfBirth = "";
            firstName = "";
            middleName = "";
            lastName = "";
            phone = "";
            homeAddress = "";
            bio = "";
            speciality = "";
            individualRate = "0.00";
            additionalPersonRate = "0.00";

    }

    public int TrainerId
        {
            get { return trainerId; }
            set { trainerId = value; }
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

        public string Bio
        {
            get { return bio; }
            set { bio = value; }
        }

        public string Speciality
        {
            get { return speciality; }
            set { speciality = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string IndividualRate
        {
            get { return individualRate; }
            set { individualRate = value; }
        }

        public string AdditionalPersonRate
        {
            get { return additionalPersonRate; }
            set { additionalPersonRate = value; }
        }

        public void CopyTrainerObject(TrainerObject objToCopy)
        {
            this.TrainerId = objToCopy.TrainerId;
            this.ImagePath = objToCopy.ImagePath;
            this.Email = objToCopy.Email;
            this.Gender = objToCopy.Gender;
            this.DateOfBirth = objToCopy.DateOfBirth;
            this.FirstName = objToCopy.FirstName;
            this.MiddleName = objToCopy.MiddleName;
            this.LastName = objToCopy.LastName;
            this.Phone = objToCopy.Phone;
            this.HomeAddress = objToCopy.HomeAddress;
            this.Bio = objToCopy.Bio;
            this.Speciality = objToCopy.Speciality;
            this.AdditionalPersonRate = objToCopy.AdditionalPersonRate;
            this.IndividualRate = objToCopy.IndividualRate;
        }
    }
}