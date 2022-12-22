using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    AudioSource audioSource;

    [SerializeField]
    AudioClip[] audioClips;

    void Start() => audioSource = GetComponent<AudioSource>();

    public void PlaySound(int WhichClip)
    {
        if(PlayerPrefs.GetInt("AudioMute") == 0)
        if (audioClips[WhichClip] != null)
        audioSource.PlayOneShot(audioClips[WhichClip]);
    }
}
