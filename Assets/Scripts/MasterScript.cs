using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    public float sensitivityScale;

    //Audio Attributes:

    public AudioClip[] audioE;

    public AudioSource audioEvent;

    public bool soundPlaying = true;

    //Resolution Attributes:

    private Camera cam;

    private float camX = 0;
    private float camY = 1;
    private float camZ = -20;

    private Vector3 camPosition;

    private float screenWidth;

    private float screenHeight;

    private float aspectRatio;

    //PersistantData:

    GameObject PD;

    //Dynamic Difficulty Adjuster:

    private Color initialColor;
    private Color targetColor;
    private Color lerpColor;

    private Material emissionMat;

    public GameObject emissionObj;

    private int dda;

    private int hardMode;

    private float emissionVal;

    private float initialSpeed;

    private float ddaTimer = 0f;

    float timerReduction = 0.1f;

    //Gyro:

    private Gyroscope gyro;

    private float tiltAngle;

    public float sensitivity = 5f;

    public float minAngle = -30f;

    public float maxAngle = 30f;

    //Options Menu:

    public GameObject optionsMenu;

    public float bopRotationThreshold = 50f;

    //Other Audio Sources:

    public AudioSource soundThrusters;
    public AudioSource soundMusic;
    public AudioSource soundEngine;

    //Visual Damage Indicator:

    public GameObject Explosion;

    private bool showExplosion;

    public float cooldownExplosion;


    //DDA Incrementer: (dda/hardMode = x/100) ~ (dda*hardMode = 100*x) ~ ((dda*hardMode)/100)=x
    private void DDA() 
    {

        if (hardMode != 0) 
        {
            dda = hardMode - PersistantData.score;

            dda = Mathf.Clamp(dda, 0, hardMode);

            emissionVal = dda / (float)hardMode;

            lerpColor = Color.Lerp(initialColor, targetColor, emissionVal);

            emissionMat.SetColor("_EmissionColor", lerpColor);

            emissionMat.EnableKeyword("_EMISSION");

            emissionMat.globalIlluminationFlags &= ~MaterialGlobalIlluminationFlags.EmissiveIsBlack;

            UpDifficulty();
        }
    
    }

    private void UpDifficulty() 
    {
        ddaTimer += Time.deltaTime;

        if (ddaTimer >= 10f && DebrisSpawn.speed != hardMode)
        {
            DebrisSpawn.speed += 5;
            if(DebrisSpawn.timerCooldown - timerReduction <= 0)
            {
                timerReduction = (timerReduction / 5.0f);
            }

            DebrisSpawn.timerCooldown -= timerReduction;

            ddaTimer = 0f;
        }
    }

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

    private void MovementControl()
    {
        shipPosition = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
    }

    private void Movement()
    {
        Rigidbody shipRigidbody = ship.GetComponent<Rigidbody>();
        shipRigidbody.MovePosition(shipRigidbody.position + speed * Time.deltaTime * shipPosition);
    }

    private void StopMovement()
    {
        shipPosition = Vector3.zero;
    }

    private void UIC_Gyro() //UIC 1
    {
        //Input.gyro.enabled = true;

        //// Get the gyroscope rotation rate
        //Vector3 rotationRate = Input.gyro.rotationRateUnbiased;

        //// Rotate the ship around the z-axis based on the gyroscope input
        //ship.transform.Rotate(Vector3.forward, -rotationRate.z * sensitivity);

        Input.gyro.enabled = true;

        // Check if the device supports gyroscope
        if (SystemInfo.supportsGyroscope)
        {
            // Get the gyroscope rotation rate
            Vector3 rotationRate = Input.gyro.rotationRateUnbiased;

            // Rotate the ship around the z-axis based on the gyroscope input
            float rotationAngle = -rotationRate.z * sensitivity;

            // Limit the rotation between -30 and 30 degrees
            float clampedRotation = Mathf.Clamp(rotationAngle, -30f, 30f);

            // Apply the initial offset to the clamped rotation
            float totalRotation = clampedRotation + 90f;

            // Set the ship's rotation directly to the compensated value
            ship.transform.rotation = Quaternion.Euler(0f, 0f, totalRotation);
        }
    }

    private void UIC_Swipe() //UIC 2
    {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                //Action:
                shipPosition = new Vector3(touch.deltaPosition.x, touch.deltaPosition.y, 0f) * sensitivityScale;
                Movement();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                StopMovement();
            }
        }

    }

    private void UIC_Bop() //UIC 3 
    {
        // Opens Options Menu:

        if (Input.acceleration.magnitude > 3f)
        {
            optionsMenu.SetActive(true);
            Time.timeScale = 0f;
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

    public void CloseMenu() 
    {
        optionsMenu.SetActive(false);
        Time.timeScale = 1f;
    }



    private void Awake()
    {
        MobileResolution();

        emissionMat = emissionObj.GetComponent<Renderer>().material;

        initialSpeed = DebrisSpawn.speed;

        PD = GameObject.FindWithTag("Singleton");

        optionsMenu.SetActive(false);
    }

    void Start()
    {
        showExplosion = false;

        initialColor = Color.red;//actually initial

        targetColor = Color.black;//actually target

        hardMode = PersistantData.hardMode;

        PersistantData.health = 3;

        PersistantData.score = 0;

        InvokeRepeating(nameof(AddScore), scoreCooldown, scoreCooldown);

    }

    private void FixedUpdate()
    {
        MovementControl();
        UIC_Swipe();//Works
        UIC_Gyro();//No Work---------------------------------------
        UIC_Bop();//Works

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Pulling up options");
            optionsMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void Update()
    {
        ScoreSync();
        HealthSync();
        DDA();
        ExplosionVisual();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            playSoundE(audioE[1]);

            PersistantData.score = 0;

            DebrisSpawn.speed = 5;

            DebrisSpawn.timerCooldown = 1;
            DebrisSpawn.spawn_timer = 1;

            PersistantData.health -= 1;

            if (PersistantData.health <= 0) 
            {
                SceneManager.LoadScene(2);// Ending
            }

            //Explosion:
            showExplosion = true;
            StartCoroutine(ToggleExplosionActivationOff());
        }
    }
    public void easyMode()
    {
        hardMode = 0;
    }
    public void difficultMode()
    {
        hardMode = 50;
    }
    public void ToggleSounds()
    {
        if (soundPlaying)
        {
            audioEvent.volume = 0;
            soundEngine.volume = 0;
            soundThrusters.volume = 0;
            soundMusic.volume = 0;
        }
        else
        {
            audioEvent.volume = .7f; // Adjust the value as needed
            soundEngine.volume = .5f;
            soundThrusters.volume = .5f;
            soundMusic.volume = 0.6f;
        }

        soundPlaying = !soundPlaying;
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
