using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{

    public int X, Y, Type;
    public UnityEngine.UI.Image BuildingIcon;

    public UnityEngine.UI.Text BuildingName;
    public UnityEngine.UI.Text BuildingInfo;
    public UnityEngine.UI.Button ConstructButton;

    public UIController UIController;

    // Use this for initialization
    private void OnEnable()
    {
        var constructionType = DataHolder.ConstructionTypes.Find(X => X.ID == Type);
        BuildingName.text = constructionType.Name;
        if (constructionType.Tile as UnityEngine.Tilemaps.Tile)
            BuildingIcon.sprite = ((UnityEngine.Tilemaps.Tile)constructionType.Tile).sprite;
        else if (constructionType.Tile as UnityEngine.Tilemaps.AnimatedTile)
            BuildingIcon.sprite = ((UnityEngine.Tilemaps.AnimatedTile)constructionType.Tile).m_AnimatedSprites[0];
        else if (constructionType.Tile as UnityEngine.Tilemaps.RandomTile)
            BuildingIcon.sprite = ((UnityEngine.Tilemaps.RandomTile)constructionType.Tile).m_Sprites[0];
        BuildingInfo.text = constructionType.Info;
        ConstructButton.onClick.AddListener(() => UIController.CreateConstruction(X, Y, Type));

    }

    // Update is called once per frame
    void Update()
    {

    }
}
