using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChat chat;
        private readonly IMyProfile myProfile;
        public ChatController(IMyProfile myProfile, IChat chat)
        {
            this.chat = chat;
            this.myProfile = myProfile;
        }
        public ActionResult Contacts()
        {
            int id = Convert.ToInt32(Session["ID"]);
          
            ViewBag.Notifications = myProfile.GetNotification(id);
            return View(chat.GetContacts(id));
        }
        public ActionResult Conversation(int contactId)
        {
            int id = Convert.ToInt32(Session["ID"]);
            ViewBag.Notifications = myProfile.GetNotification(id);
            return View(chat.GetConversation(id, contactId));
        }
    }
}