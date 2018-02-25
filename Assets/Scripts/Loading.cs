using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

    public Slider Slider;
    // Use this for initialization
    bool Loaded = false;
    public List<UnityEngine.Tilemaps.Tile> CityConstructionTiles;

    public List<UnityEngine.Tilemaps.Tile> RegionCityTiles;

    public TextAsset[] ConstructionsJson;

    void Start()
    {
        DataHolder.RegionCities = new List<City>();
        DataHolder.UserCities = new List<City>();
        Client.SetupSocket();

        StartCoroutine(LoadCityView());
        StartCoroutine(LoadInitialInfo());
    }

    private IEnumerator LoadInitialInfo()
    {
        Client.WriteToServer(AnswerTypes.GetCitiesFromUser, null);
        PopulateConstructionList();
        yield return new WaitUntil(() => DataHolder.UserCities.Count > 0);
        var map = new Dictionary<string, int>();
        Debug.Log(DataHolder.SelectedCity.ID);
        map["CityID"] = DataHolder.SelectedCity.ID;
        Client.WriteToServer(AnswerTypes.GetConstructions, map);
        yield return new WaitUntil(() => DataHolder.SelectedCity.Data != null);
        foreach (var ConstructionType in DataHolder.ConstructionTypes)
        {
            ConstructionType.Tile = CityConstructionTiles.Find(x => x.name == ConstructionType.ID.ToString());
        }
        DataHolder.ConstructionTypes = DataHolder.ConstructionTypes.OrderBy(x => x.ID).ToList();
        DataHolder.RegionCityTiles = RegionCityTiles.OrderBy(x => x.name).ToList();
        Loaded = true;

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
            DataHolder.ConstructionTypes.Add(construction);
            Debug.Log(construction.ID);
        }
        DataHolder.ConstructionTypes = DataHolder.ConstructionTypes.OrderBy(x => x.ID).ToList();

    }
}
