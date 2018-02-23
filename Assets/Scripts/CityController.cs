using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CityController : MonoBehaviour
{
    public Tilemap Tilemap;

    private UIController uiController;
    // Use this for initialization

    public TextAsset[] ConstructionsJson;
    public List<ConstructionType> Constructions;
    private bool constructionsLoaded;

    private void Awake()
    {
        Constructions = new List<ConstructionType>();
        var i = 0;
        StartCoroutine(PopulateConstructionList());


    }
    private IEnumerator PopulateConstructionList()
    {
        foreach (var ConstructionJson in ConstructionsJson)
        {
            var construction = JsonConvert.DeserializeObject<ConstructionType>(ConstructionJson.text);
            string[] path = { "Assets/Modules/constructions" };
            var guid = AssetDatabase.FindAssets(construction.Name + " t:tile");
            if (guid.Any())
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid[0]);
                construction.Tile = (UnityEngine.Tilemaps.Tile)AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Tilemaps.Tile));
                Debug.Log(assetPath);
            }
            Constructions.Add(construction);
            Debug.Log(construction.ID);
        }
        constructionsLoaded = true;
        yield return null;

    }
    private void OnEnable()
    {
        if (DataHolder.SelectedCity != null)
            StartCoroutine(UpdateCityView());
    }

    public IEnumerator UpdateCityView()
    {
        yield return new WaitUntil(() => constructionsLoaded);

        if (DataHolder.SelectedCity.Data == null || DataHolder.SelectedCity.Data.Constructions == null)
        {
            var map = new Dictionary<string, int>();
            map["CityID"] = DataHolder.SelectedCity.ID;
            Client.WriteToServer(AnswerTypes.GetConstructions, map);

        }
        else
        {

            Tilemap.ClearAllTiles();
            if (DataHolder.SelectedCity.Data != null &&
                DataHolder.SelectedCity.Data.Constructions != null &&
                DataHolder.SelectedCity.Data.Constructions.Any())
                foreach (var construction in DataHolder.SelectedCity.Data.Constructions)
                {
                    var constructionClass = Constructions.First(x => x.ID == construction.Type);
                    if (constructionClass != null)
                    {
                        if (constructionClass.Tile != null)
                        {
                            Tilemap.SetTile(new Vector3Int(construction.X, construction.Y, 0), constructionClass.Tile);
                        }
                        else
                        {
                            Debug.Log("A construcao existe, mas nao tem tile definido");
                        }
                    }
                    else
                    {
                        Debug.Log("Tile nao encontrado, tipo:" + construction.Type);

                    }
                }
            else
            {
                Debug.Log("Nenhuma construção");
                Debug.Log(DataHolder.SelectedCity.Data);
            }
        }
        gameObject.transform.position -= Tilemap.CellToWorld(new Vector3Int(11, 11, 0));
        yield return null;

    }

}