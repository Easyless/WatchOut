using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	float hor; //수평
	float ver; //수직
    public float speed;//속도
    public float jumpheigt=3;//속도
    bool jdown;//점프
    bool ddown;//회피
    bool jumping;//점프중일때
    bool dodging;//점프중일때

    Vector3 moveVec;

    Rigidbody rigid;

    Animator anim; //애니메이션 변수

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
        }
        
    }

    void Dodge()//회피
    {
        if (ddown &&!dodging && !jumping)
        {
            speed *= 2;
            anim.SetTrigger("dododge");//회피 애니메이션
            dodging = true;

            Invoke("DodgeOut",0.4f); //시간차두고 회피그만하기
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
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
    }
}
