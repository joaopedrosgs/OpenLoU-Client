using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class RegionController : MonoBehaviour
{

    private TilemapController RegionTileMapController;

    // Use this for initialization
    void Start()
    {
        RegionTileMapController = FindObjectOfType<TilemapController>();
        RegionTileMapController.ClearTiles();

        var map = new Dictionary<string, int>();
        if (DataHolder.UserCities.Count > 0)
        {
            map["X"] = DataHolder.UserCities[0].X;
            map["Y"] = DataHolder.UserCities[0].Y;
            map["Continent"] = DataHolder.UserCities[0].ContinentID;
            map["Range"] = 10;
            var received = Client.WriteToServer(AnswerTypes.GetCities, map);
            if (received != null)
            {
                ProcessAnswers(received);
            }

            Camera.main.GetComponent<CameraScript>().GoToTile(
                DataHolder.UserCities[0].X,
                DataHolder.UserCities[0].Y,
                DataHolder.UserCities[0].ContinentID);
        }
    }

    // Update is called once per frame

    private void ProcessAnswers(string answerJson)
    {
        Debug.Log(answerJson + "\n");
        var answerGeneric = JsonConvert.DeserializeObject<AnswerGeneric>(answerJson);

        if (!answerGeneric.Ok)
            return;

        switch (answerGeneric.Type)
        {
            case AnswerTypes.GetCities:
            {
                var cities = JsonConvert.DeserializeObject<Cities>(answerJson);
                DataHolder.RegionCities = cities.Data;
                Debug.Log(cities.Data[0].Name);
                UpdateRegionView();
                break;
            }
            case AnswerTypes.CreateCity:
            {
                break;
            }
            case AnswerTypes.UpgradeConstruction:
            {
                break;
            }
            case AnswerTypes.NewConstruction:
            {
                break;
            }
            case AnswerTypes.GetConstructions:
            {
                break;
            }
            default:
            {
                break;
            }
        }
    }

    private void UpdateRegionView()
    {;

        foreach (var city in DataHolder.RegionCities)
        {
            Debug.Log("Setting tile on" + city.X + "And" + city.Y);
            RegionTileMapController.SetTile(city.X, city.Y,city.ContinentID, city.Type);
        }
    }
}