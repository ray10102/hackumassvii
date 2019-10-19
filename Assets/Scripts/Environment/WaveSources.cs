using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSources : MonoBehaviour
{
    public static List<WaveSource> Waves = new List<WaveSource>();
}

public struct WaveSource {
        public float amplitude;
        public float speed;
        public float frequency;
        public float x;
        public float z;
    }
