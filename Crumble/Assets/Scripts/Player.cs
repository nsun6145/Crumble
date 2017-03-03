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

    private float stunTime = 1.5f;


    private bool canJump = true;
    private bool canPunch = true;
    private bool canMove = true;

    private Rigidbody _rigidbody;
    private Quaternion rotation;


    private Fist rightFist;
    //private GameObject punchBox;

	float moveX = 0;
	float moveY = 0;

	// Use this for initialization
	void Start () {
        _rigidbody = this.GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -29.8f, 0);
        Physics.gravity = new Vector3(0, -29.8f, 0); //<

        //controllerNumber = 1;

        rightFist = this.transform.GetChild(0).gameObject.GetComponent<Fist>();
        //punchBox = this.transform.Find("PunchBox").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        //keyboard is automatically applied to player one
        if (!canMove)
        {
            return;
        }
        else if (controllerNumber == 1)
        {
            moveX = Input.GetAxis("Horizontal_" + controllerNumber);
            moveY = Input.GetAxis("Vertical_" + controllerNumber);
        }
        if (controllerNumber != 1)
        {
            if (Input.GetKey("up")) {
                moveY = Mathf.Min(1, moveY + .1f);
                moveY = Mathf.Min(1, moveY + .4f);
            }
            if (Input.GetKey("down")) {
                moveY = Mathf.Max(-1, moveY - .1f);
                moveY = Mathf.Max(-1, moveY - .4f);
            }
            if (Input.GetKey("left")) {
                moveX = Mathf.Max(-1, moveX - .1f);
                moveX = Mathf.Max(-1, moveX - .4f);
            }
            if (Input.GetKey("right")) {
                moveX = Mathf.Min(1, moveX + .1f);
                moveX = Mathf.Min(1, moveX + .4f);
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
        //Punching
        if (canPunch && Input.GetButtonDown("Punch_" + controllerNumber))
        {
            Debug.Log("Punch");
            StartCoroutine(Punch(2.0f));
        }
    }

    IEnumerator Jump()
    {
        canJump = false;
        jump();
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
    }

    IEnumerator Punch(float time)
    {
        canPunch = false;
        //float timer = 0.0f;
        //Vector3 originalPos = rightFist.transform.position;
        //direction.Normalize();
        //Debug.Log("Above the loop");
        //while(timer <= time)
        //{

        //    rightFist.transform.position = originalPos + (Mathf.Sin(timer / time * Mathf.PI) + 1.0f) * direction;
        //    Debug.Log(rightFist.transform.position);
        //    yield return null;
        //    timer += Time.deltaTime;
        //}
        //rightFist.transform.position = originalPos;
        yield return new WaitForSeconds(time);
        canPunch = true;
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
        _rigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.VelocityChange);
    }


    public bool CanPunch
    {
        get { return canPunch; }
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
        _rigidbody.AddForce(new Vector3(0, jumpPower / 2, 0), ForceMode.VelocityChange);
    }

    private void bounce() {
        _rigidbody.AddForce(new Vector3(-3.0f, jumpPower / 2, 0), ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision) {

    }

}
