using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetServicesStore.Models
{
    public class Test
    {
        public int id { get; set; }
        public string name { get; set; }

        public Test()
        {
        }
        public Test(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}