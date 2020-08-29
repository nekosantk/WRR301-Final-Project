using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loginserver.Commands
{
    class Registration
    {
        private string ipAddress;
        private string username;
        public string password;
        public string email;
        public Registration(IPEndPoint ipAddress, string username, string password, string email)
        {
            this.ipAddress = ipAddress.ToString();
            this.username = username;
            this.password = password;
            this.email = email;
        }
        public string getUsername()
        {
            return username;
        }
        public string getIpAddress()
        {
            return ipAddress;
        }
    }
}
