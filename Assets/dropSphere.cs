using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropSphere : MonoBehaviour
{
    public GameObject obj;
    public float createTimer = 1.0f;//�������� ��ֹ� ���� Ÿ�̸�
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > createTimer)
        {
            Instantiate(obj, transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
