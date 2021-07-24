using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using PetServicesStore.Models;
using PetServicesStore.Models.Entity;

namespace PetServicesStore.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult initAppointment()
        {
            if (HttpContext.Request.Cookies["userinfo"] == null) return RedirectToAction("../Login/login");
            else
            {
                DAO ob = new DAO();
                ob.getData();
                string username = Request.Cookies["userinfo"]["username"];
                string dogName = Request.Form["dog_name"];
                string dogKind = Request.Form["dog_kind"];
                DateTime appointedDate = DateTime.Parse(Request.Form["appointed_date"]);
                int serviceId = ob.ServicesLstnameKey[Request.Form["services"]].id;
                string msg = Request.Form["msg"];
                ob.saveAppointment(new appointment(username, dogName, dogKind, appointedDate, serviceId, msg));
                return RedirectToAction("Index");
            }
        }

        public ActionResult ManageAccount()
        {
            return View();
        }

        public ActionResult EditAccount()
        {
            string username = Request.QueryString["username"];
            Response.Cookies["editUser"].Value = username;
            return RedirectToAction("AccountInfo","Login");
        }

        public ActionResult ManageAppointment()
        {
            return View();
        }

        public ActionResult deleteAppointment()
        {
            int aptID = Int32.Parse(Request.Params["aptID"]);
            DAO ob = new DAO();
            ob.deleteAppoitment(aptID);
            return RedirectToAction("ManageAppointment");
        }
    }
}