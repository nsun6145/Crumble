using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private int playerNumber; //player number in relation to the game
    public int controllerNumber; //joystick number of player, public for debug

    private Rigidbody _rigidbody;
    private float maxSpeed;
    private float jumpPower;

	// Use this for initialization
	void Start () {
        _rigidbody = this.GetComponent<Rigidbody>();

        //controllerNumber = 1;
        maxSpeed = 10;
        jumpPower = 10.5f;
	}
	
	// Update is called once per frame
	void Update () {

        //position of the movement thumbstick
        Vector3 axisAmount = new Vector3(
            Input.GetAxis("Horizontal_" + controllerNumber),
            0, 
            Input.GetAxis("Vertical_" + controllerNumber));

        //moving character
        _rigidbody.velocity = new Vector3(axisAmount.x * maxSpeed, _rigidbody.velocity.y, axisAmount.z * maxSpeed);

        if (Input.GetButtonDown("Jump_" + controllerNumber))
        {
            _rigidbody.velocity += new Vector3(0,jumpPower, 0);
        }
	}
}
