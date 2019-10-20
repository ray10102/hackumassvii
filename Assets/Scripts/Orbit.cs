using UnityEngine;
using System.Collections;
 
[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class Orbit : MonoBehaviour {
 
    public float autoOrbitSpeed = 0.1f;
    public Transform target;
    public float distance = 50.0f;
    public float xSpeed = 120.0f;

    public bool active = true;
 
    private Rigidbody rigidbody;
 
    float x = 0.0f;
    float y = 0.0f;
 
    // Use this for initialization
    void Start () 
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
 
        rigidbody = GetComponent<Rigidbody>();
 
        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }
 
    void LateUpdate () 
    {
        if (target) 
        {
            x += autoOrbitSpeed * xSpeed * distance * 0.02f;
            // y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
 
            Quaternion rotation = Quaternion.Euler(y, x, 0);
 
            RaycastHit hit;
            if (Physics.Linecast (target.position, transform.position, out hit)) 
            {
                distance -=  hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;
 
            transform.rotation = rotation;
            transform.position = position;
        }
    }
 
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}