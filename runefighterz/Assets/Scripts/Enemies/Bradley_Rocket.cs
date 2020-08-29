using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bradley_Rocket : MonoBehaviour {

    public GameObject explosion_1;
    public float detonationDelay;
    void Start()
    {
        Invoke("Detonate", detonationDelay);
    }
    void Detonate()
    {
        Instantiate(explosion_1, new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z), new Quaternion());
        Destroy(gameObject);
    }
}