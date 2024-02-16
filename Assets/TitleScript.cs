using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

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
