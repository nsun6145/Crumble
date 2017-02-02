using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public bool fallen = false;
    private float fallSpeed = -0.1f;
    Animator anim;
    private bool played = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Fall();
	}

    public void Fall()
    {
        if(fallen && !played)
        {
            played = true;
            anim.Play(0);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Cube_Drop"))
        {
            if (fallen) transform.Translate(new Vector3(0, fallSpeed, 0));
        }
    }
}
