using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSin : MonoBehaviour
{

	public float speed = 1.5f;
	public float rotateAngle = 75f; 
	public bool direction = false;
	private float random;
	float angle;
	void Start()
	{
		random = Random.Range(0f, 1f);
	}

	// Update is called once per frame
	void Update()
	{
		angle = rotateAngle * Mathf.Sin(Time.time + random * speed);
		if (direction)
		{
			transform.rotation = Quaternion.Euler(0, 90, angle);
		}
		else
		{
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}
}
