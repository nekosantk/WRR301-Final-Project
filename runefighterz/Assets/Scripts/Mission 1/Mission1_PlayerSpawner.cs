using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1_PlayerSpawner : MonoBehaviour {

    public GameObject playerObject;
    public GameObject playerHUD;
	void Start () {
        string selectedHero = GameObject.Find("ResourceManager").GetComponent<ResourceManager>().selectedHero;
        Debug.Log("Loading " + selectedHero);
        playerHUD = Instantiate(Resources.Load("Prefabs/HUD/" + selectedHero + "_HUD")) as GameObject;
        playerHUD.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 playerSpawnLocation = GameObject.Find("PlayerSpawnpoint").transform.position;
        Quaternion playerSpawnRotation = GameObject.Find("PlayerSpawnpoint").transform.rotation;
        playerObject = Instantiate(Resources.Load("Prefabs/Heroes/" + selectedHero) as GameObject, playerSpawnLocation, playerSpawnRotation);
        playerObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animators/" + selectedHero + "/" + selectedHero + "_main") as RuntimeAnimatorController;
        GameObject.Find("Main Camera").GetComponent<CameraControlScript>().player = playerObject;
        GameObject.Find("LevelManager").GetComponent<Mission1_Manager>().playerObject = playerObject;
    }
}
