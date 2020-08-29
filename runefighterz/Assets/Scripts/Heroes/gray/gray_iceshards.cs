using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class gray_iceshards : NetworkBehaviour {

    public int Damage = 20;
    [SyncVar] public bool m_FacingRight = true;

    void Update()
    {
        if (m_FacingRight)
        {
            float posX = transform.position.x + 15 * Time.deltaTime;
            transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        }
        else
        {
            float posX = transform.position.x - 15 * Time.deltaTime;
            transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        }
    }
    [ServerCallback]
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<PlayerInformation_MP>().SpellDamage(Damage);
        RpcColDetection(collider.gameObject.GetComponent<GamePlayer>().playerName + " took " + Damage + " damage");
        RpcPlaySwoosh();
    }
    [ClientRpc]
    public void RpcColDetection(string nameToPrint)
    {
        Debug.Log("Impact : " + nameToPrint);
    }
    [ClientRpc]
    public void RpcPlaySwoosh()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("dragonbreath_swoosh");
    }
}
