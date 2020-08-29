using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class LobbyHook : MonoBehaviour
{
    public virtual void OnLobbyServerSceneLoadedForPlayer(LobbyManager manager, GameObject lobbyPlayer, GameObject gamePlayer) {

    }
}
