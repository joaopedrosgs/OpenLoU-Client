using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDetail : MonoBehaviour
{
    public Text ConstructionName;
    public Image ConstructionImage;
    public Text ConstructionText;

    public Button UpgradeButton;

    public UIController UiController;

    public void SetConstruction(Construction _construction)
    {
        var constructionType = DataHolder.ConstructionTypes.Find(x => x.ID == _construction.Type);
        ConstructionName.text = constructionType.Name;
        if (constructionType.Tile as UnityEngine.Tilemaps.Tile)
            ConstructionImage.sprite = ((UnityEngine.Tilemaps.Tile)constructionType.Tile).sprite;
        else if (constructionType.Tile as UnityEngine.Tilemaps.AnimatedTile)
            ConstructionImage.sprite = ((UnityEngine.Tilemaps.AnimatedTile)constructionType.Tile).m_AnimatedSprites[0];
        else if (constructionType.Tile as UnityEngine.Tilemaps.RandomTile)
            ConstructionImage.sprite = ((UnityEngine.Tilemaps.RandomTile)constructionType.Tile).m_Sprites[0];
        ConstructionText.text = constructionType.Info;
        UpgradeButton.onClick.AddListener(() => UiController.UpgradeConstruction(_construction));
    }

    // Use this for initialization
}