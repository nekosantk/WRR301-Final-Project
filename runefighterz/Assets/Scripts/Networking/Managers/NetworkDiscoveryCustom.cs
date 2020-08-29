using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkDiscoveryCustom : NetworkDiscovery
{
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if (!NetworkManager.singleton.IsClientConnected())
        {
            NetworkManager.singleton.networkAddress = fromAddress;
            NetworkManager.singleton.StartClient();
            GameObject.Find("MenuManager").GetComponent<MenuManager>().EnableMenu("lobby");
        }
    }
}
