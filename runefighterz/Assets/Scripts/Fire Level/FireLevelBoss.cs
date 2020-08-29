using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLevelBoss : MonoBehaviour {
    void OnDestroy()
    {
        Time.timeScale = 0f;
        foreach (Transform child in GameObject.Find("Screens").transform)
        {
            child.gameObject.SetActive(true);
        }
        Debug.Log("Fire boss was destroyed");
    }
}
