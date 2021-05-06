using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float rotateSpeedX = 0.0f;
    public float rotateSpeedY = 0.0f;
    public float rotateSpeedZ = 0.0f;
    public bool isRotateX = false;
    public bool isRotateY = false;
    public bool isRotateZ = false;
    float rotateValx, rotateValy, rotateValz;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateValx = isRotateX ? rotateSpeedX * Time.deltaTime / 0.01f : 0.0f;
        rotateValy = isRotateY ? rotateSpeedY * Time.deltaTime / 0.01f : 0.0f;
        rotateValz = isRotateZ ? rotateSpeedZ * Time.deltaTime / 0.01f : 0.0f;
        transform.Rotate(rotateValx, rotateValy, rotateValz, Space.Self);
    }
}
