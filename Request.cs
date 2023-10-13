using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Request:Message
    {
        private Network network;

        public Request( string content, string sender, string recipient, Network network) : base( content, sender, recipient)
        {
            this.network = network;
        }
        public void Take()//принять
        {
            network.MakeFriends(Sender,Recipient);
            network.AddDialog(Sender, Recipient, this);
        }

        public void Reject()//отклонить
        {
            network.DeleteRequest(Recipient, this);
        }
    }
}
