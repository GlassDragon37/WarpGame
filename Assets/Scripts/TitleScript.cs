using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
///
/// Author: Michael Knighten
/// Start Date: 2/11/2024
/// 
/// </summary>
public class TitleScript : MonoBehaviour
{

    public void GoToTitle()
    {
        SceneManager.LoadScene(0);// Title
    }
    public void GoToLevel()
    {
        SceneManager.LoadScene(1); // GamePlay
    }
    public void GoToOptions()
    {
        SceneManager.LoadScene(2); // Options
    }
    public void GoToEnd()
    {
        SceneManager.LoadScene(3); // Ending
    }
}
