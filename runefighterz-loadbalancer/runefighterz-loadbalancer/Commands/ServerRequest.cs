using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loadbalancer.Commands
{
    class ServerRequest
    {
        public string ipAddress;
        public ServerRequest(IPEndPoint ipAddress)
        {
            this.ipAddress = ipAddress.ToString();
        }
    }
}
