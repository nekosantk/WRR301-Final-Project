using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPopup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("SelfDestruct", 1f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f * Time.deltaTime, transform.position.z);
	}
    private void SelfDestruct()
    {
        Destroy(transform.parent.gameObject);
    }
}
