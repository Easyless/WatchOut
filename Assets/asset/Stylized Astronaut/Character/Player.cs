using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	float hor; //수평
	float ver; //수직
    public float speed;//속도
    public float jumpheigt=3;//속도
    bool jdown;//점프
    bool ddown;//회피

    bool jumping;//점프중일때
    bool dodging;//점프중일때

    bool isEnd = false;
    float endTimer = 0;

    int dashcount = 0;

    AudioSource audioSource;

    public Text missiontext;
    public Text cleartext;

    //게임도중 생성되는 장애물들의 생성을 도울 보이지않는 포인트

    private GameObject firstobstaclepoint;//미션1지점 오라 삭제용
    private GameObject secondobstaclepoint;//미션2지점 오라 삭제용
    private GameObject thirdobstaclepoint;//미션3지점 오라 삭제용
    private GameObject fourthobstaclepoint;//미션4지점 오라 삭제용

    private GameObject clockitem;//시계 아이템삭제
    private GameObject teleport;//시계 아이템삭제
    private GameObject shoes;//시계 아이템삭제



    //----------------------------------------------------------------------


    Vector3 moveVec;

    Rigidbody rigid;

    Animator anim; //애니메이션 변수

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        this.audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
       


        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
      

        if(transform.position.y < -10) //추락시 초기위치에서 리스폰
        {
            transform.position = new Vector3(3, 10, -116);
        }

        if (transform.position.z > -29 && transform.position.z < -22 && transform.position.y < 30) 
        {
            missiontext.gameObject.SetActive(true);
            missiontext.text = "개수가 가장 적은 바위를 찾으시오";
        }
      
        else
        {
            missiontext.gameObject.SetActive(false);
        }

        if (isEnd)
        {
            endTimer += Time.deltaTime;
            if (endTimer > 3.0f)
            {
                SceneManager.LoadScene("Menu 3D");
            }
        }


        //데모용 순간이동
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            KeyDown_1();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            KeyDown_2();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            KeyDown_3();
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            KeyDown_4();
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            KeyDown_5();
        }
    }

    private void KeyDown_1()
    {
        transform.position = new Vector3(3, 3, -65);
    }
    private void KeyDown_2()
    {
        transform.position = new Vector3(3, 50, 31);
    }
    private void KeyDown_3()
    {
        transform.position = new Vector3(3, 47, 75);
    }
    private void KeyDown_4()
    {
        transform.position = new Vector3(3, 83, 145);
    }

    private void KeyDown_5()
    {
        transform.position = new Vector3(3, 97, 218);
    }


    void GetInput()
    {

        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
        jdown = Input.GetButtonDown("Jump");//스페이스바
        ddown = Input.GetButtonDown("Fire1");//왼쪽 시프트
     
    }


 
    void Move()
    {
        moveVec = new Vector3(hor, 0, ver).normalized; //방향값 1로보정

        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("run", moveVec != Vector3.zero);//가만히있지 않을시 뛰는 애니메이션
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);//플레이어 회전
    }

    void Jump()
    {
        if(jdown && !jumping && !dodging)//무한점프방지
        {
            
            rigid.AddForce(Vector3.up * jumpheigt, ForceMode.Impulse);
            anim.SetBool("isjump",true);//가만히있지 않을시 뛰는 애니메이션
            anim.SetTrigger("dojump");//가만히있지 않을시 뛰는 애니메이션
            jumping = true;
            audioSource.Play();
        }
        
    }


    void Dodge()//회피
    {
        if (ddown &&!dodging && !jumping && dashcount < 3)
        {
            speed *= 2.0f;
            dodging = true;
            dashcount += 1;
            Invoke("DodgeOut",1.0f); //시간차두고 회피그만하기
        }
    }

    void DodgeOut()
    {
        speed /= 2.0f;
        dodging = false; 
        anim.SetBool("isjump", false);
    }

    void OnCollisionEnter(Collision collision)//바닥에 닿았을시
    {
        if (collision.gameObject.tag == "ground")//ground태그값 바닥에 있을시 점프한번만
        {
            anim.SetBool("isjump", false);
          
        
            jumping = false;
        }

        if (collision.gameObject.tag == "obstacle1")//장애물포인트1도달시 삭제 및 장애물발동
        {


            firstobstaclepoint = GameObject.FindGameObjectWithTag("obstacle1");//미션포인트1 오라 받아오기
            Destroy(firstobstaclepoint);//삭제

            GameObject.Find("Cyllinderpoint").GetComponent<dropSphere>().Makecyllinderrepeat();//dropspher 내의 파이프생성 함수 가져오기
         
        }

        if (collision.gameObject.tag == "obstacle2")//장애물포인트2도달시 몬스터 움직이기시작
        {
            secondobstaclepoint = GameObject.FindGameObjectWithTag("obstacle2");//미션포인트1 오라 받아오기
            Destroy(secondobstaclepoint);//삭제

            GameObject.Find("Enemy2").GetComponent<enemy>().enabled = true;//몬스터 동작 시작
           GameObject.Find("Enemy3").GetComponent<enemy>().enabled = true;
        }

        if (collision.gameObject.tag == "obstacle3")//장애물포인트3도달시 삭제 및 장애물발동
        {


            thirdobstaclepoint = GameObject.FindGameObjectWithTag("obstacle3");//미션포인트3 오라 받아오기
            Destroy(thirdobstaclepoint);//삭제

            GameObject.Find("rollingrock").GetComponent<dropSphere>().Makerockrepeat();//dropspher 내의 파이프생성 함수 가져오기

        }

 


        if (collision.gameObject.tag == "rock")//바위와 충돌시 뒤로 튕기고 중력의 영향으로 잠시동안 속도가 느려진다.
        {

            rigid.AddForce(Vector3.back * 4, ForceMode.VelocityChange);
            rigid.AddForce(Vector3.up * 2, ForceMode.Impulse);
           
        }



        if (collision.gameObject.tag == "monster")//몬스터와 충돌시 위로 다시 돌아간다
        {
            transform.position = new Vector3(3, 55, 35);
           
        }

        if (collision.gameObject.tag == "portal1")//파란포탈  = 정답
        {
            transform.position = new Vector3(3, 55, 71);
            GameObject.Find("portal").GetComponent<AudioSource>().Play();
        }


        if (collision.gameObject.tag == "portal2")//초록포탈 = 초록돌 앞에서 재시작
        {
            transform.position = new Vector3(3, 35, -25);
            GameObject.Find("portal2").GetComponent<AudioSource>().Play();
        }


        if (collision.gameObject.tag == "portal3")//빨간포탈 = 빨간돌 앞에서 재시작
        {
            transform.position = new Vector3(-2, 35, -25);
            GameObject.Find("portal3").GetComponent<AudioSource>().Play();
        }

        if (collision.gameObject.tag == "rollingrock")//굴러떨어지는돌과 충돌시 뒤로 튕기고 중력의 영향으로 잠시동안 속도가 느려진다.
        {

            rigid.AddForce(Vector3.back * 11, ForceMode.VelocityChange);
            rigid.AddForce(Vector3.up * 4, ForceMode.Impulse);
           
        }


        if (collision.gameObject.tag == "tire")//위아래로 움직이는돌과 충돌시 뒤로 튕기고 중력의 영향으로 잠시동안 속도가 느려진다.
        {

            rigid.AddForce(Vector3.back * 10, ForceMode.VelocityChange);
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
         
        }

        if (collision.gameObject.tag == "rocket")//로켓 충돌시 게임클리어
        {
            GameObject.Find("clearsound").GetComponent<AudioSource>().Play();
            cleartext.gameObject.SetActive(true);
            cleartext.text = "미션 성공";
            isEnd = true;
        }


        if (collision.gameObject.tag == "clockitem")//시계 아이템과 충돌
        {

            clockitem = GameObject.FindGameObjectWithTag("clockitem");
            Destroy(clockitem);//삭제

            GameObject.Find("time").GetComponent<time>().clockitem();
           
        }

        if (collision.gameObject.tag == "teleportitem")//텔레포트 아이템과 충돌
        {

           teleport= GameObject.FindGameObjectWithTag("teleportitem");
           Destroy(teleport);//삭제

          
            int randomX = Random.Range(-3, 8);
            int randomZ = Random.Range(-100, 180);

            GameObject.Find("teleportsound").GetComponent<AudioSource>().Play();

           transform.position = new Vector3(randomX, 80, randomZ);
        }
    }
}
