using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
///
/// Author: Michael Knighten
/// Start Date: 2/11/2024
/// 
/// </summary>
public class ShipManager : MonoBehaviour
{


    //Score Attributes:

    public TextMeshProUGUI scoreText;

    public float scoreCooldown = 10f;

    //Health Attributes:

    public TextMeshProUGUI healthText;

    public AudioClip[] audioE;

    public AudioSource audioEvent;

    //Visual Damage Indicator:

    public GameObject Explosion;

    private bool showExplosion;

    public float cooldownExplosion;

    public string gameMode;


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
        gameMode = SceneManager.GetActiveScene().name;
        showExplosion = false;

        PersistantData.health = 3;

        PersistantData.score = 0;

        InvokeRepeating(nameof(AddScore), scoreCooldown, scoreCooldown);
    }

    void Update()
    {
        ScoreSync();
        HealthSync();
        ExplosionVisual();
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
                if (gameMode == "AR Mode")
                {
                    SceneManager.LoadScene(4);
                }
                else
                {
                    SceneManager.LoadScene(2);// Ending
                }
            }

            //Explosion:
            showExplosion = true;
            StartCoroutine(ToggleExplosionActivationOff());

        }
    }

    private IEnumerator ToggleExplosionActivationOff()
    {
        yield return new WaitForSeconds(cooldownExplosion);
        showExplosion = false;

    }

    private void ExplosionVisual() 
    {
        if (showExplosion)
        {
            Explosion.SetActive(true);
        }
        else 
        {
            Explosion.SetActive(false);
        }
    }
}
