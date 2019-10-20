using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 startPos;
 
    private float masterScale = 1f;
  
    private float linearWaveScale = 1f;
   
    private float linearWaveSpeed = .05f;
    
    private float linearWaveFrequency = 1f;

    private MaterialPropertyBlock mat;
    private MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        startPos = transform.position;
        mat = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {   
        float offsetDiagonal = startPos.x + startPos.z;
        float totalOffsetY = masterScale * Mathf.Sin(Time.time * linearWaveSpeed + offsetDiagonal * linearWaveFrequency);
        Color color = Color.black;
        foreach(WaveSource wave in WaveSources.Waves) {
            float timeAlive = (Time.time - wave.createTime);
            if (Vector2.Distance(new Vector2(wave.x, wave.z), new Vector2(startPos.x, startPos.z)) < wave.speed *  timeAlive) {
                float circleOffset = (startPos.x - wave.x) * (startPos.x - wave.x) + (startPos.z - wave.z) * (startPos.z - wave.z);
                totalOffsetY += wave.Amplitude * (masterScale * Mathf.Sin(Time.time * wave.speed + circleOffset * wave.frequency));
                color += wave.color * (1 - timeAlive / WaveSources.MAX_WAVE_LIFETIME);
            }
        }
        transform.position = startPos + new Vector3(0, totalOffsetY, 0);
        mat.Clear();
        float maxOffset = 5f * masterScale;
        mat.SetColor("_Color", color * ((totalOffsetY + maxOffset) / (maxOffset * 2)));
        // renderer.SetPropertyBlock(mat);

    }

    public void init(float masterScale, float linearWaveScale, float linearWaveSpeed, float linearWaveFrequency) {
        this.masterScale = masterScale;
        this.linearWaveFrequency = linearWaveFrequency;
        this.linearWaveScale = linearWaveScale;
        this.linearWaveSpeed = linearWaveSpeed;
    }
}
