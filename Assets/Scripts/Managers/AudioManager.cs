using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, .4f);
    }

    public void PlayClip(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
    }
}
