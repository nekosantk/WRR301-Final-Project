using Lidgren.Network;
using runefighterz_loginserver.Handlers;
using System;
using System.Threading;

namespace runefighterz_loginserver.Managers
{
    class NetworkManager
    {
        private NetServer netServer;
        private MessageHandler messageHandler;
        public NetworkManager()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("Runefighterz");
            config.SetMessageTypeEnabled(NetIncomingMessageType.UnconnectedData, true);
            config.Port = 14243;
            netServer = new NetServer(config);
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            netServer.RegisterReceivedCallback(new SendOrPostCallback(RecieveMessages));
            netServer.Start();
            messageHandler = new MessageHandler(netServer);
            while (true)
            {
                Thread.Sleep(100000);
            }
        }
        private void RecieveMessages(object peer)
        {
            var inc = netServer.ReadMessage();
            if (inc != null)
            {
                switch (inc.MessageType)
                {
                    case NetIncomingMessageType.UnconnectedData:
                        {
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = false;
                                messageHandler.ProcessMessage(inc.ReadString(), inc);
                            }).Start();
                        }
                        break;
                    case NetIncomingMessageType.Data:
                        {
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = false;
                                messageHandler.ProcessMessage(inc.ReadString(), inc);
                            }).Start();
                        }
                        break;
                }
            }
        }
    }
}