using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
/// <summary>
/// 
/// -PersistantData-
/// 
/// Main Author: Jackson Cook
/// Secondary Author: Michael Knighten
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

            //For testing (teleport from singleton) No clean up in yet:
            GameObject asteroid = Instantiate(Debris[random_Object], position, Quaternion.identity);
            Destroy(asteroid, 5f);//Temp delete this:

            Rigidbody asteroidRigidbody = asteroid.GetComponent<Rigidbody>();
            asteroidRigidbody.velocity = Vector3.back * speed;
        }

    }

}
