using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{

    private AudioSource audioSource1;
    private AudioSource audioSource2;
    private AudioSource[] audioSources;

    public AudioClip powerupPickupSound;

    public AudioClip enemySound;

    public AudioClip orbSound;

    public AudioClip speedSound;


    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        audioSource1 = audioSources[0];
        audioSource2 = audioSources[1];
    }

 

    public void playPowerupPickupSound()
    {
        playSound(powerupPickupSound, 0.95f, 0.80f);
    }

    public void playEnemySound()
    {
        playSound(enemySound, 1.3f, 0.80f);
    }

    public void playerKillsEnemySound()
    {
        playSound(enemySound, 5f, 0.80f);
    }

    public void playOrbSound()
    {
        playSound(orbSound, 0.74f, 0.50f, true);
    }

    public void playSpeedSound()
    {
        playSound(speedSound, 0.55f,0.90f);
    }

    private void playSound(AudioClip clip, float pitch, float vol)
    {
        playSound(clip, pitch, vol, false);
    }

    private void playSound(AudioClip clip, float pitch, float vol, bool shiftPitch)
    {
        AudioSource audioSource1 = getAvailableAudioSource();
        if (audioSource1 != null)
        {
            audioSource1.clip = clip;
            if (shiftPitch) {
             audioSource1.pitch = slightPitchShift(pitch);
            }
            else
            {
                audioSource1.pitch = pitch;
            }
            audioSource1.volume = vol;
            audioSource1.Play();
        }
    }

    private AudioSource getAvailableAudioSource()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                return audioSource;
            }
        }
        return audioSources[0];
    }

    private float slightPitchShift(float startingNumber)
    {
        return Random.Range(startingNumber - 0.2f, startingNumber + 0.2f);
    }
}
