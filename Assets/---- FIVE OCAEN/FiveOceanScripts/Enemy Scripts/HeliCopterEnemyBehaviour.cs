using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HeliCopterEnemyBehaviour : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 1f;

    Rigidbody2D myRigidbody;
    public float missileSpeed = 2f;
    //missile system
    public GameObject missile;
    public float nextFire = 2f;
    public float fireRate = 2f;
    private SimpleFlash simpleFlash;
    //health system


    public float health;
    public float startHealth;
    public HealthBarBehaviour healthBarBehaviour;

    public GameObject[] burningList;
    public GameObject damagePos;

    void Start()
    {
        simpleFlash = GetComponent<SimpleFlash>();
        nextFire = Time.time;
        health = startHealth;
        healthBarBehaviour.SetHealth(health, startHealth);
        myRigidbody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (IsFacingRight())
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);
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
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine("Sinking");

    }
    void FixedUpdate()
    {
        LaunchMissile();

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
            EncryptedPlayerPrefs.SetInt("Funds", UIScript.funds);

        }
        else if (gameObject.CompareTag("RedEnemy"))
        {
            ScoreScript.scoreNumber += 70;
            // uiScript.AddCoins(gameObject.transform.position, 7);
            UIScript.Instance.AddCoins(gameObject.transform.position, 70);
            UIScript.funds += 2000;
            EncryptedPlayerPrefs.SetInt("Funds", UIScript.funds);
        }
        else if (gameObject.CompareTag("YellowEnemy"))
        {
            ScoreScript.scoreNumber += 100;
            // uiScript.AddCoins(gameObject.transform.position, 7);
            UIScript.Instance.AddCoins(gameObject.transform.position, 20);
            UIScript.funds += 3000;
            EncryptedPlayerPrefs.SetInt("Funds", UIScript.funds);
        }
        else if (gameObject.CompareTag("HellicopterEnemy"))
        {
            ScoreScript.scoreNumber += 200;
            // uiScript.AddCoins(gameObject.transform.position, 7);
            UIScript.Instance.AddCoins(gameObject.transform.position, 20);
            UIScript.funds += 5000;
            EncryptedPlayerPrefs.SetInt("Funds", UIScript.funds);
        }
        SpawnEnemy.Instance.enemyCount--;
        Destroy(gameObject);


    }
}
