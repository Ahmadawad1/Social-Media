using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MyProfileLogic : IMyProfile
    {
        Context context;
        Posts posts;
        Notifications notifications;
        public MyProfileLogic()
        {
            notifications = new Notifications();
            posts = new Posts();
            context = new Context();
        }
        public Users GetUser(string email)
        {
            return context.Users.Single(x => x.Email == email);
        }
        public void ChangeImage(string path,string email)
        {
            if (path.Contains("ProfileImages"))
            {
                context.Users.Single(x => x.Email == email).ProfileImage = path;
            }
                
            else
            {
                context.Users.Single(x => x.Email == email).CoverImage = path;
            }
            context.SaveChanges();
        }

        public void ChangeBio(string bio, string email)
        {
            context.Users.Single(x => x.Email == email).Bio= bio;
            context.SaveChanges();
        }

        public void ChangeInfo(PersonalInfo personalInfo)
        {
          var user=  context.Users.Single(X => X.Email == personalInfo.Email);

            user.DateOfBirth = personalInfo.DateOfBirth;
            user.College = personalInfo.College;
            user.Major = personalInfo.Major;
            user.Job = personalInfo.Place;
            user.Position = personalInfo.Position;
            context.SaveChanges();
        }

        public void ChangeContactInfo(string phone, string place, string email)
        {
            context.Users.Single(X => X.Email == email).Place = place;
            context.Users.Single(X => X.Email == email).Phone = phone;
            context.SaveChanges();
        }

        public void Post(string email, string caption, string path)
        {
            context.Users.Single(X => X.Email == email).Posts++;
         
            posts.Poster = context.Users.Single(X => X.Email == email).ID;
            posts.Date = DateTime.Now;
            if (String.IsNullOrEmpty(path))
            {
                posts.PathOrText = caption;
                posts.Type = "Text";
            }
            else
            {
                posts.PathOrText = path;
                posts.ShareOrImageCaption = caption;
                posts.Type = "Image";
            }
            context.Posts.Add(posts);
            context.SaveChanges();
            
        }

        public List<Posts> Posts(int id)
        {
          
            return context.Posts.Where(x => x.Poster == id).OrderByDescending(x=>x.Date).ToList();
        }

        public void EditPost(int id, string caption)
        {
            var post = context.Posts.Single(x => x.ID == id);
            if (post.Type == "Image")
            {
                post.ShareOrImageCaption = caption;
            }
            else if (post.Type == "Text")
            {
                post.PathOrText = caption;
            }
            context.SaveChanges();
        }

        public void DeletePost(int id,string email)
        {
            context.Users.Single(X => X.Email == email).Posts--;
            var post = context.Posts.Single(x => x.ID == id);
            context.Posts.Remove(post);
            context.SaveChanges();

            foreach (var i  in context.Comments)
            {
                if (i.PostID == id)
                {
                    context.Comments.Remove(i);
                }
            }
            foreach (var i in context.Likes)
            {
                if (i.PostID == id)
                {
                    context.Likes.Remove(i);
                }
            }
            context.SaveChanges();

        }

        public string Like(int postID, int liker, int poster)
        {
            Likes like = new Likes();
            bool found = false;
            if (poster != liker)
            {
                notifications.Sender = liker;
                notifications.Notified = poster;
                notifications.Date = DateTime.Now;
                notifications.Content = "Liked your Post";
                context.Notifications.Add(notifications);
                context.SaveChanges();
            }
            var likesOnPost= context.Likes.Where(x => x.PostID == postID).ToList();
            foreach(var i  in likesOnPost)
            {
                if (i.Liker == liker)
                {
                    like = i;
                    found = true;
                    break;
                }
               
            }
            if (found)
            {
                context.Likes.Remove(like);
                context.Posts.Single(x => x.ID == postID).Likes--; 
                context.SaveChanges();
                return "IsLiked";
            }
            else
            {
                like.Liker = liker;
                like.Poster = poster;
                like.PostID = postID;
                context.Posts.Single(x => x.ID == postID).Likes++;
                context.Likes.Add(like);
                context.SaveChanges();

                return "NotLiked";
            }
           
           

        }

        public List<Likes> Likes()
        {
            return context.Likes.ToList();
        }
        public List<CommentsData> Comments(int id)
        {
            List<CommentsData> li = new List<CommentsData>();

            foreach(var i in context.Comments.Where(x=>x.PostID==id).ToList())
            {
                CommentsData commentsData = new CommentsData();
                commentsData.Comment = i.Comment;
                commentsData.Date = i.Date;
                commentsData.Name = context.Users.Single(y => y.ID == i.CommenterID).Name;
                commentsData.Image = context.Users.Single(y => y.ID == i.CommenterID).ProfileImage;
                li.Add(commentsData);

            }
            return li;
        }

        public int GetLikesNumber(int postID)
        {
            return context.Likes.Where(x => x.PostID == postID).Count();
        }

        public void AddComment(int commenter, int postId, string comment)
        {
            if (commenter != context.Posts.Single(x => x.ID == postId).Poster)
            { 
            notifications.Sender = commenter;
            notifications.Notified = context.Posts.Single(x => x.ID == postId).Poster;
            notifications.Date = DateTime.Now;
            notifications.Content = "Commented on your Post";
            context.Notifications.Add(notifications);
            context.SaveChanges();
            }
            Comments comments = new Comments();
            comments.PostID = postId;
           
            comments.Date = DateTime.Now;
            comments.CommenterID = commenter;
            comments.Comment = comment;
            context.Comments.Add(comments);
            context.Posts.Single(x => x.ID == postId).Comments++;
            context.SaveChanges();
        }

        public string GetImage(string email)
        {
            return context.Users.Single(x=>x.Email==email).ProfileImage;
        }

        public List<Users> Search(string txt)
        {
            return context.Users.Where(x => x.Name.Contains(txt)).ToList();
        }

        public Posts PostInfo(int postID)
        {
            return context.Posts.Single(x => x.ID == postID);
        }

        public List<Users> GetResults(string name, string email)
        {
            return context.Users.Where(x => x.Email != email && x.Name.Contains(name)).ToList();
        }
        public List<NotificationData> GetNotification(int id)
        {
            List<NotificationData> notificationData = new List<NotificationData>();
            List<Notifications> notifications = context.Notifications.Where(x=>x.Notified==id).ToList();
            foreach (var  i in notifications)
            {
              
                    NotificationData n = new NotificationData();
                    n.Content = i.Content;
                    n.Date = i.Date;
                    n.Image = context.Users.Single(x => x.ID == i.Sender).ProfileImage;
                    n.Name = context.Users.Single(x => x.ID == i.Sender).Name;
                    notificationData.Add(n);
                

            }
            List<NotificationData> sorted = notificationData.OrderByDescending(x => x.Date).ToList();
            return sorted;
        }
    }
}
