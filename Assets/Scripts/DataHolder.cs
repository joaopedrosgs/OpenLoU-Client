using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public static class DataHolder
{
    public static User User;
    public static List<City> Cities { get; set; }
    public static List<UnityEngine.Tilemaps.TileBase> RegionCityTiles;
    public static List<ConstructionType> ConstructionTypes;


    private static City _selectedCity;
    public static Construction SelectedConstruction { get; set; }
    public static City SelectedCity
    {
        get
        {
            if (_selectedCity == null)
            {
                if (Cities != null && Cities.Count() > 0)
                    _selectedCity = Cities.ElementAt(0);
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
        if (Cities != null)
        {
            var userCities = Cities.FindAll(x => x.UserName == User.Name);
            foreach (var city in userCities)
            {
                CityDropdownData data = new CityDropdownData(city.Name, city.X, city.Y);
                names.Add(data);
            }
            names = names.OrderBy(x => x.text).ToList();
        }
        return names;
    }
}

public class CityDropdownData : Dropdown.OptionData
{
    public City City;

    public CityDropdownData(string text, City city) : base(text)
    {
        City = city;
    }

    public CityDropdownData(string text, Sprite image, City city) : base(text, image)
    {
        City = city;

    }

}