using System.Collections.Generic;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;

public class CityController : MonoBehaviour
{
	private TilemapController _constructionsTilemapController;

	private UIController _uiController;
	
	// Use this for initialization
	void Start ()
	{
		_uiController = GameObject.Find("UI").GetComponent<UIController>();
		_constructionsTilemapController = FindObjectOfType<TilemapController>();
		OnDropdownUpdate();
		

	}
	
	// Update is called once per frame
	private void ProcessAnswers(string answerJson)
	{
		Debug.Log(answerJson);
		var answerGeneric = JsonConvert.DeserializeObject<AnswerGeneric>(answerJson);

		if (!answerGeneric.Ok)
			return;

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
				var constructions = JsonConvert.DeserializeObject<Constructions>(answerJson);
				if (DataHolder.UserCities != null)
				{
					var cityFound = DataHolder.UserCities.Find(city => city.ID == constructions.Data[0].CityID);
					if (cityFound.Data == null)
					{
						cityFound.Data = new CityData();
					}
					cityFound.Data.Constructions = constructions.Data;
				}

				break;
			}
			default:
			{
				break;
			}
		}
	}

	public void OnDropdownUpdate()
	{
		DataHolder.SelectedCity = _uiController.GetSelectedCity();
		Camera.main.GetComponent<CameraScript>().GoToTile(11,11);
		_uiController.OnUpdateDropdownCities();
		UpdateCityView();

	}

	private void UpdateCityView()
	{
		if (DataHolder.SelectedCity.Data == null || DataHolder.SelectedCity.Data.Constructions == null)
		{
			var map = new Dictionary<string, int>();
			map["CityID"] = DataHolder.SelectedCity.ID;
			var received = Client.WriteToServer(AnswerTypes.GetConstructions, map);
			ProcessAnswers(received);
		}
		_constructionsTilemapController.ClearTiles();
		if (DataHolder.SelectedCity.Data != null && DataHolder.SelectedCity.Data.Constructions != null)
			foreach (var construction in DataHolder.SelectedCity.Data.Constructions)
			{
				_constructionsTilemapController.SetTile(construction.X, construction.Y, construction.Type);
			}
	}
}
