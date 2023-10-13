using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class User
    {
        protected string login;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }
     

        public User(string login)
        {
            Login = login;
        }
        public  Dictionary<string,Dialog> DictDialogs = new Dictionary<string,Dialog>();

        public List<string> ListFriends = new List<string>();

        public List<Request> ListNotification = new List<Request>();

        public List<string> ListChannels = new List<string>();   

        public void RemoveFriend(string login)
        {
            if(ListFriends.Contains(login))
            ListFriends.Remove(login);
        }
        public void RemoveDialogs()
        {
            DictDialogs.Clear();
        }
        public void AddNotification(Request NewReqest)
        {
            ListNotification.Add( NewReqest);
        }
        public void AddMessage(Message message,string login)
        {
            DictDialogs[login].AddMessage(message);
        }

        public void AddDialog(string login, Dialog dialog)
        {
            DictDialogs.Add(login, dialog);
        }
        public void RemoveNotification(Request NewReqest)
        {
            if(ListNotification.Contains(NewReqest))
            ListNotification.Remove(NewReqest);
        }
        public bool CheckFriend(string Us)
        {
            return ListFriends.Contains(Us);
        }

        public void AddFriend(string Us)
        {
            if (!ListFriends.Contains(Us));
            ListFriends.Add(Us);
        }
        public List<string> GetListFriends()
        {
            return ListFriends;
        }

        public List<Request> GetListNotification()
        {
            return ListNotification;
        }

        public void DeleteFriend(string Us)
        {
            if (ListFriends.Contains(Us))
            ListFriends.Remove(Us);
        }

        public void Subscribe(string channel)
        {
            if(!ListChannels.Contains(channel))
            ListChannels.Add(channel);
        }

        public List<string> GetChannels()
        {
            return ListChannels;
        }

        public List<Message> GetAllMesaggesFromDialog(string login)
        {
            if (DictDialogs.ContainsKey(login))
                return DictDialogs[login].GetAllMessages();
             return null;

        }
    }
}
