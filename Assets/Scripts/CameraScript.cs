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
    public float ScrollSpeed;


    void Start()
    {
        float mScale = Screen.height / 600F;
        minimumOrtographicSize = (Screen.height / (mScale * 128));
        CameraComponent = GetComponent<Camera>();
        CameraComponent.orthographicSize = minimumOrtographicSize;
    }

    private void Awake()
    {
        TilemapController = FindObjectOfType<TilemapController>();
    }

    // Update is called once per frame
    void Update()
    {
        var inputWheel = Input.GetAxis("Mouse ScrollWheel");
        if (CameraComponent.orthographicSize + inputWheel >= minimumOrtographicSize || inputWheel > 0)
        {
            CameraComponent.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ScrollSpeed *
                                                CameraComponent.orthographicSize;
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