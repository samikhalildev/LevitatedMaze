using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownSight : MonoBehaviour {


    private CrosshairScript crosshairScript;
    public GameObject cam;

	// Use this for initialization
	void Awake () {
        crosshairScript = GetComponent<CrosshairScript>();
	}


	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(1)){
            cam.SetActive(true);
            crosshairScript.enabled = false;
        }

        if(Input.GetMouseButtonUp(1)){
            cam.SetActive(false);
            crosshairScript.enabled = true;

        }
	}
}
