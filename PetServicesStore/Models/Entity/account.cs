using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetServicesStore.Models.Entity
{
    public class account
    {
        public string username { get; set; }
        public string password { get; set; }
        public int role { get; set; }
        public string fullName { get; set; }
        public int phone { get; set; }
        public string email { get; set; }
        public Nullable<bool> gender { get; set; }
        public string question { get; set; }
        public string answer { get; set; }

        public account(string username, string password, int role, string fullName, int phone, string email, Nullable<bool> gender, string question, string answer)
        {
            this.username = username;
            this.password = password;
            this.role = role;
            this.fullName = fullName;
            this.phone = phone;
            this.email = email;
            this.gender = gender;
            this.question = question;
            this.answer = answer;
        }
    }
}