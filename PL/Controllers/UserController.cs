using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser user;
        private readonly IMyProfile myProfile;
        public UserController(IUser user,IMyProfile myProfile)
        {
            this.user = user;
            this.myProfile = myProfile;
        }
        [HttpGet]
        public new ActionResult User(int ID)
        {
            string email = user.GetUserEmail(ID);
            if (Session["Email"] == null)
            {
                return RedirectToAction("SignIn", "SignIn");
            }
            ViewBag.Following = user.GetFollowing(myProfile.GetUser(email).ID);
            ViewBag.Followers = user.GetFollowers(myProfile.GetUser(email).ID);
            Session["Name"] = myProfile.GetUser(Session["Email"].ToString()).Name;
            Session["ID"] = myProfile.GetUser(Session["Email"].ToString()).ID;
            ViewBag.IsFollowed = user.IsFollowed(myProfile.GetUser(email).ID, myProfile.GetUser(Session["Email"].ToString()).ID);
            ViewBag.Likes = myProfile.Likes();
            ViewBag.Posts = myProfile.Posts(myProfile.GetUser(email).ID);
            ViewBag.Notifications = myProfile.GetNotification(Convert.ToInt32(Session["ID"]));
            return View(myProfile.GetUser(email));
        }
        [HttpPost]
        public JsonResult Follow(string follower,string followed)
        {
            
            user.Follow(myProfile.GetUser(followed).ID, myProfile.GetUser(follower).ID);
            return Json("");
        }
        [HttpPost]
        public JsonResult Unfollow(string follower, string followed)
        {
            user.Unfollow(myProfile.GetUser(followed).ID, myProfile.GetUser(follower).ID);
            return Json("");
        }


    }
}