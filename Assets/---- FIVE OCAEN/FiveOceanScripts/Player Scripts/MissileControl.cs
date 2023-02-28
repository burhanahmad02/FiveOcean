using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissileControl : MonoBehaviour
{
	PlayerMobileInput player;
	public float moveSpeed = 2f;
	Rigidbody2D rb;
	//particles

	public GameObject[] particleList;
	private int listSize;
	//public Transform spawnpos;
	private Vector3 spawnpos;
	private Quaternion spawnrot;
	public float explosionRadius;
	//public static int damage = 50;

	void Awake()
	{
		spawnpos = gameObject.transform.position;
		spawnrot = gameObject.transform.rotation;
		
	}
	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		moveSpeed = 7f;
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.y < -30)
			Destroy(gameObject);

		if (moveSpeed > -6.0f)
        {
			moveSpeed -= Time.deltaTime*10;
		}
			
		
	}
	//Hit Enemy
	void HitTarget()
    {
		
		GameObject effectIns = (GameObject)Instantiate(particleList[Random.Range(0, particleList.Length)],transform.position, transform.rotation);
		Destroy(effectIns, 2f);
		
		Destroy(gameObject);
	}
	void FixedUpdate()
	{

		rb.velocity = new Vector2(0, moveSpeed);
		
		//rb.velocity = new Vector2(0, -moveSpeed);
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("EnemyMissile"))
        {
			HitTarget();
			//Destroy(gameObject);
			ScoreScript.scoreNumber += 5;
			SoundManager.Instance.PlayDestructionSound(1f);
		}
		if (collision.gameObject.CompareTag("Enemy"))
		{
			HitTarget();
			SoundManager.Instance.PlayDestructionSound(1f);
			
		}
		else if (collision.gameObject.CompareTag("RedEnemy"))
		{
			HitTarget();
			SoundManager.Instance.PlayDestructionSound(1f);

		}
		else if (collision.gameObject.CompareTag("YellowEnemy"))
		{
			HitTarget();
			SoundManager.Instance.PlayDestructionSound(1f);

		}

	}
}
