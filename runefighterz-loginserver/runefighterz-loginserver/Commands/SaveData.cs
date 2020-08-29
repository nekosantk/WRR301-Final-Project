﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loginserver.Commands
{
    class SaveData
    {
        private string ipAddress;
        private string username;
        public SaveData(IPEndPoint ipAddress, string username)
        {
            this.ipAddress = ipAddress.ToString();
            this.username = username;

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
