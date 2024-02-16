using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// -RotateScript-
/// 
/// Main Author: Michael Knighten
///
/// Start Date: 2/14/2024
/// 
/// </summary>
public class RotateScript : MonoBehaviour
{

    private Vector3 rotationAxis;
    private float rotationSpeed;

    private void Awake()
    {
        rotationSpeed = 70.0f;
        rotationAxis = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
