using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    Camera cam;

    [SerializeField]
    float cameraRotationLimit = 85f;

    Vector3 movement = Vector3.zero;
    Vector3 rotation = Vector3.zero;
    Vector3 jump = Vector3.zero;

    float cameraRotation;
    float currentCameraRotation;

    Rigidbody rb;


    void Start () {
        rb = GetComponent<Rigidbody>();
	}


    // These function are called in the PlayerController when inputs are pressed
    public void Move(Vector3 movement) {
        this.movement = movement;
    }

    public void Rotate(Vector3 rotation) {
        this.rotation = rotation;
    }

    public void RotateCamera(float cameraRotation) {
        this.cameraRotation = cameraRotation;
    }

    public void Jump(Vector3 jump) {
        this.jump = jump;
    }


    // Run every physics iteration
    void FixedUpdate() {
        PerformMovement();
        PerformRotation();
    }



    // This function takes the movement vector and applys it to the player
    void PerformMovement() {
        
        if(movement != Vector3.zero){
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }

        if(jump != Vector3.zero){
            rb.AddForce(jump * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    // This function takes the rotation vector and applys it to the player
    void PerformRotation() {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if(cam != null){

            // setting camera rotation limit
            currentCameraRotation -= cameraRotation;
            currentCameraRotation = Mathf.Clamp(currentCameraRotation, -cameraRotationLimit, cameraRotationLimit);

            cam.transform.localEulerAngles = new Vector3(currentCameraRotation, 0f, 0f);
        }
    }
}
