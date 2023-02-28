using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{

    public static int scoreNumber = 0;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    
// Start is called before the first frame update
void Start()
    {
        highScore.text = PlayerPrefs.GetInt("FiveOceanHighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("Score Number" + scoreNumber);
        score.text = scoreNumber.ToString();
        PlayerPrefs.SetInt("FiveOceanHighScore", scoreNumber);
        //Debug.Log(scoreNumber);
        if (scoreNumber > PlayerPrefs.GetInt("FiveOceanHighScore", 0))
        {
            PlayerPrefs.SetInt("FiveOceanHighScore", scoreNumber);
            highScore.text = scoreNumber.ToString();
        }
       /* if(scoreNumber == 40)
        {
           // UIScript.Instance.GameComplete(true);
        }*/
       
    }
}
