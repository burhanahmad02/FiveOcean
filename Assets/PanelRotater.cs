using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRotater : MonoBehaviour
{
    public GameObject prefabOfHighScoreBox;
    public GameObject highscoreInsertionParent;
    public Sprite[] highscoreRoverSprites;
    public int highscorePanelIndex;
    public float timeAfterWhichPanelWillSwitch;
    public List <GameObject> panels;
    private int panelIndex = 0;
    private bool highscoreStringHasBeenChecked = false;

    private bool uiHasBeenMade = false;

    void Awake()
    {
        EncryptedPlayerPrefs.SetString("HighScoreForNews", "");
        //GameServerData.Instance.GetDataForMenu(5);
    }
    void OnEnable()
    {
        foreach (var item in panels)
        {
            item.SetActive(true);
        }
        panelIndex = 0;
        panels[panelIndex].GetComponent<Animator>().SetTrigger("center");
        StartCoroutine(WaitForTheTimeOut());
    }
    public static void AssignHighscoreString(string highscore)
    {
        EncryptedPlayerPrefs.SetString("HighScoreForNews", highscore);
    }
    void RotatePanels()
    {
        int previousIndex = panelIndex - 1;

        if (previousIndex < 0)
        {
            previousIndex = panels.Count - 1;
        }
        
        panels[panelIndex].GetComponent<Animator>().SetTrigger("center");
        panels[previousIndex].GetComponent<Animator>().SetTrigger("right");
        StartCoroutine(WaitForTheTimeOut());
    }
    IEnumerator WaitForTheTimeOut()
    {
        WaitForSeconds ws = new WaitForSeconds(timeAfterWhichPanelWillSwitch);
        yield return ws;
        panelIndex++;
        if (panelIndex >= panels.Count)
        {
            panelIndex = 0;
        }

        if (panelIndex == highscorePanelIndex)
        {
            string highScoresString = EncryptedPlayerPrefs.GetString("HighScoreForNews");
            if (highScoresString == "" && !highscoreStringHasBeenChecked)
            {
                highscoreStringHasBeenChecked = true;
                panels[highscorePanelIndex].SetActive(false);
                panels.RemoveAt(highscorePanelIndex);
                if (panelIndex >= panels.Count)
                {
                    panelIndex = 0;
                }
            }
            else
            {
                if (!uiHasBeenMade)
                {
                    uiHasBeenMade = true;
                    MakeUIForTopPlayers();
                }
            }
        }
        StopCoroutine(WaitForTheTimeOut());
        RotatePanels();
    }
    void MakeUIForTopPlayers()
    {
        string highScoresString = EncryptedPlayerPrefs.GetString("HighScoreForNews");
        string [] highscoreSplit = highScoresString.Split('!');

        //string[] rows = highScoresString.Split(';');
        string[] rows = highscoreSplit[1].Split(';');
        ;
        int limit = rows.Length;
        limit = Mathf.Clamp(limit, 0, 5);

        for (int i = 0; i < limit; i++)
        {
            if (rows[i] != "")
            {
                string[] rowResult = rows[i].Split(',');
                string name = rowResult[0];
                string score = rowResult[1];
                int selected = int.Parse(rowResult[2]);

                GameObject user = (GameObject)Instantiate(prefabOfHighScoreBox, highscoreInsertionParent.transform);
              /*  user.GetComponent<UserHandler>().playerName.text = name;
                user.GetComponent<UserHandler>().playerScore.text = score;
                //user.GetComponent<UserHandler>().selected.sprite = highscoreRoverSprites[selected];
                user.GetComponent<UserHandler>().rankNumber.text = (i + 1).ToString();*/
                user.name = "User_" + (i + 1);
            }
        }
    }
}
