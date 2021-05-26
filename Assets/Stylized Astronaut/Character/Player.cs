using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;
	Rigidbody rigid;

		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
    private Vector3 moveDirection2 = Vector3.zero;
    public float gravity = 20.0f;


	
		void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
		rigid = GetComponent<Rigidbody>();
		}

		void Update (){
        if (Input.GetKey("w"))//�̵�Ű
        {
            anim.SetInteger("AnimationPar", 1);//1��Ű ���ڸ�->�̵� �ִϸ��̼�
        
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);//0��Ű �̵�->���ڸ� �ִϸ��̼�
        }


        if (controller.isGrounded)
        {
            moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
            moveDirection2 = transform.forward * Input.GetAxis("Horizontal") * speed;
        }

        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;

        Jump();
	}

	void Jump()
    {
        if (Input.GetKey("space"))//����Ű
        {
            controller.Move(moveDirection2 * Time.deltaTime);

            anim.SetInteger("AnimationPar", 2);//2��Ű ���ڸ�->���� �ִϸ��̼�
         
        }
        else
        {
            anim.SetInteger("AnimationPar", 3);//3��Ű ���� -> ���ڸ� �ִϸ��̼�
        }
    }
}
