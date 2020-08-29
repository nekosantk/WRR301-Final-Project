using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerroristSpawner : MonoBehaviour {
    private Vector3 spawnLocation;
    private Quaternion spawnRotation;
    private GameObject terroristPrefab;
    public GameObject spawnLocationObj;
    private int numEnemies;
    public int numEnemiesToSpawn;
	// Use this for initialization
	void Start () {
        spawnLocation = spawnLocationObj.transform.position;
        spawnRotation = spawnLocationObj.transform.rotation;
        terroristPrefab = Resources.Load("Prefabs/Enemies/Terrorist") as GameObject;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        InvokeRepeating("SpawnTerrorist", 1f, 2f);
    }
    private void SpawnTerrorist()
    {
        Debug.Log("Spawning enemies");
        if (numEnemies == numEnemiesToSpawn)
        {
            CancelInvoke();
        }
        else
        {
            Instantiate(terroristPrefab, spawnLocation, spawnRotation);
            numEnemies++;
        }
    }
    void FixedUpdate()
    {
        if (numEnemies == numEnemiesToSpawn)
        {
            Terrorist[] Terrorists = FindObjectsOfType(typeof(Terrorist)) as Terrorist[];
            foreach (Terrorist terroristInfo in Terrorists)
            {
            }
            if (Terrorists.Length == 0)
            {
                //All spawned enemies are dead
                //Disable boundary
                gameObject.SetActive(false);
            }
        }
    }
}