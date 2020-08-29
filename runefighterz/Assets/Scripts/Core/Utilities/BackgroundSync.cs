using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BackgroundSync : NetworkBehaviour
{
    [SyncVar]
    public int mapIndex;

    [Server]
    void Awake()
    {
        mapIndex = Random.Range(1, 10);
        StartCoroutine("BeginGame", 4f);
    }
    void Start()
    {
        GameObject.Find("Background").GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animators/arenas/arena_"
            + mapIndex) as RuntimeAnimatorController;
    }
    IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(2f);
        CmdStartGame();
    }
    [Command]
    public void CmdStartGame()
    {
        RpcStartGame();
    }
    [ClientRpc]
    public void RpcStartGame()
    {
    }
}