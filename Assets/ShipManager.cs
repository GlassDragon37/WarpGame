using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShipManager : MonoBehaviour
{


    //Score Attributes:

    public TextMeshProUGUI scoreText;

    public float scoreCooldown = 10f;

    //Health Attributes:

    public TextMeshProUGUI healthText;

    public AudioClip[] audioE;

    public AudioSource audioEvent;


    private void ScoreSync()
    {
        scoreText.text = "X " + PersistantData.score;
    }

    private void HealthSync()
    {
        healthText.text = "" + PersistantData.health;
    }

    private void AddScore()
    {
        PersistantData.score += 5;

        playSoundE(audioE[0]);
    }

    private void playSoundE(AudioClip sound)
    {
        audioEvent.clip = sound;
        audioEvent.Play();
    }

    private void Start()
    {
        PersistantData.health = 3;

        PersistantData.score = 0;

        InvokeRepeating(nameof(AddScore), scoreCooldown, scoreCooldown);
    }

    void Update()
    {
        ScoreSync();
        HealthSync();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {

            playSoundE(audioE[1]);

            PersistantData.score = 0;

            DebrisSpawn.speed = 5;

            PersistantData.health -= 1;

            if (PersistantData.health <= 0)
            {
                SceneManager.LoadScene(2);// Ending
            }
        }
    }
}
