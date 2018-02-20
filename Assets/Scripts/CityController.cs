using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;

public class CityController : MonoBehaviour
{
    private TilemapController _constructionsTilemapController;

    // Use this for initialization
    void Awake()
    {
        _constructionsTilemapController = FindObjectOfType<TilemapController>();
    }

    private void OnEnable()
    {
        if (DataHolder.SelectedCity != null)
            UpdateCityView();
    }

    public void UpdateCityView()
    {
        if (DataHolder.SelectedCity.Data == null || DataHolder.SelectedCity.Data.Constructions == null)
        {
            var map = new Dictionary<string, int>();
            map["CityID"] = DataHolder.SelectedCity.ID;
            Client.WriteToServer(AnswerTypes.GetConstructions, map);

        }
        else
        {

            _constructionsTilemapController.ClearTiles();
            if (DataHolder.SelectedCity.Data != null &&
                DataHolder.SelectedCity.Data.Constructions != null &&
                DataHolder.SelectedCity.Data.Constructions.Any())
                foreach (var construction in DataHolder.SelectedCity.Data.Constructions)
                {
                    _constructionsTilemapController.SetTile(construction.X, construction.Y, construction.Type);

                }
            else
            {
                Debug.Log("Nenhuma construção");
                Debug.Log(DataHolder.SelectedCity.Data);
            }
        }
    }
}