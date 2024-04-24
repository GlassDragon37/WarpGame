using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///
/// Author: Michael Knighten
/// Start Date: 2/11/2024
/// 
/// </summary>
public class OptionsScene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleGyro(bool option)
    {
        PersistantData.gyro = !PersistantData.gyro;
    }
}
