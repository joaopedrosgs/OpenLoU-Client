using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingConstructionList : MonoBehaviour
{
    private int type;
    public GameObject ConstructionIcon;
    public BuildingConstruction BuildingConstruction;

    public GameObject Content;
    // Use this for initialization
    void Awake()
    {
        foreach (var constructiontype in DataHolder.ConstructionTypes)
        {
            if (constructiontype.ID < 0)
                continue;
            var newIcon = Instantiate(ConstructionIcon);
            if (constructiontype.Tile as UnityEngine.Tilemaps.Tile)
                newIcon.transform.GetChild(0).GetComponent<Image>().sprite = ((UnityEngine.Tilemaps.Tile)constructiontype.Tile).sprite;
            else if (constructiontype.Tile as UnityEngine.Tilemaps.AnimatedTile)
                newIcon.transform.GetChild(0).GetComponent<Image>().sprite = ((UnityEngine.Tilemaps.AnimatedTile)constructiontype.Tile).m_AnimatedSprites[0];
            else if (constructiontype.Tile as UnityEngine.Tilemaps.RandomTile)
                newIcon.transform.GetChild(0).GetComponent<Image>().sprite = ((UnityEngine.Tilemaps.RandomTile)constructiontype.Tile).m_Sprites[0];

            newIcon.transform.GetChild(1).GetComponent<Text>().text = constructiontype.Name;
            newIcon.GetComponent<Button>().onClick.AddListener(() => ConstructionSelected(constructiontype.ID));
            newIcon.transform.SetParent(Content.transform);
            newIcon.transform.localScale = Vector3.one;
            var position = newIcon.transform.localPosition = Vector3.zero;
        }
    }

    void ConstructionSelected(int id)
    {
        BuildingConstruction.Type = id;
        BuildingConstruction.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
