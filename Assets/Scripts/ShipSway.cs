using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipSway : MonoBehaviour
{

    public TextMeshProUGUI WhatDirection;

    public GameObject northObject;
    public GameObject eastObject;
    public GameObject southObject;
    public GameObject westObject;

    void Update()
    {
        // Get the camera's forward direction
        Vector3 cameraForward = Camera.main.transform.forward;

        // Calculate the angle between the camera's forward direction and each cardinal direction
        float angleNorth = Vector3.Angle(cameraForward, northObject.transform.position - Camera.main.transform.position);
        float angleEast = Vector3.Angle(cameraForward, eastObject.transform.position - Camera.main.transform.position);
        float angleSouth = Vector3.Angle(cameraForward, southObject.transform.position - Camera.main.transform.position);
        float angleWest = Vector3.Angle(cameraForward, westObject.transform.position - Camera.main.transform.position);

        // Determine which object the user is looking at based on the smallest angle
        if (Mathf.Min(angleNorth, angleEast, angleSouth, angleWest) == angleNorth)
        {
            WhatDirection.text = "North";
        }
        else if (Mathf.Min(angleNorth, angleEast, angleSouth, angleWest) == angleEast)
        {
            WhatDirection.text = "East";
        }
        else if (Mathf.Min(angleNorth, angleEast, angleSouth, angleWest) == angleSouth)
        {
            WhatDirection.text = "South";
        }
        else if (Mathf.Min(angleNorth, angleEast, angleSouth, angleWest) == angleWest)
        {
            WhatDirection.text = "West";
        }
    }
}
