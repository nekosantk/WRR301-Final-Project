using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireLevelManager : MonoBehaviour
{
    public void QuitGame()
    {
        Time.timeScale = 1f;
        try
        {
            GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddFireLevel();
            GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddMission();
        }
        catch { }
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGameLose()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}