using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] Debris;

    public float height;
    public float width;
    public static float spawn_timer = 1.0f;
    public static float timerCooldown = 1.0f;
    public static float speed = 5.0f;
    private float random_y;
    private float random_x;
    private int random_Object;

    public bool activateSpawner = false;
    public GameObject ship; // Reference to the ship object

    void Start()
    {
        SetRandomValues();
    }

    void Update()
    {
        spawn_timer -= Time.deltaTime;
        if (spawn_timer <= 0.0f)
        {
            SpawnDebris(activateSpawner);
        }
    }

    public void ChangeActive(bool x)
    {
        activateSpawner = x;
    }

    void SetRandomValues()
    {
        random_y = Random.Range(-height, height);
        random_x = Random.Range(-width, width);
        random_Object = Random.Range(0, Debris.Length);
    }

    void SpawnDebris(bool x)
    {
        if (x == true)
        {
            SetRandomValues();
            spawn_timer = timerCooldown;
            Vector3 position = transform.position;
            position.y += random_y;
            position.x += random_x;

            GameObject asteroid = Instantiate(Debris[random_Object], position, Quaternion.identity);

            // Calculate direction towards the ship
            Vector3 direction = (ship.transform.position - position).normalized;

            // Apply random offset to the direction
            direction += Random.insideUnitSphere * 0.2f; // Adjust the magnitude (0.2f) for desired randomness

            // Set velocity towards the ship with random offset
            Rigidbody asteroidRigidbody = asteroid.GetComponent<Rigidbody>();
            asteroidRigidbody.velocity = direction * speed;

            Destroy(asteroid, 5f);
        }
    }
}
