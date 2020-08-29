using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class lucy_spells : NetworkBehaviour
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

    #region Air Attack
    public void Lucy_AirAttack()
    {
        gamePlayer.m_Anim.Play("lucy_airattack");
    }
    public void Lucy_AirAttackCleanup()
    {
        if (gamePlayer.m_Grounded == true)
        {
            gamePlayer.m_Anim.Play("lucy_idle");
        }
        else
        {
            gamePlayer.m_Anim.CrossFade("lucy_fall", 0.6f);
        }
    }
    public void Lucy_SpawnAirAttack()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        bool m_FacingRight = GetComponentInParent<GamePlayer>().m_FacingRight;
        CmdSpawnAirAttack(m_FacingRight);
    }
    [Command]
    public void CmdSpawnAirAttack(bool m_FacingRight)
    {
        GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/lucy_air_attack"),
            new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        NetworkServer.Spawn(enemy);
        if (!m_FacingRight)
        {
            RpcFlipAirAttack(enemy);
        }
        RpcFollowAirAttack(enemy);
    }
    [ClientRpc]
    public void RpcFlipAirAttack(GameObject temp)
    {
        Vector3 theScale = temp.transform.localScale;
        theScale.x *= -1;
        temp.transform.localScale = theScale;
    }
    [ClientRpc]
    public void RpcFollowAirAttack(GameObject temp)
    {
        temp.GetComponent<lucy_airattack>().posToFollow = transform;
    }
    #endregion
    #region  Virgo attack
    public void Lucy_VirgoAttack()
    {
        gamePlayer.m_Anim.Play("lucy_summonvirgo");
    }
    public void Lucy_SpawnVirgo()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        float offsetX = 0f;
        if (GetComponentInParent<GamePlayer>().m_FacingRight)
        {
            offsetX = 5f;
        }
        else
        {
            offsetX = -5f;

        }
        CmdSpawnVirgo(offsetX);
    }
    [Command]
    public void CmdSpawnVirgo(float offsetX)
    {
        GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/virgo_attack"),
            new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z), transform.rotation);
        NetworkServer.Spawn(enemy);
    }
    #endregion
    #region Bull attack
    public void Lucy_BullAttack()
    {
        gamePlayer.m_Anim.Play("lucy_bullsummon");
    }
    public void Lucy_SpawnBull()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        bool m_FacingRight = GetComponentInParent<GamePlayer>().m_FacingRight;
        CmdSpawnBull(m_FacingRight);
    }
    [Command]
    public void CmdSpawnBull(bool m_FacingRight)
    {
        float offsetX;
        float dashDistance;
        if (m_FacingRight)
        {
            offsetX = 1f;
            dashDistance = 3f;
        }
        else
        {
            offsetX = -1f;
            dashDistance = -3f;
        }

        GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/bull_attack"),
            new Vector3(transform.position.x + offsetX, transform.position.y - 1.3f, transform.position.z), transform.rotation);
        enemy.GetComponent<lucy_bullattack>().dashDistance = dashDistance;

        NetworkServer.Spawn(enemy);

        if (!m_FacingRight)
        {
            RpcFlipBull(enemy);
        }
    }
    [ClientRpc]
    public void RpcFlipBull(GameObject temp)
    {
        Vector3 theScale = temp.transform.localScale;
        theScale.x *= -1;
        temp.transform.localScale = theScale;
    }
    #endregion
    #region Leo attack
    public void Lucy_LeoAttack()
    {
        gamePlayer.m_Anim.Play("lucy_summonleo");
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("leo_summon");
    }
    public void Lucy_SpawnLeo()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        bool m_FacingRight = GetComponentInParent<GamePlayer>().m_FacingRight;
        CmdSpawnLeo(m_FacingRight);
    }
    [Command]
    public void CmdSpawnLeo(bool m_FacingRight)
    {
        float offsetX;
        float dashDistance;
        if (m_FacingRight)
        {
            offsetX = 1.3f;
            dashDistance = 3f;
        }
        else
        {
            offsetX = -1.3f;
            dashDistance = -3f;
        }

        GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/leo_attack"),
            new Vector3(transform.position.x + offsetX, transform.position.y - 0.3f, transform.position.z), transform.rotation);
        enemy.GetComponent<lucy_leoattack>().dashDistance = dashDistance;

        NetworkServer.Spawn(enemy);

        if (!m_FacingRight)
        {
            RpcFlipLeo(enemy);
        }
    }
    [ClientRpc]
    public void RpcFlipLeo(GameObject temp)
    {
        Vector3 theScale = temp.transform.localScale;
        theScale.x *= -1;
        temp.transform.localScale = theScale;
    }
    #endregion
    #region Charge
    public void Charge()
    {
        gamePlayer.m_Anim.Play("lucy_charge");
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