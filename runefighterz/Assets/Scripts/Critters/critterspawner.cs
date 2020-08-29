using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class critterspawner : MonoBehaviour {

    public GameObject critterPrefab;
    public float timeBetweenSpawns;
    public float spawnDelay;
    // Use this for initialization
    void Start () {
        InvokeRepeating("SpawnCritter", spawnDelay, timeBetweenSpawns);
    }
    private void SpawnCritter()
    {
        Instantiate(critterPrefab, transform.position, transform.rotation, transform);
    }
}
