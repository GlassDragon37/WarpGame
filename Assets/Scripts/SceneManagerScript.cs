using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public Slider DifficultySlider;
    public Slider AudioSlider;
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

    public void DDASetter()
    {
        PersistantData.hardMode = (int)DifficultySlider.value;
        
    }//Use this to set the hardmode in the Singleton from the options menu.

    public void AudioSetter(int x) 
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
