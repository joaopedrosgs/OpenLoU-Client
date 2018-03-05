using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

    public Slider Slider;
    // Use this for initialization
    bool Loaded = false;
    public List<UnityEngine.Tilemaps.TileBase> CityConstructionTiles;

    public List<UnityEngine.Tilemaps.TileBase> RegionCityTiles;

    public TextAsset[] ConstructionsJson;

    public Text InfoLoading;

    void Awake()
    {

        DataHolder.Cities = new List<City>();
        Client.SetupSocket();
    }
    private void Start()
    {

        StartCoroutine(LoadCityView());
        StartCoroutine(LoadInitialInfo());
    }

    private IEnumerator LoadInitialInfo()
    {
        InfoLoading.text = "Requesting user data";
        Client.GetCitiesFromUser();
        yield return new WaitUntil(() => DataHolder.Cities.Count > 0);
        InfoLoading.text = "Populating Construction List";
        PopulateConstructionList();
        InfoLoading.text = "Populating Tile List";
        PopulateTilesList();
        InfoLoading.text = "Requesting User constructions";
        Client.GetCityConstructions(DataHolder.SelectedCity);
        yield return new WaitUntil(() => DataHolder.SelectedCity.Constructions != null);
        Client.GetUpdatesFromCity(DataHolder.SelectedCity);
        yield return new WaitForSeconds(1);

        Loaded = true;

    }

    private void PopulateTilesList()
    {

        for (var i = 0; i < DataHolder.ConstructionTypes.Count; i++)
        {
            var type = DataHolder.ConstructionTypes[i];
            type.Tile = CityConstructionTiles.First(x => x.name == type.ID.ToString()); // structs are immutable
            DataHolder.ConstructionTypes[i] = type;
        }
        DataHolder.ConstructionTypes = DataHolder.ConstructionTypes.OrderBy(x => x.ID).ToList();
        DataHolder.RegionCityTiles = RegionCityTiles.OrderBy(x => x.name).ToList();
    }

    private IEnumerator LoadCityView()
    {
        var loading = SceneManager.LoadSceneAsync(2);
        loading.allowSceneActivation = false;
        float progress = 0;
        while (progress < 0.9)
        {
            progress = Mathf.Clamp01(loading.progress / .9f);
            Slider.value = progress;
            yield return null;
        }
        yield return new WaitUntil(() => Loaded);
        loading.allowSceneActivation = true;

    }
    private void PopulateConstructionList()
    {

        DataHolder.ConstructionTypes = new List<ConstructionType>();
        foreach (var ConstructionJson in ConstructionsJson)
        {

            var construction = JsonConvert.DeserializeObject<ConstructionType>(ConstructionJson.text);
            if (!construction.Equals(default(ConstructionType)))
                DataHolder.ConstructionTypes.Add(construction);
            else
            {
                Debug.Log("Erro ao ler arquivo Json de nome: " + ConstructionJson.name);
            }
        }

        DataHolder.ConstructionTypes = DataHolder.ConstructionTypes.OrderBy(x => x.ID).ToList();
        Debug.Log("Numero:" + DataHolder.ConstructionTypes.Count);
    }
}
