using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollRectSnap_vertical : MonoBehaviour 
{
	// Public Variables
	public RectTransform panel;	// To hold the ScrollPanel
	public Button[] bttn;
	public RectTransform center;	// Center to compare the distance for each button

	// Private Variables
	public float[] distance;	// All buttons' distance to the center
	public float[] distReposition;
	private bool dragging = false;	// Will be true, while we drag the panel
	private int bttnDistance;	// Will hold the distance between the buttons
	private int minButtonNum;	// To hold the number of the button, with smallest distance to center
	private int bttnLength;

	void Start()
	{
		bttnLength = bttn.Length;
		distance = new float[bttnLength];
		distReposition = new float[bttnLength];

		// Get distance between buttons
		bttnDistance  = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.y - bttn[0].GetComponent<RectTransform>().anchoredPosition.y);

	}

	void Update()
	{
		for (int i = 0; i < bttn.Length; i++)
		{
			distReposition[i] = center.GetComponent<RectTransform>().position.y - bttn[i].GetComponent<RectTransform>().position.y;
			distance[i] = Mathf.Abs(distReposition[i]);

//			print ( distance[i] );

//			if (distReposition[i] > 1000)
//			{
//				float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
//				float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;
//
//				Vector2 newAnchoredPos = new Vector2 (curX + (bttnLength * bttnDistance), curY);
//				bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
//			}
//
//			if (distReposition[i] < -1000)
//			{
//				float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
//				float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;
//
//				Vector2 newAnchoredPos = new Vector2 (curX - (bttnLength * bttnDistance), curY);
//				bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
//			}
		}
	


		float minDistance = Mathf.Min(distance);	// Get the min distance



		for (int a = 0; a < bttn.Length; a++)
		{
			if (minDistance == distance[a])
			{
				minButtonNum = a;
			}
		}



		if (!dragging)
		{
//			LerpToBttn(minButtonNum * -bttnDistance);
//			LerpToBttn (-bttn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);

//			print ( panel.anchoredPosition.y+" |||| is greater "+ bttn [0].GetComponent<RectTransform> ().anchoredPosition.y);
//			return;

			if (  panel.anchoredPosition.y < bttn [0].GetComponent<RectTransform> ().anchoredPosition.y) 
			{
//				print ( panel.anchoredPosition.y+" 111 "+ bttn [0].GetComponent<RectTransform> ().anchoredPosition.y);
				LerpToBttn (-bttn[0].GetComponent<RectTransform>().anchoredPosition.y);
			}
			else if( panel.anchoredPosition.y > -bttn [bttn.Length-1].GetComponent<RectTransform> ().anchoredPosition.y )
			{
//				print ( panel.anchoredPosition.y+" 222 "+ bttn [bttn.Length-1].GetComponent<RectTransform> ().anchoredPosition.y);
				LerpToBttn (-bttn[bttn.Length-1].GetComponent<RectTransform>().anchoredPosition.y);
			}

		}
	}

	void LerpToBttn(float position)
	{
//		print ("its been called  =  " + position);
		if (Time.timeScale == 0)
        {
			float newX = Mathf.Lerp(panel.anchoredPosition.y, position, 15f);
			Vector2 newPosition = new Vector2(panel.anchoredPosition.x, newX);

			panel.anchoredPosition = newPosition;
		}
		else
		{
			float newX = Mathf.Lerp(panel.anchoredPosition.y, position, Time.deltaTime * 15f);
			Vector2 newPosition = new Vector2(panel.anchoredPosition.x, newX);

			panel.anchoredPosition = newPosition;
		}
	}

	public void StartDrag()
	{
		dragging = true;
	}

	public void EndDrag()
	{
		dragging = false;
	}

}













