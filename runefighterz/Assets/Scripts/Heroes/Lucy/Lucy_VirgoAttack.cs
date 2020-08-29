using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class lucy_virgoattack : NetworkBehaviour {

    public int Damage = 20;

    public void Cleanup()
    {
        Debug.Log("Cleaning up VirgoAttack");
        Destroy(gameObject);
    }
    [ServerCallback]
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<PlayerInformation_MP>().SpellDamage(Damage);
        RpcColDetection(collider.gameObject.GetComponent<GamePlayer>().playerName + " took " + Damage + " damage");
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("virgo_swoosh");
    }
    [ClientRpc]
    public void RpcColDetection(string nameToPrint)
    {
        Debug.Log("Impact : " + nameToPrint);
    }
}