using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerCameraToBoat : MonoBehaviour {

    private GameObject cameraObject;
    private Vector3 desPosition;
    void Start()
    {
        cameraObject = GameObject.Find("Main Camera");
        desPosition = new Vector3(transform.position.x, 10.0F, transform.position.z);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        float x = cameraObject.transform.position.x;
        float y = -0.7f;
        float z = cameraObject.transform.position.z;
        desPosition = new Vector3(x, y, z);
        cameraObject.transform.position = desPosition;
        Debug.Log("Lowering camera to boat level");
        Destroy(gameObject);
    }
}
