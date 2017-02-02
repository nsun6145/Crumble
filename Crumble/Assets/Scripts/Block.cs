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

    public void animate()
    {
        anim.SetBool("Animating",true);
    }

    public void Fall()
    {
        if (fallen)
        {
            anim.SetBool("Animating", false);
            transform.Translate(new Vector3(0, fallSpeed, 0));
        }
    }
}
