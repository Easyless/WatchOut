using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class runningmapplayer : MonoBehaviour
{

    float hor; //����
    float ver; //����
    public float speed;//�ӵ�
    public float jumpheigt = 3;//�ӵ�
    bool jdown;//����
    bool ddown;//ȸ��

    bool jumping;//�������϶�
    bool dodging;//�������϶�

    bool isEnd = false;
    float endTimer = 0;

    int dashcount = 0;

    AudioSource audioSource;

 
    public Text cleartext;
    //���ӵ��� �����Ǵ� ��ֹ����� ������ ���� �������ʴ� ����Ʈ

    private GameObject firstobstaclepoint;//�̼�1���� ���� ������
    private GameObject secondobstaclepoint;//�̼�2���� ���� ������
    private GameObject thirdobstaclepoint;//�̼�3���� ���� ������
    private GameObject fourthobstaclepoint;//�̼�4���� ���� ������

    private GameObject clockitem;//�ð� �����ۻ���
    private GameObject teleport;//�ð� �����ۻ���
    private GameObject shoes;//�ð� �����ۻ���
                             //----------------------------------------------------------------------

 

    Vector3 moveVec;

    Rigidbody rigid;

    Animator anim; //�ִϸ��̼� ����

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        this.audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
       // transform.LookAt(rotatering.transform);
      
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
      

        if (transform.position.y < -20) //�߶��� �ʱ���ġ���� ������
        {
            transform.position = new Vector3(30, 0, -45);
        }


        if (isEnd)
        {
            endTimer += Time.deltaTime;
            if (endTimer > 3.0f)
            {
                SceneManager.LoadScene("Menu 3D");
            }
        }


        //����� �����̵�
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

    //����� �����̵�
    private void KeyDown_1()
    {
        transform.position = new Vector3(30, 0, -6);
    }
    private void KeyDown_2()
    {
        transform.position = new Vector3(30, 0, 81);
    }
    private void KeyDown_3()
    {
        transform.position = new Vector3(18, 0, 184);
    }
    private void KeyDown_4()
    {
        transform.position = new Vector3(18, 0, 358);
    }

    private void KeyDown_5()
    {
        transform.position = new Vector3(18, 0, 461);
    }

    void GetInput()
    {

        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
        jdown = Input.GetButtonDown("Jump");//�����̽���
        ddown = Input.GetButtonDown("Fire1");//���� ����Ʈ
       
    }



    void Move()
    {
        moveVec = new Vector3(hor, 0, ver).normalized; //���Ⱚ 1�κ���

        transform.position += moveVec * speed * Time.deltaTime;
        

        anim.SetBool("run", moveVec != Vector3.zero);//���������� ������ �ٴ� �ִϸ��̼�
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);//�÷��̾� ȸ��
    }

    void Jump()
    {
        if (jdown && !jumping && !dodging)//������������
        {

            rigid.AddForce(Vector3.up * jumpheigt, ForceMode.Impulse);
            anim.SetBool("isjump", true);//���������� ������ �ٴ� �ִϸ��̼�
            anim.SetTrigger("dojump");//���������� ������ �ٴ� �ִϸ��̼�
            jumping = true;
            audioSource.Play();
        }

    }


    void Dodge()
    {
        if (ddown && !dodging && !jumping && dashcount < 3)
        {
            speed *= 2.0f;
            dodging = true;
            dashcount += 1;
            Invoke("DodgeOut", 1.0f); 
        }
    }

    void DodgeOut()
    {
        speed /= 2.0f;
        dodging = false;
        anim.SetBool("isjump", false);
    }

    void OnCollisionEnter(Collision collision)//�ٴڿ� �������
    {
        if (collision.gameObject.tag == "ground")//ground�±װ� �ٴڿ� ������ �����ѹ���
        {
            anim.SetBool("isjump", false);


            jumping = false;
        }

        if (collision.gameObject.tag == "obstacle1")//��ֹ�����Ʈ1���޽� ���� �� ��ֹ��ߵ�
        {


            firstobstaclepoint = GameObject.FindGameObjectWithTag("obstacle1");//�̼�����Ʈ1 ���� �޾ƿ���
            Destroy(firstobstaclepoint);//����

            GameObject.Find("Cyllinderpoint").GetComponent<dropSphere>().Makecyllinderrepeat();//dropspher ���� ���������� �Լ� ��������

        }

        if (collision.gameObject.tag == "obstacle2")//��ֹ�����Ʈ2���޽� ���� �����̱����
        {
            secondobstaclepoint = GameObject.FindGameObjectWithTag("obstacle2");//�̼�����Ʈ1 ���� �޾ƿ���
            Destroy(secondobstaclepoint);//����

            GameObject.Find("Enemy2").GetComponent<enemy>().enabled = true;//���� ���� ����
            GameObject.Find("Enemy3").GetComponent<enemy>().enabled = true;
        }

        if (collision.gameObject.tag == "obstacle3")//��ֹ�����Ʈ3���޽� ���� �� ��ֹ��ߵ�
        {


            thirdobstaclepoint = GameObject.FindGameObjectWithTag("obstacle3");//�̼�����Ʈ3 ���� �޾ƿ���
            Destroy(thirdobstaclepoint);//����

            GameObject.Find("rollingrock").GetComponent<dropSphere>().Makerockrepeat();//dropspher ���� ���������� �Լ� ��������

        }




        if (collision.gameObject.tag == "rock")//������ �浹�� �ڷ� ƨ��� �߷��� �������� ��õ��� �ӵ��� ��������.
        {

            rigid.AddForce(Vector3.back * 4, ForceMode.VelocityChange);
            rigid.AddForce(Vector3.up * 2, ForceMode.Impulse);

        }



        if (collision.gameObject.tag == "monster")//���Ϳ� �浹�� ���� �ٽ� ���ư���
        {
            transform.position = new Vector3(3, 55, 35);

        }

        if (collision.gameObject.tag == "portal1")//�Ķ���Ż  = ����
        {
            transform.position = new Vector3(3, 55, 71);

        }


        if (collision.gameObject.tag == "portal2")//�ʷ���Ż = �ʷϵ� �տ��� �����
        {
            transform.position = new Vector3(3, 35, -25);

        }


        if (collision.gameObject.tag == "portal3")//������Ż = ������ �տ��� �����
        {
            transform.position = new Vector3(-2, 35, -25);

        }

        if (collision.gameObject.tag == "rollingrock")//�����������µ��� �浹�� �ڷ� ƨ��� �߷��� �������� ��õ��� �ӵ��� ��������.
        {

            rigid.AddForce(Vector3.back * 11, ForceMode.VelocityChange);
            rigid.AddForce(Vector3.up * 4, ForceMode.Impulse);
            
            
        }


        if (collision.gameObject.tag == "tire")//���Ʒ��� �����̴µ��� �浹�� �ڷ� ƨ��� �߷��� �������� ��õ��� �ӵ��� ��������.
        {

            rigid.AddForce(Vector3.back * 10, ForceMode.VelocityChange);
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);

        }

        if (collision.gameObject.tag == "rocket")//���� �浹�� ����Ŭ����
        {
            GameObject.Find("clearsound").GetComponent<AudioSource>().Play();
            cleartext.gameObject.SetActive(true);
            cleartext.text = "�̼� ����";
            isEnd = true;
        }


        if (collision.gameObject.tag == "clockitem")//�ð� �����۰� �浹
        {

            clockitem = GameObject.FindGameObjectWithTag("clockitem");
            Destroy(clockitem);//����

            GameObject.Find("time").GetComponent<time>().clockitem();

        }

        if (collision.gameObject.tag == "teleportitem")//�ڷ���Ʈ �����۰� �浹
        {

            teleport = GameObject.FindGameObjectWithTag("teleportitem");
            Destroy(teleport);//����


            int randomX = Random.Range(18, 19);
            int randomZ = Random.Range(-40, 460);

            GameObject.Find("teleportsound").GetComponent<AudioSource>().Play();

            transform.position = new Vector3(randomX, 0, randomZ);
        }
    }
}
