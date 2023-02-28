using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioPlayer;

    [SerializeField]
    private float genericVolumeMulitplier = 1;

    [Header("UI Sounds")]
    public AudioClip uiClickedSound;
    public AudioClip uiCloseSound;
    public AudioClip uiPauseSound;
    public AudioClip uiPlaySound;
    public AudioClip uiSuccessSound;
    public AudioClip uiFailSound;
    public AudioClip uiTapToPlaySound;
    public AudioClip uiTappedSound;
    public AudioClip bombSpawnSound;
    public AudioClip bombBlastSound;
    public AudioClip bombHitSound;
    public AudioClip tapBombSound;
    public AudioClip bombCallSound;
    public AudioClip headTransitionSound;
    public AudioClip headSpawnSound;
    public AudioClip sparkSound;
    public AudioClip bossSwapSound;
    public AudioClip bossDuplicateSound;
    public AudioClip bossDeathSound;
    public AudioClip coinGoingSound;
    public AudioClip coinRestingSound;
    public AudioClip cryptoCoinSound;
    public AudioClip uiRewardSound;
    public AudioClip tiktik;
    public AudioClip missileShoot;
    public AudioClip cooldown;
    public AudioClip[] destructionSounds;
    public AudioClip[] torpedoSounds;
    public AudioClip[] swimmingSounds;
    public AudioClip[] hullSounds;
    public AudioClip[] subEngineSounds;
    public AudioClip[] zombieDeathSouds;
    public AudioClip[] eyePopSound;
    public AudioClip[] headPopSound;
    public AudioClip[] headCrushSound;
    [Header("Audio Clips")]
    public AudioClip axe1;
    public static SoundManager Instance;
    public GameObject soundSource;

    void Awake ()
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
    public void PlayPopSound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            audioPlayer.PlayOneShot(headPopSound[Random.Range(0, headPopSound.Length - 1)], GameManager.Instance.soundValue * intensity);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayHeadPopSounds(int count)
    {
        StartCoroutine(PlayHeadPopSound(Mathf.Clamp(count, 0, headPopSound.Length)));
    }

    public IEnumerator PlayHeadPopSound(int headPopCount, float intensity = 2.0f)
    {
        List<AudioClip> headPopSoundsList = new List<AudioClip>();
        
        for (int n = 0; n < headPopSound.Length; n++)
        {
            headPopSoundsList.Add(headPopSound[n]);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < headPopCount; i++)
        {
            int rnadomIndex = Random.Range(0, headPopSoundsList.Count);
            AudioClip clip = headPopSoundsList[rnadomIndex];

            if (audioPlayer)
            {
                audioPlayer.PlayOneShot(clip, GameManager.Instance.soundValue * intensity);
            }

            headPopSoundsList.Remove(clip);
            PlayZombvieDeathSound(0.8f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void PlayEyePopSound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            audioPlayer.PlayOneShot(eyePopSound[Random.Range(0, eyePopSound.Length - 1)], GameManager.Instance.soundValue * intensity);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayZombvieDeathSound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            audioPlayer.PlayOneShot(zombieDeathSouds[Random.Range(0, zombieDeathSouds.Length - 1 )], GameManager.Instance.soundValue * intensity);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlaySparkSound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            audioPlayer.PlayOneShot(sparkSound, GameManager.Instance.soundValue * intensity);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayCoinGoing(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            audioPlayer.PlayOneShot(coinGoingSound, GameManager.Instance.soundValue * intensity);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayCoinResting(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            audioPlayer.PlayOneShot(coinRestingSound, GameManager.Instance.soundValue * intensity);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayTikTik(float intensity = 1.0f)
    {
        if (GameManager.Instance.soundValue < 0)
        {
            return;
        }
        audioPlayer.PlayOneShot(tiktik, intensity);
    }


    public void PlayCryptoCoin(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            audioPlayer.PlayOneShot(cryptoCoinSound, GameManager.Instance.soundValue * intensity);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayUIClickedSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(uiClickedSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }
    
    public void PlayRewardPanelSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(uiRewardSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayUICloseSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(uiCloseSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayPauseSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(uiPauseSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayPlaySound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(uiPlaySound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlaySuccessSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(uiSuccessSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayFailSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(uiFailSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayAxe1Sound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(axe1, GameManager.Instance.soundValue * intensity);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayTaptoplaySound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(uiTapToPlaySound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayTappedSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(uiTappedSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayBombSpawnSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(bombSpawnSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayBombBlastSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(bombBlastSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayBombHitSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(bombHitSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayTapBombSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(tapBombSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayBossSwapSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(bossSwapSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }  
    
    public void PlayBossDuplicateSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(bossDuplicateSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayBossDeathSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(bossDeathSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayBombCallSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(bombCallSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayHeadMoveSound()
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(headTransitionSound, GameManager.Instance.soundValue * genericVolumeMulitplier);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }

    public void PlayHeadSapwnSound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            if (uiClickedSound)
            {
                audioPlayer.PlayOneShot(headSpawnSound, GameManager.Instance.soundValue * intensity);
            }
            else
                Utility.ErrorLog("Sound of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }
    //fire sound
    public void PlayFireSound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            
                audioPlayer.PlayOneShot(missileShoot, GameManager.Instance.soundValue * intensity);
            
            
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }
    //torpedo firesound
    //destruction sound
    public void PlayTorpedoSound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            audioPlayer.PlayOneShot(torpedoSounds[Random.Range(0, torpedoSounds.Length)], GameManager.Instance.soundValue * intensity);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }
    //destruction sound
    public void PlayDestructionSound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            audioPlayer.PlayOneShot(destructionSounds[Random.Range(0, destructionSounds.Length)], GameManager.Instance.soundValue * intensity);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }
    public void PlaySwimSound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            audioPlayer.PlayOneShot(swimmingSounds[Random.Range(0, swimmingSounds.Length)], GameManager.Instance.soundValue * intensity);
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }
    public void PlayHullSound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            int randomIndex = Random.Range(0, subEngineSounds.Length);
            audioPlayer.PlayOneShot(hullSounds[randomIndex], GameManager.Instance.soundValue * intensity);
            soundSource.GetComponent<AudioSource>().clip = subEngineSounds[randomIndex];
            soundSource.GetComponent<AudioSource>().loop = true;
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }
    public void PlaySubSound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {
            int randomIndex = Random.Range(0,subEngineSounds.Length);
            audioPlayer.PlayOneShot(subEngineSounds[randomIndex], GameManager.Instance.soundValue * intensity);
            soundSource.GetComponent<AudioSource>().clip = subEngineSounds[randomIndex];
            soundSource.GetComponent<AudioSource>().loop = true;
        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }
    public void PlayCooldownSound(float intensity = 1.0f)
    {
        if (audioPlayer)
        {

            audioPlayer.PlayOneShot(cooldown, GameManager.Instance.soundValue * intensity);


        }
        else
            Utility.ErrorLog("Audio Source of " + this.gameObject.name + " in SoundManager.cs is not assigned", 1);
    }
}