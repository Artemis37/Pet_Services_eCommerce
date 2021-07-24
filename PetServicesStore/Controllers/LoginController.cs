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
            //valid value
            //==============================
            DAO ob = new DAO();
            string result = "";
            ob.getData();
            string username = ob.checkUsername(Request.Form["username"], out result);
            //to avoid overridden result
            string password="";
            if (result.Equals(""))
            {
                password = ob.checkPassword(Request.Form["password"], out result);
            }else
            {
                password = Request.Form["password"];
            }
            ////////////////////////////
            string fullname = Request.Form["fullname"];
            int phone;
            try
            {
                phone = Int32.Parse(Request.Form["phone"]);
            }
            catch (FormatException)
            {
                phone = 0;
                result = "phone number must be number";
            }
            catch (OverflowException) ///character like '@,#,$,~,...et cetera' may cause overflowexception
            {
                phone = 0;
                result = "phone number must be number";
            }
            string email = Request.Form["email"];
            string genderString = Request.Form["gender"];
            Nullable<bool> gender;
            if (genderString.Equals("male")) gender = true;
            else if (genderString.Equals("female")) gender = false;
            else gender = null;
            string ques = Request.Form["ques"];
            string ans = Request.Form["ans"];
            account tempAcc = new account(username, password,1,fullname,phone,email,gender,ques,ans);
            if (result.Equals(""))
            {
                result = "Create account success";
                ob.register(tempAcc);
            }
            Response.Cookies["result"].Value = result;
            return View("RegisterForm");
        }

        [HttpPost]
        public ActionResult CheckUserName()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            DAO ob = new DAO();
            ob.getData();
            try
            {
                if(ob.LoginAcc[username].password == password)
                {
                    Response.Cookies["userinfo"].Expires = DateTime.Now.AddMinutes(5);
                    Response.Cookies["userinfo"]["username"] = username;
                    Response.Cookies["userinfo"]["role"] = ob.LoginAcc[username].role.ToString();
                    return RedirectToAction("../Default/Index");
                }
                else
                {
                    Response.Cookies["loginResult"].Value = "Wrong password";
                    return View("Login");
                }
            }
            catch (KeyNotFoundException e)
            {
                Response.Cookies["loginResult"].Value = "Wrong username";
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            Response.Cookies["userinfo"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index","Default");
        }

        public ActionResult AccountInfo()
        {
            return View();
        }

        public ActionResult EditAccountInfo()
        {
            ViewBag.State = 1;
            return View("AccountInfo");
        }

        public ActionResult UpdateAccount()
        {
            //==============================
            //valid value
            //==============================
            DAO ob = new DAO();
            string result = "";
            ob.getData();
            string username = Request.Form["username"];
            string password = ob.checkPassword(Request.Form["password"], out result);
            int role=1;
            if(Request.Form["userRole"] != null)
            {
                role = Int32.Parse(Request.Form["userRole"]);
            }
            string fullname = Request.Form["fullname"];
            int phone;
            try
            {
                phone = Int32.Parse(Request.Form["phone"]);
            }
            catch (FormatException)
            {
                phone = 0;
                result = "phone number must be number";
            }
            catch (OverflowException) ///character like '@,#,$,~,...et cetera' may cause overflowexception
            {
                phone = 0;
                result = "phone number must be number";
            }
            string email = Request.Form["email"];
            string genderString = Request.Form["gender"];
            Nullable<bool> gender;
            if (genderString.Equals("male")) gender = true;
            else if (genderString.Equals("female")) gender = false;
            else gender = null;
            string ques = Request.Form["ques"];
            string ans = Request.Form["ans"];
            account tempAcc = new account(username, password, role, fullname, phone, email, gender, ques, ans);
            if (result.Equals(""))
            {
                result = "Update account info success";
                ob.updateAccount(tempAcc);
            }
            Response.Cookies["result"].Value = result;
            return View("AccountInfo");
        }

        public ActionResult testDB()
        {
            return View();
        }
    }
}