using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CityController : MonoBehaviour
{
    public Tilemap Tilemap;

    private UIController uiController;
    // Use this for initialization

    public GameObject Decal;

    private void OnEnable()
    {
        if (DataHolder.SelectedCity != null)
            StartCoroutine(UpdateCityView());
    }

    public IEnumerator UpdateCityView()
    {

        if (DataHolder.SelectedCity != null && (DataHolder.SelectedCity.Data == null || DataHolder.SelectedCity.Data.Constructions == null))
        {
            var map = new Dictionary<string, int>();
            map["CityID"] = DataHolder.SelectedCity.ID;
            Client.WriteToServer(AnswerTypes.GetConstructions, map);

        }
        else
        {
            Debug.Log("Setando tiles");

            Tilemap.ClearAllTiles();
            foreach (var construction in DataHolder.SelectedCity.Data.Constructions)
            {
                var constructionClass = DataHolder.ConstructionTypes.First(x => x.ID == construction.Type);
                Debug.Log(construction.Type);
                Debug.Log(DataHolder.ConstructionTypes.Count);
                if (!constructionClass.Equals(default(ConstructionType)))
                {
                    if (constructionClass.Tile)
                    {
                        Tilemap.SetTile(new Vector3Int(construction.X, construction.Y, 0), constructionClass.Tile);
                        Debug.Log("Setando tile");
                    }
                    else
                        Debug.Log("Tile nao encontrado:" + constructionClass.Name);
                }
                else
                    Debug.Log("Tile nao encontrado, tipo:" + construction.Type);
            }
        }

        gameObject.transform.position -= Tilemap.CellToWorld(new Vector3Int(11, 11, 0));
        yield return null;

    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, -Vector2.up);
            if (hit && hit.collider != null)
            {
                HighlightTile(hit.point);
            }
            else
                Debug.Log("click errado");
        }
    }

    public void HighlightTile(Vector3 globalPos)
    {
        var cell = Tilemap.WorldToCell(globalPos);
        Decal.transform.localPosition = Tilemap.CellToLocal(cell);
        Decal.SetActive(true);
        var construction = DataHolder.SelectedCity.Data.Constructions.Find(x => x.X == cell.x & x.Y == cell.y);
        if (construction.Equals(construction))
            uiController.ShowBuildMenu(cell.x, cell.y);
        else
            uiController.ShowInfoAboutConstruction(construction);

    }

}