using Assets.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : NetworkLobbyManager
{

    static public LobbyManager s_Singleton;
    CustomLobbyHook _lobbyHook;

    void Start()
    {
        s_Singleton = this;
        _lobbyHook = GetComponent<CustomLobbyHook>();
        DontDestroyOnLoad(gameObject);
    }

    public override void OnLobbyClientExit()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            GameObject.Find("text_player2").GetComponent<Text>().text = "Player 2";
            GameObject.Find("image_selectedHero_2").GetComponent<Image>().sprite = GameObject.Find("MenuManager").GetComponent<MenuManager>().GetHeroPortrait("default");
            GameObject.Find("MenuManager").GetComponent<MenuManager>().EnableMenu("main");
            GameObject.Find("LobbyManager").GetComponent<NetworkDiscoveryCustom>().StopBroadcast();
        }
    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            base.OnServerDisconnect(conn);
            Debug.Log("Player timed out");
            GameObject.Find("text_player2").GetComponent<Text>().text = "Player 2";
            GameObject.Find("image_selectedHero_2").GetComponent<Image>().sprite = GameObject.Find("MenuManager").GetComponent<MenuManager>().GetHeroPortrait("default");
        }

    }
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        if (_lobbyHook)
        {
            _lobbyHook.OnLobbyServerSceneLoadedForPlayer(this, lobbyPlayer, gamePlayer);
        }
        return true;
    }
    public void StartGame()
    {
        ServerChangeScene(playScene);
    }
}