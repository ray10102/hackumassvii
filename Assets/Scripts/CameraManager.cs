using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    public Camera orbitCamera;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            Debug.Log("Switching cameras...");
            orbitCamera.enabled = !orbitCamera.enabled;
        }
    }
}