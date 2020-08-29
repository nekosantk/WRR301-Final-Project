using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MP_CD : NetworkBehaviour {

    [SyncVar(hook = "UpdateTimeLeft")]
    public float timeLeft = 1f;

    [ServerCallback]
    void Update() {
        if (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            RpcEnableNotification("Draw");
        }
    }
    private void UpdateTimeLeft(float newTime)
    {
        timeLeft = newTime;
        GameObject.Find("text_timer").GetComponent<Text>().text = Convert.ToInt32(timeLeft).ToString();
    }
    [ClientRpc]
    public void RpcEnableNotification(string pushName)
    {
        foreach (Transform child in GameObject.Find("Notifications").transform)
        {
            if (pushName == child.name)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
        if (pushName == "Win")
        {
            PlayerInformation_MP[] playerInfoList = FindObjectsOfType(typeof(PlayerInformation_MP)) as PlayerInformation_MP[];
            foreach (PlayerInformation_MP playerInfo in playerInfoList)
            {
                if (playerInfo.health <= 0)
                {
                    GameObject.Find("text_winner").GetComponent<Text>().text = playerInfo.GetComponentInParent<GamePlayer>().playerName + " is the winner!";
                }
            }
        }
        Time.timeScale = 0f;
    }
}