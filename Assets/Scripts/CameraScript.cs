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
    public float ScrollStep = 0.01f;
    public int MaxDistance = 8;
    private int MinDistance;
    private float Size = 1;


    void Start()
    {
        SetSize(Size);

    }
    public void SetSize(float size)
    {
        minimumOrtographicSize = (Screen.height / (128));
        Debug.Log(minimumOrtographicSize);
        Debug.Log(size);
        CameraComponent = GetComponent<Camera>();
        CameraComponent.orthographicSize = minimumOrtographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        var inputWheel = Input.GetAxis("Mouse ScrollWheel");
        if (inputWheel < 0 && Size - ScrollStep > 0 && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            //SetSize(Size -= ScrollStep);
        }
        else if (inputWheel > 0 && Size + ScrollStep < MaxDistance && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            // SetSize(Size += ScrollStep);
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
    public void GoToTile(int x, int y)
    {

        var tilePos = CityController.Tilemap.CellToWorld(new Vector3Int(x, y, 0));
        CityController.gameObject.transform.position -= tilePos;

    }

    public void GoToTile(City city)
    {
        var tilePos = RegionController.Tilemap.CellToWorld(new Vector3Int(city.X, city.Y, 0));
        RegionController.gameObject.transform.position -= tilePos;
    }

    // Use this for initialization

}