using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class lucy_airattack_sp : MonoBehaviour
{
    public int Damage = 20;
    public Transform posToFollow = null;
    void Start()
    {
        Invoke("Cleanup", 0.1f);
    }
    void Update()
    {
        if (posToFollow != null)
        {
            transform.SetParent(posToFollow);
        }
    }
    public void Cleanup()
    {
        Debug.Log("Cleaning up Airattack");
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<Enemy_Information>().TakeDamage(Damage);
        ColDetection(collider.gameObject.name + " took " + Damage + " damage");
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("whip_swoosh");
    }
    public void ColDetection(string nameToPrint)
    {
        Debug.Log("Impact : " + nameToPrint);
    }
    public void DashForward()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}