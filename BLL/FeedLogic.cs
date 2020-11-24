using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FeedLogic : IFeed
    {
        Context context;
        public FeedLogic()
        {
            context = new Context();
        }
        public List<FeedPosts> GetPosts(int id)
        {
           
            List<Posts> posts = context.Posts.Where(X => X.Poster != id).ToList();
            List<FeedPosts> feed = new List<FeedPosts>();
            foreach(var i in posts)
            {
                if(context.Follows.Any(X => X.Follower == id && X.Followed==i.Poster))
                {
                    FeedPosts feedPosts = new FeedPosts();
                    feedPosts.Image = context.Users.Single(x => x.ID == i.Poster).ProfileImage;
                    feedPosts.Name = context.Users.Single(x => x.ID == i.Poster).Name;
                    feedPosts.Email= context.Users.Single(x => x.ID == i.Poster).Email;
                    feedPosts.Post = i;
                    feed.Add(feedPosts);
                }
            }
          List<FeedPosts> sorted =  feed.OrderByDescending(y => y.Post.Date).ToList();
            return sorted;
        }
    }
}
