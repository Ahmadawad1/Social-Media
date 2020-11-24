using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class FeedController : Controller
    {
        private readonly IMyProfile myProfile;
        private readonly IFeed feed; 
        public FeedController(IMyProfile myProfile,IFeed feed)
        {   
            this.myProfile = myProfile;
            this.feed = feed;
        }
        [HttpGet]
        public ActionResult Feed()
        {
            ViewBag.Notifications = myProfile.GetNotification(Convert.ToInt32(Session["ID"]));
            ViewBag.Posts = feed.GetPosts(Convert.ToInt32(Session["ID"]));
            ViewBag.Likes = myProfile.Likes();
            return View(myProfile.GetUser(Session["Email"].ToString()));
        }
        [HttpPost]
        public ActionResult Post(HttpPostedFileBase image, string caption)
        {
            if (image != null)
            {
                myProfile.Post(Session["Email"].ToString(), caption, GetPath("post", image));
            }
            else if (image == null && caption != null)
            {
                myProfile.Post(Session["Email"].ToString(), caption, null);
            }
            else if (image == null && String.IsNullOrWhiteSpace(caption))
            {

            }
            return RedirectToAction("Feed");
        }
        public string GetPath(string type, HttpPostedFileBase image)
        {
            if (image == null) return null;
            else
            {
                string fileName = Path.GetFileName(image.FileName);
                string path = string.Empty;
                if (type == "profile")
                {
                    path = "~/ProfileImages/" + fileName;
                    image.SaveAs(Path.Combine(Server.MapPath("~/ProfileImages/"), fileName));
                }
                else if (type == "cover")
                {
                    path = "~/CoverImages/" + fileName;
                    image.SaveAs(Path.Combine(Server.MapPath("~/CoverImages/"), fileName));
                }
                else if (type == "post")
                {
                    path = "~/PostsImages/" + fileName;
                    image.SaveAs(Path.Combine(Server.MapPath("~/PostsImages/"), fileName));
                }

                return path;
            }
        }
    }
}