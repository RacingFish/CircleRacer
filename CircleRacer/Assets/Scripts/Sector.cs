using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour {

    private float sectorDifficulty = 1.0f;

    private bool isStartFinishStraight = false;

    public float fastestSectorTime = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAsStartFinishStraight(bool value)
    {
        print("Setting as start finish straight");
        isStartFinishStraight = value;
    }
}
