using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
   public interface Interfaces
    {
    }
    public interface IFeed
    {
        List<FeedPosts> GetPosts(int id);
    }
    public interface IChat
    {
        
        List<Users> GetContacts(int id);
        Conversation GetConversation(int myID, int contactID);
    }
    public interface ISignUp
    {
        void DeleteUser(int id);
       void AddUser(SignUpCollection signUpCollection);
        bool IsEmailExist(string email);
    }
    public interface ISignIn
    {
        HttpCookie CreateCookie(string email, string password);
        bool IsPasswordCorrect(string email,string password);
        Users GetUserById(string id);
        void ChangeName(string name, string email);
        bool ChangeEmail(string newEmail, string email,int id);
        bool ChangePassword(string password, string email);
    }
  
    public interface IUser
    {
        void Unfollow(int followed, int follower);
        void Follow(int followed, int follower);
        bool IsFollowed(int followed, int follower);
        string GetUserEmail(int id);
        List<Users> GetFollowers(int followed); 
        List<Users> GetFollowing(int follower);
    }
    public interface IMyProfile
    {
        List<NotificationData> GetNotification(int id);
        Users GetUser(string email);
        void ChangeImage(string path, string email);
        void ChangeBio(string bio, string email);
        void ChangeInfo(PersonalInfo personalInfo);
        void ChangeContactInfo(string phone ,string place,string email);
        void Post(string email, string caption, string path);
        List<Posts> Posts(int id);
        void EditPost(int id,string caption);
        void DeletePost(int id,string email);
        string Like(int postID,int liker,int poster);
        List<Likes> Likes();
        List<CommentsData> Comments(int id);
        int GetLikesNumber(int postID);
        void AddComment(int commenter, int postId, string comment);
        string GetImage(string email);
        List<Users> Search(string txt);
        Posts PostInfo(int postID);

        List<Users> GetResults(string name,string email);
    }
}
