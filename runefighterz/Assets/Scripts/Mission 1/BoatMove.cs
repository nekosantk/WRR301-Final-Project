using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.Find("LevelManager").GetComponent<Mission1_Manager>().MoveToWaypoint1();
        Destroy(gameObject);
    }
}