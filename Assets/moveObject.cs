using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour
{
	public bool direction = false;
	public float moveLimit = 5.0f;
	public float moveSpeed = 3.0f;
	private float random;
	public float startPos = 0f; 
	
	private Vector3 position;
	private bool moveBack = true;
	void Start()
	{
		random = Random.Range(0f, moveLimit);
		position = transform.position;
		if (direction)
			transform.position += Vector3.right * random;
		else
			transform.position += Vector3.forward * random;
	}

	void Update()
	{
		if (direction)
		{
			if (moveBack)
			{
				if (transform.position.x < position.x + moveLimit)
				{
					transform.position += Vector3.right * Time.deltaTime * moveSpeed;
				}
				else
					moveBack = false;
			}
			else
			{
				if (transform.position.x > position.x)
				{
					transform.position -= Vector3.right * Time.deltaTime * moveSpeed;
				}
				else
					moveBack = true;
			}
		}
		else
		{
			if (moveBack)
			{
				if (transform.position.z < position.z + moveLimit)
				{
					transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
				}
				else
					moveBack = false;
			}
			else
			{
				if (transform.position.z > position.z)
				{
					transform.position -= Vector3.forward * Time.deltaTime * moveSpeed;
				}
				else
					moveBack = true;
			}
		}
	}
}
