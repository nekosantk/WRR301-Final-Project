using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_CannonBall : MonoBehaviour {

    public int damageAmount = 40;
    private bool moveBall = false;
	// Use this for initialization
	void Start () {
        Invoke("StartRolling", 0.8f);
        Invoke("SelfDestruct", 7f);
	}

	void Update () {
        if (moveBall)
        {
            transform.position = new Vector3(transform.position.x - 3.5f * Time.deltaTime, transform.position.y, transform.position.z);
        }
	}
    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
    private void StartRolling()
    {
        moveBall = true;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
        {
            return;
        }
        collider.gameObject.GetComponent<PlayerInformation_SP>().TakeDamage(damageAmount);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("virgo_swoosh");

    }
}
