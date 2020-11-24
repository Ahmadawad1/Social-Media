using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class SignInController : Controller
    {
        private readonly ISignIn signIn;
        private readonly ISignUp signUp;
        public SignInController(ISignIn signIn,ISignUp signUp)
        {
            this.signIn = signIn;
            this.signUp = signUp;
        }
        
        public ActionResult SignOut()
        {
            Session.Clear();
            HttpCookie cookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Now.AddDays(-1); // make it expire yesterday
                Response.Cookies.Add(cookie); // overwrite it
            }
            return RedirectToAction("SignIn");
        }
        [HttpGet]
        public ActionResult SignIn()
        {
         
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(FormCollection formCollection)
        {
            SignInCollection signInCollection = new SignInCollection();
            signInCollection.Email = formCollection["email"];
            signInCollection.Password = formCollection["password"];
            if (signUp.IsEmailExist(formCollection["email"]))
            {
                if (signIn.IsPasswordCorrect(formCollection["email"], formCollection["password"]))
                {
                    Session["Email"] = formCollection["email"];
                    
                    Response.Cookies.Add(signIn.CreateCookie(formCollection["email"], formCollection["password"]));
                    HttpContext.Response.SetCookie(signIn.CreateCookie(formCollection["email"], formCollection["password"]));
                    return RedirectToAction("MyProfile","MyProfile");
                }
                else
                {
                    ViewBag.Error = "Incorrect Password";
                    return View();
                }
            }
            else
            {
                ViewBag.Error= "Invalid Email";
                return View();
            }
         
        }
    }
}