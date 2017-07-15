using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float zoomSpeed = 147.0f; //Zoom speed when you scroll in or out
    public float zoomRotate = 40.0f; //When the camera rotate a little while zooming
    public float zoomMin = 7.0f; //Minimun value to zoom in
    public float zoomMax = 20.0f; //Maximun value to zoom out

    public Vector3 endRotate;
    public Vector3 endMarker;
    public bool isRotate = false;
    public int Boundary;
    public int speed;
    private int theScreenWidth;
    float minFov = 15f;
    float maxFov = 90f;
    float sensitivity = 30f;

    void Start()
    {
        theScreenWidth = Screen.width;
        endMarker = transform.position;
        endRotate = transform.eulerAngles;
    }

    void Update()
    {
        if (Input.mousePosition.x > theScreenWidth - Boundary)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * speed);
            isRotate = true;
        }
        if (Input.mousePosition.x < 0 + Boundary)
        {
            transform.RotateAround(Vector3.zero, Vector3.down, Time.deltaTime * speed);
            isRotate = true;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > zoomMin)
        {
            isRotate = false;
            if (transform.position.y > zoomRotate)
            {
                endMarker = transform.position + Vector3.down * (int)(Time.deltaTime * zoomSpeed);
            }
            else
            {
                endMarker = transform.position + Vector3.down * (int)(Time.deltaTime * zoomSpeed);
                endRotate = transform.eulerAngles + Vector3.left * (int)(Time.deltaTime * zoomSpeed);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y < zoomMax)
        {
            isRotate = false;
            if (transform.position.y < zoomRotate)
            {
                endMarker = transform.position + Vector3.up * (int)(Time.deltaTime * zoomSpeed);
                endRotate = transform.eulerAngles + Vector3.right * (int)(Time.deltaTime * zoomSpeed);
            }
            else
            {
                endMarker = transform.position + Vector3.up * (int)(Time.deltaTime * zoomSpeed);
            }
        }
        if (!isRotate)
        {
            transform.position = Vector3.Lerp(transform.position, endMarker, Time.deltaTime * 4);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, endRotate, Time.deltaTime * 4);
        }
    }

}
