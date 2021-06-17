using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	float hor; //����
	float ver; //����
    public float speed;//�ӵ�
    public float jumpheigt=3;//�ӵ�
    bool jdown;//����
    bool ddown;//ȸ��
    bool demokey;//����� �����̵�
    bool jumping;//�������϶�
    bool dodging;//�������϶�

    bool isEnd = false;
    float endTimer = 0;

    AudioSource audioSource;

    public Text missiontext;
    public Text cleartext;

    //���ӵ��� �����Ǵ� ��ֹ����� ������ ���� �������ʴ� ����Ʈ

    private GameObject firstobstaclepoint;//�̼�1���� ���� ������
    private GameObject secondobstaclepoint;//�̼�2���� ���� ������
    private GameObject thirdobstaclepoint;//�̼�3���� ���� ������
    private GameObject fourthobstaclepoint;//�̼�4���� ���� ������

    private GameObject clockitem;//�ð� �����ۻ���
    private GameObject teleport;//�ð� �����ۻ���
    private GameObject shoes;//�ð� �����ۻ���


    public GameObject rotatering;

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
        transform.LookAt(rotatering.transform);


        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
        demo();

        if(transform.position.y < -10) //�߶��� �ʱ���ġ���� ������
        {
            transform.position = new Vector3(3, 10, -116);
        }

        if (transform.position.z > -29 && transform.position.z < -22 && transform.position.y < 30) 
        {
            missiontext.gameObject.SetActive(true);
            missiontext.text = "������ ���� ���� ������ ã���ÿ�";
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
    }

    void GetInput()
    {

        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
        jdown = Input.GetButtonDown("Jump");//�����̽���
        ddown = Input.GetButtonDown("Fire1");//���� ����Ʈ
        demokey = Input.GetButtonDown("Fire2");//���� ����Ʈ
    }


    void demo()
    {
        if(demokey)
        {
            transform.position = new Vector3(3, 85, 145);
        }
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
        if(jdown && !jumping && !dodging)//������������
        {
            
            rigid.AddForce(Vector3.up * jumpheigt, ForceMode.Impulse);
            anim.SetBool("isjump",true);//���������� ������ �ٴ� �ִϸ��̼�
            anim.SetTrigger("dojump");//���������� ������ �ٴ� �ִϸ��̼�
            jumping = true;
            audioSource.Play();
        }
        
    }


    void Dodge()//ȸ��
    {
        if (ddown &&!dodging && !jumping)
        {
            speed *= 2;
         
            anim.SetTrigger("dododge");//ȸ�� �ִϸ��̼�
            dodging = true;

            Invoke("DodgeOut",0.4f); //�ð����ΰ� ȸ�Ǳ׸��ϱ�
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
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
            GameObject.Find("portal").GetComponent<AudioSource>().Play();
        }


        if (collision.gameObject.tag == "portal2")//�ʷ���Ż = �ʷϵ� �տ��� �����
        {
            transform.position = new Vector3(3, 35, -25);
            GameObject.Find("portal2").GetComponent<AudioSource>().Play();
        }


        if (collision.gameObject.tag == "portal3")//������Ż = ������ �տ��� �����
        {
            transform.position = new Vector3(-2, 35, -25);
            GameObject.Find("portal3").GetComponent<AudioSource>().Play();
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

           teleport= GameObject.FindGameObjectWithTag("teleportitem");
           Destroy(teleport);//����

          
            int randomX = Random.Range(-3, 8);
            int randomZ = Random.Range(-100, 180);

            GameObject.Find("teleportsound").GetComponent<AudioSource>().Play();

           transform.position = new Vector3(randomX, 80, randomZ);
        }
    }
}
