using Assets.Scripts;
using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public string currentMenu = "login";
    public string currentStatus = "";
    public string username = "";
    public string password = "";
    public string email = "";

    //Portraits
    [SerializeField]
    public Sprite[] heroPortraits;

    void Start()
    {
        Invoke("KillIntroVid", 10f);
        if (GameObject.Find("ResourceManager").GetComponent<ResourceManager>().devMode)
        {
            EnableMenu("main");
        }
        else
        {
            if (GameObject.Find("ResourceManager").GetComponent<ResourceManager>().username != "")
            {
                EnableMenu("main");
            }
            else
            {
                Debug.Log("Loading login");
                EnableMenu("login");
            }
        }
    }
    private void KillIntroVid()
    {
        GameObject.Find("IntroVid").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("IntroVid").GetComponent<AudioSource>().enabled = false;
    }
    public void EnableMenu(string menuName)
    {
        //Debug.Log(menuName + " menu enabled");
        currentMenu = menuName;
        foreach (Transform child in GameObject.Find("Menus").transform)
        {
            if (currentMenu == child.name)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
        if (currentMenu == "login")
        {
        }
        if (currentMenu == "forgot")
        {
        }
        if (currentMenu == "main")
        {
            GameObject.Find("NetworkManager").GetComponent<ClientManager>().GetAvalServer("killrequest");
            if (GameObject.Find("ResourceManager").GetComponent<ResourceManager>().username != "")
            {
                if (GameObject.Find("MusicManager").GetComponent<AudioSource>().clip == null)
                {
                    GameObject.Find("MusicManager").GetComponent<MusicManager>().StartMusic();
                }
                KillIntroVid();
            }           
            EnableBackground("main");
            GameObject.Find("player_name").GetComponent<Text>().text = "Welcome " + GameObject.Find("ResourceManager").GetComponent<ResourceManager>().username;
            if (!GameObject.Find("ResourceManager").GetComponent<ResourceManager>().devMode || GameObject.Find("ResourceManager").GetComponent<ResourceManager>().username != "DevUser")
            {
                if (GameObject.Find("button_admin") != null)
                    GameObject.Find("button_admin").SetActive(false);
            }
        }
        if (currentMenu == "story")
        {
        }
        if (currentMenu == "multiplayer")
        {
        }
        if (currentMenu == "settings")
        {
            GameObject.Find("slider_sound").GetComponent<Slider>().value = PlayerPrefs.GetFloat("gameVolume");

            if (PlayerPrefs.GetInt("musicToggle") == 0)
            {
                GameObject.Find("toggle_music").GetComponent<Toggle>().isOn = false;
            }
            else
            {
                GameObject.Find("toggle_music").GetComponent<Toggle>().isOn = true;
            }
        }
        if (currentMenu == "achievements")
        {
            //Retrieve achievements
            GameObject.Find("NetworkManager").GetComponent<ClientManager>().GetAvalServer("achievementdata");
            StartCoroutine("AvalServerTimeout");
        }
        if (currentMenu == "statistics")
        {
            GameObject.Find("NetworkManager").GetComponent<ClientManager>().GetAvalServer("statdata");
            StartCoroutine("AvalServerTimeout");
        }
        if (currentMenu == "lobby")
        {
            EnableBackground("lobby");
        }
    }
    private void EnableBackground(string bgName)
    {
        foreach (Transform child in GameObject.Find("Backgrounds").transform)
        {
            if ("background_" + bgName == child.name)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    IEnumerator AvalServerTimeout()
    {
        yield return new WaitForSeconds(2f);
        if (currentStatus == "Connecting...")
        {
            SetLoginStatus("FAILED - Timed out");
        }
        else
        {
            SetLoginStatus("");
        }
    }
    public void SetLoginStatus(string status)
    {
        currentStatus = status;
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            GameObject.Find("text_loginstatus").GetComponent<Text>().text = status;
        }
    }
    public Sprite GetHeroPortrait(string heroName)
    {
        switch(heroName)
        {
            case "natsu":
                {
                    return heroPortraits[0];
                }
            case "lucy":
                {
                    return heroPortraits[1];
                }
            case "mirajane":
                {
                    return heroPortraits[2];
                }
            case "erza":
                {
                    return heroPortraits[3];
                }
            case "elfman":
                {
                    return heroPortraits[4];
                }
            case "mystogan":
                {
                    return heroPortraits[5];
                }
            case "makarov":
                {
                    return heroPortraits[6];
                }
            case "gray":
                {
                    return heroPortraits[7];
                }
            case "gajeel":
                {
                    return heroPortraits[8];
                }
            case "jellal":
                {
                    return heroPortraits[9];
                }
            case "juvia":
                {
                    return heroPortraits[10];
                }
        }
        return null;
    }

    #region Button Functions

    #region Login

    public void Login()
    {
        username = GameObject.Find("username").GetComponent<Text>().text;
        password = GameObject.Find("password").GetComponent<Text>().text;
        StartCoroutine("AvalServerTimeout");
        GameObject.Find("NetworkManager").GetComponent<ClientManager>().GetAvalServer("login");
    }
    public void Register()
    {
        EnableMenu("register");
    }
    public void ForgotPass()
    {
        EnableMenu("forgotpass");
    }

    #endregion

    #region Register

    public void SendRegistration()
    {
        username = GameObject.Find("username").GetComponent<Text>().text;
        password = GameObject.Find("password").GetComponent<Text>().text;
        email = GameObject.Find("email").GetComponent<Text>().text;
        StartCoroutine("AvalServerTimeout");
        GameObject.Find("NetworkManager").GetComponent<ClientManager>().GetAvalServer("registration");
    }
    public void BackToLogin()
    {
        EnableMenu("login");
    }
    public void OnClickCancelConnecting()
    {
        EnableMenu("main");
        GameObject.Find("LobbyManager").GetComponent<NetworkDiscoveryCustom>().StopBroadcast();
    }

    #endregion

    #region Forgotpass

    public void RetrievePass()
    {
        username = GameObject.Find("username").GetComponent<Text>().text;
        StartCoroutine("AvalServerTimeout");
        GameObject.Find("NetworkManager").GetComponent<ClientManager>().GetAvalServer("forgotpass");
    }

    #endregion

    #region Main Menu

    public void StoryMenu()
    {
        EnableMenu("story");
    }
    public void MultiplayerMenu()
    {
        EnableMenu("multiplayer");
    }
    public void AchievementsMenu()
    {
        EnableMenu("achievements");
    }
    public void StatisticsMenu()
    {
        EnableMenu("statistics");
    }
    public void SettingsMenu()
    {
        EnableMenu("settings");
    }
    public void AdminMenu()
    {
        EnableMenu("admin_menu");
    }
    public void QuitGame()
    {
        AudioListener.volume = 0f;
        if (!Application.isEditor) System.Diagnostics.Process.GetCurrentProcess().Kill();
    }

    #endregion

    #region Settings

    public void BackFromSettings()
    {
        SetLoginStatus("");
        EnableMenu("main");
    }
    public void AdjustSound()
    {
        PlayerPrefs.SetFloat("gameVolume", GameObject.Find("slider_sound").GetComponent<Slider>().value);
        AudioListener.volume = PlayerPrefs.GetFloat("gameVolume");
    }
    public void ToggleMusic()
    {
        if (GameObject.Find("toggle_music").GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("musicToggle", 1);
            GameObject.Find("MusicManager").GetComponent<AudioSource>().volume = 1;
        }
        else
        {
            PlayerPrefs.SetInt("musicToggle", 0);
            GameObject.Find("MusicManager").GetComponent<AudioSource>().volume = 0;
        }
    }

    #endregion

    #region Multiplayer

    public void OnClickCreateGame()
    {
        GameObject.Find("LobbyManager").GetComponent<NetworkDiscoveryCustom>().Initialize();
        GameObject.Find("LobbyManager").GetComponent<NetworkDiscoveryCustom>().StartAsServer();
        GameObject.Find("LobbyManager").GetComponent<NetworkLobbyManager>().StartHost();

        if (!GameObject.Find("LobbyManager").GetComponent<NetworkLobbyManager>().isNetworkActive)
        {
            return;
        }
        EnableMenu("lobby");
        foreach (Transform child in GameObject.Find("Backgrounds").transform)
        {
            if ("background_" + currentMenu == child.name)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    public void OnClickJoinGame()
    {
        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().networkAddress = GameObject.Find("manualIP").GetComponent<Text>().text;
        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().StartClient();

        if (!GameObject.Find("LobbyManager").GetComponent<NetworkLobbyManager>().isNetworkActive)
        {
            return;
        }
        EnableMenu("connecting");
    }
    public void OnClickAutoJoin()
    {
        Debug.Log("Finding Game");
        GameObject.Find("LobbyManager").GetComponent<NetworkDiscoveryCustom>().Initialize();
        GameObject.Find("LobbyManager").GetComponent<NetworkDiscoveryCustom>().StartAsClient();
        EnableMenu("connecting");
    }

    #endregion

    #region Lobby

    public void OnClickCancelLobby()
    {
        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().StopHost();
    }

    #endregion

    #region Admin Tools

    public void SearchUser()
    {
        Debug.Log("Searching for user");
        StartCoroutine("AvalServerTimeout");
        GameObject.Find("NetworkManager").GetComponent<ClientManager>().GetAvalServer("datarequest");
    }
    public void UpdateData()
    {
        Debug.Log("Updating user data");
        string newBanned = "";
        string newPassword = "";
        string newEmail = "";
        string newName = GameObject.Find("playernameToSearch").GetComponent<Text>().text;

        if (GameObject.Find("newbanned").GetComponent<Text>().text == "")
        {
            newBanned = GameObject.Find("data_banned").GetComponent<Text>().text;
        }
        else
        {
            newBanned = GameObject.Find("newbanned").GetComponent<Text>().text;
        }
        if (GameObject.Find("newpassword").GetComponent<Text>().text == "")
        {
            newPassword = GameObject.Find("data_password").GetComponent<Text>().text;
        }
        else
        {
            newPassword = GameObject.Find("newpassword").GetComponent<Text>().text;
        }
        if (GameObject.Find("newemail").GetComponent<Text>().text == "")
        {
            newEmail = GameObject.Find("data_email").GetComponent<Text>().text;
        }
        else
        {
            newEmail = GameObject.Find("newemail").GetComponent<Text>().text;
        }
        GameObject.Find("NetworkManager").GetComponent<ClientManager>().newName = newName;
        GameObject.Find("NetworkManager").GetComponent<ClientManager>().newBanned = newBanned;
        GameObject.Find("NetworkManager").GetComponent<ClientManager>().newPassword = newPassword;
        GameObject.Find("NetworkManager").GetComponent<ClientManager>().newEmail = newEmail;

        StartCoroutine("AvalServerTimeout");
        GameObject.Find("NetworkManager").GetComponent<ClientManager>().GetAvalServer("dataupdate");
    }

    #endregion

    #region Storymode

    public void FirePath()
    {
        SceneManager.LoadScene("FireLevel");
    }
    public void WaterPath()
    {
        SceneManager.LoadScene("WaterLevel");
    }
    public void AirPath()
    {
        SceneManager.LoadScene("AirLevel");
    }
    
    #endregion

    #endregion
}