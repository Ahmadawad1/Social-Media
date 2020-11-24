using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ChatLogic : IChat
    {
        Context context;
        public ChatLogic()
        {
            context = new Context();
        }
        public List<Users> GetContacts(int id)
        {
            List<int> ids  = new List<int>();
            var v = context.Messages.Where(x => x.Receiver == id).ToList();
            foreach (var i in v)
            {
                if (ids.Contains(i.Sender)) continue;
                ids.Add(i.Sender);
            }
            var m = context.Messages.Where(x => x.Sender == id).ToList();
            foreach (var i in m)
            {
                if (ids.Contains(i.Receiver)) continue;
               ids.Add(i.Receiver);
            }
            List<Users> users = new List<Users>();
            foreach (var i in ids)
            {
                users.Add(context.Users.Single(X => X.ID == i));
            }
            return users;
        }

        public Conversation GetConversation(int myID, int contactID)
        {
            List<Messages> li = new List<Messages>();
            var to_me = context.Messages.Where(x => x.Receiver == myID && x.Sender == contactID).ToList();
            var from_me = context.Messages.Where(x => x.Sender == myID && x.Receiver == contactID).ToList();
            foreach (var i in to_me)
            {
                Messages m = new Messages();

                m.Content = i.Content;
                m.Date = i.Date;
                m.Sender = contactID;
                m.Receiver = myID;
                li.Add(m);
            }
            foreach (var i in from_me)
            {
                Messages m = new Messages();

                m.Content = i.Content; 
                m.Date = i.Date; 
                m.Sender =myID;
                m.Receiver = contactID;
                li.Add(m);
            }
            Conversation conversation = new Conversation();
            conversation.Messages = li;
            conversation.Messages.Sort((m1, m2) => DateTime.Compare(m1.Date, m2.Date));
            conversation.Contact = context.Users.Single(x => x.ID == contactID);
            return conversation;
        }
    }
}
