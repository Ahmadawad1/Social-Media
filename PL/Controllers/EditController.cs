using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class EditController : Controller
    {
        private readonly ISignIn signIn;
        private readonly ISignUp signUp;
        private readonly IMyProfile myProfile;
        public EditController(IMyProfile myProfile,ISignUp signUp,ISignIn signIn)
        {
            this.signIn = signIn;
            this.myProfile = myProfile;
            this.signUp = signUp; ViewBag.RemoteValidation = "";
        }
        [HttpGet]
        public ActionResult Account()
        {
            ViewBag.Notifications = myProfile.GetNotification(Convert.ToInt32(Session["ID"]));
            if (Session["Email"] == null)
            {
                if (HttpContext.Request.Cookies.Get("Cookie") == null)
                {
                    return RedirectToAction("SignIn", "SignIn");
                }
                else
                {
                    string data = SignInLogic.Decrypt(HttpContext.Request.Cookies.Get("Cookie").Value);
                    string email = string.Empty;
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] == ' ') break;
                        email += (data[i]);
                    }
                    string name = myProfile.GetUser(email).Name;
                    Session["Name"] = name;
                    Session["Email"] = email; 
                    Session["ImageProfile"] = myProfile.GetImage(Session["Email"].ToString());
                    ViewBag.Notifications = myProfile.GetNotification(Convert.ToInt32(Session["ID"]));
                    return View(signIn.GetUserById(Session["ID"].ToString()));
                }
            }
            else
            {

                return View(signIn.GetUserById(Session["ID"].ToString()));
            }
          
        }
        [HttpPost]
        public ActionResult ChangeName(string fullname)
        {
            ViewBag.Notifications = myProfile.GetNotification(Convert.ToInt32(Session["ID"]));
            signIn.ChangeName(fullname, Session["Email"].ToString());
            return View("Account", signIn.GetUserById(Session["ID"].ToString()));
        }
        [HttpPost]
        public ActionResult ChangeEmail(string email)
        {
            ViewBag.Notifications = myProfile.GetNotification(Convert.ToInt32(Session["ID"]));
            if (!signIn.ChangeEmail(email, Session["Email"].ToString(),myProfile.GetUser(Session["Email"].ToString()).ID))
            {
                ViewBag.RemoteValidation = "Email is Already Existed";
            }
            else
            {
                ViewBag.RemoteValidation = "Email Was Changed Successfully";
                Session["Email"] = email;
            }
            Thread.Sleep(1000);
            return View("Account", signIn.GetUserById(Session["ID"].ToString()));
        }
        [HttpPost]
        public ActionResult ChangePassword(string password)
        {
            ViewBag.Notifications = myProfile.GetNotification(Convert.ToInt32(Session["ID"]));
            if (!signIn.ChangePassword(password, Session["Email"].ToString()))
            {
                ViewBag.PasswordValidation = "Sorry, Password is Wrong !";
            }
          else
           {

                ViewBag.PasswordValidation = "Password was Successfully Changed";
           }
            return View("Account", signIn.GetUserById(Session["ID"].ToString()));
        }
        [HttpPost]
        public ActionResult DeleteAccount(int id)
        {
            signUp.DeleteUser(id);
            return View("SignIn","SignIn");
        }
    }
}