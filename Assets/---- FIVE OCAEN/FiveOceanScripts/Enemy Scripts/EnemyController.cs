using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class EnemyController : MonoBehaviour
{
    //movement and patrolling
    public float moveSpeed = 5f;
    public float missileSpeed = 2f;
    private Rigidbody2D myBody;
    [SerializeField]
    public List<Transform> movementPoints;
    private Vector2 currentMovementPoint;
    private int currentMovementPointIndex;
    private int previousMovementPointIndex;
    private Vector3 tempScale;
    UIScript uiScript;
  
    //missile system
    public GameObject missile;
    public float nextFire = 2f;
    public float fireRate = 2f;

    //health system
   
 
    public float health;
    public float startHealth;
    public HealthBarBehaviour healthBarBehaviour;

    public GameObject[] burningList;
    public GameObject damagePos;
    public static EnemyController Instance;
    //[Header("Unity Stuff")]
    //public Image healthBar;
    private SimpleFlash simpleFlash;
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            return;
        }

    }
   
    // Start is called before the first frame update
    
    private void Start()
    {
        simpleFlash = GetComponent<SimpleFlash>();
        nextFire = Time.time;
        health = startHealth;
        healthBarBehaviour.SetHealth(health, startHealth);
        SetMovementPointTarget();
    }
    //damage system
     public void TakeDamage(int amount)
      {
        simpleFlash.Flash();
        health -= amount;
        //healthBar.fillAmount = health / startHealth;
        healthBarBehaviour.SetHealth(health, startHealth);
        if (health <= 0)
          {
              Die();
          }
        if (health < 50)
        {
            GameObject effectIns = (GameObject)Instantiate(burningList[Random.Range(0, 2)], damagePos.transform.position, transform.rotation);
            effectIns.transform.SetParent(gameObject.transform);
        }
    }
     void Die()
     {
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
       
        StartCoroutine("Sinking");
       
     }
    //enemy speed
    void FixedUpdate()
    {
        LaunchMissile();
        MoveToTarget();
        HandleFacingDirection();
        
        //myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
    }
    void LaunchMissile()
    {
        //SoundManager.Instance.PlayFireSound(1f);
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            SoundManager.Instance.PlayFireSound(1f);
            Instantiate(missile, transform.position, missile.transform.rotation);
           
        }
    }
    //enemy patrolling
    void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentMovementPoint, Time.deltaTime * moveSpeed);
        if (Vector2.Distance(transform.position, currentMovementPoint) < 0.1f)
        {
            // set the new movement point
            SetMovementPointTarget();
        }
    }
    //setting movement points 
    void SetMovementPointTarget()
    {
        while (true)
        {
            if (movementPoints.Count < 2)
            {
                break;
            }
            currentMovementPointIndex = Random.Range(0, movementPoints.Count);

            if (currentMovementPointIndex != previousMovementPointIndex)
            {
                previousMovementPointIndex = currentMovementPointIndex;
                currentMovementPoint = movementPoints[currentMovementPointIndex].position;
                break;
            }
        }
    }
    //missile collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        // detecting collision with the enemies
        if (collision.gameObject.CompareTag("Missile"))
        {
            gameObject.transform.DOShakeScale(0.5f, 0.1f, 1, 90, true);
            TakeDamage(PlayerMobileInput.Instance.missileDamage);
        }
    }
    //Ship sinking enemy
    IEnumerator Sinking()
    {
        for (int i = 0; i <= 180; i++)
        {
            transform.rotation = Quaternion.Euler(0, 0, i);
            transform.position = new Vector2(transform.position.x,
                                                transform.position.y - 0.05f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (gameObject.CompareTag("Enemy"))
        {
            ScoreScript.scoreNumber += 20;
            /* uiScript.AddCoins(gameObject.transform.position,7);*/
            UIScript.Instance.AddCoins(gameObject.transform.position, 7);
            UIScript.funds += 1000;
            PlayerPrefs.SetInt("Funds", UIScript.funds);

        }
        else if (gameObject.CompareTag("RedEnemy"))
        {
            ScoreScript.scoreNumber += 70;
           // uiScript.AddCoins(gameObject.transform.position, 7);
            UIScript.Instance.AddCoins(gameObject.transform.position, 70);
            UIScript.funds += 2000;
            PlayerPrefs.SetInt("Funds", UIScript.funds);
        }
        else if (gameObject.CompareTag("YellowEnemy"))
        {
            ScoreScript.scoreNumber += 100;
           // uiScript.AddCoins(gameObject.transform.position, 7);
            UIScript.Instance.AddCoins(gameObject.transform.position, 20);
            UIScript.funds += 3000;
            PlayerPrefs.SetInt("Funds", UIScript.funds);
        }
        SpawnEnemy.Instance.enemyCount--;
        Destroy(gameObject);
       

    }
    //enemy flip
    void HandleFacingDirection()
    {
        tempScale = transform.localScale;

        if (transform.position.x > currentMovementPoint.x)
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
            gameObject.transform.GetChild(1).transform.eulerAngles = new Vector2(0, 0);
            gameObject.transform.GetChild(2).transform.eulerAngles = new Vector2(0, 0);

        }
        else if (transform.position.x < currentMovementPoint.x)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            gameObject.transform.GetChild(1).transform.eulerAngles = new Vector2(0, 180);
            gameObject.transform.GetChild(2).transform.eulerAngles = new Vector2(0, 180);
        }

        transform.localScale = tempScale;
    }
}