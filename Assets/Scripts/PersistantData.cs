using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 
/// -PersistantData-
/// 
/// Main Author: Michael Knighten
/// Secondary Author: Adele Rousseau
/// Start Date: 2/11/2024
/// 
/// </summary>
public class PersistantData : MonoBehaviour
{

    //Static reference to the instance of the Singleton
    public static PersistantData Instance { get; private set; }
    //How to access: PersistantData Singleton = PersistantData.Instance;

    //Music:
    public AudioSource Scene_Music;
    public AudioClip[] music;
    public static int score;
    public static int health;

    //Difficulty:
    public static int hardMode;

    //Gyro:
    public static bool gyro = false;

    //Toggle
    public bool musicPlaying = true;

    private void Awake()
    {
        //Ensure that there is only one instance of the Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            hardMode = 50;//-------------------------------------------Test
        }
        else
        {
            //If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playSound(music[0]);
    }

    private void playSound(AudioClip sound)
    {
        Scene_Music.clip = sound;
        Scene_Music.Play();
    }

    public void toggleMusic()
    {
        if(musicPlaying == true)
        {
            Scene_Music.volume = 0;
        }
        else if(musicPlaying == false)
        {
            Scene_Music.volume = 50;
        }
        musicPlaying = !musicPlaying;
    }

}