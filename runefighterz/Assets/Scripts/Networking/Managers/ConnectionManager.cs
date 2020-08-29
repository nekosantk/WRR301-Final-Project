using Assets.Scripts;
using Assets.Scripts.Managers;
using Lidgren.Network;
using System.Net;
using UnityEngine;

public class ConnectionManager : MonoBehaviour {

    private static NetServer netServer;
    private static MessageHandler messageHandler;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        Debug.Log("Network Manager started...");
        NetPeerConfiguration config = new NetPeerConfiguration("Runefighterz");
        config.SetMessageTypeEnabled(NetIncomingMessageType.UnconnectedData, true);
        config.Port = 14244;
        netServer = new NetServer(config);
        netServer.Start();
        messageHandler = (MessageHandler)GetComponentInParent(typeof(MessageHandler));
    }
    void Start()
    {
        GameObject.Find("NetworkManager").GetComponent<ClientManager>().GetAvalServer("killrequest");
        GameObject.Find("MenuManager").GetComponent<MenuManager>().StartCoroutine("AvalServerTimeout");
    }
    void FixedUpdate()
    {
        if (netServer.Status != NetPeerStatus.Running)
            return;
        RecieveMessages();
    }
    #region DO NOT EDIT
    private void RecieveMessages()
    {
        var inc = netServer.ReadMessage();
        if (inc != null)
        {
            switch (inc.MessageType)
            {
                case NetIncomingMessageType.UnconnectedData:
                    {
                        messageHandler.ProcessMessage(inc.ReadString(), inc);
                    }
                    break;
                case NetIncomingMessageType.Data:
                    {
                        messageHandler.ProcessMessage(inc.ReadString(), inc);
                    }
                    break;
            }
        }
    }
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
    public void SendUnconnected(string outMessage, string destURL)
    {
        string[] temp = destURL.Split(':');
        NetOutgoingMessage msg = netServer.CreateMessage();
        msg.Write(outMessage);
        IPEndPoint receiver = new IPEndPoint(NetUtility.Resolve(temp[0]), int.Parse(temp[1]));
        netServer.SendUnconnectedMessage(msg, receiver);
    }
    #endregion
}