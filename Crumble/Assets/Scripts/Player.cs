using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float dragAmount;
    public string collisionTag;
    private int playerNumber; //player number in relation to the game
    public int controllerNumber; //joystick number of player, public for debug
    private Vector3 startingPosition = new Vector3(0.0f,1.0f,-6.5f);
    public float maxSpeed = 9.0f;
    private float jumpPower = 30f;
    private float jumpDelay = 0.6f;
    public float jumpTimer; //equal to delay
    private float punchDelay = .6f;
    private float canPunch;
    public GameObject[] otherPlayers;
    private float stunTime = 1.5f;


    private bool canJump = true;
    private bool canMove = true;

    private Rigidbody _rigidbody;
    private Quaternion rotation;


    private Fist rightFist;
    //private GameObject punchBox;

	float moveX = 0;
	float moveY = 0;

	// Use this for initialization
	void Start () {
        jumpTimer = jumpDelay;
        canPunch = punchDelay;
        collisionTag = null;
        _rigidbody = this.GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -29.8f, 0);
        Physics.gravity = new Vector3(0, -29.8f, 0); //<

        //controllerNumber = 1;
	}
	
	// Update is called once per frame
	void Update () {
        //Collision
        Ray ray = new Ray(this.transform.position, this.transform.up * -1);
        RaycastHit hit;
        maxSpeed = 9.0f;
        if (Physics.Raycast(ray, out hit, 2))
        {
            collisionTag = hit.transform.tag;
            if (hit.transform.tag == "Ice")
            {
                _rigidbody.drag = .75f;
                maxSpeed = 4.0f;
            }
            else if (hit.transform.tag == "Player")
            {
                hit.rigidbody.AddForce(-4 * this.transform.forward, ForceMode.VelocityChange);
                _rigidbody.AddForce(10 * Vector3.up);
            }
            else
            {
                _rigidbody.drag = dragAmount;
                _rigidbody.angularDrag = 0.05f;
            }
        }
        else
        {
            _rigidbody.drag = dragAmount;
            _rigidbody.angularDrag = 0.05f;
            collisionTag = null;
        }
    
        //keyboard is automatically applied to player one
        if (!canMove)
        {
            return;
        }
        else if (controllerNumber != 1)
        {
            moveX = Input.GetAxis("Horizontal_" + controllerNumber);
            moveY = Input.GetAxis("Vertical_" + controllerNumber);
        }
        if (controllerNumber == 1)
        {
            if (Input.GetKey("up")) {
                moveY = Mathf.Min(1, moveY + .8f);
            }
            if (Input.GetKey("down")) {
                moveY = Mathf.Max(-1, moveY - .8f);
            }
            if (Input.GetKey("left")) {
                moveX = Mathf.Max(-1, moveX - .8f);
            }
            if (Input.GetKey("right")) {
                moveX = Mathf.Min(1, moveX + .8f);
            }

            if (!Input.GetKey("up") && !Input.GetKey("down"))
            {
                if (moveY < 0)
                {
                    moveY = Mathf.Max(0, moveY - .9f);
                }
                else
                {
                    moveY = Mathf.Min(0, moveY + .9f);
                }
            }
            if (!Input.GetKey("left") && !Input.GetKey("right"))
            {
                if (moveX < 0)
                {
                    moveX = Mathf.Max(0, moveX - .9f);
                }
                else
                {
                    moveX = Mathf.Min(0, moveX + .9f);
                }
            }

            if (Mathf.Abs(moveX) > .75 && Mathf.Abs(moveY) > .75)
            {
                if (moveX < 0)
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
 
        if (moveX != 0 || moveY != 0) //only does movement code if you are moving
        {
            transform.gameObject.GetComponent<Animator>().SetBool("Moving", true);
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
        else
        {
            transform.gameObject.GetComponent<Animator>().SetBool("Moving", false);
        }

        if (canJump && Input.GetButtonDown("Jump_" + controllerNumber) || canJump && (Input.GetKeyDown(KeyCode.Space) && controllerNumber == 1))
        {
            Jump();
        }

        if (Input.GetButtonDown("Submit_" + controllerNumber))
        {
            _rigidbody.velocity = new Vector3();
            _rigidbody.position = startingPosition;
        }
        //Punching
        if (canPunch < 0 && Input.GetButtonDown("Punch_" + controllerNumber) || canPunch < 0 && (Input.GetKeyDown(KeyCode.LeftShift) && controllerNumber == 1))
        {
            StartCoroutine(Punch());
        }

        jumpTimer-= Time.deltaTime;
        canPunch -= Time.deltaTime;
    }

    void Jump()
    {
        if(collisionTag != null && jumpTimer < 0)
        {
            jumpTimer = jumpDelay;
            jump();
        }
       
    }

    IEnumerator Punch()
    {
        Debug.Log("Punch");
        transform.gameObject.GetComponent<Animator>().SetTrigger("Punching");
        canPunch = punchDelay;

        yield return new WaitForSeconds(.35f);
        for (int i = 0; i < otherPlayers.Length; i++)
        {
            if ((otherPlayers[i].transform.position - (transform.position + 1.3f * transform.forward)).magnitude < 2)
            {
                otherPlayers[i].GetComponent<Rigidbody>().AddForce(800*transform.forward);
                
            }
        }
        
    }

    IEnumerator Stun(Vector3 direction)
    {
        direction.Normalize();
        _rigidbody.AddForce(direction, ForceMode.Impulse);
        canMove = false;
        yield return new WaitForSeconds(stunTime);
        canMove = true;
    }

    private void jump()
    {
        _rigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
    }

    public float MoveX
    {
        get { return moveX; }
    }

    public float MoveY
    {
        get { return moveY; }
    }

    public void stun(Vector3 Direction)
    {
        StartCoroutine(Stun(Direction));
    }

    private void footstool() {
        _rigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.VelocityChange);
    }

    private void bounce() {
        _rigidbody.AddForce(new Vector3(-3.0f, jumpPower / 2, 0), ForceMode.VelocityChange);
    }


}
