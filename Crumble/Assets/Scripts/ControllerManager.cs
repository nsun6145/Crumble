using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerManager : MonoBehaviour {

    private static ControllerManager instance = null;

    public static ControllerManager Instance {  get { return instance; } }

    private string[] joystickNames;
    private int joystickArrayLength; //Size of Input.GetJoystickNames()


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        Debug.Log(joystickArrayLength);
    }
	
	// Update is called once per frame
	void Update () {

        //Updating the controller array
        joystickNames = Input.GetJoystickNames();
        joystickArrayLength = joystickNames.Length;

	}
}
