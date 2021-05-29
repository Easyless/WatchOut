using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 5.0f;//z
    public float yy= 5.0f;//y
    public float xx = 0.0f;//x
    //  public Vector3 offset;
    private float currentX = 0.0f;
    private float currentY = 90.0f;
    private float sensitivityX = 10.0f;
    private float sensitivityY = 10.0f;

    private void Start()
    {
        camTransform = transform;
    }

    // void Update()
    //{
    //    transform.position = lookAt.position + offset;
    //}
     void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        // currentY += Input.GetAxis("Mouse Y");
      
        // currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(xx,yy, -distance);
        // Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Quaternion rotation = Quaternion.Euler(0, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
