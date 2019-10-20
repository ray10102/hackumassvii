using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviManager : MonoBehaviour
{
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private GameObject prefab;
    private List<TwitterAvi> avis = new List<TwitterAvi>();

    public void Create(Vector2 xz, Texture2D image) {
        TwitterAvi aviToCreate = null;
        foreach(TwitterAvi avi in avis) {
            if (!avi.isActive) {
                aviToCreate = avi;
                break;
            }
        }

        if (aviToCreate == null) {
            GameObject newAvi = Instantiate(prefab);
            newAvi.transform.parent = parent;
            aviToCreate = newAvi.GetComponent<TwitterAvi>();
            avis.Add(aviToCreate);
        }

        aviToCreate.Create(xz, image);
    }
}
