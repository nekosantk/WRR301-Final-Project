using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip[] musicTrack;

    private AudioSource audioSource;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        audioSource = GetComponentInParent<AudioSource>();
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("musicToggle", 1) == 0)
        {
            audioSource.volume = 0;
        }
    }
    public void StartMusic()
    {
        Debug.Log("Playing music");
        StartCoroutine(playSoundtrack());
    }
    IEnumerator playSoundtrack()
    {
        int Value = Random.Range(0,20);
        audioSource.clip = musicTrack[Value];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.clip = musicTrack[Value];
        audioSource.Play();
    }
}
