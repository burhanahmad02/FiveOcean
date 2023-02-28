using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
	Vector3 movementInput;
	float dirX;
	public float moveSpeed = 5f;
	Rigidbody2D rb;
	public GameObject missile;
	public float nextFire = 2f;
	public float fireRate = 2f;
	float horizontalMove = 0f;
	public static PlayerController Instance;
	//clamping
	public float padding = 0.8f;
	public float minX;
	public float maxX;
	//
	public GameObject missilePos;

	//health system
	public float health;
	public float startHealth = 200;

	[Header("Unity Stuff")]
	public Image healthBar;

	//joystick
	
	void Awake()
    {
		if (!Instance)
		{
			Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
			return;
		}
	}
	
	
	// Use this for initialization
	void Start()
	{
		

	}
	// Update is called once per framev
	
	void Update()
	{
		
	//dirX = Input.GetAxis("Horizontal") + Time.deltaTime;
		
		

		if (Input.GetKeyDown("space"))
		{
			LaunchMissile();
			//SoundManager.Instance.PlayFireSound(1f);
		}
		/*transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7, 7),
			transform.position.y);*/
	}
	void FixedUpdate()
	{
		movementInput = Input.GetAxisRaw("Horizontal") * Vector3.right;
		movementInput.Normalize();
		float y = rb.velocity.y;
		if (movementInput != Vector3.zero)
		{
			if (transform.forward != movementInput)
			{
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movementInput), Time.deltaTime * 180);

				rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.zero, Time.deltaTime * 30);
			}
			else
			{
				rb.velocity = Vector3.MoveTowards(rb.velocity, movementInput * 10, Time.deltaTime * 30);
			}
		}
		else
		{
			rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.zero, Time.deltaTime * 30);
		}
		Vector3 velocity = rb.velocity;
		velocity.y = y;
		rb.velocity = velocity;
		//rb.velocity = new Vector2(dirX*moveSpeed, 0);
		/*if (dirX < 0)
		{
			transform.eulerAngles = new Vector2(0, 180);
		}
		else
		if (dirX > 0)
		{
			transform.eulerAngles = new Vector2(0, 0);
		}*/
	}
	
	void LaunchMissile()
	{
		if (Time.time >= nextFire)
		{
			Instantiate(missile, missilePos.transform.position, Quaternion.identity);
			SoundManager.Instance.PlayFireSound(1f);
			
			nextFire = Time.time + fireRate;
		}
	}

	

}
