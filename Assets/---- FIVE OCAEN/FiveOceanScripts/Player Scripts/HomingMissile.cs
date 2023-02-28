    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public ParticleSystem[] backSmoke;
    public GameObject[] particleList;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        EnemyController closestEnemy = null;
        EnemyController[] allEnemies = GameObject.FindObjectsOfType<EnemyController>();
        HeliCopterEnemyBehaviour closestEnemy1 = null;
        HeliCopterEnemyBehaviour[] heliEnemies = GameObject.FindObjectsOfType<HeliCopterEnemyBehaviour>();
        
        foreach (EnemyController currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
                target = closestEnemy.transform;
            }
        }
        foreach(HeliCopterEnemyBehaviour currentHeliEnemy in heliEnemies)
        {
            float distanceToEnemy = (currentHeliEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {

                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy1 = currentHeliEnemy;
                target = closestEnemy1.transform;
            }
        }
       /* Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
        Debug.DrawLine(this.transform.position, closestEnemy1.transform.position);*/
    }
    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(particleList[Random.Range(0, particleList.Length)], transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        Destroy(gameObject);
    }
    void Update()
    {
       /* if(PlayerMobileInput.Instance.hominggMissile.transform.position.y < 44.9f)
        {
            backSmoke[0].enableEmission = true;
            backSmoke[1].enableEmission = false;
            backSmoke[0].Play();
            backSmoke[1].Pause();
           
        }
        else if(PlayerMobileInput.Instance.hominggMissile.transform.position.y >= 44.9f)
        {
            backSmoke[0].enableEmission = false;
            backSmoke[1].enableEmission = true;
            backSmoke[0].Pause();
            backSmoke[1].Play();
        }*/
        FindClosestEnemy();
       
       /* Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        Vector3.Cross(direction, transform.up);
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;*/
        if (transform.position.y < -30)
            Destroy(gameObject);
       


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;
        point2Target.Normalize();
        float value = Vector3.Cross(point2Target, transform.right).z;
        rb.angularVelocity = rotateSpeed * value;
        rb.velocity = transform.right * speed;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyMissile"))
        {
            HitTarget();
            //Destroy(gameObject);
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
        else if (collision.gameObject.CompareTag("HelicopterEnemy"))
        {
            HitTarget();
            SoundManager.Instance.PlayDestructionSound(1f);

        }

    }
}
