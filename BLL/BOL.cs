using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class BOL
    {

    }
    public class SignUpCollection
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string ConfirmPassword { set; get; }
    }
    public class SignInCollection
    {
        public string Email { set; get; }
        public string Password { set; get; }
    }
    public class PersonalInfo
    {
        public string Place { set; get; }
        public string Email { set; get; }
        public string Position { set; get; }
        public string Major { set; get; }
        public string College { set; get; }
        public string DateOfBirth { set; get; }
    }
    public class CommentsData
    {
        public string Name { set; get; }
        public string Comment { set; get; }
        public DateTime Date { set; get; }
        public string Image { set; get; }

    }
    public class Conversation
    {
        public Users Contact { set; get; }
        public List<Messages> Messages { set; get; }

    }
    public class NotificationData
    {
        public string Image { set; get; }
        public string Name { set; get; }
        public string Content { set; get; }
        public DateTime Date { set; get; }
    }
    public class FeedPosts
    {
        public string Image { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public Posts Post { set; get; }
    }
}
