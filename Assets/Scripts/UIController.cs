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
    public CityController CityView;

    public CameraScript CameraScript;

    public BuildingQueue BuildingQueue;

    public BuildingDetail BuildingDetailPopup;
    public BuildingConstruction BuildingConstructionPopup;
    public BuildingConstructionList BuildingConstructionListPopup;

    public GameObject PopupToolbar;

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
        continentDropdown.value = DataHolder.SelectedCity.ContinentID();
        XInputField.text = DataHolder.SelectedCity.X.ToString();
        YInputField.text = DataHolder.SelectedCity.Y.ToString();
    }

    public void OpenRegionView()
    {
        CityView.gameObject.SetActive(false);
        RegionView.SetActive(true);
        CameraScript.GoToTile(DataHolder.SelectedCity);

    }

    public void OpenCityView()
    {
        CityView.gameObject.SetActive(true);
        RegionView.SetActive(false);
        CameraScript.GoToTile(11, 11);

    }

    public City GetSelectedCity()
    {
        var indice = CitiesDropdown.value;
        var DropdownData = ((CityDropdownData)CitiesDropdown.options[indice]);
        return DropdownData.City;
    }

    public void ShowInfoAboutConstruction(Construction construction)
    {

        PopupToolbar.SetActive(true);
        BuildingDetailPopup.gameObject.SetActive(true);
        BuildingConstructionPopup.gameObject.SetActive(false);
        BuildingQueue.gameObject.SetActive(false);
        BuildingConstructionListPopup.gameObject.SetActive(false);
        BuildingDetailPopup.SetConstruction(construction);
    }

    public void UpgradeConstruction(Construction construction)
    {
        Client.UpgradeConstruction(construction);
    }



    public void UpdateCityInfo()
    {
        Debug.Log(JsonConvert.SerializeObject(GetSelectedCity()));
    }

    public void ShowConstructionList(int x, int y)
    {
        BuildingDetailPopup.gameObject.SetActive(false);
        BuildingConstructionPopup.gameObject.SetActive(false);
        BuildingQueue.gameObject.SetActive(false);
        BuildingConstructionListPopup.gameObject.SetActive(true);
        BuildingConstructionPopup.X = x;
        BuildingConstructionPopup.Y = y;
        PopupToolbar.GetComponentInChildren<Text>().text = "Select Building";
        PopupToolbar.SetActive(true);
    }
    public void CloseLeftSidePopup()
    {
        BuildingQueue.gameObject.SetActive(true);
        BuildingDetailPopup.gameObject.SetActive(false);
        BuildingConstructionPopup.gameObject.SetActive(false);
        BuildingConstructionListPopup.gameObject.SetActive(false);
        PopupToolbar.SetActive(false);
    }
    public void CreateConstruction(int x, int y, int type)
    {
        var index = DataHolder.SelectedCity.BuildingQueue.Count;
        if (index > 9)
            return;
        Client.CreateNewConstruction(x, y, type);
        var update = new ConstructionUpdate { };
        update.CityX = DataHolder.SelectedCity.X;
        update.CityY = DataHolder.SelectedCity.Y;
        update.Index = index + 1;
        update.Duration = TimeSpan.FromSeconds(10);
        if (index == 1) update.Start = DateTime.Now;
        DataHolder.SelectedCity.BuildingQueue.Add(update);

        Assets.Scripts.Construction construction = new Construction(DataHolder.SelectedCity.X, DataHolder.SelectedCity.Y, x, y, type, 0);
        DataHolder.SelectedCity.Constructions.Add(construction);
        CityView.AddConstruction(construction);
        BuildingQueue.AddElement(update);


    }
    public void UpdateBuildingQueue()
    {
        BuildingQueue.UpdateQueue();
    }

    public void ChangeCity(int direction)
    {
        CitiesDropdown.value = +direction;
        DataHolder.SelectedCity = DataHolder.Cities.ElementAt(CitiesDropdown.value);

    }

}