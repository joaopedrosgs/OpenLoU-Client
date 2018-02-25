using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionInfo : MonoBehaviour
{
    public Text ConstructionName;
    public Image ConstructionImage;
    public Text ConstructionText;
    private Construction construction;
    private void Update()
    {

    }

    public void SetConstruction(Construction _construction)
    {
        // var type = DataHolder

        construction = _construction;
    }

    // Use this for initialization
}