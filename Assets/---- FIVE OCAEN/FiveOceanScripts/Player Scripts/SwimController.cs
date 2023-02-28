using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimController : MonoBehaviour
{
    PlayerMobileInput player;
    public float moveSpeed = 0f;
    public float rotSmoothing = 0f;
    [HideInInspector] public bool swimming = false;
    //[HideInInspector] public Vector2 swimDir;
    private bool inputRecieved = false;
    private float dirY = 0f;
    private float dirX = 0f;
    Rigidbody2D rb;

    //missile
    //missile
    public GameObject missile;
    public float nextFire = 2f;
    public float fireRate = 2f;
    public GameObject missilePos;

    //health system
    public float health;
    public float startHealth = 200;

    [Header("Unity Stuff")]
    public HealthBarBehaviour healthBarBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        inputRecieved = false;
        rb.velocity *= 0.9f;
        rb.gravityScale = 0;
        health = startHealth;
        healthBarBehaviour.SetHealth(health, startHealth);
    }

    // Update is called once per frame
    void Update()
    {
       /* 
        GetInput();
        var swimDir = new Vector2(dirX, dirY).normalized;*/
        if (inputRecieved)
        {
            Debug.Log("Something wrong here");
            //rb.gravityScale = 0;
            Debug.Log("Dirx " + dirX);
            Debug.Log("Diry " + dirY);

           /* rb.AddForce(swimDir * moveSpeed * 10 * Time.deltaTime, ForceMode2D.Impulse);*/
        }
        else
        {
            rb.gravityScale = 0.5f;
        }
       /* rb.rotation = Mathf.LerpAngle(rb.rotation, Vector2.SignedAngle(Vector2.right, swimDir), rotSmoothing * Time.deltaTime);*/
    }
/*
    private void GetInput()
    {
       
            if (Input.GetKey(KeyCode.W))
        {
            inputRecieved = true;
            dirY = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            inputRecieved = true;
            dirY = -1;
        }
        else
        {
            dirY = 0;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputRecieved = true;
            dirX = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            inputRecieved = true;
            dirX = -1;
        }
        else
        {
            dirX = 0;
        }
    }*/
    
    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBarBehaviour.SetHealth(health, startHealth);
        //healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        StartCoroutine("Sinking");

    }
    IEnumerator Sinking()
    {
        for (int i = 0; i <= 180; i++)
        {
            transform.rotation = Quaternion.Euler(0, 0, i);
            transform.position = new Vector2(transform.position.x,
                                                transform.position.y - 0.05f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);
    }
 /*   private void OnTriggerStay2D(Collider2D collision)
    {
        if (!swimming && collision.CompareTag("Water"))
        {
            swimming = true;

            rb.gravityScale = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!swimming && collision.CompareTag("Water"))
        {
            swimming = false;
            rb.gravityScale = 1;


        }
        rb.rotation = 0;

    }*/
}
