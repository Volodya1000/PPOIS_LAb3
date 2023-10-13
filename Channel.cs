using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public enum ChannelType
    {
        Hobby,
        Art,
        Politic,
        Sport,
        Music
    }
    public class Channel
    {
        private string Creator;
        private List<string> ListRedactors = new List<string>();
        private List<string> ListSubscribers = new List<string>();
        private List<Publication> ListPublications = new List<Publication>();

        private string Name;

        private ChannelType type;

        public ChannelType Type
        {
            get { return type; }
            set { type = value; }
        }


        public string GetName()
        {
            return Name;
        }

        public  Channel(string name, string creator, ChannelType type)
        {
            Name = name;
            Creator = creator;
            Type = type;
            ListRedactors.Add(creator);
            ListSubscribers.Add(creator);
        }

        public List<Publication> GetAllPublications()
        {
            return ListPublications;
        }

        public bool CheckIsSubscriber(string user)
        {
            return ListSubscribers.Contains(user);
        }

        public void Subscribe(string user)
        {
            if(!CheckIsSubscriber(user))
            {
                ListSubscribers.Add(user);
            }
        }

        public bool IsRedactor(string user)
        {
            return ListRedactors.Contains(user);
        }

        public void AddPublication( string redactor, Publication publication)
        {
            if(IsRedactor(redactor))
            ListPublications.Add(publication);
        }



    }
}
