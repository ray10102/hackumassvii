using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField]
    private float scale = 1f;
    [SerializeField]
    private float speed = .05f;
    [SerializeField]
    private float frequency = 1f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   float circleOffset = (startPos.x * startPos.x) + (startPos.z * startPos.z); // (startPos.x + startPos.z);
        Vector3 totalOffset = Vector3.zero;
        foreach(WaveSource wave in WaveSources.Waves) {
            totalOffset.y += wave.amplitude * (scale * Mathf.Sin(Time.time * wave.speed * wave.frequency + circleOffset + (startPos.x * wave.x) + (startPos.z * wave.z)));
        }
        transform.position = startPos + totalOffset;
    }
}
