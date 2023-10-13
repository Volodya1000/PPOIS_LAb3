using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Message
    {
        
        public Message(string text, string sender, string recipient )
        {
            DepartureTime= DateTime.Now;
            Text= text;
            Sender = sender;
            Recipient= recipient;
            
        }

        protected DateTime departuretime;//Дата отправления

        public DateTime DepartureTime
        {
            get { return departuretime; }
            set { departuretime = value; }
        }
        public string GetStringDate()
        {
            return DepartureTime.ToString("dd.MM.yyyy HH:mm"); 
        }

        protected string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        protected string Sender;
        protected string Recipient;

        public string GetSenderLogin()
        { return Sender; }

    }
}
