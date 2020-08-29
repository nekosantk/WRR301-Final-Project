using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loginserver.Commands
{
    class StatUpdate
    {
        private string ipAddress;
        private string username;
        public string statName;
        public string statChange;
        public StatUpdate(IPEndPoint ipAddress, string username, string statName, string statChange)
        {
            this.ipAddress = ipAddress.ToString();
            this.username = username;
            this.statName = statName;
            this.statChange = statChange;
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