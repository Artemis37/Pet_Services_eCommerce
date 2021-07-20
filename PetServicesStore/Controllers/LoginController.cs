using PetServicesStore.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetServicesStore.Models;

namespace PetServicesStore.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RegisterForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterProcess()
        {
            //==============================
            //need valid value
            //==============================
            string username = HttpContext.Request.Form["username"];
            string password = HttpContext.Request.Form["password"];
            string fullname = HttpContext.Request.Form["fullname"];
            int phone = Int32.Parse(HttpContext.Request.Form["phone"]);
            string email = HttpContext.Request.Form["email"];
            string genderString = HttpContext.Request.Form["gender"];
            Nullable<bool> gender;
            if (genderString.Equals("male")) gender = true;
            else if (genderString.Equals("female")) gender = false;
            else gender = null;
            string ques = HttpContext.Request.Form["ques"];
            string ans = HttpContext.Request.Form["ans"];
            account tempAcc = new account(username, password,1,fullname,phone,email,gender,ques,ans);
            DAO ob = new DAO();
            string result;
            result = ob.register(tempAcc);
            HttpCookie cookie = new HttpCookie("result");
            cookie.Expires = DateTime.Now.AddSeconds(10);
            cookie.Value = result;
            Response.Cookies.Add(cookie);
            return View("RegisterForm");
        }

        [HttpPost]
        public ActionResult CheckUserName()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            DAO ob = new DAO();
            ob.getData();
            if(ob.LoginAcc[username].password == password)
            {
                Response.Cookies["userinfo"].Expires = DateTime.Now.AddMinutes(5);
                Response.Cookies["userinfo"]["username"] = username;
                Response.Cookies["userinfo"]["role"] = ob.LoginAcc[username].role.ToString();
            }
            return RedirectToAction("../Default/Index");
        }

        public ActionResult Logout()
        {
            Response.Cookies["userinfo"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index","Default");
        }

        public ActionResult testDB()
        {
            return View();
        }
    }
}