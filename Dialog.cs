using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Dialog
    {
        private List<Message> ListMessages = new List<Message>();

        public void AddMessage(Message message)
        {
            ListMessages.Add(message);
        }

        public List<Message> GetAllMessages()
        {
            return ListMessages;
        }
    }
}
