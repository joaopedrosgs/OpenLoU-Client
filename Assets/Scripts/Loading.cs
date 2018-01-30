using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

	public Slider slider;
	// Use this for initialization
	void Start () {
		DataHolder.RegionCities = new List<City>();
		DataHolder.UserCities = new List<City>();
		Client.SetupSocket();
		var received = Client.WriteToServer(AnswerTypes.GetCitiesFromUser,null);
		var Generic = JsonConvert.DeserializeObject<AnswerGeneric>(received);
		if (Generic.Ok)
		{
			var UserCities = JsonConvert.DeserializeObject<Cities>(received);
			DataHolder.UserCities.AddRange(UserCities.Data);
		}

		StartCoroutine(LoadCityView());
	}

	private IEnumerator LoadCityView()
	{
		AsyncOperation loading = SceneManager.LoadSceneAsync(2);
		while (!loading.isDone)
		{
			float progress = Mathf.Clamp01(loading.progress / .9f);
			slider.value = progress;
			yield return null;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
