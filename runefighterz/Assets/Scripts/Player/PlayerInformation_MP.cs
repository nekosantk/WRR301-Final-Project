using Assets.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInformation_MP : NetworkBehaviour
{
    [SyncVar]
    public float health = 100;                  // The player's health.
    [SyncVar]
    public float mana = 100;

    public float repeatDamagePeriod = 2f;       // How frequently the player can be damaged.
    public AudioClip[] ouchClips;               // Array of clips to play when the player is damaged.

    [HideInInspector]
    public Slider healthBar;
    // Reference to the sprite renderer of the health bar.
    [HideInInspector]
    public Slider manaBar;

    private float lastHitTime;                  // The time at which the player was last hit.
    private float fillSpeed = 0.5f;

    void Update()
    {
        if (healthBar == null)
        {
            if (GetComponentInParent<GamePlayer>().isLocalPlayer)
            {
                GetComponentInParent<PlayerInformation_MP>().healthBar = GameObject.Find("healthbar_full_1").GetComponent<Slider>();
                GetComponentInParent<PlayerInformation_MP>().manaBar = GameObject.Find("mpbar_full_1").GetComponent<Slider>();
                GameObject.Find("hero_portraits_1").transform.Find(GetComponentInParent<GamePlayer>().selectedHero).gameObject.SetActive(true);
                GetComponentInParent<AudioListener>().enabled = true;
                GameObject.Find("name_1").GetComponent<Text>().text = GetComponent<GamePlayer>().playerName;
            }
            else
            {
                GetComponentInParent<PlayerInformation_MP>().healthBar = GameObject.Find("healthbar_full_2").GetComponent<Slider>();
                GetComponentInParent<PlayerInformation_MP>().manaBar = GameObject.Find("mpbar_full_2").GetComponent<Slider>();
                GameObject.Find("hero_portraits_2").transform.Find(GetComponentInParent<GamePlayer>().selectedHero).gameObject.SetActive(true);
                GetComponentInParent<AudioListener>().enabled = false;
                GameObject.Find("name_2").GetComponent<Text>().text = GetComponent<GamePlayer>().playerName;
            }
        }
        else
        {
            healthBar.value = Mathf.MoveTowards(healthBar.value, health / 100, Time.deltaTime * fillSpeed);
            manaBar.value = Mathf.MoveTowards(manaBar.value, mana / 100, Time.deltaTime * fillSpeed);
        }
    }
    [Command]
    public void CmdUpdateHP(float newHP)
    {
        health += newHP;

        if (health <= 0)
        {
            RpcDeath();
        }
        if (health > 100)
        {
            health = 100;
        }
    }
    [Command]
    public void CmdUpdateMana(float newMana)
    {
        mana += newMana;
    }
    [ClientRpc]
    public void RpcDeath()
    {
        string winnerName = "";
        string loserName = "";
        GamePlayer[] GamePlayers = FindObjectsOfType(typeof(GamePlayer)) as GamePlayer[];
        foreach (GamePlayer gamePlayer in GamePlayers)
        {
            string curName = gamePlayer.GetComponent<GamePlayer>().playerName;
            if (gamePlayer.playerName == curName)
            {
                winnerName = curName;
            }
            else
            {
                loserName = curName;
            }
        }
        if (GetComponentInParent<GamePlayer>().playerName == winnerName)
        {
            //Send win statistic
            GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddWin();
            Invoke("AddKill", 1f);
        }
        else
        {
            //Send loss statistic
            GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddLoss();
        }
        CmdEnableNotification("Win");
        CmdSetWinnerName(winnerName);
        Debug.Log(loserName + " has died");
    }
    private void AddKill()
    {
        GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddKill();
    }
    [Command]
    public void CmdSetWinnerName(string winnerName)
    {
        RpcSetWinnerName(winnerName);
    }
    [ClientRpc]
    public void RpcSetWinnerName(string winnerName)
    {
        GameObject.Find("text_winner").GetComponent<Text>().text = winnerName + " is the winner!";
    }
    [Command]
    public void CmdEnableNotification(string pushName)
    {
        RpcEnableNotification(pushName);
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
    public void InvQuitMatch()
    {
        Network.Disconnect();
    }
    public void SpellDamage(int damageAmount)
    {
        CmdUpdateHP(damageAmount * -1);
    }
}
