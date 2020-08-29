using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceManager : MonoBehaviour {

    //Local variables
    public string loadBalancerIP;
    public bool devMode;
    public string username;

    //Singeplayer
    public string selectedHero;
    public string selectedMap;

    //Achievements

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        Debug.Log("Reading config file");
        ReadConfig();
    }
    private void ReadConfig()
    {
        string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\config.ini");
        List<string> configParams = new List<string>();
        foreach (string x in lines)
        {
            configParams.Add(x.Split('=')[1]);
        }

        //Add each config parameter in order here

        //loadbalancer=ip:port
        loadBalancerIP = configParams[0];

        //devmode=true/false
        if (configParams[1] == "true")
        {
            devMode = true;
            SetupDevMode();
        }
        else
        {
            devMode = false;
        }
    }
    private void SetupDevMode()
    {
        username = "DevUser";
        selectedMap = "Mission 1";
        selectedHero = "Lucy";
        GameObject.Find("MenuManager").GetComponent<MenuManager>().username = username;
        GameObject.Find("NetworkManager").GetComponent<ClientManager>().loggedIn = true;
        //Set all achievements to true
    }
}
