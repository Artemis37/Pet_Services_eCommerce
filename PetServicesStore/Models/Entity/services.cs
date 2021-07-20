using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetServicesStore.Models.Entity
{
    public class services
    {
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }

        public services(int id, string name, double price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }
    }
}