using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour {

    [SerializeField]
    float moveAmount = 2f;
    [SerializeField]
    float moveSpeed = 2f;

    float moveOnX;
    float moveOnY;

    Vector3 gunCurrentPosition;
    Vector3 changeGunPosition;

	// Use this for initialization
	void Start () {
        gunCurrentPosition = transform.localPosition;	
	}
	
	// Update is called once per frame
	void Update () {
        moveOnX = Input.GetAxis("Mouse X") * Time.deltaTime * moveAmount;
        moveOnY = Input.GetAxis("Mouse Y") * Time.deltaTime * moveAmount;

        float x = gunCurrentPosition.x + moveOnX;
        float y = gunCurrentPosition.y + moveOnY;

        changeGunPosition = new Vector3(x, y, gunCurrentPosition.z);

        transform.localPosition = Vector3.Lerp(transform.localPosition, changeGunPosition, moveSpeed * Time.deltaTime);
	}
}
