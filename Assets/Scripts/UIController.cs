using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Use this for initialization

    private Dropdown CitiesDropdown;
    private Dropdown continentDropdown;

    private InputField XInputField;
    private InputField YInputField;

    public GameObject RegionView;
    public GameObject CityView;

    public CameraScript CameraScript;
    public ConstructionInfo ConstructionInfo;

    private void Awake()
    {
        XInputField = GameObject.Find("XInput").GetComponent<InputField>();
        YInputField = GameObject.Find("YInput").GetComponent<InputField>();
        CitiesDropdown = GameObject.Find("CitiesDropdown").GetComponent<Dropdown>();
        continentDropdown = GameObject.Find("ContinentDropdown").GetComponent<Dropdown>();
        continentDropdown.ClearOptions();
        for (int i = 1; i < 37; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData("C" + i.ToString());
            continentDropdown.options.Add(option);
        }

        PopulateCitiesDropdown();
    }

    public void PopulateCitiesDropdown()
    {
        CitiesDropdown.ClearOptions();
        CitiesDropdown.AddOptions(DataHolder.GetDropdownOptions());
    }

    public void OnSelectCityDropDown()
    {
        DataHolder.SelectedCity = GetSelectedCity();
        //Camera.main.GetComponent<CameraScript>().GoToTile(11, 11);
        continentDropdown.value = DataHolder.SelectedCity.ContinentID;
        XInputField.text = DataHolder.SelectedCity.X.ToString();
        YInputField.text = DataHolder.SelectedCity.Y.ToString();
    }

    public void OpenRegionView()
    {
        CityView.SetActive(false);
        RegionView.SetActive(true);
        CameraScript.GoToTile(DataHolder.SelectedCity);

    }

    public void OpenCityView()
    {
        CityView.SetActive(true);
        RegionView.SetActive(false);
        CameraScript.GoToTile(DataHolder.SelectedCity.Data.Constructions.Find(x => x.X == 11 && x.Y == 11));

    }

    public City GetSelectedCity()
    {
        if (CitiesDropdown == null)
        {
            CitiesDropdown = GameObject.Find("CitiesDropdown").GetComponent<Dropdown>();
        }

        var indice = CitiesDropdown.value;
        var CityID = ((CityDropdownData)CitiesDropdown.options[indice]).CityID;
        return DataHolder.UserCities.First(city => city.ID == CityID);
    }

    public void ShowInfoAboutConstruction(Construction construction)
    {
        ConstructionInfo.gameObject.SetActive(true);
        ConstructionInfo.SetConstruction(construction);
    }

    public void UpgradeConstruction(int x, int y, int cityId)
    {
        var dic = new Dictionary<string, int>();
        dic["X"] = x;
        dic["Y"] = y;
        dic["CityID"] = cityId;
        Client.WriteToServer(AnswerTypes.UpgradeConstruction, dic);
    }
    public void CreateConstruction(int Type)
    {
        var dic = new Dictionary<string, int>();
        dic["X"] = DataHolder.SelectedConstruction.X;
        dic["Y"] = DataHolder.SelectedConstruction.Y;
        dic["CityID"] = DataHolder.SelectedCity.ID;
        dic["Type"] = Type;
        Client.WriteToServer(AnswerTypes.NewConstruction, dic);
    }

    public void UpdateCityInfo()
    {
        Debug.Log(JsonConvert.SerializeObject(GetSelectedCity()));
    }

    public void ShowBuildMenu(int x, int y)
    {

    }
}