using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveTowards : MonoBehaviour
{
    private Vector3 target;
    private int index = -1;
    private bool move = false;
    public Transform[] points;
    public float speed = 5f;
    public float waitTime1;
    public float waitTime2;

    private void Start()
    {
        move = true;
        target = points[0].transform.position;
    }

    void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, target, Time.deltaTime * Random.Range(speed, (speed*2)));

            if (Utility.Distance(this.transform.position, target) < 0.1f)
            {
                move = false;
                StartCoroutine(WaitFor());
            }
        }
    }

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(Random.Range(waitTime1, waitTime2));
        AssignNext();
    }

    void AssignNext()
    {
        //if (index < points.Length - 1)
        //{
        //    index++;
        //}
        //else
        //{
        //    index = 0;
        //}
        //target = points[index].transform.position;

        target = points[Random.Range(0, points.Length - 1)].transform.position;

        move = true;
    }
}