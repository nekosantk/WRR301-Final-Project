using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArenaManager : MonoBehaviour {

    public void QuitGame()
    {
        Time.timeScale = 1f;

        if (GameObject.Find("text_winner").GetComponent<Text>().text.Split(' ')[0] == GameObject.Find("ResourceManager").GetComponent<ResourceManager>().username)
        {
            GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddWin();
            GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddKill();
        }
        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().StopHost();
        Network.Disconnect();
    }
    public void QuitGameDraw()
    {
        Time.timeScale = 1f;
        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().StopHost();
        Network.Disconnect();
    }
}
