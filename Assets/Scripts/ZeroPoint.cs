using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///
/// Author: Michael Knighten
/// Start Date: 2/11/2024
/// 
/// </summary>
public class ZeroPoint : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
        }
    }
}
