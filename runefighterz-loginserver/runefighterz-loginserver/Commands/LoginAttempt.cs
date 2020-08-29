using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loginserver.Commands
{
    class LoginAttempt
    {
        private string ipAddress;
        private string username;
        private string password;
        public LoginAttempt(IPEndPoint ipAddress, string username, string password)
        {
            this.ipAddress = ipAddress.ToString();
            this.username = username;
            this.password = password;
        }
        public string getUsername()
        {
            return username;
        }
        public string getPassword()
        {
            return password;
        }
        public string getIpAddress()
        {
            return ipAddress;
        }
    }
}
