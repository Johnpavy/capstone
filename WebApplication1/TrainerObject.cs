﻿using System;
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
            get { return Gender; }
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
    }
}