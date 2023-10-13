using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Admin:User
    {
        Network network;
        public Admin(string login, Network network):base(login)
        {
            this.network = network;
        }

        public void DeleteUser(string deluser)
        {
            network.DeleteUser(Login, deluser);
        }
    }
}
