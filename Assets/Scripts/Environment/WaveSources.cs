using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSources : MonoBehaviour
{
    public static readonly float MAX_WAVE_LIFETIME = 5f;
    public static List<WaveSource> Waves = new List<WaveSource>();

    public void CreateWave(float amplitude, float speed, float frequency, float x, float z) {
        Waves.Add(new WaveSource {
            amplitude = amplitude,
            speed = speed,
            frequency = frequency,
            x = x,
            z = z,
            createTime = Time.time,
            color = Random.Range(0f, 1f) < 0.33f ? Color.red : Random.Range(0f, 1f) < 0.5f ? Color.green : Color.blue
        });
    }

    public void CreateRandomWave() {
        CreateWave(Random.Range(.5f, 8f), Random.Range(2f, 5f), Random.Range(0.01f, 0.5f), Random.Range(-25f, 25f), Random.Range(-25f, 25f)); //, Random.Range(0f, 1f) < 0.33f ? Color.red : Random.Range(0f, 1f) < 0.5f ? Color.green : Color.blue);
    }

    public void LateUpdate() {
        int i;
        for (i = 0; i < Waves.Count; i++) {
            if (Time.time - Waves[i].createTime < MAX_WAVE_LIFETIME) {
                break;
            }

        }
        if (i > 0) {
            Waves.RemoveRange(0, i);
        }
    }

    public void Update() {
        if (Input.GetKeyDown("space")) {
            CreateRandomWave();
        }
    }
}

public struct WaveSource {
    public float Amplitude {
        get {
            return Mathf.SmoothStep(amplitude, 0, (Time.time - createTime) / WaveSources.MAX_WAVE_LIFETIME);
        }
    }
    public float speed;
    public float frequency;
    public float x;
    public float z;
    public float createTime;
    public Color color;
    public float amplitude {
        set;
        private get;
    }

    public WaveSource(float speed, float frequency, float x, float z, float amplitude, Color color) {
        this.speed = speed;
        this.frequency = frequency;
        this.x = x;
        this.z = z;
        this.createTime = Time.time;
        this.amplitude = amplitude;
        this.color = color;
    }
}
