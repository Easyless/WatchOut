using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropSphere : MonoBehaviour
{
    public GameObject rollcyllinder;
    public GameObject roliingrock;
    // float time = 0f;
    void Start()
    {
        StartCoroutine(test());
   

    }

    void Update()
    {
     
    }


    IEnumerator test()
    {


        Makecyllinder();
        yield return new WaitForSecondsRealtime(2.0f);



    }

    public void Makecyllinder()
    {
          
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        Instantiate(rollcyllinder,new Vector3(-3,25,-30) ,rotation);//������1 ����
        Instantiate(rollcyllinder, new Vector3(10, 25, -30), rotation);//������2 ����
       // Instantiate(rollcyllinder, new Vector3(-2, 25, -31), rotation);//������3 ����
       // Instantiate(rollcyllinder, new Vector3(9, 25, -27), rotation);//������4 ����
       
    }

    public void Makerock()
    {

        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        Instantiate(roliingrock, new Vector3(-3, 90, 125), rotation);//�������µ�1 ����
        Instantiate(roliingrock, new Vector3(3, 90, 105), rotation);//�������µ�2 ����
        Instantiate(roliingrock, new Vector3(13, 90, 130), rotation);//�������µ�3 ����
        Instantiate(roliingrock, new Vector3(3, 90, 130), rotation);//�������µ�4 ����


    }

    public void Makecyllinderrepeat()//������ ���� 3�ʸ��� �ݺ�
    {
        InvokeRepeating("Makecyllinder", 1,3);
      
    }

    public void Makerockrepeat()//�������µ� ���� 4�ʸ��� �ݺ�
    {
        InvokeRepeating("Makerock", 1, 3);

    }
}
