using System.Collections;
using System.Collections.Generic;
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

    private void Update()
    {
        string received = Client.ReadFromServer();
        if (!Client.IsAlive())
        {
            SceneManager.LoadScene(0);
        }
        else if (received != null)
        {
            ProcessAnswers(received);
        }
    }

    private void ProcessAnswers(string received)
    {
        Debug.Log(received);
        var answerGeneric = JsonConvert.DeserializeObject<AnswerGeneric>(received);

        if (!answerGeneric.Ok)
        {

        }
        else
            switch (answerGeneric.Type)
            {
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
                        Debug.Log("Recebendo construcoes");
                        var constructions = JsonConvert.DeserializeObject<Constructions>(received);
                        if (DataHolder.UserCities != null && constructions != null)
                        {
                            var cityFound = DataHolder.UserCities.Find(city => city.ID == constructions.Data[0].CityID);
                            if (cityFound.Data == null)
                            {
                                cityFound.Data = new CityData();
                            }

                            cityFound.Data.Constructions = constructions.Data;
                            if (CityController != null)
                                StartCoroutine(CityController.UpdateCityView());
                        }
                        else
                        {
                            Debug.Log("Erro ao receber");
                        }

                        break;
                    }
                case AnswerTypes.CreateCity:
                    break;
                case AnswerTypes.GetCities:
                    {
                        var cities = JsonConvert.DeserializeObject<Cities>(received);
                        DataHolder.RegionCities = cities.Data;
                        Debug.Log(cities.Data[0].Name);
                        if (RegionController.gameObject.activeInHierarchy)
                            RegionController.UpdateRegionView();
                    }
                    break;
                case AnswerTypes.GetCitiesFromUser:
                    {
                        var userCities = JsonConvert.DeserializeObject<Cities>(received);
                        DataHolder.UserCities.AddRange(userCities.Data);
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