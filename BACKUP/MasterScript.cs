using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 
/// -Master Script-
/// 
/// Author: Michael Knighten
/// Start Date: 2/11/2024
/// 
/// </summary>
public class MasterScript : MonoBehaviour
{
    //Button Attributes:

    private bool controlsOption;

    private float previousTapTime;

    public float doubleTapTimeCoolDown = .2f;


    //Score Attributes:

    public TextMeshProUGUI scoreText;

    public float scoreCooldown = 10f;

    //Health Attributes:

    public TextMeshProUGUI healthText;

    //Movement Attributes:

    public GameObject ship;

    public float speed = 10f;

    private Vector3 shipPosition;

    private Touch touch;

    //Audio Attributes:

    public AudioClip[] audioE;

    public AudioSource audioEvent;

    //Resolution Attributes:

    public Camera cam;

    private float camX = 0;
    private float camY = 1;
    private float camZ = -20;

    private Vector3 camPosition;

    private float screenWidth;

    private float screenHeight;

    private float aspectRatio;

    private void MobileResolution()
    {

        cam = Camera.main;

        screenWidth = Screen.width;

        screenHeight = Screen.height;

        aspectRatio = screenWidth / screenHeight;

        cam.aspect = aspectRatio;

        camPosition = new Vector3(camX, camY, camZ - (aspectRatio * 10));

        cam.transform.position = camPosition;

    }

    private void playSoundE(AudioClip sound)
    {
        audioEvent.clip = sound;
        audioEvent.Play();
    }

    public void ToggleGyro(bool option)
    {
        controlsOption = !controlsOption;
    }

    private void PlatformCheck()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            //Platform: Android.
            UIC_Swap(controlsOption);
        }
        else
        {
            //Platform: PC.
            MovementControl();
            Movement();

        }
    }

    private void MovementControl()
    {
        shipPosition = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
    }

    private void Movement()
    {
        Rigidbody shipRigidbody = ship.GetComponent<Rigidbody>();
        shipRigidbody.MovePosition(shipRigidbody.position + shipPosition * speed * Time.deltaTime);
    }

    private void StopMovement()
    {
        shipPosition = Vector3.zero;
    }

    private void UIC_Accelerometer() //UIC 1
    {
        //shipPosition = new Vector3(Input.acceleration.x, Input.acceleration.y, 0f);
        Rigidbody shipRigidbody = ship.GetComponent<Rigidbody>();
        shipRigidbody.MovePosition(shipRigidbody.position + shipPosition * speed * Time.deltaTime);
        //Gyroscope.

        Movement();

    }

    private void UIC_Swipe() //UIC 2
    {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                //Action:
                shipPosition = new Vector3(touch.deltaPosition.x, touch.deltaPosition.y, 0f);
                Movement();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                StopMovement();
            }
        }

    }

    private void UIC_DoubleTap() //UIC 3 
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if touch is at the top of the screen
            if (touch.position.y > 0)//Screen.height * 0.6f) //Top 40% of screen.
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (Time.time - previousTapTime < doubleTapTimeCoolDown)
                    {
                        // Action:

                    }
                    previousTapTime = Time.time;
                }
            }
        }
    }

    public void UIC_Swap(bool accelerationOn)
    {
        if (accelerationOn)
        {
            UIC_Accelerometer();
        }
        else
        {
            UIC_Swipe();
            UIC_DoubleTap();
        }
    }

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

    private void Awake()
    {
        MobileResolution();
    }

    void Start()
    {
        PersistantData.health = 3;

        PersistantData.score = 0;

        InvokeRepeating("AddScore", scoreCooldown, scoreCooldown);
    }

    private void FixedUpdate()
    {
        PlatformCheck();
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

            PersistantData.health -= 1;

            if (PersistantData.health <= 0) 
            {
                PersistantData.Instance.goToEnd();
            }
        }
    }
}
