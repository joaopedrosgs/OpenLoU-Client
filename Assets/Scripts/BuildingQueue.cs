using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class BuildingQueue : MonoBehaviour
{
    public GameObject Content;
    public GameObject QueueElementPrefab;
    // Use this for initialization
    private void OnEnable()
    {
        UpdateQueue();




    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateQueue()
    {
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            Destroy(Content.transform.GetChild(i));
        }
        if (DataHolder.SelectedCity.BuildingQueue != null)
            foreach (var upgrade in DataHolder.SelectedCity.BuildingQueue)
            {
                AddElement(upgrade);

            }
    }
    public void AddElement(Assets.Scripts.ConstructionUpdate upgrade)
    {
        var newQueueElement = Instantiate(QueueElementPrefab);

        var icon = newQueueElement.transform.Find("ConstructionQueueElementIcon").GetComponent<Image>();
        var textName = newQueueElement.transform.Find("ConstructionQueueElementName").GetComponent<Text>();
        var construction = DataHolder.SelectedCity.Constructions.Find(x => x.X == upgrade.X && x.Y == upgrade.Y);
        var constructionType = DataHolder.ConstructionTypes.Find(x => x.ID == construction.Type);
        textName.text = constructionType.Name;
        if (constructionType.Tile as Tile)
            icon.GetComponent<Image>().sprite = ((Tile)constructionType.Tile).sprite;
        else if (constructionType.Tile as AnimatedTile)
            icon.GetComponent<Image>().sprite = ((AnimatedTile)constructionType.Tile).m_AnimatedSprites[0];
        else if (constructionType.Tile as RandomTile)
            icon.GetComponent<Image>().sprite = ((RandomTile)constructionType.Tile).m_Sprites[0];
        newQueueElement.transform.SetParent(Content.transform);
    }
}
