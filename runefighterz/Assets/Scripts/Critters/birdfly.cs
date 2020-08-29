using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdfly : MonoBehaviour {
    void Start ()
    {
        Invoke("KillBird", 180f);
    }
	void Update () {
        transform.position = new Vector3(transform.position.x + 1.5f * Time.deltaTime, transform.position.y, transform.position.z);
	}
    private void KillBird()
    {
        Destroy(gameObject);
    }
}
