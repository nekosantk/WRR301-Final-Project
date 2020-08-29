using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loadbalancer.Commands
{
    class Heartbeat
    {
        public string ipAddress;
        public int cpuPercentage;
        public Heartbeat(IPEndPoint ipAddress, string cpuPercentage)
        {
            if (ipAddress.ToString() == "127.0.0.1:14243")
            {
                this.ipAddress = LocalIPAddress().ToString() + ":14243";
            }
            else
            {
                this.ipAddress = ipAddress.ToString();
            }
            this.cpuPercentage = int.Parse(cpuPercentage);
        }
        private IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}
