using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteMusic : MonoBehaviour
{
    public static LevelCompleteMusic Instance;

    [SerializeField]
    private AudioSource audioPlayer;
    [SerializeField]
    private float genericVolumeMulitplier = 1f;

    public AudioClip[] SuccessMusic;
    public AudioClip[] FailMusic;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        if (!audioPlayer)
        {
            if (GetComponent<AudioSource>())
            {
                audioPlayer = GetComponent<AudioSource>();
            }
        }
    }

    public void PlaySuccessMusic()
    {
        if (audioPlayer)
        {
            if (SuccessMusic.Length > 0)
            {
                audioPlayer.clip = SuccessMusic[Random.Range(0, SuccessMusic.Length)];
                audioPlayer.Play();
            }
            else
                Utility.ErrorLog("Main Menu of " + this.gameObject.name + " in MusicManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in MusicManager.cs is not assigned", 1);
    }

    public void PlayFailMusic()
    {
        if (audioPlayer)
        {
            if (FailMusic.Length > 0)
            {
                audioPlayer.clip = FailMusic[Random.Range(0, FailMusic.Length)];
                audioPlayer.Play();
            }
            else
                Utility.ErrorLog("Ingame of " + this.gameObject.name + " in MusicManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in MusicManager.cs is not assigned", 1);
    }
}