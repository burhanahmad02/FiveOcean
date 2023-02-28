using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnits : MonoBehaviour
{
    public GameObject[] units;
    public GameObject unitPrefab;
    public int numUnits = 10;
    public Vector3 range = new Vector3(5, 5, 5);
    public Transform fishes;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, range * 2);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, 0.2f);
    }
    //Use for initialization
    private void Start()
    {
        units = new GameObject[numUnits];
        for(int i=0; i<numUnits; i++)
        {
            Vector3 unitPos = new Vector3(Random.Range(-range.x,range.x),
                                            Random.Range(-range.y,range.y),
                                            Random.Range(0,0));
            units[i] = Instantiate(unitPrefab, this.transform.position + unitPos, Quaternion.identity) as GameObject;
            units[i].GetComponent<Unit>().manager = this.gameObject;
        }
    }
}
