using Lidgren.Network;
using runefighterz_loadbalancer.Commands;
using runefighterz_loadbalancer.Handlers;
using runefighterz_loadbalancer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace runefighterz_loadbalancer
{
    class MessageHandler
    {
        private ServerManager serverManager;
        public MessageHandler(NetServer netServer)
        {
            serverManager = new ServerManager(new MessageSender(netServer));
        }
        public void ProcessMessage(string incMessage, NetIncomingMessage netIncMessage)
        {
            try
            {
                string[] splitMessage = incMessage.Split('∰');
                switch (splitMessage[0])
                {
                    case "heartbeat":
                        {
                            Console.WriteLine("Heartbeat");
                            Heartbeat incCommand = new Heartbeat(netIncMessage.SenderEndPoint, splitMessage[1]);
                            serverManager.Heartbeat(incCommand);
                        }
                        break;
                    case "serverrequest":
                        {
                            Console.WriteLine("Server requested");
                            ServerRequest incCommand = new ServerRequest(netIncMessage.SenderEndPoint);
                            serverManager.ServerRequest(incCommand);
                        }
                        break;
                }
            }
            catch
            {
                Console.WriteLine("ERROR: Message wrong format");
            }
        }
    }
}
