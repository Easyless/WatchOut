using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movedoor : MonoBehaviour
{
	public bool isDown = true; 
	public bool isRandom = true;
	public float speed = 2f;

	private float height;
	public float doordistance;
	private float posYDown; 
	private bool isWaiting = false; //기다리기
	private bool canChange = true; 

	void Awake()
	{
		//height = transform.localScale.y;
		height = doordistance;//문의 이동거리
		if (isDown)
			posYDown = transform.position.y;
		else
			posYDown = transform.position.y - height;
	}

	// Update is called once per frame
	void Update()
	{
		if (isDown)
		{
			if (transform.position.y < posYDown + height)
			{
				transform.position += Vector3.up * Time.deltaTime * speed;
			}
			else if (!isWaiting)
				StartCoroutine(WaitToChange(0.5f));
		}
		else
		{
			if (!canChange)
				return;

			if (transform.position.y > posYDown)
			{
				transform.position -= Vector3.up * Time.deltaTime * speed;
			}
			else if (!isWaiting)
				StartCoroutine(WaitToChange(0.5f));
		}
	}

	
	IEnumerator WaitToChange(float time)
	{
		isWaiting = true;
		yield return new WaitForSeconds(time);
		isWaiting = false;
		isDown = !isDown;

		if (isRandom && !isDown) 
		{
            int num = Random.Range(0, 1);

            if (num == 1)
                StartCoroutine(Retry(1.5f));
		}
	}

	
	IEnumerator Retry(float time)
	{
		canChange = false;
		yield return new WaitForSeconds(time);
		int num = Random.Range(0, 1);
		if (num == 1)
			StartCoroutine(Retry(1.25f));
		else
			canChange = true;
	}
}
