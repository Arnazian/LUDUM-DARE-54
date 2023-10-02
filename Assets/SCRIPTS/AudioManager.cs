using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip mainMusic;
    [SerializeField] private AudioClip deathMusic;
    private AudioSource musicSource;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }
    public void PlayMainMusic()
    {
        musicSource.clip = mainMusic;
        musicSource.Play();
    }
    public void PlayDeathMusic()
    {
        musicSource.clip = deathMusic;
        musicSource.Play();
    }
}
