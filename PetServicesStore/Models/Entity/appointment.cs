using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetServicesStore.Models.Entity
{
    public class appointment
    {
        public int id { get; set; }
        public string username { get; set; }
        public string dogName { get; set; }
        public string dogKind { get; set; }
        public DateTime dateAppointed { get; set; }
        public int servicesId { get; set; }
        public string message { get; set; }

        public appointment(string username, string dogName, string dogKind, DateTime dateAppointed, int servicesId, string message)
        {
            this.username = username;
            this.dogName = dogName;
            this.dogKind = dogKind;
            this.dateAppointed = dateAppointed;
            this.servicesId = servicesId;
            this.message = message;
        }

        public appointment(int id, string username, string dogName, string dogKind, DateTime dateAppointed, int servicesId, string message)
        {
            this.id = id;
            this.username = username;
            this.dogName = dogName;
            this.dogKind = dogKind;
            this.dateAppointed = dateAppointed;
            this.servicesId = servicesId;
            this.message = message;
        }
    }
}