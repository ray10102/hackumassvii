using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    private AudioSource randomSound;

    public AudioClip[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        randomSound = GetComponent<AudioSource>();
        InvokeRepeating("PlayRandomSound", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayRandomSound()
    {
        int random = Random.Range(0, audioSources.Length);
        Debug.Log("Playing clip: " + audioSources[random]);
        randomSound.clip = audioSources[random];
        randomSound.Play();
    }
}
