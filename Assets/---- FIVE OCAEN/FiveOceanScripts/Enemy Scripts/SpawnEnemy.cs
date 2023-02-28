using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpawnEnemy : MonoBehaviour
{
	/*
		int randomSpawnPoint, randomBoat;
		Vector2 spawnPosition;
		public GameObject[] boats;
		
		// Use this for initialization
		
		//public Transform destroyerPos;
		public float speed;

		public float spawnRate;
		public float nextEnemy;

		public GameObject myBoat;*/
	public bool canSpawn = true;
	public GameObject player;
	public static SpawnEnemy Instance;
	[SerializeField]
	private GameObject[] enemyPrefabs;
	[SerializeField] private GameObject[] Level1enemies; //1hit
    [SerializeField] private GameObject[] Level3enemies; //2hits
    [SerializeField] private GameObject[] Level4enemies; //faster missile
    [SerializeField] private GameObject[] Level5enemies; //faster ms, 3hits, fast missile shoot
    [SerializeField] private GameObject[] Level6enemies; //new enemy
    [SerializeField] private GameObject[] Level7enemies; //stronger
    [SerializeField] private GameObject[] Level8enemies; //stronger
    [SerializeField] private GameObject[] Level9enemies; //stronger missile
    [SerializeField] private GameObject[] Level10enemies; //stronger missile
    [SerializeField] private GameObject[] Level11enemies; //new mine enemy
    [SerializeField] private GameObject[] Level12enemies; //mine moves faster
    [SerializeField] private GameObject[] Level13enemies; //mine moves faster
    [SerializeField] private GameObject[] Level14enemies; //mine moves faster
    [SerializeField] private GameObject[] Level15enemies; //mine moves faster
    [SerializeField] private GameObject Helicopter; //mine moves faster
    private GameObject spawnedEnemy;
    //enemy2
	private GameObject spawnedEnemy1;
    //enemy3
    private GameObject spawnedEnemy2;
    //enemy4
    private GameObject spawnedEnemy3;
    [SerializeField]
	public List<Transform> leftSide,rightSide;
    [SerializeField]
    public List<Transform> leftSideSky, rightSideSky;
    //private Transform[] leftSide, rightSide;
    public List<Transform> left1, left2,left3,left4,left5,left6,left7,leftSky1;
	public List<Transform> right1, right2,right3,right4,right5,right6,right7,rightSky2;
	private int randomIndex;
    private int rSide;
    private int randomSide;
    private int randomSide1;
    public int spawnCount;
    public Transform enemies;
    public int enemyCount = 2;
    public UIScript uiscript;
    public GameObject flashWarning;
    public GameObject buttonDescription;
    public RectTransform flash, description;
    public bool stopAnim1 = false;
    public bool stopAnim2 = false;
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
    public void CloseWarning()
    {
        Time.timeScale = 1f;
        flashWarning.SetActive(false);
        
    }
    public void CloseDescription()
    {
        buttonDescription.SetActive(false);
        Time.timeScale = 1f;
    }
    private void Update()
    {

        if (PlayerPrefs.GetInt("LevelNumber") == 0)
        {

        
            if (ClockUI.Instance.hours >= 6f && ClockUI.Instance.hours < 8f)
        {
            Debug.Log("activate");
            
            if(stopAnim1)
            {
                Time.timeScale = 0f;
                flashWarning.SetActive(true);
                // Moves this object to x:2, y:2, z:2 within 1 second.
                flash.DOAnchorPos(new Vector2(2f, 224f), 5f).SetUpdate(true);

                // Punches the rotation of this object for 1 second. Meaning it moves back to the original rotation
                flash.DOPunchAnchorPos(new Vector2(4, 230), 6,10,1).SetUpdate(true);
                
                flash.DOShakeAnchorPos( 6, 100, 10,90).SetUpdate(true);
                    stopAnim1 = false;
            }
            
            
        }
        if (ClockUI.Instance.hours >= 10f && ClockUI.Instance.hours < 12f)
        {
           
            if (stopAnim2)
            {
                Time.timeScale = 0f;
                buttonDescription.SetActive(true);
            // Moves this object to x:2, y:2, z:2 within 1 second.
                description.DOAnchorPos(new Vector2(2f, 224f), 5f).SetUpdate(true);

                    // Punches the rotation of this object for 1 second. Meaning it moves back to the original rotation
                    description.DOPunchAnchorPos(new Vector2(4, 230), 6, 10, 1).SetUpdate(true);

                    description.DOShakeAnchorPos(6, 100, 10, 90).SetUpdate(true);
                    stopAnim2 = false;
            }
        }
        }
    }
    IEnumerator SpawnMonsters()
	{

        //for (spawnCount = 0; spawnCount < 2; spawnCount++)
        //{
        //level 1 enemies
        if (PlayerPrefs.GetInt("LevelNumber") == 0)
        {

            stopAnim1 = true;
            stopAnim2 = true;
            uiscript.ingameLevelText.text = "1";
            enemyCount = 2;
            yield return new WaitForSeconds(Random.Range(0, 3));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level1enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level1enemies[1], enemies);
            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                spawnedEnemy.transform.position = leftSide[left].transform.position;
                //spawnedEnemy1.transform.position = leftSide[left].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        Debug.Log(element);
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                leftSide.RemoveAt(left);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 6;
                // spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 8;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                //spawnedEnemy.transform.position = rightSide[right].transform.position;
                spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 6;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 8;
            }
        }
        //level2
        else if (PlayerPrefs.GetInt("LevelNumber") == 2)
        {
            if(UIScript.currentLevel<25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if(UIScript.currentLevel>=25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 2;
            yield return new WaitForSeconds(Random.Range(1, 3));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level1enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level1enemies[1], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                //spawnedEnemy.transform.position = leftSide[left].transform.position;
                spawnedEnemy1.transform.position = leftSide[left].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                leftSide.RemoveAt(left);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 8;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 10;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                spawnedEnemy.transform.position = rightSide[right].transform.position;
                //spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 8;
                // spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 10;
            }
        }
        //level3
        else if (PlayerPrefs.GetInt("LevelNumber") == 3)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 2;
            yield return new WaitForSeconds(Random.Range(1, 3));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level3enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level3enemies[1], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                spawnedEnemy.transform.position = leftSide[left].transform.position;
                //spawnedEnemy1.transform.position = leftSide[left].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                leftSide.RemoveAt(left);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 10;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 10;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                //spawnedEnemy.transform.position = rightSide[right].transform.position;
                spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 10;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 12;
            }
        }
        //level4
        else if (PlayerPrefs.GetInt("LevelNumber") == 4)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 2;
            yield return new WaitForSeconds(Random.Range(1, 3));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level4enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level4enemies[1], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                spawnedEnemy.transform.position = leftSide[left].transform.position;
                //spawnedEnemy1.transform.position = leftSide[left].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                leftSide.RemoveAt(left);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 10;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 10;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                //spawnedEnemy.transform.position = rightSide[right].transform.position;
                spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 10;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 12;
            }
        }
        //level 5
        else if (PlayerPrefs.GetInt("LevelNumber") == 5)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 2;
            yield return new WaitForSeconds(Random.Range(1, 3));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level5enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level5enemies[1], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                //spawnedEnemy.transform.position = leftSide[left].transform.position;
                spawnedEnemy1.transform.position = leftSide[left].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                leftSide.RemoveAt(left);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                spawnedEnemy.transform.position = rightSide[right].transform.position;
                //spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
        }
        //level 6
        else if (PlayerPrefs.GetInt("LevelNumber") == 6)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 3;
            yield return new WaitForSeconds(Random.Range(1, 3));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level6enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level6enemies[1], enemies);
            spawnedEnemy2 = Instantiate(Level6enemies[2], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                //spawnedEnemy.transform.position = leftSide[left].transform.position;
                spawnedEnemy1.transform.position = leftSide[left].transform.position;
                spawnedEnemy2.transform.position = leftSide[left].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                leftSide.RemoveAt(left);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
                spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 17;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                spawnedEnemy.transform.position = rightSide[right].transform.position;
                //spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
        }
        //level 7
        else if (PlayerPrefs.GetInt("LevelNumber") == 7)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 3;
            yield return new WaitForSeconds(Random.Range(1, 4));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level7enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level7enemies[1], enemies);
            spawnedEnemy2 = Instantiate(Level7enemies[2], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                //spawnedEnemy.transform.position = leftSide[left].transform.position;
                spawnedEnemy1.transform.position = leftSide[left].transform.position;
                spawnedEnemy2.transform.position = leftSide[left].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                leftSide.RemoveAt(left);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
                spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 17;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                spawnedEnemy.transform.position = rightSide[right].transform.position;
                //spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
        }
        //level 8
        else if (PlayerPrefs.GetInt("LevelNumber") == 8)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 3;
            yield return new WaitForSeconds(Random.Range(1, 4));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level8enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level8enemies[1], enemies);
            spawnedEnemy2 = Instantiate(Level8enemies[2], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                //spawnedEnemy.transform.position = leftSide[left].transform.position;
                spawnedEnemy1.transform.position = leftSide[left].transform.position;

                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                leftSide.RemoveAt(left);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                spawnedEnemy.transform.position = rightSide[right].transform.position;
                spawnedEnemy2.transform.position = leftSide[right].transform.position;
                //spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
        }
        //level 9
        else if (PlayerPrefs.GetInt("LevelNumber") == 9)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 3;
            yield return new WaitForSeconds(Random.Range(1, 4));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level9enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level9enemies[1], enemies);
            spawnedEnemy2 = Instantiate(Level9enemies[2], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                //spawnedEnemy.transform.position = leftSide[left].transform.position;
                spawnedEnemy1.transform.position = leftSide[left].transform.position;

                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                leftSide.RemoveAt(left);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                spawnedEnemy.transform.position = rightSide[right].transform.position;
                spawnedEnemy2.transform.position = leftSide[right].transform.position;
                //spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
        }
        //level 10
        else if (PlayerPrefs.GetInt("LevelNumber") == 10)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 3;
            yield return new WaitForSeconds(Random.Range(1, 4));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level10enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level10enemies[1], enemies);
            spawnedEnemy2 = Instantiate(Level10enemies[2], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                int nleft = 3;
                //spawnedEnemy.transform.position = leftSide[left].transform.position;
                spawnedEnemy1.transform.position = leftSide[left].transform.position;
                spawnedEnemy2.transform.position = leftSide[nleft].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                if (nleft == 3)
                {
                    foreach (Transform element in left3)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                leftSide.RemoveAt(left);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
                spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                spawnedEnemy.transform.position = rightSide[right].transform.position;

                //spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                // spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
        }
        //level 11
        else if (PlayerPrefs.GetInt("LevelNumber") == 11)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 4;
            yield return new WaitForSeconds(Random.Range(1, 4));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level11enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level11enemies[1], enemies);
            spawnedEnemy2 = Instantiate(Level11enemies[2], enemies);
            spawnedEnemy3 = Instantiate(Level11enemies[2], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                int nleft = 3;
                //spawnedEnemy.transform.position = leftSide[left].transform.position;
                spawnedEnemy1.transform.position = leftSide[left].transform.position;
                spawnedEnemy2.transform.position = leftSide[nleft].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);


                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                if (nleft == 3)
                {
                    foreach (Transform element in left3)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                leftSide.RemoveAt(left);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
                spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);

                int nright = 3;
                spawnedEnemy.transform.position = rightSide[right].transform.position;
                spawnedEnemy3.transform.position = leftSide[nright].transform.position;
                //spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (nright == 3)
                {
                    foreach (Transform element in right3)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy3.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                rightSide.RemoveAt(right);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy3.GetComponent<EnemyController>().moveSpeed = 8;
                // spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
        }
        //level 12
        else if (PlayerPrefs.GetInt("LevelNumber") == 12)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 4;
            yield return new WaitForSeconds(Random.Range(1, 4));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level12enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level12enemies[1], enemies);
            spawnedEnemy2 = Instantiate(Level12enemies[2], enemies);
            spawnedEnemy3 = Instantiate(Level12enemies[3], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                int nleft = 3;
                //spawnedEnemy.transform.position = leftSide[left].transform.position;
                spawnedEnemy1.transform.position = leftSide[left].transform.position;
                spawnedEnemy2.transform.position = leftSide[nleft].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                if (nleft == 3)
                {
                    foreach (Transform element in left3)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                leftSide.RemoveAt(left);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
                spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                int nright = 4;
                spawnedEnemy.transform.position = rightSide[right].transform.position;
                spawnedEnemy3.transform.position = rightSide[right].transform.position;

                //spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy3.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (nright == 4)
                {
                    foreach (Transform element in right4)
                    {
                        spawnedEnemy3.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy3.GetComponent<EnemyController>().moveSpeed = 10;
                // spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
        }
        //level 13
        else if (PlayerPrefs.GetInt("LevelNumber") == 13)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 4;
            yield return new WaitForSeconds(Random.Range(1, 4));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level13enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level13enemies[1], enemies);
            spawnedEnemy2 = Instantiate(Level13enemies[2], enemies);
            spawnedEnemy3 = Instantiate(Level13enemies[3], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                int nleft = 4;
                //spawnedEnemy.transform.position = leftSide[left].transform.position;
                spawnedEnemy1.transform.position = leftSide[left].transform.position;
                spawnedEnemy2.transform.position = leftSide[nleft].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                if (nleft == 4)
                {
                    foreach (Transform element in left4)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                leftSide.RemoveAt(left);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
                spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                int nright = 3;
                spawnedEnemy.transform.position = rightSide[right].transform.position;
                spawnedEnemy3.transform.position = rightSide[right].transform.position;

                //spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy3.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (nright == 3)
                {
                    foreach (Transform element in right4)
                    {
                        spawnedEnemy3.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy3.GetComponent<EnemyController>().moveSpeed = 12;
                // spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
        }
        //level 14
        else if (PlayerPrefs.GetInt("LevelNumber") == 14)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 4;
            yield return new WaitForSeconds(Random.Range(1, 4));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level14enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level14enemies[1], enemies);
            spawnedEnemy2 = Instantiate(Level14enemies[2], enemies);
            spawnedEnemy3 = Instantiate(Level14enemies[3], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                int nleft = 4;
                //spawnedEnemy.transform.position = leftSide[left].transform.position;
                spawnedEnemy1.transform.position = leftSide[left].transform.position;
                spawnedEnemy2.transform.position = leftSide[nleft].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);


                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                if (nleft == 4)
                {
                    foreach (Transform element in left4)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                leftSide.RemoveAt(left);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
                spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                int nright = 3;
                spawnedEnemy.transform.position = rightSide[right].transform.position;
                spawnedEnemy3.transform.position = rightSide[right].transform.position;

                //spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (nright == 3)
                {
                    foreach (Transform element in right4)
                    {
                        spawnedEnemy3.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy3.GetComponent<EnemyController>().moveSpeed = 12;
                // spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
        }
        //level 15
        else if (PlayerPrefs.GetInt("LevelNumber") == 15)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 4;
            yield return new WaitForSeconds(Random.Range(1, 4));
            if (leftSide.Count == null && rightSide.Count == null)
            {
                canSpawn = false;
                //break;
            }
            randomSide = 0;
            randomSide1 = 1;
            spawnedEnemy = Instantiate(Level14enemies[0], enemies);
            spawnedEnemy1 = Instantiate(Level14enemies[1], enemies);
            spawnedEnemy2 = Instantiate(Level14enemies[2], enemies);
            spawnedEnemy3 = Instantiate(Level14enemies[3], enemies);

            // left side
            if (randomSide == 0)
            {
                int left = Random.Range(0, 2);
                int nleft = 4;
                //spawnedEnemy.transform.position = leftSide[left].transform.position;
                spawnedEnemy3.transform.position = leftSide[left].transform.position;
                spawnedEnemy.transform.position = leftSide[nleft].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (left == 0)
                {
                    foreach (Transform element in left1)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy3.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                else if (left == 1)
                {
                    foreach (Transform element in left2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy3.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                if (nleft == 4)
                {
                    foreach (Transform element in left4)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                    }
                }
                leftSide.RemoveAt(left);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy3.GetComponent<EnemyController>().moveSpeed = 15;
                spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 18;
            }
            // right side
            if (randomSide1 == 1)
            {
                int right = Random.Range(0, 2);
                int nright = 3;
                spawnedEnemy1.transform.position = rightSide[right].transform.position;
                spawnedEnemy2.transform.position = rightSide[right].transform.position;

                //spawnedEnemy1.transform.position = rightSide[right].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (right == 0)
                {
                    foreach (Transform element in right1)
                    {
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (right == 1)
                {
                    foreach (Transform element in right2)
                    {
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (nright == 3)
                {
                    foreach (Transform element in right4)
                    {
                        spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);

                        //spawnedEnemy2.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(right);
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 13;
                spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 12;
                // spawnedEnemy2.GetComponent<EnemyController>().moveSpeed = 18;
                //spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 15;
            }
        }
        //level 16
        else if (PlayerPrefs.GetInt("LevelNumber") == 16)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 6;
            for (spawnCount = 0; spawnCount <= 5; spawnCount++)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                if (leftSide.Count == null && rightSide.Count == null)
                {
                    canSpawn = false;
                    break;
                }

                randomIndex = Random.Range(0, enemyPrefabs.Length);
                randomSide = Random.Range(0, 2);

                spawnedEnemy = Instantiate(Level14enemies[randomIndex],enemies);
                // left side
                if (randomSide == 0)
                {
                    int left = Random.Range(0, leftSide.Count);
                    spawnedEnemy.transform.position = leftSide[left].transform.position;

                    //remove spawnPoint that has been used and start patrolling
                    if (left == 0)
                    {
                        foreach (Transform element in left1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 1)
                    {
                        foreach (Transform element in left2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 2)
                    {
                        foreach (Transform element in left3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 3)
                    {
                        foreach (Transform element in left4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 4)
                    {
                        foreach (Transform element in left5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    leftSide.RemoveAt(left);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                }
                // right side
                else
                {
                    int right = Random.Range(0, rightSide.Count);
                    spawnedEnemy.transform.position = rightSide[right].transform.position;

                    //remove spawnPoint that has been used and start patrolling

                    if (right == 0)
                    {
                        foreach (Transform element in right1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 1)
                    {
                        foreach (Transform element in right2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 2)
                    {
                        foreach (Transform element in right3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 3)
                    {
                        foreach (Transform element in right4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 4)
                    {
                        foreach (Transform element in right5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    rightSide.RemoveAt(right);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                    //spawnedEnemy.transform.localScale = new Vector3(-1f, 1f, 1f);

                }

            }
        }
        //level 17
        else if (PlayerPrefs.GetInt("LevelNumber") == 17)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 4;
            for (spawnCount = 0; spawnCount <= 3; spawnCount++)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                if (leftSide.Count == null && rightSide.Count == null)
                {
                    canSpawn = false;
                    break;
                }

                randomIndex = Random.Range(0, Level15enemies.Length);
                randomSide = Random.Range(0, 2);

                spawnedEnemy = Instantiate(Level15enemies[randomIndex],enemies);
                // left side
                if (randomSide == 0)
                {
                    int left = Random.Range(0, leftSide.Count);
                    spawnedEnemy.transform.position = leftSide[left].transform.position;

                    //remove spawnPoint that has been used and start patrolling
                    if (left == 0)
                    {
                        foreach (Transform element in left1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 1)
                    {
                        foreach (Transform element in left2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 2)
                    {
                        foreach (Transform element in left3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 3)
                    {
                        foreach (Transform element in left4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 4)
                    {
                        foreach (Transform element in left5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    leftSide.RemoveAt(left);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                }
                // right side
                else
                {
                    int right = Random.Range(0, rightSide.Count);
                    spawnedEnemy.transform.position = rightSide[right].transform.position;

                    //remove spawnPoint that has been used and start patrolling

                    if (right == 0)
                    {
                        foreach (Transform element in right1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 1)
                    {
                        foreach (Transform element in right2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 2)
                    {
                        foreach (Transform element in right3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 3)
                    {
                        foreach (Transform element in right4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 4)
                    {
                        foreach (Transform element in right5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    rightSide.RemoveAt(right);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                    //spawnedEnemy.transform.localScale = new Vector3(-1f, 1f, 1f);

                }

            }
        }
        //level 18
        else if (PlayerPrefs.GetInt("LevelNumber") == 18)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 5;
            for (spawnCount = 0; spawnCount <= 4; spawnCount++)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                if (leftSide.Count == null && rightSide.Count == null)
                {
                    canSpawn = false;
                    break;
                }

                randomIndex = Random.Range(0, Level15enemies.Length);
                randomSide = Random.Range(0, 2);

                spawnedEnemy = Instantiate(Level15enemies[randomIndex],enemies);
                // left side
                if (randomSide == 0)
                {
                    int left = Random.Range(0, leftSide.Count);
                    spawnedEnemy.transform.position = leftSide[left].transform.position;

                    //remove spawnPoint that has been used and start patrolling
                    if (left == 0)
                    {
                        foreach (Transform element in left1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 1)
                    {
                        foreach (Transform element in left2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 2)
                    {
                        foreach (Transform element in left3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 3)
                    {
                        foreach (Transform element in left4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 4)
                    {
                        foreach (Transform element in left5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    leftSide.RemoveAt(left);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                }
                // right side
                else
                {
                    int right = Random.Range(0, rightSide.Count);
                    spawnedEnemy.transform.position = rightSide[right].transform.position;

                    //remove spawnPoint that has been used and start patrolling

                    if (right == 0)
                    {
                        foreach (Transform element in right1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 1)
                    {
                        foreach (Transform element in right2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 2)
                    {
                        foreach (Transform element in right3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 3)
                    {
                        foreach (Transform element in right4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 4)
                    {
                        foreach (Transform element in right5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    rightSide.RemoveAt(right);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                    //spawnedEnemy.transform.localScale = new Vector3(-1f, 1f, 1f);

                }

            }
        }
        //level 19
        else if (PlayerPrefs.GetInt("LevelNumber") == 19)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 6;
            for (spawnCount = 0; spawnCount <= 5; spawnCount++)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                if (leftSide.Count == null && rightSide.Count == null)
                {
                    canSpawn = false;
                    break;
                }

                randomIndex = Random.Range(0, Level15enemies.Length);
                randomSide = Random.Range(0, 2);

                spawnedEnemy = Instantiate(Level15enemies[randomIndex],enemies);
                // left side
                if (randomSide == 0)
                {
                    int left = Random.Range(0, leftSide.Count);
                    spawnedEnemy.transform.position = leftSide[left].transform.position;

                    //remove spawnPoint that has been used and start patrolling
                    if (left == 0)
                    {
                        foreach (Transform element in left1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 1)
                    {
                        foreach (Transform element in left2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 2)
                    {
                        foreach (Transform element in left3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 3)
                    {
                        foreach (Transform element in left4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 4)
                    {
                        foreach (Transform element in left5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 5)
                    {
                        foreach (Transform element in left6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 6)
                    {
                        foreach (Transform element in left7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    leftSide.RemoveAt(left);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                }
                // right side
                else
                {
                    int right = Random.Range(0, rightSide.Count);
                    spawnedEnemy.transform.position = rightSide[right].transform.position;

                    //remove spawnPoint that has been used and start patrolling

                    if (right == 0)
                    {
                        foreach (Transform element in right1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 1)
                    {
                        foreach (Transform element in right2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 2)
                    {
                        foreach (Transform element in right3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 3)
                    {
                        foreach (Transform element in right4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 4)
                    {
                        foreach (Transform element in right5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 5)
                    {
                        foreach (Transform element in right6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 6)
                    {
                        foreach (Transform element in right7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    rightSide.RemoveAt(right);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                    //spawnedEnemy.transform.localScale = new Vector3(-1f, 1f, 1f);

                }

            }
        }
        //level 20
        else if (PlayerPrefs.GetInt("LevelNumber") == 20)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 8;
            for (spawnCount = 0; spawnCount <= 6; spawnCount++)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                if (leftSide.Count == null && rightSide.Count == null)
                {
                    canSpawn = false;
                    break;
                }

                randomIndex = Random.Range(0, Level15enemies.Length);
                randomSide = Random.Range(0, 2);

                spawnedEnemy = Instantiate(Level15enemies[randomIndex],enemies);
                // left side
                if (randomSide == 0)
                {
                    int left = Random.Range(0, leftSide.Count);
                    spawnedEnemy.transform.position = leftSide[left].transform.position;

                    //remove spawnPoint that has been used and start patrolling
                    if (left == 0)
                    {
                        foreach (Transform element in left1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 1)
                    {
                        foreach (Transform element in left2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 2)
                    {
                        foreach (Transform element in left3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 3)
                    {
                        foreach (Transform element in left4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 4)
                    {
                        foreach (Transform element in left5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 5)
                    {
                        foreach (Transform element in left6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 6)
                    {
                        foreach (Transform element in left7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    leftSide.RemoveAt(left);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                }
                // right side
                else
                {
                    int right = Random.Range(0, rightSide.Count);
                    spawnedEnemy.transform.position = rightSide[right].transform.position;

                    //remove spawnPoint that has been used and start patrolling

                    if (right == 0)
                    {
                        foreach (Transform element in right1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 1)
                    {
                        foreach (Transform element in right2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 2)
                    {
                        foreach (Transform element in right3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 3)
                    {
                        foreach (Transform element in right4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 4)
                    {
                        foreach (Transform element in right5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 5)
                    {
                        foreach (Transform element in right6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 6)
                    {
                        foreach (Transform element in right7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    rightSide.RemoveAt(right);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                    //spawnedEnemy.transform.localScale = new Vector3(-1f, 1f, 1f);

                }

            }
        }
        //level 21 (helicopter spawning)
        else if (PlayerPrefs.GetInt("LevelNumber") == 21)
        {

            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 6;

            spawnedEnemy3 = Instantiate(Helicopter, enemies);
            spawnedEnemy3.transform.position = leftSideSky[0].transform.position;
            spawnedEnemy3.GetComponent<HeliCopterEnemyBehaviour>().moveSpeed = 5f;
            //random spawning
            for (spawnCount = 0; spawnCount <= 7; spawnCount++)

            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                if (leftSideSky.Count == null && rightSideSky.Count == null && leftSide.Count == null && rightSide.Count == null)
                {
                    canSpawn = false;
                    break;
                }

                randomIndex = Random.Range(0, Level15enemies.Length);
                randomSide = Random.Range(0, 2);
;               spawnedEnemy = Instantiate(Level15enemies[randomIndex],enemies);
               
                // left side random
                if (randomSide == 0)
                {
                    int left = Random.Range(0, 5);
                    spawnedEnemy.transform.position = leftSide[left].transform.position;

                    //remove spawnPoint that has been used and start patrolling
                    if (left == 0)
                    {
                        foreach (Transform element in left1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 1)
                    {
                        foreach (Transform element in left2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 2)
                    {
                        foreach (Transform element in left3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 3)
                    {
                        foreach (Transform element in left4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 4)
                    {
                        foreach (Transform element in left5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 5)
                    {
                        foreach (Transform element in left6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 6)
                    {
                        foreach (Transform element in left7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    leftSide.RemoveAt(left);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                }
                // right side random
                else
                {
                    int right = Random.Range(0, 5);
                    spawnedEnemy.transform.position = rightSide[right].transform.position;
                    //remove spawnPoint that has been used and start patrolling

                    if (right == 0)
                    {
                        foreach (Transform element in right1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 1)
                    {
                        foreach (Transform element in right2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 2)
                    {
                        foreach (Transform element in right3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 3)
                    {
                        foreach (Transform element in right4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 4)
                    {
                        foreach (Transform element in right5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 5)
                    {
                        foreach (Transform element in right6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 6)
                    {
                        foreach (Transform element in right7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    rightSide.RemoveAt(right);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                    //spawnedEnemy.transform.localScale = new Vector3(-1f, 1f, 1f);

                }

            }
        }
        //level 22 (helicopter spawning)
        else if (PlayerPrefs.GetInt("LevelNumber") == 22)
        {

            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            spawnedEnemy1 = Instantiate(Helicopter, enemies);
            spawnedEnemy1.transform.position = rightSideSky[0].transform.position;
            spawnedEnemy1.GetComponent<HeliCopterEnemyBehaviour>().moveSpeed = 7f;
            spawnedEnemy2 = Instantiate(Helicopter, enemies);
            spawnedEnemy2.transform.position = leftSideSky[0].transform.position;
            spawnedEnemy2.GetComponent<HeliCopterEnemyBehaviour>().moveSpeed = 5f;
            enemyCount = 5;
           
            //random spawning
            for (spawnCount = 0; spawnCount <= 7; spawnCount++)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                if (leftSide.Count == null && rightSide.Count == null)
                {
                    canSpawn = false;
                    break;
                }

                randomIndex = Random.Range(0, Level15enemies.Length);
                randomSide = Random.Range(0, 2);

                spawnedEnemy = Instantiate(Level15enemies[randomIndex],enemies);
                // left side
                if (randomSide == 0)
                {
                    int left = Random.Range(0, 5);
                    spawnedEnemy.transform.position = leftSide[left].transform.position;

                    //remove spawnPoint that has been used and start patrolling
                    if (left == 0)
                    {
                        foreach (Transform element in left1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 1)
                    {
                        foreach (Transform element in left2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 2)
                    {
                        foreach (Transform element in left3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 3)
                    {
                        foreach (Transform element in left4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 4)
                    {
                        foreach (Transform element in left5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 5)
                    {
                        foreach (Transform element in left6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 6)
                    {
                        foreach (Transform element in left7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    leftSide.RemoveAt(left);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                }
                // right side
                else
                {
                    int right = Random.Range(0, 5);
                    spawnedEnemy.transform.position = rightSide[right].transform.position;

                    //remove spawnPoint that has been used and start patrolling

                    if (right == 0)
                    {
                        foreach (Transform element in right1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 1)
                    {
                        foreach (Transform element in right2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 2)
                    {
                        foreach (Transform element in right3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 3)
                    {
                        foreach (Transform element in right4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 4)
                    {
                        foreach (Transform element in right5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 5)
                    {
                        foreach (Transform element in right6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 6)
                    {
                        foreach (Transform element in right7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    rightSide.RemoveAt(right);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                    //spawnedEnemy.transform.localScale = new Vector3(-1f, 1f, 1f);

                }

            }
        }
        //level 23 (helicopter spawning)
        else if (PlayerPrefs.GetInt("LevelNumber") == 23)
        {

            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            spawnedEnemy1 = Instantiate(Helicopter, enemies);
            spawnedEnemy1.transform.position = leftSideSky[0].transform.position;
            spawnedEnemy1.GetComponent<HeliCopterEnemyBehaviour>().moveSpeed = 9f;
            spawnedEnemy2 = Instantiate(Helicopter, enemies);
            spawnedEnemy2.transform.position = rightSideSky[0].transform.position;
            spawnedEnemy2.GetComponent<HeliCopterEnemyBehaviour>().moveSpeed = 7f;
            enemyCount = 5;
            //random spawning
            for (spawnCount = 0; spawnCount <= 7; spawnCount++)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                if (leftSide.Count == null && rightSide.Count == null)
                {
                    canSpawn = false;
                    break;
                }

                randomIndex = Random.Range(0, Level15enemies.Length);
                randomSide = Random.Range(0, 2);

                spawnedEnemy = Instantiate(Level15enemies[randomIndex],enemies);
                // left side
                if (randomSide == 0)
                {
                    int left = Random.Range(0, 5);
                    spawnedEnemy.transform.position = leftSide[left].transform.position;

                    //remove spawnPoint that has been used and start patrolling
                    if (left == 0)
                    {
                        foreach (Transform element in left1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 1)
                    {
                        foreach (Transform element in left2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 2)
                    {
                        foreach (Transform element in left3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 3)
                    {
                        foreach (Transform element in left4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 4)
                    {
                        foreach (Transform element in left5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 5)
                    {
                        foreach (Transform element in left6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 6)
                    {
                        foreach (Transform element in left7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    leftSide.RemoveAt(left);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                }
                // right side
                else
                {
                    int right = Random.Range(0, 5);
                    spawnedEnemy.transform.position = rightSide[right].transform.position;

                    //remove spawnPoint that has been used and start patrolling

                    if (right == 0)
                    {
                        foreach (Transform element in right1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 1)
                    {
                        foreach (Transform element in right2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 2)
                    {
                        foreach (Transform element in right3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 3)
                    {
                        foreach (Transform element in right4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 4)
                    {
                        foreach (Transform element in right5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 5)
                    {
                        foreach (Transform element in right6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 6)
                    {
                        foreach (Transform element in right7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    rightSide.RemoveAt(right);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                    //spawnedEnemy.transform.localScale = new Vector3(-1f, 1f, 1f);

                }

            }
        }
        //level 24 (helicopter spawning)
        else if (PlayerPrefs.GetInt("LevelNumber") == 24)
        {

            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            spawnedEnemy1 = Instantiate(Helicopter, enemies);
            spawnedEnemy1.transform.position = rightSideSky[0].transform.position;
            spawnedEnemy1.GetComponent<HeliCopterEnemyBehaviour>().moveSpeed = 9f;
            spawnedEnemy2 = Instantiate(Helicopter, enemies);
            spawnedEnemy2.transform.position = leftSideSky[0].transform.position;
            spawnedEnemy2.GetComponent<HeliCopterEnemyBehaviour>().moveSpeed = 11f;
            enemyCount = 5;
            //random spawning
            for (spawnCount = 0; spawnCount <= 7; spawnCount++)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                if (leftSide.Count == null && rightSide.Count == null)
                {
                    canSpawn = false;
                    break;
                }

                randomIndex = Random.Range(0, Level15enemies.Length);
                randomSide = Random.Range(0, 2);

                spawnedEnemy = Instantiate(Level15enemies[randomIndex],enemies);
                // left side
                if (randomSide == 0)
                {
                    int left = Random.Range(0, 5);
                    spawnedEnemy.transform.position = leftSide[left].transform.position;

                    //remove spawnPoint that has been used and start patrolling
                    if (left == 0)
                    {
                        foreach (Transform element in left1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 1)
                    {
                        foreach (Transform element in left2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 2)
                    {
                        foreach (Transform element in left3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 3)
                    {
                        foreach (Transform element in left4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 4)
                    {
                        foreach (Transform element in left5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 5)
                    {
                        foreach (Transform element in left6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 6)
                    {
                        foreach (Transform element in left7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    leftSide.RemoveAt(left);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                }
                // right side
                else
                {
                    int right = Random.Range(0, 5);
                    spawnedEnemy.transform.position = rightSide[right].transform.position;

                    //remove spawnPoint that has been used and start patrolling

                    if (right == 0)
                    {
                        foreach (Transform element in right1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 1)
                    {
                        foreach (Transform element in right2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 2)
                    {
                        foreach (Transform element in right3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 3)
                    {
                        foreach (Transform element in right4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 4)
                    {
                        foreach (Transform element in right5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 5)
                    {
                        foreach (Transform element in right6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 6)
                    {
                        foreach (Transform element in right7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    rightSide.RemoveAt(right);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                    //spawnedEnemy.transform.localScale = new Vector3(-1f, 1f, 1f);

                }

            }
        }
        //level 25 (helicopter spawning)
        else if (PlayerPrefs.GetInt("LevelNumber") == 25)
        {

            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            spawnedEnemy1 = Instantiate(Helicopter, enemies);
            spawnedEnemy1.transform.position = rightSideSky[0].transform.position;
            spawnedEnemy1.GetComponent<HeliCopterEnemyBehaviour>().moveSpeed = 13f;
            spawnedEnemy2 = Instantiate(Helicopter, enemies);
            spawnedEnemy2.transform.position = leftSideSky[0].transform.position;
            spawnedEnemy2.GetComponent<HeliCopterEnemyBehaviour>().moveSpeed = 13f;
            enemyCount = 6;
            //random spawning
            for (spawnCount = 0; spawnCount <= 7; spawnCount++)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                if (leftSide.Count == null && rightSide.Count == null)
                {
                    canSpawn = false;
                    break;
                }

                randomIndex = Random.Range(0, Level15enemies.Length);
                randomSide = Random.Range(0, 2);

                spawnedEnemy = Instantiate(Level15enemies[randomIndex],enemies);
                // left side
                if (randomSide == 0)
                {
                    int left = Random.Range(0, 5);
                    spawnedEnemy.transform.position = leftSide[left].transform.position;

                    //remove spawnPoint that has been used and start patrolling
                    if (left == 0)
                    {
                        foreach (Transform element in left1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 1)
                    {
                        foreach (Transform element in left2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 2)
                    {
                        foreach (Transform element in left3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 3)
                    {
                        foreach (Transform element in left4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 4)
                    {
                        foreach (Transform element in left5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 5)
                    {
                        foreach (Transform element in left6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 6)
                    {
                        foreach (Transform element in left7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    leftSide.RemoveAt(left);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                }
                // right side
                else
                {
                    int right = Random.Range(0, 5);
                    spawnedEnemy.transform.position = rightSide[right].transform.position;

                    //remove spawnPoint that has been used and start patrolling

                    if (right == 0)
                    {
                        foreach (Transform element in right1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 1)
                    {
                        foreach (Transform element in right2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 2)
                    {
                        foreach (Transform element in right3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 3)
                    {
                        foreach (Transform element in right4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 4)
                    {
                        foreach (Transform element in right5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 5)
                    {
                        foreach (Transform element in right6)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 6)
                    {
                        foreach (Transform element in right7)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    rightSide.RemoveAt(right);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                    //spawnedEnemy.transform.localScale = new Vector3(-1f, 1f, 1f);

                }

            }
        }


        //level 22 (helicopter spawning)
        /*else if (PlayerPrefs.GetInt("LevelNumber") == 2)
        {
            if (UIScript.currentLevel < 25)
            {
                uiscript.ingameLevelText.text = PlayerPrefs.GetInt("LevelNumber").ToString();
            }
            else if (UIScript.currentLevel >= 25)
            {
                uiscript.ingameLevelText.text = UIScript.currentLevel.ToString();
            }
            enemyCount = 9;
            //helicopter spawning
            rSide = Random.Range(0, 2);
            spawnedEnemy1 = Instantiate(Helicopter, enemies);
            // left side
            if (rSide == 0)
            {
                int lt = Random.Range(5, 7);
                spawnedEnemy1.transform.position = leftSide[lt].transform.position;
                //spawnedEnemy1.transform.position = leftSide[left].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (lt == 5)
                {
                    foreach (Transform element in leftSky1)
                    {
                        Debug.Log(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (lt == 6)
                {
                    foreach (Transform element in leftSky1)
                    {
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                        //spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                leftSide.RemoveAt(lt);
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 6;
                // spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 8;
            }
            // right side
            if (rSide == 1)
            {
                int rt = Random.Range(5, 7);
                //spawnedEnemy.transform.position = rightSide[right].transform.position;
                spawnedEnemy1.transform.position = rightSide[rt].transform.position;
                //remove spawnPoint that has been used and start patrolling
                if (rt == 0)
                {
                    foreach (Transform element in rightSky2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                else if (rt == 1)
                {
                    foreach (Transform element in rightSky2)
                    {
                        //spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        spawnedEnemy1.GetComponent<EnemyController>().movementPoints.Add(element);
                    }
                }
                rightSide.RemoveAt(rt);
                //spawnedEnemy.GetComponent<EnemyController>().moveSpeed = 6;
                spawnedEnemy1.GetComponent<EnemyController>().moveSpeed = 8;
            }
            //random spawning
            for (spawnCount = 0; spawnCount <= 7; spawnCount++)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
                if (leftSide.Count == null && rightSide.Count == null)
                {
                    canSpawn = false;
                    break;
                }

                randomIndex = Random.Range(0, Level15enemies.Length);
                randomSide = Random.Range(0, 2);

                spawnedEnemy = Instantiate(Level15enemies[randomIndex]);
                // left side
                if (randomSide == 0)
                {
                    int left = Random.Range(0, 5);
                    spawnedEnemy.transform.position = leftSide[left].transform.position;

                    //remove spawnPoint that has been used and start patrolling
                    if (left == 0)
                    {
                        foreach (Transform element in left1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 1)
                    {
                        foreach (Transform element in left2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 2)
                    {
                        foreach (Transform element in left3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 3)
                    {
                        foreach (Transform element in left4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    else if (left == 4)
                    {
                        foreach (Transform element in left5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);
                        }
                    }
                    leftSide.RemoveAt(left);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                }
                // right side
                else
                {
                    int right = Random.Range(0, 5);
                    spawnedEnemy.transform.position = rightSide[right].transform.position;

                    //remove spawnPoint that has been used and start patrolling

                    if (right == 0)
                    {
                        foreach (Transform element in right1)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 1)
                    {
                        foreach (Transform element in right2)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 2)
                    {
                        foreach (Transform element in right3)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 3)
                    {
                        foreach (Transform element in right4)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    else if (right == 4)
                    {
                        foreach (Transform element in right5)
                        {
                            spawnedEnemy.GetComponent<EnemyController>().movementPoints.Add(element);

                        }
                    }
                    rightSide.RemoveAt(right);
                    spawnedEnemy.GetComponent<EnemyController>().moveSpeed = Random.Range(4, 10);
                    //spawnedEnemy.transform.localScale = new Vector3(-1f, 1f, 1f);

                }

            }
        }*/
    }
    void Start()
	{
		this.GetComponent<SpawnEnemy>().player = player;
		StartCoroutine(SpawnMonsters());
	}
}	

