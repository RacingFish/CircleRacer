using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    public GameObject gameManager;
    public TrackManager trackManager;

    public string teamName = "Mercedes";
    public int carNumber = 1;
    public int checkpoint = 1;
    public int lap = 1;
    private float currentLapTime;
    public float fastestLapTime;
    public float raceDistanceRemaining;
    private Vector3 positionLastFrame;

    public int aerodynamics = 0;
    public int chassis = 0;
    public int engine = 0;
    public int reliability = 0;

    public int driver = 0;

    public float engineMode = 1.0f;

    public float engineHealth = 100.0f;

    public float baseMaxSpeed = 10.0f;
    public float maxSpeed = 10.0f;
    public float baseMaxCorneringSpeed = 5.0f;
    public float maxCorneringSpeed = 5.0f;
    public float currentSpeed = 0.0f;

    // Use this for initialization
    void Start () {
        currentSpeed = maxSpeed;

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        trackManager = gameManager.GetComponent<TrackManager>();

        aerodynamics = Random.Range(0, 500);
        chassis = Random.Range(0, 500);
        engine = Random.Range(0, 500);
        //reliability = Random.Range(0, 500);
        driver = Random.Range(0, 500);

        raceDistanceRemaining = 2.0f * Mathf.PI * trackManager.trackRadius;
    }
 
	// Update is called once per frame
	void Update () {
        
        //Updates the car's speed depending on different variables
        ProcessCarSpeed();

        //Movement
        transform.RotateAround(gameManager.transform.position, Vector3.forward, -currentSpeed * Time.deltaTime);

        currentLapTime += Time.deltaTime;

        float xPos = trackManager.trackRadius * (float)Mathf.Cos((Mathf.Deg2Rad * 12.0f) * 0.0f) + gameObject.transform.position.x;
        float yPos = trackManager.trackRadius * (float)Mathf.Sin((Mathf.Deg2Rad * 12.0f) * 0.0f) + gameObject.transform.position.y;

        Vector3 vPos = new Vector3(xPos, yPos, 0);

        Vector3 spokeToActual = gameObject.transform.position - trackManager.transform.position;
        Vector3 spokeToCorrect = vPos - trackManager.transform.position;
        float angle = Vector3.Angle(spokeToActual, spokeToCorrect);

        print(angle);

        float distance = 2 * Mathf.PI * trackManager.trackRadius * (angle / 360);
        //print(distance)
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("Checkpoint hit");
        checkpoint++;

        currentSpeed = maxSpeed;
        if (other.tag == "Corner")
        {
            currentSpeed = maxCorneringSpeed;
        }

        //Essentially processing a new lap
        if (checkpoint > 30)
        {
            ProcessNewLap();
        }
    }

    void ProcessNewLap()
    {
        checkpoint = 1;
        lap++;

        if (currentLapTime < fastestLapTime || fastestLapTime == 0.0f)
        {
            print("New Fastest Lap!");
            fastestLapTime = currentLapTime;
            currentLapTime = 0.0f;
        }
    }

    void ProcessCarSpeed()
    {
        //Straight line
        maxSpeed = baseMaxSpeed + engineMode * (engine * 0.01f) + (aerodynamics * 0.004f);

        maxSpeed += driver * 0.001f;

        //Cornering
        maxCorneringSpeed = baseMaxCorneringSpeed + engineMode * (chassis * 0.003f) + (aerodynamics * 0.002f);

        maxCorneringSpeed += driver * 0.001f;
    }
}
