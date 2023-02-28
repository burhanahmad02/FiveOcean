using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMissileControl : MonoBehaviour
{
	public float moveSpeed = 2f;
	Rigidbody2D rb;
	//particles
	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		moveSpeed = 7f;
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.y < -324.5)
			Destroy(gameObject);

		if (moveSpeed > -6.0f)
		{
			moveSpeed -= Time.deltaTime * 10;
		}
	}
	void FixedUpdate()
	{

		rb.velocity = new Vector2(0, moveSpeed);

		//rb.velocity = new Vector2(0, -moveSpeed);
	}
}
