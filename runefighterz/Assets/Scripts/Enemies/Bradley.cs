using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bradley : MonoBehaviour {

    public GameObject rocketBody;
    public GameObject rocketFire;

    public Transform fireHole;
    public Transform exhaustPipe;

	// Use this for initialization
	void Start () {
        InvokeRepeating("FireRocket", 0f, 5f);
	}
    private void FireRocket()
    {
        GameObject temp = Instantiate(rocketBody, fireHole.position, fireHole.rotation);
        temp.transform.position = transform.position;
        Instantiate(rocketFire, exhaustPipe.position, exhaustPipe.rotation);
    }
    void FixedUpdate()
    {
        if (GetComponent<Enemy_Information>().healthAmount <= 0)
        {
            Debug.Log("ayyy");
            Destroy(gameObject);
        }
    }
}
