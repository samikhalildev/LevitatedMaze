using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private float startTime;
    private float score;
    private GameObject enemy;

	// Use this for initialization
	void Start () {
       startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        score = Time.time - startTime;

        

        //print("Score: " + (int)score);

    }
}
