using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public bool fallen = false;
    private float fallSpeed = -0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Fall();
	}

    public void Fall()
    {
        if (fallen) transform.Translate(new Vector3(0,fallSpeed,0));
    }
}
