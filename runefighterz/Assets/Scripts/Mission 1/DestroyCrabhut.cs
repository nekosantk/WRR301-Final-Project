using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCrabhut : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            return;
        }
        GameObject.Find("LevelManager").GetComponent<Mission1_Manager>().DestoryCrabhut();
        Destroy(gameObject);
    }
}
