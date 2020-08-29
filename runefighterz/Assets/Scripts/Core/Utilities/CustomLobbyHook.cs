using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(LobbyManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        gamePlayer.GetComponent<GamePlayer>().playerName = lobbyPlayer.GetComponent<LobbyPlayer>().playerName;
        string selectedHero = lobbyPlayer.GetComponent<LobbyPlayer>().selectedHero;
        gamePlayer.GetComponent<GamePlayer>().selectedHero = selectedHero;

        if (selectedHero == "")
        {
            gamePlayer.GetComponent<GamePlayer>().selectedHero = "lucy";
        }
        else
        {
            gamePlayer.GetComponent<GamePlayer>().selectedHero = selectedHero;
        }
    }
}
