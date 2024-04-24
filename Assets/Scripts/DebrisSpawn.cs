using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
/// <summary>
/// 
/// Primary Author: Michael Knighten
/// Secondary Author: Jackson Cook
/// Start Date: 2/11/2024
/// 
/// </summary>
public class DebrisSpawn : MonoBehaviour
{
    public GameObject[] Debris;

    public float height;
    public float width;
    public static float spawn_timer = 1.0f;
    public static float timerCooldown = 1.0f;
    private float random_y;
    private float random_x;
    private int random_Object;
    private float spawn_z;

    [NonSerialized]
    public static float speed;

    Vector3 camPosition;

    //Idle Detection:
    public float idleCheck = 5f;
    private Vector3 PreviousPosition;
    private Coroutine IdleCoroutine;
    public GameObject Ship;
    private Vector3 AutoPosition;

    private void Awake()
    {
        //Adjusts position to camera after calculating aspect ratio:
        camPosition = Camera.main.transform.position;
        spawn_z = camPosition.z - 15f;
        speed = 5;
    }

    void Start()
    {
        //Debug.Log("Set Randoms");
        random_y = Random.Range(-height, height);
        random_x = Random.Range(-width, width);
        random_Object = Random.Range(0, Debris.Length);

        //Record starting position:
        PreviousPosition = Ship.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        spawn_timer -= Time.deltaTime;
        if (spawn_timer <= 0.0)
        {
            //Debug.Log("Rock Spawned");
            random_y = Random.Range(-height, height);
            random_x = Random.Range(-width, width);
            random_Object = Random.Range(0, Debris.Length);
            spawn_timer = timerCooldown;
            Vector3 position = new Vector3(random_x, random_y, spawn_z);

            AutoPosition = position;

            
            GameObject asteroid = Instantiate(Debris[random_Object], position, Quaternion.identity);
            Destroy(asteroid, 5f);//Temp delete this:

            Rigidbody asteroidRigidbody = asteroid.GetComponent<Rigidbody>();
            asteroidRigidbody.velocity = Vector3.back * speed;
        }

        IdleDetector();

    }

    private void IdleDetector() 
    {
        if (Ship.transform.position != PreviousPosition)
        {
            PreviousPosition = Ship.transform.position; // Update the last position
            if (IdleCoroutine != null)
            {
                StopCoroutine(IdleCoroutine); // Stop the idle coroutine if the object moves
                IdleCoroutine = null;
            }
        }
        else if (IdleCoroutine == null) // If the object hasn't moved and there's no coroutine running
        {
            IdleCoroutine = StartCoroutine(CheckIdle()); // Start the coroutine
        }
    }

    private IEnumerator CheckIdle()
    {
        yield return new WaitForSeconds(idleCheck); // Wait for the specified idle time
        AsteroidAutoTarget(); // Perform the action when the object is idle
    }

    private void AsteroidAutoTarget() 
    {
        
        GameObject asteroid = Instantiate(Debris[random_Object], AutoPosition, Quaternion.identity);
        Destroy(asteroid, 5f);//Temp delete this:

        Rigidbody asteroidRigidbody = asteroid.GetComponent<Rigidbody>();
        asteroidRigidbody.velocity = (PreviousPosition - AutoPosition).normalized * speed;
    }

}
