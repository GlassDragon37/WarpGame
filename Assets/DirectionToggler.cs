using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DirectionToggler : MonoBehaviour
{
    public float interval; //Seconds
    private float timer = 0.0f;
    private int index = 0;
    private readonly bool[] asteroidDirection = new bool[4];

    public TextMeshProUGUI TextDirectionIndicator;

    private void Start()
    {
        TextDirectionIndicator.text = "Face North";
    }

    void Update()
    {
        // Increments the timer by the time elapsed since the last frame:
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            ToggleDirection();

            // Reset Timer
            timer = 0.0f; 
        }

        AsteroidSpawnerActive();
    }

    void ToggleDirection()
    {
        for (int i = 0; i < asteroidDirection.Length; i++)
        {
            asteroidDirection[i] = (i == index);
            // if i is equal to the value of the index then asteroidDirection[i] becomes true.
        }
        index = (index + 1) % asteroidDirection.Length;
        //Toggles through the array and ensures a reset to 0 when completing a loop;

    }

    void AsteroidSpawnerActive() 
    {
        if (asteroidDirection[0])
        {
            //North:
            TextDirectionIndicator.text = "Face North";
            GameObject.Find("North").GetComponent<AsteroidSpawner>().ChangeActive(true);
            GameObject.Find("South").GetComponent<AsteroidSpawner>().ChangeActive(false);
            GameObject.Find("East").GetComponent<AsteroidSpawner>().ChangeActive(false);
            GameObject.Find("West").GetComponent<AsteroidSpawner>().ChangeActive(false);

        }
        else if (asteroidDirection[1])
        {
            //South:
            TextDirectionIndicator.text = "Face South";
            GameObject.Find("North").GetComponent<AsteroidSpawner>().ChangeActive(false);
            GameObject.Find("South").GetComponent<AsteroidSpawner>().ChangeActive(true);
            GameObject.Find("East").GetComponent<AsteroidSpawner>().ChangeActive(false);
            GameObject.Find("West").GetComponent<AsteroidSpawner>().ChangeActive(false);

        }
        else if (asteroidDirection[2])
        {
            //East:
            TextDirectionIndicator.text = "Face East";
            GameObject.Find("North").GetComponent<AsteroidSpawner>().ChangeActive(false);
            GameObject.Find("South").GetComponent<AsteroidSpawner>().ChangeActive(false);
            GameObject.Find("East").GetComponent<AsteroidSpawner>().ChangeActive(true);
            GameObject.Find("West").GetComponent<AsteroidSpawner>().ChangeActive(false);

        }
        else if (asteroidDirection[3])
        {
            //West:
            TextDirectionIndicator.text = "Face West";
            GameObject.Find("North").GetComponent<AsteroidSpawner>().ChangeActive(false);
            GameObject.Find("South").GetComponent<AsteroidSpawner>().ChangeActive(false);
            GameObject.Find("East").GetComponent<AsteroidSpawner>().ChangeActive(false);
            GameObject.Find("West").GetComponent<AsteroidSpawner>().ChangeActive(true);

        }
    }
}
