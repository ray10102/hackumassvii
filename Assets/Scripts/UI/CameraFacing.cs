using UnityEngine;
using System.Collections;
 
 [ExecuteInEditMode]
public class CameraFacing: MonoBehaviour
{
    public Camera m_Camera;

    void Start() {
        m_Camera = Camera.main;
    }
 
    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}