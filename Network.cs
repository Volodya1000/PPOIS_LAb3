using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Network
    {
        private Dictionary<string, User> DictUsers = new Dictionary<string, User>();

        private Dictionary<string, string> DictUserPasswords = new Dictionary<string, string>();

        private Dictionary<string, Channel> DictChannels = new Dictionary<string, Channel>();
        public string AddUser(string Login, string Password)
        {
            string ret = CheckLoginAndPassword(Login, Password);
            if (ret != "") return ret;
            else
            {
                if (Password.StartsWith("admin"))
                {
                    Admin NewUser = new Admin(Login,this);
                    DictUsers.Add(Login, NewUser);
                    DictUserPasswords.Add(Login, Password);
                    return "Аккаунт администратора создан";
                }
                else
                {
                    User NewUser = new User(Login);
                    DictUsers.Add(Login, NewUser);
                    DictUserPasswords.Add(Login, Password);
                    return "Аккаунт создан";
                }
            }
        }

        public string CheckLoginAndPassword(string Login, string Password)
        {
            if (Login == "" | Password == "")
                return "Логин или пароль не введён";
            else if (DictUsers.ContainsKey(Login))
                return "Данный логин уже занят";
            else if (Password.Length < 8)
                return "Длина пароля должна быть минимум 8 символов";
            else return "";
        }

        public bool IsAdmin(string Login)
        {
            if ( DictUsers[Login] is Admin)
                return true;
            return false;
        }

        public void DeleteUser(string Login, string deluser)
        {
            if(IsAdmin(Login)&& DictUsers.ContainsKey(deluser))
            {
                DictUsers[deluser].RemoveDialogs();
                foreach (string i in DictUsers[deluser].GetListFriends())
                {
                    DictUsers[i].RemoveFriend(deluser);
                }
                DictUsers.Remove(deluser);
            }
        }
        public string SendFriendshipRequest(string Login, string Password, string RecipientLogin)
        {
            if (DictUsers.TryGetValue(Login, out User LoginUser) && DictUsers.TryGetValue(RecipientLogin, out User RecipientUser))
            {
                //Проверить что уже являются друзьями

                if (LoginUser.CheckFriend(RecipientLogin))
                    return RecipientLogin + " уже ваш друг";
                else
                {
                    Request NewReqest = new Request( "", Login, RecipientLogin, this);
                    RecipientUser.AddNotification(NewReqest);
                    return "Запрос на дружбу отправлен";
                }
            }
            return "";
        }

        public bool CheckAccount(string Login, string Password)
        {
            return DictUserPasswords.ContainsKey(Login) && DictUserPasswords[Login] == Password;
        }

        public void AddDialog(string sender, string recipient, Request req)
        {
            if (DictUsers.TryGetValue(sender, out User SenderUser) && DictUsers.TryGetValue(recipient, out User RecipientUser))
            {
                Dialog NewDialog = new Dialog();
                SenderUser.AddDialog(RecipientUser.Login, NewDialog);
                RecipientUser.AddDialog(SenderUser.Login, NewDialog);
                RecipientUser.RemoveNotification(req);
            }
        }

        public List<string> GetAllUsers()
        {
            return DictUsers.Keys.ToList();
        }

        public List<Request> GetRequests(string Login, string Password)
        {
            if (DictUserPasswords.ContainsKey(Login) && DictUserPasswords[Login] == Password)
            {
                return DictUsers[Login].GetListNotification();
            }
            return null;
        }

        public List<string> GetFriends(string Login, string Password)
        {
            if (DictUserPasswords.ContainsKey(Login) && DictUserPasswords[Login] == Password)
            {
                return DictUsers[Login].GetListFriends();
            }
            return null;
        }

        public void MakeFriends(string f1, string f2)
        {
            if (DictUsers.TryGetValue(f1, out User F1) && DictUsers.TryGetValue(f2, out User F2))
            {
                F1.AddFriend(f2);
                F2.AddFriend(f1);
            }
        }

        public void DeleteRequest(string login, Request request)
        {
            DictUsers[login].RemoveNotification(request);
        }

        //Возможность отправлять сообщения только если получатель является другом
        public void SendMessage(string Login, string Password, string RecipientLogin, Message message)
        {
            if (DictUsers.TryGetValue(Login, out User SenderUser) &&
                DictUsers.TryGetValue(RecipientLogin, out User RecipientUser) &&
                DictUserPasswords[Login] == Password &&
                SenderUser.CheckFriend(RecipientLogin))
            {
                DictUsers[RecipientLogin].AddMessage(message, Login);
            }
        }

        public List<Message> GetAllMessages(string Login, string Password, string RecipientLogin)
        {
            if (DictUsers.TryGetValue(Login, out User SenderUser) &&
                DictUsers.TryGetValue(RecipientLogin, out User RecipientUser) &&
                DictUserPasswords[Login] == Password &&
                SenderUser.CheckFriend(RecipientLogin))
            {
                return DictUsers[Login].GetAllMesaggesFromDialog(RecipientLogin);
            }
            return null;
        }

        public void CreateChannel(string name, string login,string password, ChannelType type)
        {
            if (DictUserPasswords.ContainsKey(login) && DictUserPasswords[login] == password)
            {
                Lab3.Channel NewChannel = new Lab3.Channel(name, login, type);
                DictChannels.Add(name, NewChannel);
                DictUsers[login].Subscribe(name);
            }
        }

        public List<String> GetAllChannels(string Login, string Password)
        {
            if (DictUserPasswords.ContainsKey(Login) && DictUserPasswords[Login] == Password)
            {
                return DictChannels.Keys.ToList();
            }
            return null;
        }

        public List<String> GetUserChannels(string Login, string Password)
        {
            if (DictUserPasswords.ContainsKey(Login) && DictUserPasswords[Login] == Password)
            {
                return DictUsers[Login].GetChannels();
            }
            return null;
        }

        public bool CheckIsSubscriber(string Login, string Password, string Channel)
        {
            if (DictUserPasswords.ContainsKey(Login) && DictUserPasswords[Login] == Password)
            {
                return DictChannels[Channel].CheckIsSubscriber(Login);
            }
            return false;
        }

        public void Subscribe(string Login, string Password, string Channel)
        {
            if (DictUserPasswords.ContainsKey(Login) && DictUserPasswords[Login] == Password)
            {
                if(!CheckIsSubscriber(Login,  Password,  Channel))
                {
                    DictChannels[Channel].Subscribe(Login);
                    DictUsers[Login].Subscribe(Channel);
                }
            }
        }

        public void AddPublication(string Login, string Password, string Channel,string text)
        {
            if (DictUserPasswords.ContainsKey(Login) && 
                DictUserPasswords[Login] == Password&&
                DictChannels[Channel].IsRedactor(Login))
            {
                Publication NewPublication = new Publication(text, Login,"");
                DictChannels[Channel].AddPublication(Login,NewPublication);
            }
        }

        public List<Publication> GetAllPublications(string Login, string Password, string Channel)
        {
            if (DictUserPasswords.ContainsKey(Login) &&
                DictUserPasswords[Login] == Password &&
                DictChannels[Channel].CheckIsSubscriber(Login))
            {
                return DictChannels[Channel].GetAllPublications();
            }
            return null;
        }

        public bool CheckIsRedactor(string Login, string Password, string Channel)
        {
            if (DictUserPasswords.ContainsKey(Login) && DictUserPasswords[Login] == Password)
            {
                return DictChannels[Channel].IsRedactor(Login);
            }
            return false;
        }

    }
}
