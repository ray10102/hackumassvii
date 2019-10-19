using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundScript : MonoBehaviour
{
    public string chirpsDir;
    private AudioSource audioSource;
    private AudioClip[] audioClips;
    // Start is called before the first frame update
    void Start()
    {
        audioClips = Resources.LoadAll(chirpsDir, typeof(AudioClip)).Cast<AudioClip>().ToArray();
        Debug.Log("Loaded " + audioClips.Length + " chirps");
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("PlayRandomSound", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayRandomSound()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }
}
