using System.Collections;
using UnityEngine;

public class MineController : MonoBehaviour
{
	public float moveSpeed = 2f;
	Rigidbody2D rb;
	//particle
	public GameObject[] particleList;
	public GameObject shockWave;
	public static int damage = 100;
	public Animator anim;
	void HitTarget()
	{
		GameObject effectIns = (GameObject)Instantiate(particleList[Random.Range(0, 7)], transform.position, transform.rotation);
		Destroy(effectIns, 2f);
		Destroy(gameObject);
		GameObject shock = (GameObject)Instantiate(shockWave, transform.position, transform.rotation);
	}
	// Use this for initialization
	void Start()
	{
		anim = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	void FixedUpdate()
	{
		if (transform.position.y < 41f)
        {
			rb.velocity = new Vector2(0, EnemyController.Instance.missileSpeed);
		}
		else if(transform.position.y > 41f)
        {
			rb.velocity = new Vector2(0, 0);
			StartCoroutine(MineBombCo());
			//Destroy(gameObject);
		}
			
	}
	IEnumerator MineBombCo()
    {
		anim.speed = 5f;
		//GameObject effectIns = (GameObject)Instantiate(particleList[Random.Range(0, particleList.Length)], transform.position, transform.rotation);
		yield return new WaitForSeconds(1);
		anim.speed = 7f;
		yield return new WaitForSeconds(3);
		anim.speed = 10f;
		yield return new WaitForSeconds(4);
		GameObject effectIns = (GameObject)Instantiate(particleList[Random.Range(0,7)], transform.position, transform.rotation);
		Destroy(effectIns, 2f);
		Destroy(gameObject);
		GameObject shock = (GameObject)Instantiate(shockWave, transform.position, transform.rotation);
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
