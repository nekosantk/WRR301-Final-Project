using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class lucy_bullattack_sp : MonoBehaviour
{
    public int Damage = 20;
    public float dashDistance = 2f;

    public void Cleanup()
    {
        Debug.Log("Cleaning up BullAttack");
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<Enemy_Information>().TakeDamage(Damage);
        ColDetection(collider.gameObject.name + " took " + Damage + " damage");
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("bull_swoosh");
    }
    public void ColDetection(string nameToPrint)
    {
        Debug.Log("Impact : " + nameToPrint);
    }
    public void DashForward()
    {
        transform.position = new Vector3(transform.position.x + dashDistance, transform.position.y, transform.position.z);
    }
}