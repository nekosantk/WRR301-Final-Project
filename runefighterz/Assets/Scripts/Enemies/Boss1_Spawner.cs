using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Spawner : MonoBehaviour {

    public GameObject boss1Prefab;
    private bool spawned = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (spawned)
        {
            return;
        }
        else
        {
            foreach (Transform child in transform.Find("Boundaries"))
            {
                child.gameObject.SetActive(true);
            }
            Instantiate(boss1Prefab, transform.position, transform.rotation, transform);
            spawned = true;
        }
    }
}
