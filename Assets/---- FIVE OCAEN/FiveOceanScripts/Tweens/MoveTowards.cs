using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public enum Action
    {
        move,
        fly,
        repeat,
        movenddestroy,
        motherShip
    }
    public bool i_am_last = false;
    public Action actionType;
    public Transform[] points;
    public float speed = 5f;
    public float waitTime;

    private Vector3 target;
    private int index = -1;
    private bool move = false;

    public int appearanceCount = 0;

    private int count = 0;

    public bool effectObject = false;

    public bool special_bool_for_airstrike = false;

    // Start is called before the first frame update
    void Start()
    {
        if (actionType == Action.repeat)
        {
            this.transform.position = points[0].transform.position;

            if (GetComponent<AudioSource>())
            {
                StartCoroutine(WaitForSound());
            }
        }


      /*  if (actionType == Action.motherShip)
        {
            target = LevelInfoScript.Instance.bossPoint0.transform.position;
        }*/
        StartCoroutine(WaitFor());
    }

    IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(Random.Range(1, 4));
        GetComponent<AudioSource>().Play();
    }

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(waitTime);
        AssignNext();
    }

    // Update is called once per frame
    void Update()
    {
        if (special_bool_for_airstrike)
        {
            return;
        }
        if (move)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, target, Time.deltaTime * speed);

            if (Utility.Distance(this.transform.position, target) < 0.1f)
            {
                count++;

                if (count > appearanceCount && effectObject)
                {
                    Destroy(this.gameObject);
                }

                move = false;
                StartCoroutine(WaitFor());
                //AssignNext();
            }
        }
    }

    void AssignNext()
    {
        if (actionType == Action.move)
        {
            if (index < points.Length - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            target = points[index].transform.position;
            move = true;
        }
        else if (actionType == Action.fly)
        {

        }
        else if (actionType == Action.repeat)
        {
            if (index < points.Length - 1)
            {
                index++;
            }
            else
            {
                this.transform.position = points[0].transform.position;
                index = points.Length - 1;
            }
            target = points[index].transform.position;
            move = true;
        }
        else if (actionType == Action.movenddestroy)
        {
            if (index < points.Length - 1)
            {
                index++;
            }
            else
            {
               /* if (i_am_last)
                {
                    if (transform.parent.gameObject.GetComponent<air_strike_movement>())
                        transform.parent.gameObject.GetComponent<air_strike_movement>().DestroyMe();
                }*/
                Destroy(this.gameObject);
            }
            target = points[index].transform.position;
            move = true;
        }
        else if (actionType == Action.motherShip)
        {
            move = true;
        }
    }
}