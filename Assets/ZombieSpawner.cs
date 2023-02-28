using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombies;

    public float spawnRate;
    private float nextZombie;

    private GameObject myZombie;
    private void Start()
    {
        nextZombie = Time.time;
    }
    private void Update()
    {
        if (Time.time >= nextZombie)
        {
            nextZombie = Time.time + spawnRate;

            int rand = Random.Range(0, zombies.Length);
            
            if (zombies[rand])
            {
                myZombie = Instantiate(zombies[rand], this.transform.position, Quaternion.identity) as GameObject;
            }
        }
    }
}