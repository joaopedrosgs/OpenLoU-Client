using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Receiver : MonoBehaviour
{
    public RegionController RegionController;
    public CityController CityController;
    public UIController UIController;

    private void Start()
    {
        StartCoroutine(Client.PopAndWrite());
        StartCoroutine(Client.ReadFromServer());
    }
    private void Update()
    {

        if (!Client.IsAlive())
        {
            SceneManager.LoadScene(0);
        }
        else if (Client.AnswerList.Any())
        {
            ProcessAnswers(Client.AnswerList.Dequeue());
        }
    }

    private void ProcessAnswers(string received)
    {
        Debug.Log(received);
        var answerGeneric = JsonConvert.DeserializeObject<AnswerGeneric>(received);

        switch (answerGeneric.Type)
        {
            case RequestType.UpgradeConstruction:
                {
                    break;
                }
            case RequestType.GetUpgrades:
                {
                    Debug.Log("Recebendo Upgrades");
                    var updates = JsonConvert.DeserializeObject<ConstructionUpdates>(received);
                    if (updates.Data.Any() && DataHolder.Cities != null)
                    {
                        var cityFound = DataHolder.Cities.First(city => city.X == updates.Data[0].CityX && city.Y == updates.Data[0].CityY);

                        cityFound.BuildingQueue = updates.Data;
                        if (updates.Data != null && updates.Data.Any() && UIController != null)
                            UIController.UpdateBuildingQueue();
                    }
                    else
                    {

                    }
                    break;
                }
            case RequestType.GetConstructions:
                {
                    Debug.Log("Recebendo construcoes");
                    var constructions = JsonConvert.DeserializeObject<Constructions>(received);
                    if (DataHolder.Cities != null && constructions != null)
                    {
                        var cityFound = DataHolder.Cities.First(city => city.Contains(constructions.Data[0]));

                        cityFound.Constructions = constructions.Data;
                        if (CityController != null)
                            StartCoroutine(CityController.UpdateCityView());
                    }
                    else
                    {
                        Debug.Log("Erro ao receber");
                    }

                    break;
                }
            case RequestType.CreateCity:
                break;
            case RequestType.GetCities:
                {
                    var cities = JsonConvert.DeserializeObject<Cities>(received);

                    DataHolder.Cities.AddRange(cities.Data);

                    if (RegionController.gameObject.activeInHierarchy)
                        RegionController.UpdateRegionView();
                }
                break;
            case RequestType.GetCitiesFromUser:
                {
                    var cities = JsonConvert.DeserializeObject<Cities>(received);
                    foreach (var city in cities.Data)
                    {
                        city.Constructions = new List<Construction>();
                        city.BuildingQueue = new List<ConstructionUpdate>();
                        DataHolder.Cities.Add(city);
                    }
                    if (UIController != null)
                        UIController.PopulateCitiesDropdown();
                }
                break;
            default:
                {
                    break;
                }

        }
    }

    // Use this for initialization
}