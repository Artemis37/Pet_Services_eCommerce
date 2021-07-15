using PetServicesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetServicesStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var temp = new Test() { id=1, name="Iron Man" };
            return View(temp);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}