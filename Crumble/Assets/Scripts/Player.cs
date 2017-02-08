using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private int playerNumber; //player number in relation to the game
    public int controllerNumber; //joystick number of player, public for debug
    private Vector3 startingPosition = new Vector3(0.0f,1.0f,-6.5f);

    private float maxSpeed = 10.0f;
    public float jumpPower = 7.0f;
    private float jumpDelay = 0.6f;
    private bool canJump = true;

    private Rigidbody _rigidbody;
    private Quaternion rotation;


	// Use this for initialization
	void Start () {
        _rigidbody = this.GetComponent<Rigidbody>();

        //controllerNumber = 1;
	}
	
	// Update is called once per frame
	void Update () {

        //position of the movement thumbstick
        float moveX = Input.GetAxis("Horizontal_" + controllerNumber);
        float moveY = Input.GetAxis("Vertical_" + controllerNumber);

        if(moveX != 0 || moveY != 0) //only does movement code if you are moving

        {
            Vector3 axisVector = new Vector3(
                                               moveX,
                                               0,
                                               moveY);

            //moving character
            _rigidbody.velocity = new Vector3(axisVector.x * maxSpeed, _rigidbody.velocity.y, axisVector.z * maxSpeed);

            //rotating character
            Vector2 t = new Vector2(moveX, moveY);
            if (t.magnitude < 0.2f)
            {
                this.transform.rotation = rotation;
            }
            else
            {
                rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(moveX, moveY) * Mathf.Rad2Deg, 0));
                this.transform.rotation = rotation;
            }

        }

       

        if (canJump && Input.GetButtonDown("Jump_" + controllerNumber))
        {
            StartCoroutine(Jump());
        }

        if (Input.GetButtonDown("Submit_" + controllerNumber))
        {
            _rigidbody.velocity = new Vector3();
            _rigidbody.position = startingPosition;
        }


    }

    IEnumerator Jump()
    {
        canJump = false;
        jump();
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
    }

    private void jump()
    {
        _rigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.VelocityChange);
    }
}
