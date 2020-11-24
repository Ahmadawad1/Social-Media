using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using Microsoft.AspNet.SignalR;

namespace PL.Hubs
{
    public class ChatHub : Hub
    {
        Context context;
       public ChatHub()
        {
            context = new Context();
        }
        public void Send(int from, string message,int to)
        {
           SaveMesssage(from, message,to);
           
            Clients.All.callBack(message,from);
           
        }

        private void SaveMesssage(int from, string msg, int to)
        {
            Messages message = new Messages();
            message.Sender = from;
            message.Receiver = to;
            message.Content = msg;
            message.Date = DateTime.Now;
            context.Messages.Add(message);
            context.SaveChanges();
        }

      

    }
}