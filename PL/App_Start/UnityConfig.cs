using BLL;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace PL
{
   
    public static class UnityConfig
    {
        


        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<ISignUp, SignUpLogic>();
            container.RegisterType<ISignIn, SignInLogic>();
            container.RegisterType<IMyProfile, MyProfileLogic>(); 
            container.RegisterType<IUser, UserLogic>(); 
            container.RegisterType<IChat, ChatLogic>();
            container.RegisterType<IFeed, FeedLogic>();
        }
    }
}