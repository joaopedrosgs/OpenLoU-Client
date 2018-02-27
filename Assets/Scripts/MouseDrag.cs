using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseDrag : MonoBehaviour
{
    private float dist;
    private Vector3 MouseStart, MouseMove;

    private Camera CameraComponent;
    public GameObject CityView;
    public GameObject RegionView;
    private bool Draggin;


    void Start()
    {
        dist = transform.position.z; // Distance camera is above map
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            MouseStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            Draggin = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Draggin = false;
        }
        else if (Draggin)
        {
            MouseMove = new Vector3(Input.mousePosition.x - MouseStart.x, Input.mousePosition.y - MouseStart.y, dist);
            MouseStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            var offset = new Vector3(MouseMove.x * Time.deltaTime,
                MouseMove.y * Time.deltaTime, dist);
            if (CityView.activeInHierarchy)
            {
                var newPosition = CityView.transform.position + offset;
                CityView.transform.position = Vector3.Lerp(CityView.transform.position, newPosition, 1);
            }
            else if (RegionView.activeInHierarchy)
            {
                var newPosition = RegionView.transform.position + offset;
                RegionView.transform.position = newPosition;
            }
        }

    }
}