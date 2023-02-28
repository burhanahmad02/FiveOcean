using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomText : MonoBehaviour
{
    public string[] Sentences;
    public Text textField;

    private void Start()
    {
        textField.text = Sentences[Random.Range(0, Sentences.Length)].ToString();
    }
}