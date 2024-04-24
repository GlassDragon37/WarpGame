using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
