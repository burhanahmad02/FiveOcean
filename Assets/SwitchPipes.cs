using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPipes : MonoBehaviour
{
    public Sprite[] pipeSprites;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangePipes());
    }
    IEnumerator ChangePipes()
    {
        yield return null;
        yield return null;
        if (GetComponent<SpriteRenderer>())
        {
            if (GameManager.Instance.environmentNumber > 0 && GameManager.Instance.environmentNumber < pipeSprites.Length)
            {
                GetComponent<SpriteRenderer>().sprite = pipeSprites[GameManager.Instance.environmentNumber];
            }
        }
    }
}
