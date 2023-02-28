using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public GameObject[] missile;
    public float nextFire = 2f;
    public float fireRate = 2f;
    public float moveSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LaunchMissile();
       
    }
  
    void LaunchMissile()
    {
        int randomIndex = Random.Range(0, missile.Length); 
        //SoundManager.Instance.PlayFireSound(1f);
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            SoundManager.Instance.PlayFireSound(1f);
            Instantiate(missile[randomIndex], transform.position, missile[randomIndex].transform.rotation);

        }
    }
}
