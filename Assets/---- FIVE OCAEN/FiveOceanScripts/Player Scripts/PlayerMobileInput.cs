using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerMobileInput : MonoBehaviour, IAgentInput
{
    //temporary pause stuff
    /* public static bool isPlaying = false;
     public static bool IsPlayingIngame = false;*/
    //public GameObject pausePanel;
    //
  
    public Vector2 MovementVector { get; private set; }
    Rigidbody2D rb;
    public float dirX;
    public float dirY;
    public float moveSpeed;
    public float minmoX;
    public float maximX;
    public float minmoY;
    public float maximY;

    public float rotSmoothing;
    //private bool inputRecieved;
    
    public event Action OnAttack;
    public event Action<Vector2> OnMovement;
    public event Action OnJumpPressed;
    public event Action OnJumpReleased;
    public event Action OnWeaponChange;
    public Vector2 moveDir;
    //missile
    public GameObject missile;
    public GameObject missile2;
    public float nextFire = 2f;
    public float fireRate = 2f;
    public GameObject missilePos;
    public GameObject missilePos2;
    private float thrust = -1f;
    private float thrust2 = 1f;
    //missile cooldown
    [SerializeField]
    private Image missileImageEdge;
    [SerializeField]
    private Image missileImageCooldown;
    [SerializeField]
    
    private TMP_Text missileTextCooldown;
    private float missileCooldownTimer = 0.0f;
    private float missileCooldown = 2.0f;
    private bool ismissileCooldown = false;
    //homing missile
    public GameObject homingMissile;
    public GameObject homingMissilePos;
    public Vector2 input;
    //homing missile cooldown
    [SerializeField]
    private Image homingmissileImageEdge;
    [SerializeField]
    private Image homingmissileImageCooldown;
    [SerializeField]

    private TMP_Text homingmissileTextCooldown;
    private float homingmissileCooldownTimer = 0.0f;
    private float homingmissileCooldown = 2.0f;
    private bool ishomingmissileCooldown = false;
    
    public static PlayerMobileInput Instance;
    public Joystick joystick;

    //clamping
    public float padding = 0.8f;
    public float minX;
    public float maxX;

    //health system
    public HealthBarBehaviour healthBarBehaviour;
    public HealthBarUI healthBarUI;
    public float health;
    public float startHealth = 200;
    public Animator anim;

   /* [Header("Unity Stuff")]
    public Image healthBar;*/

    //swimming stuff

    [HideInInspector] public bool swimReady;
    public float swimCooldown = 50.0f;
    public float activeTime = 30.0f;
    [HideInInspector] public Vector2 swimDir;

    //Swim Cooldown
    [SerializeField]
    private Image imageEdge;
    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    private TMP_Text textCooldown;
    private float swimCooldownTimer = 0.0f;
    private bool isCooldown = false;
    private bool activeTimeReady;
    //GodMode Cooldown
    [SerializeField]
    private Image gModeImageEdge;
    [SerializeField]
    private Image gimageCooldown;
    [SerializeField]
    private TMP_Text gtextCooldown;
    private float gCooldownTimer = 0.0f;
    private bool isgCooldown = false;
    private bool gactiveTimeReady;
    public float godModeCooldown = 10.0f;
    public float godModeactiveTime = 5.0f;
    public bool gActive = false;
    public GameObject[] burningList;
    public GameObject damagePos;
    public int missileDamage;
    Transform cam;
    public Button missileButton;
    public Button homingButton;
    public Button swimButton;
    public Button godModeButton;
    public ParticleSystem bubble;
    public ParticleSystem backBubble;
    public ParticleSystem[] swimBubble;

    public GameObject hominggMissile;
    public GameObject[] Damaged;
    public Transform DamagedEffectPos;
    private SimpleFlash simpleFlash;
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
    private void Start()
    {
        simpleFlash = GetComponent<SimpleFlash>();
        bubble.enableEmission = false;
        cam = Camera.main.transform;
        activeTimeReady = false;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        //joystick.OnMove += Move;
        nextFire = Time.time;
        health = startHealth;
        healthBarBehaviour.SetHealth(health, startHealth);
        //healthBarUI.SetHealth(health, startHealth);
        //swim
        swimReady = false;
        textCooldown.gameObject.SetActive(false);
        imageEdge.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;
        swimCooldownTimer = swimCooldown;
        //missile cooldown
        missileTextCooldown.gameObject.SetActive(false);
        missileImageEdge.gameObject.SetActive(false);
        missileImageCooldown.fillAmount = 0.0f;
        missileCooldownTimer = missileCooldown;
        //homing missile cooldown
        homingmissileTextCooldown.gameObject.SetActive(false);
        homingmissileImageEdge.gameObject.SetActive(false);
        homingmissileImageCooldown.fillAmount = 0.0f;
        homingmissileCooldownTimer = homingmissileCooldown;
    }
    void Update()
    {
       
    
        if (UIScript.Instance.StartPanel.activeSelf)
        {
            missileButton.enabled = false;
           homingButton.enabled = false;
            swimButton.enabled = false;
            godModeButton.enabled = false;
        }
        else
        {
            missileButton.enabled = true;
            homingButton.enabled = true;
            swimButton.enabled = true;
            godModeButton.enabled = true;
        }

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minmoX, maximX), Mathf.Clamp(transform.position.y, minmoY, maximY));
        //missile cooldown
       
        if (swimCooldownTimer <= 0)
        {
            missileTextCooldown.gameObject.SetActive(false);
            missileImageCooldown.fillAmount = 0.0f;
            missileCooldown = 15f;
        }
        if (ishomingmissileCooldown)
        {
            ApplyHomingMissileCooldown();
        }
        if (ismissileCooldown)
        {
            ApplyMissileCooldown();
        }
        if (isCooldown)
        {

            ApplyCooldown();
        }
        if (isgCooldown)
        {

            ApplyGodModeCooldown();
        }
        if (activeTime<=0)
        {
            SoundManager.Instance.PlaySubSound(1f);
            SoundManager.Instance.PlayHullSound(0f);
            bubble.enableEmission = false;

            GameInitializer.Instance.waterPhysics.gameObject.GetComponent<BuoyancyEffector2D>().density = 2f;
            GameInitializer.Instance.water.GetComponent<Water2DMaterialScaler>().WaveDensity = -0.01f;
            //GameInitializer.Instance.waterPhysics.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            swimReady = false;
            activeTime = 8f;
        }
        if (swimCooldownTimer <= 0)
        {
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0.0f;
            swimCooldown = 10f;
        }
        if (godModeactiveTime <= 0)
        {
            gActive = false;
            godModeactiveTime = 4f;
        }
        if (gCooldownTimer <= 0)
        {
            gtextCooldown.gameObject.SetActive(false);
            gimageCooldown.fillAmount = 0.0f;
            godModeCooldown = 6f;
        }
        if (!swimReady)
        {
            if (joystick.Horizontal >= .2f)
            {
                dirX = +1f;
                gameObject.transform.eulerAngles = new Vector3(0, 0,3);
                


            }
            else if (joystick.Horizontal <= -.2f)
            {
                dirX = -1f;
                transform.eulerAngles = new Vector3(0, 180,3);
            }
            else
            {
                dirX = 0;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
               
            }
            moveDir = new Vector2(dirX, 0).normalized;
            
        }
        else if(swimReady)
        {

           /* if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                movePlayer();
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                movePlayer();
            }
            else if (Input.GetAxisRaw("Vertical") > 0f)
            {
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, moveSpeed, 0f);
            }
            else if (Input.GetAxis("Vertical") < 0f)
            {
                myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, -moveSpeed, 0f);
            }*/
            if (joystick.Horizontal > .2f)
            {
                dirX = +1f;
                gameObject.transform.eulerAngles = new Vector2(0, 0);
            }
            else if (joystick.Horizontal <= -.2f)
            {
                dirX = -1f;
                transform.eulerAngles = new Vector2(0, 180);
            }
            else if (joystick.Vertical >= .2f)
            {
                dirY = 1f;
            }
            else if (joystick.Vertical <= -.2f)
            {
                dirY = -1f;
            }
            else
            {
                dirX = 0;
                dirY = 0;
            }
            moveDir = new Vector2(dirX, dirY).normalized;
            rb.rotation = Mathf.LerpAngle(rb.rotation, Vector2.SignedAngle(Vector2.right, moveDir), rotSmoothing * Time.deltaTime);
           

        }
        if (GameInitializer.Instance.player.transform.position.y <= 28f)
        {
           rb.gravityScale = 1f;
        }
    }
    public void FixedUpdate()
    {
       
            rb.velocity = moveDir * moveSpeed;
      
       
        
    }
    //swim cooldown system
    public void Swim()
    {
        if(isCooldown)
        {
            SoundManager.Instance.PlayCooldownSound(1f);
            //cooldown sound
        }
        else
        {
            isCooldown = true;
            swimReady = true;
            activeTimeReady = true;
            textCooldown.gameObject.SetActive(true);
            imageCooldown.gameObject.SetActive(true);
            imageCooldown.fillAmount = 1.0f;
            imageEdge.gameObject.SetActive(true);
            swimCooldownTimer = swimCooldown;
            //SoundManager.Instance.PlayCooldownSound(1f);
            SoundManager.Instance.PlaySwimSound(1f);
            swimBubble[0].Play();
            swimBubble[1].Play();
            SoundManager.Instance.PlaySubSound(0f);
            SoundManager.Instance.PlayHullSound(1f);

        }
    }
   void ApplyCooldown()
    {
        swimCooldownTimer -= Time.deltaTime;
        //rb.gravityScale = 1f;
        if (swimCooldownTimer <= 0.0f)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageEdge.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
           
            textCooldown.text = Mathf.RoundToInt(swimCooldownTimer).ToString();
            imageCooldown.fillAmount = swimCooldownTimer / swimCooldown;

            imageEdge.transform.localEulerAngles = new Vector3(0, 0, 360.0f * (swimCooldownTimer / swimCooldown));
        }
        if(activeTimeReady)
        {
            activeTimeReady = false;
            StartCoroutine(ActiveTime());
        }
    }
  

    IEnumerator ActiveTime()
    {
        // StartCoroutine(swimCountdown());
        rb.gravityScale = 100f;
        while (swimReady)
        {
            // bubble effect
            bubble.enableEmission = true;
            backBubble.enableEmission = true;
           
            bubble.Play();
            backBubble.Play();
            // GameInitializer.Instance.waterPhysics.gameObject.GetComponent<BuoyancyEffector2D>().enabled = false;
            GameInitializer.Instance.waterPhysics.gameObject.GetComponent<BuoyancyEffector2D>().density = 0f;
            GameInitializer.Instance.water.GetComponent<Water2DMaterialScaler>().WaveDensity = 0f;
            //GameInitializer.Instance.waterPhysics.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            yield return new WaitForSeconds(1);
            
            activeTime --;
            Debug.Log("Active time " + activeTime);
        }
    }
    //God Mode
    public void GodMode()
    {
        if (isgCooldown)
        {
            SoundManager.Instance.PlayCooldownSound(1f);
            //cooldown sound
        }
        else
        {
            isgCooldown = true;
            gactiveTimeReady = true;
            gtextCooldown.gameObject.SetActive(true);
            gimageCooldown.gameObject.SetActive(true);
            gimageCooldown.fillAmount = 1.0f;
            gModeImageEdge.gameObject.SetActive(true);
            gCooldownTimer = godModeCooldown;

        }
    }
    //apply coodldown on gode mode
    void ApplyGodModeCooldown()
    {
        gCooldownTimer -= Time.deltaTime;
        //rb.gravityScale = 1f;
        if (gCooldownTimer <= 0.0f)
        {
            isgCooldown = false;
            gtextCooldown.gameObject.SetActive(false);
            gModeImageEdge.gameObject.SetActive(false);
            gimageCooldown.fillAmount = 0.0f;
        }
        else
        {

            gtextCooldown.text = Mathf.RoundToInt(gCooldownTimer).ToString();
            gimageCooldown.fillAmount = gCooldownTimer / godModeCooldown;

            gModeImageEdge.transform.localEulerAngles = new Vector3(0, 0, 360.0f * (gCooldownTimer / godModeCooldown));
        }
        if (gactiveTimeReady)
        {
            gactiveTimeReady = false;
            gActive = true;
            StartCoroutine(GodModeActiveTime());
        }
    }
    IEnumerator GodModeActiveTime()
    {
        //disable missile shooting
        //make missiles shoot automatically
        
        while (gActive && godModeactiveTime>0)
        {
            nextFire = Time.time + 1f;

            Instantiate(missile, missilePos.transform.position, Quaternion.identity);
            SoundManager.Instance.PlayFireSound(1f);
            yield return new WaitForSeconds(1);

            godModeactiveTime--;

        }
        gActive = false;


      
            Debug.Log("Active time " + godModeactiveTime);
        
    }
    //changing the speed value in pause menu
    void OnEnable()
    {
        SpeedValue.changeSensitivity += ChangeSensitivity;
    }
    void OnDisable()
    {
        SpeedValue.changeSensitivity -= ChangeSensitivity;
    }
    void ChangeSensitivity(float value)
    {
        moveSpeed = value;
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Damage");
        TakeDamage(10);
    }
    //damage system
    public void TakeDamage(int amount)
    {

        // anim.Play("damagePlayer");
        simpleFlash.Flash();
        health -= amount;
        healthBarBehaviour.SetHealth(health, startHealth);
        healthBarUI.SetHealth(health, startHealth);
        
        if (health <= 0)
        {
            Die();
        }
        if(health == 150)
        {
            GameObject effectIns = (GameObject)Instantiate(Damaged[UnityEngine.Random.Range(0,Damaged.Length)], DamagedEffectPos.transform.position, transform.rotation);
            effectIns.transform.SetParent(gameObject.transform);
        }
        if(health <100)
        {
            GameObject effectIns = (GameObject)Instantiate(burningList[UnityEngine.Random.Range(0, 2)], damagePos.transform.position, transform.rotation);
            effectIns.transform.SetParent(gameObject.transform);
        }
    }
    //death system
    void Die()
    {
       // _explode.explode(); 
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        StartCoroutine("Sinking");

    }
    //misisle launch
    public void LaunchMissile()
    {
        if (ismissileCooldown)
        {
            SoundManager.Instance.PlayCooldownSound(1f);
            //cooldown sound
        }
        if (Time.time >= nextFire)
        {
            
            nextFire = Time.time + fireRate;
            ismissileCooldown = true;
            missileTextCooldown.gameObject.SetActive(true);
            missileImageCooldown.gameObject.SetActive(true);
            missileImageCooldown.fillAmount = 1.0f;
            missileImageEdge.gameObject.SetActive(true);
            missileCooldownTimer = missileCooldown;
            Instantiate(missile, missilePos.transform.position, Quaternion.identity);

            Instantiate(missile2, missilePos2.transform.position, Quaternion.identity);
            SoundManager.Instance.PlayFireSound(1f);
           
        }
    }
    //missile cooldown
    void ApplyMissileCooldown()
    {
        missileCooldownTimer -= Time.deltaTime;
        if (missileCooldownTimer <= 0.0f)
        {
            ismissileCooldown = false;
            missileTextCooldown.gameObject.SetActive(false);
            missileImageEdge.gameObject.SetActive(false);
            missileImageCooldown.fillAmount = 0.0f;
        }
        else
        {
            
            missileTextCooldown.text = Mathf.RoundToInt(missileCooldownTimer).ToString();
            missileImageCooldown.fillAmount = missileCooldownTimer / missileCooldown;

            missileImageEdge.transform.localEulerAngles = new Vector3(0, 0, 360.0f * (missileCooldownTimer / missileCooldown));
        }
    }
    //homing missile
    public void LaunchHomingMissile()
    {
        if (ishomingmissileCooldown)
        {
            SoundManager.Instance.PlayCooldownSound(1f);
            //cooldown sound
        }
        if (swimReady)
        {
            if (Time.time >= nextFire)
            {
                nextFire = Time.time + fireRate;
                ishomingmissileCooldown = true;
                homingmissileTextCooldown.gameObject.SetActive(true);
                homingmissileImageCooldown.gameObject.SetActive(true);
                homingmissileImageCooldown.fillAmount = 1.0f;
                homingmissileImageEdge.gameObject.SetActive(true);
                homingmissileCooldownTimer = homingmissileCooldown;
                //bool isCriticalHit = true;
               hominggMissile =  Instantiate(homingMissile, homingMissilePos.transform.position, Quaternion.identity);


                SoundManager.Instance.PlayTorpedoSound(1f);


            }
        }

    }
    //homing missile cooldown
    void ApplyHomingMissileCooldown()
    {
        homingmissileCooldownTimer -= Time.deltaTime;
        if (homingmissileCooldownTimer <= 0.0f)
        {
            ishomingmissileCooldown = false;
            homingmissileTextCooldown.gameObject.SetActive(false);
            homingmissileImageEdge.gameObject.SetActive(false);
            homingmissileImageCooldown.fillAmount = 0.0f;
        }
        else
        {

            homingmissileTextCooldown.text = Mathf.RoundToInt(homingmissileCooldownTimer).ToString();
            homingmissileImageCooldown.fillAmount = homingmissileCooldownTimer / homingmissileCooldown;

            homingmissileImageEdge.transform.localEulerAngles = new Vector3(0, 0, 360.0f * (homingmissileCooldownTimer / homingmissileCooldown));
        }
    }
    //camera bounds
    public void FindBoundaries()
    {
        Camera gameCamera = Camera.main;
    }
   
    void OnCollisionEnter2D(Collision2D collision)
    {


        // detecting collision with the enemies

        if (collision.gameObject.CompareTag("EnemyMissile"))
        {
            cam.DOShakePosition(1f, 1, 10, 90, false, true);
            gameObject.transform.DOShakePosition(0.5f, 5f, 3, 90, false, true);
            TakeDamage(EnemyMissileControl.instance.damage);
        }

    }
   //sinking of ship
    IEnumerator Sinking()
    {
        for (int i = 0; i <= 180; i++)
        {
            transform.rotation = Quaternion.Euler(0, 0, i);
            transform.position = new Vector2(transform.position.x,
                                                transform.position.y - 0.05f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //can turn this to false if i want to revive the ship again
        GameInitializer.Instance.playerDead = true;
        gameObject.SetActive(false);
    }
 
}
