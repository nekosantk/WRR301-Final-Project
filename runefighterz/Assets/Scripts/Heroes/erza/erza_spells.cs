using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class erza_spells : MonoBehaviour {

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
        gamePlayer.m_Anim.Play("erza_charge");
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
