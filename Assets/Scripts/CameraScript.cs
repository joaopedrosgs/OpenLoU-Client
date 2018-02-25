using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public InputField XInput;
    public InputField YInput;
    public Tilemap Tilemap;

    public CityController CityController;
    public RegionController RegionController;
    private float minimumOrtographicSize;
    private Camera CameraComponent;
    public int ScrollStep = 1;
    public int MaxDistance = 8;
    private int MinDistance;


    void Start()
    {
        float mScale = Screen.height / 600F;
        minimumOrtographicSize = (Screen.height / (mScale * 128));
        CameraComponent = GetComponent<Camera>();
        CameraComponent.orthographicSize = minimumOrtographicSize;
        MinDistance = (int)minimumOrtographicSize;

    }

    // Update is called once per frame
    void Update()
    {
        var inputWheel = Input.GetAxis("Mouse ScrollWheel");
        if (inputWheel < 0 && CameraComponent.orthographicSize + ScrollStep < MaxDistance && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            CameraComponent.orthographicSize += ScrollStep;
        }
        else if (inputWheel > 0 && CameraComponent.orthographicSize + ScrollStep >= MinDistance && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            CameraComponent.orthographicSize -= ScrollStep;
        }

    }
    public void GoToTile()
    {
        var x = int.Parse(XInput.text);
        var y = int.Parse(YInput.text);
        var z = transform.position.z;
        var newPos = FindObjectOfType<Tilemap>().CellToWorld(new Vector3Int(x, y, 0));
        newPos.z = z;
        transform.position = newPos;
    }

    public void GoToTile(Construction construction)
    {

        var tilePos = CityController.Tilemap.CellToWorld(new Vector3Int(construction.X, construction.Y, 0));
        CityController.gameObject.transform.position -= tilePos;

    }

    public void GoToTile(City city)
    {
        var tilePos = RegionController.Tilemap.CellToWorld(new Vector3Int(city.X, city.Y, 0));
        RegionController.gameObject.transform.position -= tilePos;
    }

    // Use this for initialization

}