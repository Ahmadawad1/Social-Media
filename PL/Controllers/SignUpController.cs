using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class SignUpController : Controller
    {

        private readonly ISignUp signUp;
        public SignUpController(ISignUp signUp)
        {
            this.signUp = signUp;
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection formCollection)
        {
            SignUpCollection signUpCollection = new SignUpCollection();
            signUpCollection.Name = formCollection["fullname"];
            signUpCollection.Email = formCollection["email"];
            signUpCollection.Password = formCollection["password"];
            signUpCollection.ConfirmPassword = formCollection["confirm"];
            if (!signUp.IsEmailExist(signUpCollection.Email))
            {
                signUp.AddUser(signUpCollection);
                return RedirectToAction("Success");
            }
            else 
            {
                ViewBag.RemoteValidation = "Email is Already Existed";
                      return View();
            }

        }
        [HttpGet]
        public ActionResult Success()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Success")]
        public ActionResult SuccessPost()
        {
            return View("SignIn");
        }
    }
}