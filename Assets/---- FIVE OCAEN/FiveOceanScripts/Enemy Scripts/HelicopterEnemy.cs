using System.Collections;
using System.Runtime.InteropServices;
using UnityEditor.Experimental;
using UnityEngine;
using System.Collections.Generic;

public class MoveTowardsHelicopter : MonoBehaviour
{
    public enum MoveAction
    {
        once,
        repeat,
        loop
    }

    public enum Type
    {
       
        helicopter
    }

    public Type type;
    public MoveAction moveType;
    public float speed = 5f;
    public float speedMultiplier = 1f;
    public float waitTimeAtPoints;

    [Header("Destruction Prefabs")]
    public GameObject[] destroyParticles;

    [HideInInspector]
    public List<Transform> points;

    public static List<MoveTowards> movingInstances;

    private Transform target;
    private Vector3 targetTemp;
    private int index = -1;
    private bool move = false;
    private int inverted = 0;
    private bool onceReached = false;

    private AudioSource myAudio;

    private void Awake()
    {
        //movingInstances.Add(this);
        targetTemp = Vector3.zero;
        points = new List<Transform>();
        inverted = Random.Range(0, 2);
        if (GetComponent<AudioSource>())
        {
            myAudio = GetComponent<AudioSource>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (points.Count > 0)
        {
            StartCoroutine(WaitFor(true, true));
        }
    }

    IEnumerator WaitFor(bool assign, bool wait)
    {
       
            yield return new WaitForSeconds(waitTimeAtPoints);
        
        if (assign)
        {
            AssignNext();
        }
        else
        {
            move = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            float distance = 0;

            if (target == null)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, targetTemp, Time.deltaTime * speed * speedMultiplier  * 4);

                distance = Utility.Distance(this.transform.position, targetTemp);
            }
           if ( type == Type.helicopter)
            {
                int invertValue = 0;

                //if (type == Type.eagle)
                //{
                //    if (distance < 2f && target == PlayerStaticInstance.Instance.gameObject.transform)
                //    {
                //        targetTemp = target.position;
                //        target = null;


                //        move = false;
                //        StartCoroutine(WaitFor(false, true));
                //        return;
                //    }
                //}
                //else 
               if (type == Type.helicopter)
                {
                    if (inverted == 1)
                    {
                        invertValue = 180;
                    }
                }

                if (target)
                {
                    if (target.position.x < transform.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, invertValue + 180, 0);
                    }
                    else if (target.position.x >= transform.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, invertValue, 0);
                    }
                }
            }

            if (distance < 0.1f)
            {
                if (targetTemp != Vector3.zero)
                {
                    targetTemp = Vector3.zero;
                }
                if (!onceReached)
                {
                    onceReached = true;
                }
               
                StartCoroutine(WaitFor(true, true));
            }
        }
    }

    void AssignNext()
    {
        if (index >= (points.Count - 1))
        {
            if (moveType == MoveAction.repeat)
            {
                index = 0;
            }
            else if (moveType == MoveAction.loop)
            {
                index = 0;
                transform.position = points[index].transform.position;
                index++;
                if (index < points.Count)
                {
                    target = points[index].transform;
                    move = true;
                }
                else
                {
                    index--;
                }
                return;
            }
            else if (moveType == MoveAction.once)
            {
               // DestroyMe(false);
                return;
            }
        }
        target = points[index].transform;
        move = true;
    }

  
   

   

    
  
   

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MoveTowards moveScript = collision.gameObject.GetComponent<MoveTowards>();

            if (gameObject.CompareTag("Player"))
            {
                //CollidedWithThings();
            }
        }
    }
}