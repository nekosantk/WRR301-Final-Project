using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class lucy_virgoattack_sp : MonoBehaviour {

    public int Damage = 20;

    public void Cleanup()
    {
        Debug.Log("Cleaning up VirgoAttack");
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<Enemy_Information>().TakeDamage(Damage);
        ColDetection(collider.gameObject.name + " took " + Damage + " damage");
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("virgo_swoosh");

    }
    public void ColDetection(string nameToPrint)
    {
        Debug.Log("Impact : " + nameToPrint);
    }
}