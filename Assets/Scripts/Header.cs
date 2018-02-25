using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class Header : MonoBehaviour
{
    public Text TitleText;
    public Text InfoText;
    [SerializeField] private string title;
    public string Title
    {
        get
        {
            return title;
        }
        set
        {
            TitleText.text = value;
            title = value;
        }
    }
    [SerializeField] private string info;
    public string Info
    {
        get
        {
            return info;
        }
        set
        {
            InfoText.text = value;
            info = value;
        }
    }

    private void Start()
    {
        TitleText.text = title;
        InfoText.text = info;

    }

    // Use this for initialization
}