using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseDrag : MonoBehaviour
{
    private float dist;
    private Vector3 MouseStart, MouseMove;
    public float ScrollSpeed;
    private float minimumOrtographicSize;
    private Camera CameraComponent;

    void Start ()
    {    
        dist = transform.position.z;  // Distance camera is above map
        float mScale = Screen.height / 600F;
        minimumOrtographicSize = (Screen.height / (mScale * 128));
        CameraComponent = GetComponent<Camera>();
    }
 
    void Update ()
    {
        var inputWheel = Input.GetAxis("Mouse ScrollWheel");
        if (CameraComponent.orthographicSize + inputWheel >= minimumOrtographicSize || inputWheel > 0)
        {
            CameraComponent.orthographicSize  += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ScrollSpeed*CameraComponent.orthographicSize;
        }
        if (Input.GetMouseButtonDown (1)) {
            MouseStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            Debug.Log(MouseStart);
        }
        else if (Input.GetMouseButton (1)) {
            MouseMove = new Vector3(Input.mousePosition.x - MouseStart.x, Input.mousePosition.y - MouseStart.y, dist);
            MouseStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            transform.position = new Vector3(transform.position.x - MouseMove.x * Time.deltaTime, transform.position.y - MouseMove.y * Time.deltaTime, dist);
        }
    }

}