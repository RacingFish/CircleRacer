using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour {

    public GameObject car;
    public GameObject standardCheckpoint;
    public GameObject cornerCheckpoint;
    public float trackRadius = 2.0f;

    public List<Sector> sectors;
    public List<Car> cars;

    public int numberOfLaps = 10;
    public float trackFastestLap = 0.0f;

    // Use this for initialization
    void Start () {
        sectors = new List<Sector>();

        CreateTrack();

        CreateCars();
    }
	
	// Update is called once per frame
	void Update () {
        ProcessRacePosition();
    }

    void ProcessRacePosition()
    {
        for (int i = 0; i < 20; i++)
        {
        }
    }

    void CreateTrack()
    {
        for(int i = 0; i < 30; i++)
        {
            print("Creating Checkpoint");

            float xPos = trackRadius * (float)Mathf.Cos((Mathf.Deg2Rad * 12.0f) * (29 - i)) + gameObject.transform.position.x;
            float yPos = trackRadius * (float)Mathf.Sin((Mathf.Deg2Rad * 12.0f) * (29 - i)) + gameObject.transform.position.y;
            
            Vector3 vPos = new Vector3(xPos, yPos, 0);

            Sector sectorComponent;
            int spawn = Random.Range(0, 21);
            if(spawn > 4)
            {
                Instantiate(standardCheckpoint, vPos, Quaternion.identity);
                sectorComponent = standardCheckpoint.GetComponent<Sector>();
            }
            else
            {
                Instantiate(cornerCheckpoint, vPos, Quaternion.identity);
                sectorComponent = cornerCheckpoint.GetComponent<Sector>();
            }

            if (i == 29)
            {
                sectorComponent.SetAsStartFinishStraight(true);
            }

            sectors.Add(sectorComponent);
        }
    }

    void CreateCars()
    {
        float xPos = trackRadius * (float)Mathf.Cos((Mathf.Deg2Rad * 12.0f) * 0) + gameObject.transform.position.x;
        float yPos = trackRadius * (float)Mathf.Sin((Mathf.Deg2Rad * 12.0f) * 0) + gameObject.transform.position.y;
        Vector3 vPos = new Vector3(xPos, yPos, 0);

        //Player's car
        Instantiate(car, vPos, Quaternion.identity);

        //AI cars
        //for (int i = 1; i < 20; i++)
        //{
        //    Instantiate(car, vPos, Quaternion.identity);
        //}
    }
}
