using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{


    private AudioSource audioSource;
    [Space]

    //Lucy
    public AudioClip leo_summon;
    public AudioClip bull_swoosh;
    public AudioClip virgo_swoosh;
    public AudioClip leo_swoosh;
    public AudioClip whip_swoosh;

    //Natsu
    public AudioClip firebeath_summon;

    //Rocket
    public AudioClip explosion_swoosh;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("gameVolume");
        audioSource = GetComponentInParent<AudioSource>();
    }
    public void PlaySound(string soundName)
    {
        Debug.Log("Playing sound :" + soundName);
        switch (soundName)
        {
            case "bull_swoosh":
                {
                    audioSource.clip = bull_swoosh;
                    audioSource.Play();
                }
                break;
            case "virgo_swoosh":
                {
                    audioSource.clip = virgo_swoosh;
                    audioSource.Play();
                }
                break;
            case "leo_swoosh":
                {
                    audioSource.clip = leo_swoosh;
                    audioSource.Play();
                }
                break;
            case "whip_swoosh":
                {
                    audioSource.clip = whip_swoosh;
                    audioSource.Play();
                }
                break;
            case "leo_summon":
                {
                    audioSource.clip = leo_summon;
                    audioSource.Play();
                }
                break;
            case "firebeath_summon":
                {
                    audioSource.clip = firebeath_summon;
                    audioSource.Play();
                }
                break;
            case "explosion_swoosh":
                {
                    audioSource.clip = explosion_swoosh;
                    audioSource.Play();
                }
                break;
        }
    }
}