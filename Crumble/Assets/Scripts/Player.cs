using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private int playerNumber; //player number in relation to the game
    public int controllerNumber; //joystick number of player, public for debug
    private Vector3 startingPosition = new Vector3(0.0f,1.0f,-6.5f);

    public float maxSpeed = 8.0f;
    private float jumpPower = 12f;
    private float jumpDelay = 0.6f;
    private bool canJump = true;

    private Rigidbody _rigidbody;
    private Quaternion rotation;

	float moveX = 0;
	float moveY = 0;
	// Use this for initialization
	void Start () {
        _rigidbody = this.GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -29.8f, 0);
        //controllerNumber = 1;
	}
	
	// Update is called once per frame
	void Update () {
		//keyboard is automatically applied to player one
		if (controllerNumber != 1) {
			//position of the movement thumbstick
			moveX = Input.GetAxis ("Horizontal_" + controllerNumber);
			moveY = Input.GetAxis ("Vertical_" + controllerNumber);

		//Convert Keyboard into controller axis
		} else {
			if(Input.GetKey("up")){
				moveY = Mathf.Min(1,moveY+.4f);
			}
			if(Input.GetKey("down")){
				moveY = Mathf.Max(-1,moveY-.4f);
			}
			if(Input.GetKey("left")){
				moveX = Mathf.Max(-1,moveX-.4f);
			}
			if(Input.GetKey("right")){
				moveX = Mathf.Min(1,moveX+.4f);
			}
			if(!Input.GetKey("up") && !Input.GetKey("down")){
                if(moveY > 0)
                {
                    moveY = Mathf.Max(0, moveY - .6f);
                }
                else if(moveY < 0)
                {
                    moveY = Mathf.Min(0, moveY + .6f);
                }
				
			}
			if(!Input.GetKey("left") && !Input.GetKey("right")){
                if (moveX > 0)
                {
                    moveX = Mathf.Max(0, moveX - .8f);
                }
                else if(moveX < 0)
                {
                    moveX = Mathf.Min(0, moveX + .8f);
                }
            }
            if(Mathf.Abs(moveX) > .75 && Mathf.Abs(moveY) > .75)
            {
                if(moveX < 0)
                {
                    moveX = -.75f;
                }
                else
                {
                    moveX = .75f;
                }
                if (moveY < 0)
                {
                    moveY = -.75f;
                }
                else
                {
                    moveY = .75f;
                }
            }
		}
        if(moveX != 0 || moveY != 0) //only does movement code if you are moving

        {
            Vector3 axisVector = new Vector3(
                                               moveX,
                                               0,
                                               moveY);

            //moving character
            _rigidbody.AddForce(axisVector * 4 * maxSpeed);

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

		if (canJump && Input.GetButtonDown("Jump_" + controllerNumber) || canJump && (Input.GetKeyDown(KeyCode.Space) && controllerNumber == 1))
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
