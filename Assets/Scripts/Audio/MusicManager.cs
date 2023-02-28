using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    [SerializeField]
    private AudioSource audioPlayer;
    [SerializeField]
    private float genericVolumeMulitplier = 1f;

    [Header("Musics Backgrounds")]
    public AudioClip MainMenuMusic;
    public AudioClip[] IngameMusic;

    void Awake()
    {
        if (!audioPlayer)
        {
            if (GetComponent<AudioSource>())
            {
                audioPlayer = GetComponent<AudioSource>();
            }
        }
    }

    public void PlayMainMenuMusic()
    {
        if (audioPlayer)
        {
            if (MainMenuMusic)
            {
                audioPlayer.clip = MainMenuMusic;
                audioPlayer.Play();
            }
            else
                Utility.ErrorLog("Main Menu of " + this.gameObject.name + " in MusicManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in MusicManager.cs is not assigned", 1);
    }

    public void PlayIngameMusic()
    {
        if (audioPlayer)
        {
            if (MainMenuMusic)
            {
                audioPlayer.clip = IngameMusic[Random.Range(0, IngameMusic.Length)];
                audioPlayer.Play();
            }
            else
                Utility.ErrorLog("Ingame of " + this.gameObject.name + " in MusicManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in MusicManager.cs is not assigned", 1);
    }
}