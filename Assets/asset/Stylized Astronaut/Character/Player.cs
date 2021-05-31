using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	float hor; //����
	float ver; //����
    public float speed;//�ӵ�
    public float jumpheigt=3;//�ӵ�
    bool jdown;//����
    bool ddown;//ȸ��
    bool jumping;//�������϶�
    bool dodging;//�������϶�


    //���ӵ��� �����Ǵ� ��ֹ����� ������ ���� �������ʴ� ����Ʈ

    private GameObject firstobstaclepoint;//�̼�1���� ���� ������

    //----------------------------------------------------------------------

 
    Vector3 moveVec;

    Rigidbody rigid;

    Animator anim; //�ִϸ��̼� ����

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
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
        if(jdown && !jumping && !dodging)//������������
        {
            rigid.AddForce(Vector3.up * jumpheigt, ForceMode.Impulse);
            anim.SetBool("isjump",true);//���������� ������ �ٴ� �ִϸ��̼�
            anim.SetTrigger("dojump");//���������� ������ �ٴ� �ִϸ��̼�
            jumping = true;
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


        if (collision.gameObject.tag == "rock")//������ �浹�� �ڷ� ƨ��� �߷��� �������� ��õ��� �ӵ��� ��������.
        {

            rigid.AddForce(Vector3.back * 4, ForceMode.VelocityChange);
            anim.SetBool("isjump", true);//���������� ������ �ٴ� �ִϸ��̼�
            anim.SetTrigger("dojump");//���������� ������ �ٴ� �ִϸ��̼�
            jumping = true;
        }

    }
}
