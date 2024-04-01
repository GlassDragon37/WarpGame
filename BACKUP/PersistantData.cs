using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 
/// -PersistantData-
/// 
/// Main Author: Michael Knighten
/// Start Date: 2/20/2024
/// 
/// </summary>
public class PersistantData : MonoBehaviour
{
    //I wish I knew about Singletons from the start........

    //Static reference to the instance of the Singleton
    public static PersistantData Instance { get; private set; }
    //How to access: PersistantData Singleton = PersistantData.Instance;

    //AS:
    public AudioSource Scene_Music;
    public AudioClip[] music;
    public static int score;
    public static int health;

    private void Awake()
    {
        //Ensure that there is only one instance of the Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }
}