using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField]
    private float masterScale = 1f;
    [SerializeField]
    private float linearWaveScale = 1f;
    [SerializeField]
    private float linearWaveSpeed = .05f;
    [SerializeField]
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
            if (Vector2.Distance(new Vector2(wave.x, wave.z), new Vector2(startPos.x, startPos.z)) < wave.speed *  (Time.time - wave.createTime)) {
                float circleOffset = (startPos.x - wave.x) * (startPos.x - wave.x) + (startPos.z - wave.z) * (startPos.z - wave.z);
                totalOffsetY += wave.Amplitude * (masterScale * Mathf.Sin(Time.time * wave.speed + circleOffset * wave.frequency));
                color += wave.color;
                Debug.Log(wave.color);
            }
        }
        transform.position = startPos + new Vector3(0, totalOffsetY, 0);
        mat.Clear();
        mat.SetColor("_Color", color);
        renderer.SetPropertyBlock(mat);

    }
}
