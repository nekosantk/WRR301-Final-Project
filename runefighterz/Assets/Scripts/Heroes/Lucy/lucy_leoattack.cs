using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class lucy_leoattack : NetworkBehaviour
{
    public int Damage = 20;
    public float dashDistance = 2f;

    public void Cleanup()
    {
        Debug.Log("Cleaning up LeoAttack");
        Destroy(gameObject);
    }
    [ServerCallback]
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<PlayerInformation_MP>().SpellDamage(Damage);
        RpcColDetection(collider.gameObject.GetComponent<GamePlayer>().playerName + " took " + Damage + " damage");
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("leo_swoosh");
    }
    [ClientRpc]
    public void RpcColDetection(string nameToPrint)
    {
        Debug.Log("Impact : " + nameToPrint);
    }
    public void DashForward()
    {
        if (!isServer)
        {
            return;
        }
        transform.position = new Vector3(transform.position.x + dashDistance, transform.position.y, transform.position.z);
    }
    [ServerCallback]
    public void CmdSpawnSplash()
    {
        GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/leo_splash"), new Vector3(transform.position.x + dashDistance / 3, transform.position.y, transform.position.z),
            transform.rotation);
        NetworkServer.Spawn(temp);
        if (dashDistance < 0)
        {
            RpcFlipSplash(temp);
        }
    }
    [ClientRpc]
    public void RpcFlipSplash(GameObject temp)
    {
        Vector3 theScale = temp.transform.localScale;
        theScale.x *= -1;
        temp.transform.localScale = theScale;
    }
}