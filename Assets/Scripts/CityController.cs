using System;
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

    public UIController UIController;
    // Use this for initialization

    public GameObject Decal;

    private void OnEnable()
    {
        if (DataHolder.SelectedCity != null)
            StartCoroutine(UpdateCityView());
    }

    public IEnumerator UpdateCityView()
    {

        if (DataHolder.SelectedCity != null && (DataHolder.SelectedCity == null || DataHolder.SelectedCity.Constructions == null))
        {
            Client.GetConstructionsFrom(DataHolder.SelectedCity);
            yield return new WaitUntil(() => DataHolder.SelectedCity != null);

        }

        Tilemap.ClearAllTiles();
        foreach (var construction in DataHolder.SelectedCity.Constructions)
        {
            AddConstruction(construction);
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
            if (hit && hit.collider != null && hit.transform.CompareTag("CityView"))
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
        var construction = DataHolder.SelectedCity.Constructions.Find(x => x.X == cell.x & x.Y == cell.y);
        if (construction == null)
            UIController.ShowConstructionList(cell.x, cell.y);
        else
            UIController.ShowInfoAboutConstruction(construction);

    }

    public void AddConstruction(Construction construction)
    {
        if (construction.Level == 0)
        {
            Tilemap.SetTile(new Vector3Int(construction.X, construction.Y, 0), DataHolder.ConstructionTypes.First(x => x.ID == -1).Tile);
            return;
        }
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