
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameInitializer : MonoBehaviour
{
    [Header("Players Prefabs")]
    public GameObject[] playersPrefab;
    public Transform PlayerSpawnPosition;
    public Transform pfDamagePopup;
    [Header("Main Camera")]
    public GameObject camera;
    private PlayerController playerController;
    public static GameInitializer Instance;
    //Water2D
    public GameObject water2D;
    public GameObject water2DPhysics;
    //Particle Effects Destruction
    public ScoreScript scoreScript;
    public bool playerDead;
    [HideInInspector]
    public GameObject waterPhysics;
    public GameObject player;
    public GameObject water;
    public GameObject[] SubmarineProfiles;
    MainMenuUI main;
    private static GameInitializer _i;
    public GameObject healthBarUI;
    //background environemnts
    public GameObject[] backgroundEnvironments;
    int randomIndex;
    public static GameInitializer i
    {
        get
        {
            if(_i == null)
            _i = Instantiate(Resources.Load<GameInitializer>("Assets"));
            return _i;
            
        }
    }
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
        SpawnSelectedPlayer();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerDead = false;
        TimeController.totalTimePassed = 1;
        TimeController.totalDistanceCovered = 0;
    }

    public void SpawnSelectedPlayer()
    {
        
        randomIndex = Random.Range(0, backgroundEnvironments.Length);
        Instantiate(backgroundEnvironments[randomIndex]);
        player = Instantiate(playersPrefab[EncryptedPlayerPrefs.GetInt("SubmarineSelected")], PlayerSpawnPosition.transform.position, Quaternion.identity);
       //player
        playerController = player.GetComponent<PlayerController>();
        if (EncryptedPlayerPrefs.GetInt("SubmarineSelected")==0)
        {
            SubmarineProfiles[0].SetActive(true);
            SubmarineProfiles[2].SetActive(false);
            SubmarineProfiles[3].SetActive(false);
            SubmarineProfiles[4].SetActive(false);
            SubmarineProfiles[1].SetActive(false);
        }
        else if (EncryptedPlayerPrefs.GetInt("SubmarineSelected") == 1)
        {
                SubmarineProfiles[1].SetActive(true);
                SubmarineProfiles[2].SetActive(false);
                SubmarineProfiles[3].SetActive(false);
                SubmarineProfiles[4].SetActive(false);
            SubmarineProfiles[0].SetActive(false);
        }
        else if (EncryptedPlayerPrefs.GetInt("SubmarineSelected") == 2)
        {
            SubmarineProfiles[0].SetActive(false);
            SubmarineProfiles[2].SetActive(true);
                SubmarineProfiles[1].SetActive(false);
                SubmarineProfiles[3].SetActive(false);
                SubmarineProfiles[4].SetActive(false);
            
        }
        else if (EncryptedPlayerPrefs.GetInt("SubmarineSelected") == 3)
        {
            SubmarineProfiles[0].SetActive(false);
            SubmarineProfiles[3].SetActive(true);
                SubmarineProfiles[1].SetActive(false);
                SubmarineProfiles[2].SetActive(false);
                SubmarineProfiles[4].SetActive(false);
            
        }
        else if (EncryptedPlayerPrefs.GetInt("SubmarineSelected") == 4)
        {
            SubmarineProfiles[0].SetActive(false);
            SubmarineProfiles[4].SetActive(true);
                SubmarineProfiles[1].SetActive(false);
                SubmarineProfiles[2].SetActive(false);
                SubmarineProfiles[3].SetActive(false);
            
        }
        player.GetComponent<PlayerMobileInput>().healthBarUI = healthBarUI.GetComponent<HealthBarUI>();
        //uiScript.GetComponent<UIScript>().healthBarBehaviour = player.GetComponent<PlayerMobileInput>().healthBarBehaviour;
        //enemy
        SpawnEnemy.Instance.GetComponent<SpawnEnemy>().player = player;
        //camera
        camera.GetComponent<CameraFollow>().target = player.transform;
        //tv camera
        //water
        water = Instantiate(water2D, water2D.transform.position, Quaternion.identity);
        waterPhysics = Instantiate(water2DPhysics, water2DPhysics.transform.position, Quaternion.identity);
        //AbilityHolder.Instance.GetComponent<AbilityHolder>().playerController = player;
    }
    private void Update()
    {
       // uiScript.GetComponent<UIScript>().
        if (playerDead)
        {
            UIScript.Instance.GameComplete(false);
        }

    
    }

}