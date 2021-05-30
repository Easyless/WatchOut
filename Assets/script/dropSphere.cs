using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropSphere : MonoBehaviour
{
    public GameObject rollcyllinder;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Makecyllinder();//통나무 생성
    }

    public void Makecyllinder()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 90);
        Instantiate(rollcyllinder,new Vector3(0,15,-30) ,rotation);
        Instantiate(rollcyllinder, new Vector3(5, 15, -30), rotation);
    }
}
