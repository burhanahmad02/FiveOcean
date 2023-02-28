using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{

	public string rotate_along;
	public float speed = 10.0f;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (rotate_along == "y")
		{
			this.transform.Rotate(0, speed, 0);
		}
		else if (rotate_along == "x")
		{
			this.transform.Rotate(speed * Time.deltaTime, 0, 0);
		}
		else if (rotate_along == "z")
		{
			this.transform.Rotate(0, 0, speed );
		}
		else if (rotate_along == "-x")
		{
			this.transform.Rotate(-speed * Time.deltaTime, 0, 0);
		}

		else
		{
			print("please! check your cordinate for rotating for " + gameObject.name);
		}
	}
}
