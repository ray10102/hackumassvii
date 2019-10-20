using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitterAvi : MonoBehaviour
{
    private SpriteRenderer renderer;
    private float startTime;
    private float height;

    [SerializeField]
    private Rect rect = new Rect(0, 0, 48, 48);

    [SerializeField]
    private float timeBeforeFade = 2f;

    public bool isActive;

    void Start() {
        renderer = GetComponentInChildren<SpriteRenderer>();
        renderer.color = Color.clear;
    }
    public void Create(Vector2 xz, Texture2D image, float height) {
        isActive = true;
        renderer.sprite = Sprite.Create(image, rect, new Vector2(0.5f,0.5f), 20);
        transform.position = new Vector3(xz.x, 0, xz.y);
        startTime = Time.time;
        renderer.color = Color.white;
        this.height = height;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive && Time.time - startTime <= timeBeforeFade) {
            renderer.transform.localPosition = new Vector3(0, Mathf.SmoothStep(0f, height, (Time.time - startTime) / 0.5f), 0);
        }
        if (isActive && Time.time - startTime > timeBeforeFade) {
            renderer.color = Color.Lerp(Color.white, Color.clear, (Time.time - startTime - timeBeforeFade) / (WaveSources.MAX_WAVE_LIFETIME - timeBeforeFade));
            if (Time.time - startTime > WaveSources.MAX_WAVE_LIFETIME) {
                isActive = false;
            }
        }
    }
}
