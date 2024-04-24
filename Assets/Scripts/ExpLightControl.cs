using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///
/// Author: Michael Knighten
/// Start Date: 2/11/2024
/// 
/// </summary>
public class ExpLightControl : MonoBehaviour
{

    public Light explosionLight;


    private void Awake()
    {

        explosionLight.enabled = true;
        StartCoroutine(DelayDisableLight());
    }

    IEnumerator DelayDisableLight() 
    {
        yield return new WaitForSeconds(.07f);

        explosionLight.enabled = false;
    }
}
