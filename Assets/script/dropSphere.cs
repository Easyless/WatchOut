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
        Instantiate(rollcyllinder,new Vector3(-3,25,-30) ,rotation);//파이프1 생성
        Instantiate(rollcyllinder, new Vector3(9, 25, -30), rotation);//파이프2 생성
       // Instantiate(rollcyllinder, new Vector3(-2, 25, -31), rotation);//파이프3 생성
       // Instantiate(rollcyllinder, new Vector3(9, 25, -27), rotation);//파이프4 생성
       
    }

    public void Makerock()
    {

        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        Instantiate(roliingrock, new Vector3(-3, 75, 125), rotation);//떨어지는돌1 생성
        Instantiate(roliingrock, new Vector3(3, 85, 105), rotation);//떨어지는돌2 생성
        Instantiate(roliingrock, new Vector3(13, 75, 130), rotation);//떨어지는돌3 생성
        Instantiate(roliingrock, new Vector3(3, 75, 130), rotation);//떨어지는돌4 생성


    }

    public void Makecyllinderrepeat()//파이프 생성 3초마다 반복
    {
        InvokeRepeating("Makecyllinder", 1,3);
      
    }

    public void Makerockrepeat()//떨어지는돌 생성 4초마다 반복
    {
        InvokeRepeating("Makerock", 1, 3);

    }
}
