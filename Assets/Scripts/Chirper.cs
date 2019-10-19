using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Chirper
{
    private GameObject chirperObject;
    private AudioSource audioSource;
    private AudioClip[] audioClips;

    public Chirper(string chirpsDir)
    {
        chirperObject = new GameObject("Chirper");
        audioSource = chirperObject.AddComponent<AudioSource>();
        audioClips = Resources.LoadAll(chirpsDir, typeof(AudioClip)).Cast<AudioClip>().ToArray();
    }

    public void PlayRandomSound()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
        UnityEngine.Object.Destroy(chirperObject);
    }

    // public void Die()
    // {
    //     Destroy(chirperObject);
    // }
}