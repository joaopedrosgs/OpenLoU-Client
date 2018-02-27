using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingConstructionList : MonoBehaviour
{

    public GameObject ConstructionIcon;
    // Use this for initialization
    void Awake()
    {
        foreach (var constructiontype in DataHolder.ConstructionTypes)
        {
            var newIcon = Instantiate(ConstructionIcon);

            newIcon.transform.GetChild(0).GetComponent<Image>().sprite = constructiontype.Tile.sprite;
            newIcon.transform.GetChild(1).GetComponent<Text>().text = constructiontype.Name;
            newIcon.transform.SetParent(gameObject.transform);
            newIcon.transform.localScale = new Vector3(1, 1, 1);
            var position = newIcon.transform.localPosition;
            position.z = 0;
            newIcon.transform.localPosition = position;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
