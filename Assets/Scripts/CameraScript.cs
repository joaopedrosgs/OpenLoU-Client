using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public InputField XInput;
    public InputField YInput;
    public TilemapController TilemapController;
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

    private void Awake()
    {
        TilemapController = FindObjectOfType<TilemapController>();
    }

    // Update is called once per frame
    void Update()
    {
        var inputWheel = Input.GetAxis("Mouse ScrollWheel");
        if (inputWheel < 0 && CameraComponent.orthographicSize + ScrollStep < MaxDistance)
        {
            CameraComponent.orthographicSize += ScrollStep;
        }
        else if (inputWheel > 0 && CameraComponent.orthographicSize + ScrollStep >= MinDistance)
        {
            CameraComponent.orthographicSize -= ScrollStep;
        }

    }
    public void GoToTile()
    {
        var x = int.Parse(XInput.text);
        var y = int.Parse(YInput.text);
        var z = transform.position.z;
        var newPos = TilemapController.CellToWorld(x, y);
        newPos.z = z;
        transform.position = newPos;
    }

    public void GoToTile(int x, int y, int continent)
    {
        var z = transform.position.z;
        var newPos = TilemapController.CellToWorld(x, y, continent);
        newPos.z = z;
        transform.position = newPos;
    }

    public void GoToTile(int x, int y)
    {
        var z = transform.position.z;
        var newPos = TilemapController.CellToWorld(x, y);
        newPos.z = z;
        transform.position = newPos;
    }

    // Use this for initialization

}