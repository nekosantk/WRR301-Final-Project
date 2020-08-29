using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class lucy_leoattack_sp : MonoBehaviour
{
    public int Damage = 20;
    public float dashDistance = 2f;

    public void Cleanup()
    {
        Debug.Log("Cleaning up LeoAttack");
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<Enemy_Information>().TakeDamage(Damage);
        ColDetection(collider.gameObject.name + " took " + Damage + " damage");
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("leo_swoosh");
    }
    public void ColDetection(string nameToPrint)
    {
        Debug.Log("Impact : " + nameToPrint);
    }
    public void DashForward()
    {
        transform.position = new Vector3(transform.position.x + dashDistance, transform.position.y, transform.position.z);
    }
    public void CmdSpawnSplash()
    {
        GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/SP/leo_splash"), new Vector3(transform.position.x + dashDistance / 3, transform.position.y, transform.position.z),
            transform.rotation);
        if (dashDistance < 0)
        {
            FlipSplash(temp);
        }
    }
    public void FlipSplash(GameObject temp)
    {
        Vector3 theScale = temp.transform.localScale;
        theScale.x *= -1;
        temp.transform.localScale = theScale;
    }
}