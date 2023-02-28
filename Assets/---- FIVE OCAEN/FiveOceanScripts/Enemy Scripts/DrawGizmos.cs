using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGiz : MonoBehaviour
{
	public Color color = Color.white;
	public float Radius = 0.5f;
	void OnDrawGiz()
	{

		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, Radius);
	}
}