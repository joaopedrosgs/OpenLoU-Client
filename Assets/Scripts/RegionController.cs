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

    public Tilemap Tilemap;


    // Use this for initialization
    void Start()
    {

        Tilemap.ClearAllTiles();

        var map = new Dictionary<string, int>();
        if (DataHolder.UserCities.Count > 0)
        {
            map["X"] = DataHolder.SelectedCity.X;
            map["Y"] = DataHolder.SelectedCity.Y;
            map["Continent"] = DataHolder.SelectedCity.ContinentID;
            map["Range"] = 10;
            Client.WriteToServer(AnswerTypes.GetCities, map);
        }
    }

    // Update is called once per frame

    public void UpdateRegionView()
    {
        foreach (var city in DataHolder.RegionCities)
        {
            SetTile(city);
        }
    }
    public void SetTile(City city)
    {
        Tilemap.SetTile(new Vector3Int(city.X, city.Y, 0), FindTile(city));
    }
    public TileBase FindTile(City city)
    {


        string cityType = String.Empty;
        int number;
        switch (city.Type)
        {
            case CityType.Normal:
                cityType = "town";
                break;

            case CityType.Castle:

                cityType = "military";
                break;

            case CityType.Palace:

                cityType = "palace";
                break;
            default:
                break;
        }

        if (city.Points <= 9)
        {
            number = 1;
        }
        else if (city.Points <= 99)
        {
            number = 2;
        }
        else if (city.Points <= 299)
        {
            number = 3;
        }
        else if (city.Points <= 999)
        {
            number = 4;
        }
        else if (city.Points <= 2499)
        {
            number = 5;
        }
        else if (city.Points <= 4999)
        {
            number = 6;
        }
        else if (city.Points <= 7999)
        {
            number = 7;
        }
        else
        {
            number = 8;
        }
        return DataHolder.RegionCityTiles.Find(x => x.name.Contains(cityType) && x.name.Contains(number.ToString()));

    }
}