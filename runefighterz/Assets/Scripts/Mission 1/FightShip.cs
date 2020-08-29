using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightShip : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.Find("LevelManager").GetComponent<Mission1_Manager>().FightShip();
        Destroy(gameObject);
    }
}