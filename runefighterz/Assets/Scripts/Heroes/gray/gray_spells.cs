using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class gray_spells : NetworkBehaviour {

    private GamePlayer gamePlayer;
    private AudioSource audioSource;

    //Sounds
    public AudioClip jumpSound;

    void Start()
    {
        gamePlayer = GetComponentInParent<GamePlayer>();
        audioSource = GetComponentInParent<AudioSource>();
    }
    #region Charge
    public void Charge()
    {
        gamePlayer.m_Anim.Play("gray_charge");
    }
    #endregion
    #region Gray Icecannon
    public void Gray_Icecannon()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        gamePlayer.m_Anim.Play("gray_bazooka");
    }
    public void SpawnIceShards()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        bool m_FacingRight = GetComponentInParent<GamePlayer>().m_FacingRight;
        CmdSpawnIceshards(m_FacingRight);
    }
    [Command]
    public void CmdSpawnIceshards(bool m_FacingRight)
    {
        GameObject enemy;
        if (m_FacingRight)
        {
            enemy = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/iceshard_attack"),
                new Vector3(transform.position.x + 1.2f, transform.position.y + 1.2f, transform.position.z), transform.rotation);
        }
        else
        {
            enemy = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/iceshard_attack"),
                new Vector3(transform.position.x - 1.2f, transform.position.y + 1.2f, transform.position.z), transform.rotation);
        }

        NetworkServer.Spawn(enemy);
        if (!m_FacingRight)
        {
            RpcFlipIceshards(enemy);
        }
    }
    [ClientRpc]
    public void RpcFlipIceshards(GameObject temp)
    {
        Vector3 theScale = temp.transform.localScale;
        theScale.x *= -1;
        temp.transform.localScale = theScale;
        temp.GetComponent<gray_iceshards>().m_FacingRight = GetComponentInParent<GamePlayer>().m_FacingRight;
    }
    #endregion
    #region Utilities
    public void PlayJumpSound()
    {
        audioSource.clip = jumpSound;
        audioSource.Play();
    }
    #endregion
}
