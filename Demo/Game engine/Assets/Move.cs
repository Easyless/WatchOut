using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position += new Vector3(x: 0.1f, y: 0.1f, z: 0.1f) * Time.deltaTime;
        _transform.Rotate(eulers: new Vector3(x: 10, y: 0, z: 0) * Time.deltaTime);
    }
}
