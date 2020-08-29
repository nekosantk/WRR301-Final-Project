using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class lucy_spells_sp : MonoBehaviour
{
    private GamePlayer_SP gamePlayer;
    private AudioSource audioSource;

    //Sounds
    public AudioClip jumpSound;

    void Start()
    {
        gamePlayer = GetComponentInParent<GamePlayer_SP>();
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
        bool m_FacingRight = gamePlayer.m_FacingRight;
        GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/SP/lucy_air_attack"),
            new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        if (!m_FacingRight)
        {
            FlipAirAttack(enemy);
        }
    }
    public void FlipAirAttack(GameObject temp)
    {
        Vector3 theScale = temp.transform.localScale;
        theScale.x *= -1;
        temp.transform.localScale = theScale;
    }
    public void FollowAirAttack(GameObject temp)
    {
        temp.GetComponent<lucy_airattack>().posToFollow = transform;
    }
    #endregion
    #region  Virgo attack
    public void Lucy_VirgoAttack()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        gamePlayer.m_Anim.Play("lucy_summonvirgo");
    }
    public void Lucy_SpawnVirgo()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        float offsetX = 0f;
        if (gamePlayer.m_FacingRight)
        {
            offsetX = 5f;
        }
        else
        {
            offsetX = -5f;

        }
        GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/SP/virgo_attack"),
            new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z), transform.rotation);
    }
    #endregion
    #region Bull attack
    public void Lucy_BullAttack()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        gamePlayer.m_Anim.Play("lucy_bullsummon");
    }
    public void Lucy_SpawnBull()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        bool m_FacingRight = gamePlayer.m_FacingRight;
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

        GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/SP/bull_attack"),
            new Vector3(transform.position.x + offsetX, transform.position.y - 1.3f, transform.position.z), transform.rotation);
        enemy.GetComponent<lucy_bullattack_sp>().dashDistance = dashDistance;

        if (!m_FacingRight)
        {
            FlipBull(enemy);
        }
    }
    public void FlipBull(GameObject temp)
    {
        Vector3 theScale = temp.transform.localScale;
        theScale.x *= -1;
        temp.transform.localScale = theScale;
    }
    #endregion
    #region Leo attack
    public void Lucy_LeoAttack()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        gamePlayer.m_Anim.Play("lucy_summonleo");
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("leo_summon");
    }
    public void Lucy_SpawnLeo()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        bool m_FacingRight = gamePlayer.m_FacingRight;
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

        GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefabs/Attacks/SP/leo_attack"),
            new Vector3(transform.position.x + offsetX, transform.position.y - 0.3f, transform.position.z), transform.rotation);
        enemy.GetComponent<lucy_leoattack_sp>().dashDistance = dashDistance;

        if (!m_FacingRight)
        {
            FlipLeo(enemy);
        }
    }
    public void FlipLeo(GameObject temp)
    {
        Vector3 theScale = temp.transform.localScale;
        theScale.x *= -1;
        temp.transform.localScale = theScale;
    }
    #endregion
    #region Charge
    public void Charge()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        gamePlayer.m_Anim.Play("lucy_charge");
        StartCoroutine("UnfreezeCharge");
    }
    private IEnumerator UnfreezeCharge()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Unfreezing");
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
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