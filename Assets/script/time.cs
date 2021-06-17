using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class time : MonoBehaviour
{
    public Text missiontext;
    public Text gameover;
    public Text gamestart;

    private float GameTime = 0;

    private float StartTime = 0;
    private float min = 0;
    AudioSource audioSource;
    bool start = false;

    int total = 0;

    void Start()
    {

    }
    void Awake()
    {

        this.audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        StartTime += Time.deltaTime;


        if (start == true)
        {
            GameTime += Time.deltaTime;

            if ((int)GameTime == 60)
            {
                GameTime = 0;
                min += 1;
                GameTime += Time.deltaTime;

            }

            if ((int)GameTime < 10)
            {
                missiontext.text = min + " : " + "0" + (int)GameTime;
            }
            else
            {
                missiontext.text = min + " : " + (int)GameTime;
            }

        }

        if((int)min == 3 && (int)GameTime == 3)
        {
            SceneManager.LoadScene("Menu 3D");
        }

        if ((int)min == 3 && (int)GameTime == 0)
        {
            gameover.text = "미션 실패";
            gameover.gameObject.SetActive(true);
            missiontext.gameObject.SetActive(false);
        }

        if ((int)StartTime == 0)
        {
            gamestart.text = "미션 시작";
            gamestart.gameObject.SetActive(true);

            }

        else if ((int)StartTime == 3)
        {
            gamestart.gameObject.SetActive(false);
            missiontext.gameObject.SetActive(true);
            start = true;
        }
    }


    public void clockitem()
    {
        if (GameTime > 20)
        {
            GameTime -= 20;
        }

        if(GameTime < 20)
        {
            GameTime = 0;
        }

        if(GameTime<20 && min >= 1)
        {
            min -= 1;

            GameTime = (GameTime + 60) - 20;

        }

        audioSource.Play();
    }

}
