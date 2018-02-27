using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public static class DataHolder
{
    public static string Key { get; set; }
    public static List<City> UserCities { get; set; }
    public static List<City> RegionCities { get; set; }
    public static List<UnityEngine.Tilemaps.TileBase> RegionCityTiles;
    public static List<ConstructionType> ConstructionTypes;
    public static List<ConstructionUpdate> ConstructionUpdates;



    private static City _selectedCity;
    public static Construction SelectedConstruction { get; set; }
    public static City SelectedCity
    {
        get
        {
            if (_selectedCity == null)
            {
                if (UserCities != null && UserCities.Count() > 0)
                    _selectedCity = UserCities[0];
            }

            return _selectedCity;
        }
        set
        {
            _selectedCity = value;
            var uiController = GameObject.FindObjectOfType<UIController>();
            if (uiController != null)
            {
                uiController.UpdateCityInfo();
            }
            else
            {
                Debug.Log("UiController not found");
            }
            var cityController = GameObject.FindObjectOfType<CityController>();
            if (cityController != null)
            {
                cityController.StartCoroutine(cityController.UpdateCityView());
            }
            else
            {
                Debug.Log("CityController not found, Maybe in Region View?");
            }

        }
    }


    public static List<Dropdown.OptionData> GetDropdownOptions()
    {
        List<Dropdown.OptionData> names = new List<Dropdown.OptionData>();
        if (UserCities != null)
        {
            foreach (var city in UserCities)
            {
                CityDropdownData data = new CityDropdownData(city.Name, city.ID);
                names.Add(data);
            }
            names = names.OrderBy(x => x.text).ToList();
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