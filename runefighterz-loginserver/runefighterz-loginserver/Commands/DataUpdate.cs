using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loginserver.Commands
{
    class DataUpdate
    {
        private string ipAddress;
        public string username;
        public string banned;
        public string password;
        public string email;

        public DataUpdate(IPEndPoint ipAddress, string username, string banned, string password, string email)
        {
            this.ipAddress = ipAddress.ToString();
            this.username = username;
            this.banned = banned;
            this.password = password;
            this.email = email;
        }
        public string getIpAddress()
        {
            return ipAddress;
        }
    }
}
