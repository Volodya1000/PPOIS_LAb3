using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Publication : Message
    {
        private List<string> UsersLikes = new List<string>();
        private List<string> UsersDisLikes = new List<string>();
        public Publication( string content, string sender, string recipient) : base( content, sender, recipient)
        {
            
        }

        public void AddLike(string user)
        {
            if (!UsersLikes.Contains(user))
            {
                if (UsersDisLikes.Contains(user))
                    UsersDisLikes.Remove(user);
                UsersLikes.Add(user);
            }
        }
        public void AddDisLike(string user)
        {
            if (!UsersDisLikes.Contains(user))
            {
                if (UsersLikes.Contains(user))
                    UsersLikes.Remove(user);
                UsersDisLikes.Add(user);
            }
        }

        public void RemoveLike(string user)
        {
            if (UsersLikes.Contains(user))
                UsersLikes.Remove(user);
        }

        public void RemoveDisLike(string user)
        {
            if (UsersDisLikes.Contains(user))
                UsersDisLikes.Remove(user);
        }

        public int GetLikeCount()
        {
            return UsersLikes.Count();
        }

        public int GetDisLikeCount()
        {
            return UsersDisLikes.Count();
        }
    }


}
