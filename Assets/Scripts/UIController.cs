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

    private void Start()
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

        UpdateDropdownCities();
    }

    public void UpdateDropdownCities()
    {
        if (CitiesDropdown == null)
        {
            Start();
        }

        CitiesDropdown.ClearOptions();
        CitiesDropdown.AddOptions(DataHolder.GetDropdownOptions());
        OnUpdateDropdownCities();
    }

    public void OnUpdateDropdownCities()
    {
        continentDropdown.value = DataHolder.SelectedCity.ContinentID;
        XInputField.text = DataHolder.SelectedCity.X.ToString();
        YInputField.text = DataHolder.SelectedCity.Y.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenRegionView()
    {
        SceneManager.LoadScene(3);
    }

    public void OpenCityView()
    {
        SceneManager.LoadScene(2);
    }

    public City GetSelectedCity()
    {
        if (CitiesDropdown == null)
        {
            Start();
        }

        var indice = CitiesDropdown.value;
        var CityID = ((CityDropdownData) CitiesDropdown.options[indice]).CityID;
        return DataHolder.UserCities.First(city => city.ID == CityID);
    }

    public void ShowInfoAboutConstruction(Vector3Int cell)
    {
        if (DataHolder.SelectedCity.Data.Constructions.Contains(new Construction {X = cell.x, Y = cell.y}))
        {
            var construction =
                DataHolder.SelectedCity.Data.Constructions.First(x =>
                    x.X == cell.x && x.Y == cell.y);
            if (construction != null)
            {
                var dic = new Dictionary<string, int>();
                dic["X"] = cell.x;
                dic["Y"] = cell.y;
                dic["CityID"] = DataHolder.SelectedCity.ID;
                var res = Client.WriteToServer(AnswerTypes.UpgradeConstruction, dic);
                Debug.Log(res);
            }
        }
    }
}