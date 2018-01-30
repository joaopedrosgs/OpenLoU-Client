using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public static class DataHolder
{
    public static string Key { get; set; }

    public static List<City> UserCities { get; set; }

    public static List<City> RegionCities { get; set; }

    public static City SelectedCity;


    public static List<Dropdown.OptionData> GetDropdownOptions()
    {
        List<Dropdown.OptionData> names = new List<Dropdown.OptionData>();
        foreach (var city in UserCities)
        {
            CityDropdownData data = new CityDropdownData(city.Name, city.ID);
            names.Add(data);
        }

        return names;
    }
}

public class CityDropdownData : Dropdown.OptionData
{
    public int CityID;

    public CityDropdownData(string text, int cityId) : base(text)
    {
        CityID = cityId;
    }

    public CityDropdownData(string text, Sprite image, int cityId) : base(text, image)
    {
        CityID = cityId;
    }
}