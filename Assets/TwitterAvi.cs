using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitterAvi : MonoBehaviour
{
    private SpriteRenderer renderer;
    private Animator anim;
    private float startTime;

    [SerializeField]
    private Rect rect = new Rect(0, 0, 48, 48);

    [SerializeField]
    private float timeBeforeFade = 2f;

    public bool isActive;

    void Start() {
        anim = GetComponent<Animator>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        renderer.color = Color.clear;
    }
    public void Create(Vector2 xz, Texture2D image) {
        isActive = true;
        renderer.sprite = Sprite.Create(image, rect, new Vector2(0,0), 1);
        transform.position = new Vector3(xz.x, 0, xz.y);
        anim.Play("enter");
        startTime = Time.time;
        renderer.color = Color.white;
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive && Time.time - startTime > timeBeforeFade) {
            renderer.color = Color.Lerp(Color.white, Color.clear, (Time.time - startTime - timeBeforeFade) / (WaveSources.MAX_WAVE_LIFETIME - timeBeforeFade));
            if (Time.time - startTime > WaveSources.MAX_WAVE_LIFETIME) {
                isActive = false;
            }
        }
    }
}
