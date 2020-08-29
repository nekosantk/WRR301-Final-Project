using Assets.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyPlayer : NetworkLobbyPlayer
{

    [SyncVar(hook = "UpdateName")]
    public string playerName = "";

    [SyncVar(hook = "UpdateSelectedHero")]
    public string selectedHero = "";

    [SyncVar(hook ="UpdateTimeLeft")]
    public float timeLeft = 1f;

    void Awake()
    {
        DontDestroyOnLoad(this);
        //Used for testing with only 1 player - Instantly starts game upon hosting
        //mapIndex = UnityEngine.Random.Range(0, 9);
        //Debug.Log(mapIndex);
        //GameObject.Find("LobbyManager").GetComponent<LobbyManager>().StartGame();
    }
    void Start()
    {
        if (isLocalPlayer)
        {
            //Enable buttons
            if (PlayerPrefs.GetInt("fire", 0) != 0)
            {
                GameObject.Find("button_natsu").GetComponent<Button>().onClick.AddListener(() => CmdSetSelectedHero("natsu"));
            }
            else
            {
                GameObject.Find("button_natsu").GetComponent<Button>().interactable = false;
            }
            if (PlayerPrefs.GetInt("water", 0) != 0)
            {
                GameObject.Find("button_gray").GetComponent<Button>().onClick.AddListener(() => CmdSetSelectedHero("gray"));
            }
            else
            {
                GameObject.Find("button_gray").GetComponent<Button>().interactable = false;
            }
            GameObject.Find("button_lucy").GetComponent<Button>().onClick.AddListener(() => CmdSetSelectedHero("lucy"));
            //GameObject.Find("button_mirajane").GetComponent<Button>().onClick.AddListener(() => CmdSetSelectedHero("mirajane"));
            //GameObject.Find("button_erza").GetComponent<Button>().onClick.AddListener(() => CmdSetSelectedHero("erza"));
            //GameObject.Find("button_elfman").GetComponent<Button>().onClick.AddListener(() => CmdSetSelectedHero("elfman"));
            //GameObject.Find("button_mystogan").GetComponent<Button>().onClick.AddListener(() => CmdSetSelectedHero("mystogan"));
            //GameObject.Find("button_makarov").GetComponent<Button>().onClick.AddListener(() => CmdSetSelectedHero("makarov"));
            //GameObject.Find("button_gajeel").GetComponent<Button>().onClick.AddListener(() => CmdSetSelectedHero("gajeel"));
            //GameObject.Find("button_jellal").GetComponent<Button>().onClick.AddListener(() => CmdSetSelectedHero("jellal"));
            //GameObject.Find("button_juvia").GetComponent<Button>().onClick.AddListener(() => CmdSetSelectedHero("juvia"));
        }
    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            if (isServer && GameObject.Find("text_player2").GetComponent<Text>().text != "Player 2")
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    GameObject.Find("LobbyManager").GetComponent<LobbyManager>().StartGame();
                }
            }
            if (isServer && GameObject.Find("text_player2").GetComponent<Text>().text == "Player 2")
            {
                timeLeft = 1f;
            }
        }
    }
    public void UpdateName(string newName)
    {
        playerName = newName;
    }
    public void UpdateSelectedHero(string newHero)
    {
        Debug.Log(newHero + " selected");
        selectedHero = newHero;
        if (isLocalPlayer)
        {
            GameObject.Find("image_selectedHero_1").GetComponent<Image>().sprite = GameObject.Find("MenuManager").GetComponent<MenuManager>().GetHeroPortrait(newHero);
        }
        else
        {
            GameObject.Find("image_selectedHero_2").GetComponent<Image>().sprite = GameObject.Find("MenuManager").GetComponent<MenuManager>().GetHeroPortrait(newHero);
        }
    }
    public void UpdateTimeLeft(float newTime)
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            timeLeft = newTime;
            GameObject.Find("text_timeLeft").GetComponent<Text>().text = Convert.ToInt32(timeLeft).ToString();
        }
    }

    public override void OnClientEnterLobby()
    {
        base.OnClientEnterLobby();
        if (isLocalPlayer)
        {
            SetupLocalPlayer();
        }
        else
        {
            SetupOtherPlayer();
        }
    }
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        SetupLocalPlayer();
    }
    private void SetupLocalPlayer()
    {
        CmdSetName(GameObject.Find("ResourceManager").GetComponent<ResourceManager>().username);
        GameObject.Find("text_player1").GetComponent<Text>().text = GameObject.Find("ResourceManager").GetComponent<ResourceManager>().username;
    }
    private void SetupOtherPlayer()
    {
        OnClientReady(false);
        if (playerName != "")
        {
            GameObject.Find("text_player2").GetComponent<Text>().text = playerName;
            //Debug.Log("Other name :" + playerName);
        }
        if (selectedHero != "")
        {
            GameObject.Find("image_selectedHero_2").GetComponent<Image>().sprite = GameObject.Find("MenuManager").GetComponent<MenuManager>().GetHeroPortrait(selectedHero);
        }
    }
    [Command]
    public void CmdSetName(string newName)
    {
        playerName = newName;
        //Debug.Log("Connected :" + newName);
        if (GameObject.Find("text_player1").GetComponent<Text>().text == GameObject.Find("ResourceManager").GetComponent<ResourceManager>().username)
        {
            GameObject.Find("text_player2").GetComponent<Text>().text = playerName;
        }
    }
    [Command]
    public void CmdSetSelectedHero(string newHero)
    {
        selectedHero = newHero;
    }
}