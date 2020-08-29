using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loadbalancer.Handlers
{
    class MessageSender
    {
        private NetServer netServer;
        public MessageSender(NetServer netServer)
        {
            this.netServer = netServer;
        }
        public void SendServerIP(string clientIP, string serverIP)
        {
            Console.WriteLine("Sending aval server to client");
            Console.WriteLine(serverIP);
            SendUnconnected("serverrequest" + "∰" + serverIP, clientIP);
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
        #endregion
    }
}
