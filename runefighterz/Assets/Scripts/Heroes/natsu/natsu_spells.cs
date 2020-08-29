using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class natsu_spells : NetworkBehaviour
{

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
        gamePlayer.m_Anim.Play("natsu_charge");
    }
    #endregion
    #region Dragon Breath
    public void Natsu_Dragonbreath()
    {
        gamePlayer.m_Anim.Play("natsu_dragonbreath");
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("firebeath_summon");
    }
    public void Natsu_SpawnFirebreath()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        bool m_FacingRight = GetComponentInParent<GamePlayer>().m_FacingRight;
        CmdSpawnFirebreath(m_FacingRight);
    }
    [Command]
    public void CmdSpawnFirebreath(bool m_FacingRight)
    {
        float offsetX = 0f;
        if (GetComponentInParent<GamePlayer>().m_FacingRight)
        {
            offsetX = 1f;
        }
        else
        {
            offsetX = -1f;

        }
        GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/firebreath_attack"),
            new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z), transform.rotation);
        NetworkServer.Spawn(enemy);
        if (!m_FacingRight)
        {
            RpcFlipDragonbreath(enemy);
        }
    }
    [ClientRpc]
    public void RpcFlipDragonbreath(GameObject temp)
    {
        Vector3 theScale = temp.transform.localScale;
        theScale.x *= -1;
        temp.transform.localScale = theScale;
        temp.GetComponent<natsu_dragonbreath>().m_FacingRight = GetComponentInParent<GamePlayer>().m_FacingRight;
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