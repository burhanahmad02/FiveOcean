using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDemoScene : MonoBehaviour
{
    public Transform[] points;
    public float speed = 5f;
    public float waitTime;

    private Vector3 target;
    private int index = -1;
    private bool move = false;

    void Start()
    {
        this.transform.position = points[0].transform.position;
        target = points[0].transform.position;

        move = true;

        this.GetComponent<Animator>().SetBool("run", true);
    }

    void Update()
    {
        if (move)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, Time.unscaledDeltaTime * speed);

            if (Utility.Distance(this.transform.position, target) < 0.1f)
            {
                move = false;
                StartCoroutine(WaitFor());
            }
        }
    }

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(waitTime);
        AssignNext();
    }

    void AssignNext()
    {
        if (index < points.Length - 1)
        {
            index++;
        }
        else
        {
            this.transform.position = points[0].transform.position;
            index = 0;
        }
        target = points[index].transform.position;
        move = true;
    }
}