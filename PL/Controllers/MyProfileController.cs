using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly IUser user; 
        private readonly ISignIn signIn;
        private readonly IMyProfile myProfile;
        public MyProfileController(IMyProfile myProfile, IUser user,ISignIn signIn)
        {
            this.user = user;
            this.myProfile = myProfile;
            this.signIn = signIn;
        }
        [HttpGet]
        public ActionResult MyProfile()
        {
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
                    Session["ID"] = myProfile.GetUser(Session["Email"].ToString()).ID;

                    return View(myProfile.GetUser(email));
                }
            }

            Session["ID"] = myProfile.GetUser(Session["Email"].ToString()).ID;
                Session["ImageProfile"] = myProfile.GetImage(Session["Email"].ToString());
                Session["Name"] = myProfile.GetUser(Session["Email"].ToString()).Name;
                ViewBag.Following = user.GetFollowing(myProfile.GetUser(Session["Email"].ToString()).ID);
                ViewBag.Followers = user.GetFollowers(myProfile.GetUser(Session["Email"].ToString()).ID);
                ViewBag.Likes = myProfile.Likes();
                ViewBag.Posts = myProfile.Posts(myProfile.GetUser(Session["Email"].ToString()).ID);
            ViewBag.Notifications = myProfile.GetNotification(Convert.ToInt32(Session["ID"]));
            return View(myProfile.GetUser(Session["Email"].ToString()));
        }
        
          
        
        [HttpPost]
        public ActionResult EditPost(int id, string caption)
        {
            myProfile.EditPost(id, caption);
            return RedirectToAction("MyProfile");
        }
        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            myProfile.DeletePost(id, Session["Email"].ToString());
          
            return RedirectToAction("MyProfile");
        }
        [HttpGet]
        public ActionResult Result(string name)
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
                    string _name = myProfile.GetUser(email).Name;
                    Session["Name"] = _name;
                    Session["Email"] = email;
                    return View(myProfile.GetUser(Session["Email"].ToString()));
                }
            }
            return View(myProfile.GetResults(name, Session["Email"].ToString()));
        }
        [HttpPost]
        public ActionResult ChangeImage(string type)
        {
            HttpPostedFileBase image = Request.Files["image"];

            myProfile.ChangeImage(GetPath(type, image), Session["Email"].ToString());

            return RedirectToAction("MyProfile");
        }
        [HttpPost]
        public ActionResult ChangeBio(string bio)
        {
            myProfile.ChangeBio(bio, Session["Email"].ToString());
            return RedirectToAction("MyProfile");
        }
        [HttpPost]
        public ActionResult ChangeContactInfo(string phone, string place)
        {

            myProfile.ChangeContactInfo(phone, place, Session["Email"].ToString());
            return RedirectToAction("MyProfile");
        }
        [HttpPost]
        public ActionResult ChangeInfo(FormCollection formCollection)
        {
            PersonalInfo personalInfo = new PersonalInfo();
            personalInfo.Place = formCollection["place"];
            personalInfo.Position = formCollection["position"];
            personalInfo.Major = formCollection["major"];
            personalInfo.College = formCollection["college"];
            personalInfo.DateOfBirth = formCollection["birthday"];
            personalInfo.Email = Session["Email"].ToString();
            myProfile.ChangeInfo(personalInfo);
            return RedirectToAction("MyProfile");
        }
        [HttpPost]
        public ActionResult Post(HttpPostedFileBase image, string caption)
        {
            if (image != null)
            {
                myProfile.Post(Session["Email"].ToString(), caption, GetPath("post", image));
            }
            else if(image==null && caption!=null)
            {
                myProfile.Post(Session["Email"].ToString(), caption, null);
            }
            else if (image ==null && String.IsNullOrWhiteSpace(caption))
            {

            }
            return RedirectToAction("MyProfile");
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
        [HttpGet]
       
        public ActionResult Comments(string postID,string poster)
        {
            ViewBag.Notifications = myProfile.GetNotification(Convert.ToInt32(Session["ID"]));
            if (postID == null)
            {
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
                        return View(myProfile.GetUser(Session["Email"].ToString()));
                    }
                }
            }
            ViewBag.Poster = poster;
            ViewBag.PosterName = myProfile.GetUser(poster).Name;
            ViewBag.PostID = postID;
            ViewBag.Image = myProfile.GetImage(poster);
            ViewBag.PostInfo = myProfile.PostInfo(Convert.ToInt32(postID));
            ViewBag.Comments = myProfile.Comments(Convert.ToInt32(postID));
            return View();
        }
         
    
        [HttpPost]
        public ActionResult Like(int postID,int liker,int poster)
        {
            return Json(myProfile.Like(postID, liker, poster));
        }
        [HttpPost]
        public JsonResult AddComment(int postID, string comment)
        {
            
            myProfile.AddComment(Convert.ToInt32(Session["ID"]), postID, comment);
            return Json("Comment Was Added");
        }
        [HttpPost]
        public JsonResult Search(string txt)
        {
            return Json(new{list=myProfile.Search(txt) });
        }

    }
}