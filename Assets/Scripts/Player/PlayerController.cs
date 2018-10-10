using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    
    [SerializeField]
    float speed = 8f;
    [SerializeField]
    float sensitivity = 4f;
    [SerializeField]
    float smoothing = 5f;
    [SerializeField]
    float jumpLimit = 5f;

    PlayerMotor motor;

    void Start() {
        motor = GetComponent<PlayerMotor>();
    }

    void Update() {

        if(Cursor.lockState != CursorLockMode.Locked){
            Cursor.lockState = CursorLockMode.Locked;
        }

        if(Input.GetKeyDown("escape")){
            Cursor.lockState = CursorLockMode.None;
        }


        /*
         * Movement
         */

        // Get the input
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * smoothing;
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * smoothing;

        // Calculate movement
        Vector3 horizontal = transform.right * x;
        Vector3 vertical = transform.forward * z;

        // Final movement vector
        Vector3 movement = (horizontal + vertical).normalized * speed;

        // Apply movement
        motor.Move(movement);



        /*
         * Rotation - turning around Y axis (left and right)
         */

        // Get the input
        float yRotation = Input.GetAxisRaw("Mouse X");

        // Calculate movement
        Vector3 rotation = new Vector3 (0f, yRotation, 0f) * sensitivity;

        // Apply movement
        motor.Rotate(rotation);



        /*
         * Camera Rotation - X axis (up and down)
         */

        // Get the input
        float xRotation = Input.GetAxisRaw("Mouse Y");

        // Calculate movement
        float cameraRotation = xRotation * sensitivity;

        // Apply movement
        motor.RotateCamera(cameraRotation);


        /*
         * Player Jump
         */

        Vector3 jump = Vector3.zero;

        if(Input.GetButton("Jump")){
            jump = Vector3.up * jumpLimit;
        }

        // Apply jumping to motor
        motor.Jump(jump);

    }
}
