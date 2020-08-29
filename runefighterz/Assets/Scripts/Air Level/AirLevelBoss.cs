using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirLevelBoss : MonoBehaviour {
    void OnDestroy()
    {
        Time.timeScale = 0f;
        foreach (Transform child in GameObject.Find("Screens").transform)
        {
            if (child.gameObject.name == "winner_screen")
                child.gameObject.SetActive(true);
        }
        Debug.Log("Air boss was destroyed");
    }
}
