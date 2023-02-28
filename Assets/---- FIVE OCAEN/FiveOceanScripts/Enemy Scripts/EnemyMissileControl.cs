using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileControl : MonoBehaviour
{
	public float moveSpeed = 2f;
	Rigidbody2D rb;
	//particle
	public GameObject[] particleList;
	public GameObject particleSplash;
	private int listSize;
	//public Transform spawnpos;
	private Vector3 spawnpos;
	private Quaternion spawnrot;
	public int damage = 50;
	public static EnemyMissileControl instance;
	private void Awake()
	{
		if (!instance)
		{
			instance = this;
		}
		else
		{
			DontDestroyOnLoad(this.gameObject);
			return;
		}
		spawnpos = gameObject.transform.position;
		spawnrot = gameObject.transform.rotation;
		
	}
	void HitTarget()
	{
		GameObject effectIns = (GameObject)Instantiate(particleList[Random.Range(0, particleList.Length)], transform.position, transform.rotation);
		Destroy(effectIns, 2f);
		
		/*if(explosionRadius>0f)
        {
			
			Explode();
        }
		else
        {
			Damage();
        }*/
		Destroy(gameObject);
		//Instantiate(particleList[Random.Range(0, particleList.Length)], spawnpos, spawnrot);
	}
	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (transform.position.y > 40f)
        {
			GameObject effectIn = (GameObject)Instantiate(particleList[Random.Range(0, particleList.Length)], transform.position, transform.rotation);
			Destroy(gameObject);
			GameObject effect = (GameObject)Instantiate(particleSplash, transform.position, transform.rotation);
			Destroy(gameObject);
			SoundManager.Instance.PlayDestructionSound(1f);
			Destroy(effectIn, 2f);
			Destroy(effect, 2f);

		}
			
	}


	void FixedUpdate()
	{
		rb.velocity = new Vector2(0, EnemyController.Instance.missileSpeed);
	}
	void OnCollisionEnter2D(Collision2D collision)
	{


		// detecting collision with the enemies
		if (collision.gameObject.CompareTag("Missile"))
        {
			
			HitTarget();
			SoundManager.Instance.PlayDestructionSound(1f);
			//Destroy(gameObject);

		}
		if (collision.gameObject.CompareTag("Player"))
		{
			HitTarget();
			//Destroy(gameObject);
			SoundManager.Instance.PlayDestructionSound(1f);
			
		}


	}
}
