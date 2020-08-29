using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loadbalancer
{
    class ServerInfo
    {
        private string ipAddress;
        private int heartbeatCount;
        private int cpuPercentage;
        public ServerInfo (string ipAddress, int cpuPercentage)
        {
            heartbeatCount = 2;
            this.ipAddress = ipAddress;
            this.cpuPercentage = cpuPercentage;
        }
        public void ResetHeartbeat()
        {
            heartbeatCount = 2;
        }
        public string getIpAddress()
        {
            return ipAddress;
        }
        public int getHeartbeat()
        {
            return heartbeatCount;
        }
        public int getCpuPercentage()
        {
            return cpuPercentage;
        }
        public void setCpuPercentage(int cpuPercentage)
        {
            this.cpuPercentage = cpuPercentage;
        }
        public void reduceHeartbeat()
        {
            --heartbeatCount;
        }
    }
}
