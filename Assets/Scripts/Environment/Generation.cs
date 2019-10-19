using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int size = 10;
    [SerializeField]
    private int intervals = 3;
    [SerializeField]
    private float maxJitter = .5f;
    [SerializeField]
    private float maxYJitter = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        WaveSources.Waves.Add(new WaveSource {
            amplitude = 3f,
            speed = 5f,
            frequency = 0.01f,
            x = 20f,
            z = 30f
        });
        float maxJitterScaled = maxJitter * intervals;
        for(int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                GameObject go = GameObject.Instantiate(prefab);
                go.transform.position = new Vector3(i * intervals + Random.Range(-maxJitterScaled, maxJitterScaled), Random.Range(-maxYJitter, maxYJitter), j * intervals + Random.Range(-maxJitterScaled, maxJitterScaled));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
