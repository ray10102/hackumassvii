using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;
    [SerializeField]
    private int size = 10;
    [SerializeField]
    private float intervals = 3;
    [SerializeField]
    private float maxJitter = .5f;
    [SerializeField]
    private float maxYJitter = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        /*WaveSources.Waves.Add(new WaveSource {
            amplitude = 1f,
            speed = -2,
            frequency = 0.01f,
            x = 40f,
            z = 40f
        });*/
        float maxJitterScaled = maxJitter * intervals;
        for(int i = 0; i < size; i++) {
            float half = intervals * size * 0.5f;
            for (int j = 0; j < size; j++) {
                GameObject go = GameObject.Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
                go.transform.position = new Vector3(i * intervals - half + Random.Range(-maxJitterScaled, maxJitterScaled), Random.Range(-maxYJitter, maxYJitter), j * intervals - half + Random.Range(-maxJitterScaled, maxJitterScaled));
                go.transform.rotation = Random.rotation;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
