using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RandomTexts : MonoBehaviour
{

    public string[] Texts;
    // Use this for initialization
    void Start()
    {
        if (Texts.Length > 0)
            GetComponent<Text>().text = Texts[Random.Range(0, Texts.Length)];
    }
}

