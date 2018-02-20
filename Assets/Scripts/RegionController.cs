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
            Client.WriteToServer(AnswerTypes.GetCities, map);
            Camera.main.GetComponent<CameraScript>().GoToTile(
                DataHolder.UserCities[0].X,
                DataHolder.UserCities[0].Y,
                DataHolder.UserCities[0].ContinentID);
        }
    }

    // Update is called once per frame

    public void UpdateRegionView()
    {;

        foreach (var city in DataHolder.RegionCities)
        {
            Debug.Log("Setting tile on" + city.X + "And" + city.Y);
            RegionTileMapController.SetTile(city.X, city.Y,city.ContinentID, city.Type);
        }
    }
}