using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loginserver.Handlers
{
    class MessageSender
    {
        private PerformanceCounter cpuCounter;
        private NetServer netServer;

        public MessageSender(NetServer netServer)
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            this.netServer = netServer;
            ContactBalancer();
        }
        public void SendHeartbeat(string loadBalancerIP)
        {
            SendUnconnected("heartbeat∰" + getCurrentCpuUsage(), loadBalancerIP);
        }
        public void SendLoginState(string status, string error, string ipAddress)
        {
            SendUnconnected("loginstatus∰" + status + "∰" + error, ipAddress);
        }
        public void SendSaveData(string lastMap, string lastHero, string ipAddress)
        {
            SendUnconnected("savedata∰" + lastMap + "∰" + lastHero, ipAddress);
        }
        public void SendAchievementData(List<string> achivements, string ipAddress)
        {
            string messageToSend = "achievementdata";
            foreach (string achieved in achivements)
            {
                messageToSend += "∰" + achieved;
            }
            SendUnconnected(messageToSend, ipAddress);
        }
        public void SendStatData(List<string> statistics, string ipAddress)
        {
            string messageToSend = "statdata";
            foreach (string statistic in statistics)
            {
                messageToSend += "∰" + statistic;
            }
            SendUnconnected(messageToSend, ipAddress);
        }
        public void SendDataRequest(List<string> dataTot, string ipAddress)
        {
            string messageToSend = "datarequest";
            foreach (string data in dataTot)
            {
                messageToSend += "∰" + data;
            }
            SendUnconnected(messageToSend, ipAddress);
        }
        public void SendKillCount(string killCount, string ipAddress)
        {
            SendUnconnected("killrequest∰" + killCount, ipAddress);
        }

        #region DO NOT EDIT
        private void SendMessage(NetConnection destination, string command, string[] arguments)
        {
            string message = command + "∰";

            foreach (string x in arguments)
            {
                message += x + "∰";
            }
            NetOutgoingMessage sendMsg = netServer.CreateMessage();
            sendMsg.Write(message);
            netServer.SendMessage(sendMsg, destination, NetDeliveryMethod.ReliableOrdered);
        }
        private void SendUnconnected(string outMessage, string destURL)
        {
            string[] temp = destURL.Split(':');
            NetOutgoingMessage msg = netServer.CreateMessage();
            msg.Write(outMessage);
            IPEndPoint receiver = new IPEndPoint(NetUtility.Resolve(temp[0]), int.Parse(temp[1]));
            netServer.SendUnconnectedMessage(msg, receiver);
        }
        private void ContactBalancer()
        {
            Console.WriteLine("Contacting balance server");
            NetOutgoingMessage msg = netServer.CreateMessage();
            msg.Write("heartbeat∰50");
            IPEndPoint receiver = new IPEndPoint(NetUtility.Resolve("127.0.0.1"), 14242);
            netServer.SendUnconnectedMessage(msg, receiver);
        }
        private string getCurrentCpuUsage()
        {
            cpuCounter.NextValue();
            return cpuCounter.NextValue().ToString();
        }
        #endregion
    }
}