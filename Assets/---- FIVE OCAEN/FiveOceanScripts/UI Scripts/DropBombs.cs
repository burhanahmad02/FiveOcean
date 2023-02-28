using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBombs : MonoBehaviour
{
    public GameObject missile;
    public GameObject missile2;
    public float nextFire = 2f;
    public float fireRate = 2f;
    public GameObject missilePos;
    public GameObject missilePos2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LaunchBombCo());
    }
    IEnumerator LaunchBombCo()
    {
        yield return new WaitForSeconds(0);
        if(MainMenuUI.Instance.mainMenuPanel)
        {
            if (Time.time >= nextFire)
            {

                nextFire = Time.time + fireRate;
                GameObject mis1 = Instantiate(missile, missilePos.transform.position, Quaternion.identity);
                GameObject mis2 = Instantiate(missile2, missilePos2.transform.position, Quaternion.identity);

            }
        }
       
    }
}
