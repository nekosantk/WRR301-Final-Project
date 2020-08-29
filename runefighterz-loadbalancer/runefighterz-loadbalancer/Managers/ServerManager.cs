using runefighterz_loadbalancer.Commands;
using runefighterz_loadbalancer.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace runefighterz_loadbalancer.Managers
{
    class ServerManager
    {
        private Timer heartbeatCheck;
        private List<ServerInfo> onlineServers;
        private List<string> legitServers;
        private ServerInfo availableServer;
        private MessageSender messageSender;
        public ServerManager(MessageSender messageSender)
        {
            this.messageSender = messageSender;
            onlineServers = new List<ServerInfo>();
            legitServers = new List<string>();
            
            //Read all config parameters from config.ini
            string[] lines = File.ReadAllLines(@"config.ini", Encoding.UTF8);
            List<string> configParams = new List<string>();
            foreach (string x in lines)
            {
                configParams.Add(x.Split('=')[1]);
            }

            //Add each config parameter in order here
            foreach (string x in configParams)
            {
                legitServers.Add(x);
            }

            //Begin heartbeat checks
            heartbeatCheck = new Timer(HeartbeatCheck, null, 0, 5000);
        }
        public void Heartbeat(Heartbeat incCommand)
        {
            //Check if sever is on the whitelist
            if (legitServers.Contains(incCommand.ipAddress))
            {
                if (onlineServers.Count == 0)
                {
                    availableServer = new ServerInfo(incCommand.ipAddress, incCommand.cpuPercentage);
                    onlineServers.Add(availableServer);
                    Console.WriteLine("Adding server with ip: " + incCommand.ipAddress);
                }
                else
                {
                    foreach (ServerInfo x in onlineServers)
                    {
                        //Check if server is already listed as online
                        if (x.getIpAddress() == incCommand.ipAddress)
                        {
                            x.setCpuPercentage(incCommand.cpuPercentage);
                            x.ResetHeartbeat();
                        }
                        else
                        {
                            availableServer = new ServerInfo(incCommand.ipAddress, incCommand.cpuPercentage);
                            onlineServers.Add(availableServer);
                            Console.WriteLine("Adding server with ip: " + incCommand.ipAddress);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("WARNING: Heartbeat from unauthorized server :" + incCommand.ipAddress);
            }
        }
        public void ServerRequest(ServerRequest incCommand)
        {
            if (availableServer != null)
            {
                messageSender.SendServerIP(incCommand.ipAddress, availableServer.getIpAddress());
            }
            else
            {
                messageSender.SendServerIP(incCommand.ipAddress, "none");
            }
        }
        private void HeartbeatCheck(object sender)
        {
            Console.WriteLine("Checking for online servers...");
            Stack <ServerInfo> removeMe = new Stack<ServerInfo>();
            foreach (ServerInfo x in onlineServers)
            {
                x.reduceHeartbeat();
                if (x.getHeartbeat() <= 0)
                {
                    Console.WriteLine("Removing server :" + x.getIpAddress());
                    removeMe.Push(x);
                }
                else
                {
                    if (x.getCpuPercentage() < availableServer.getCpuPercentage())
                    {
                        availableServer = x;
                    }
                }
            }
            while (removeMe.Count > 0)
            {
                onlineServers.Remove(removeMe.Pop());
            }
            if (onlineServers.Count == 0)
            {
                availableServer = null;
            }
        }
    }
}
