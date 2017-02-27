using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {
    public float dragAmount;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(this.transform.position, this.transform.up * -1);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2)){
            if (hit.transform.tag == "Ice")
            {
                Rigidbody rigid = this.gameObject.GetComponent <Rigidbody>();
                rigid.drag = .01f;
                rigid.angularDrag = 0;
            }
            else if(hit.transform.tag == "Player")
            {
                Rigidbody rigid = this.gameObject.GetComponent<Rigidbody>();
                rigid.AddForce(new Vector3(0, 2, 0), ForceMode.VelocityChange);
            }     
        }
        else
        {
            Rigidbody rigid = this.gameObject.GetComponent<Rigidbody>();
            rigid.drag = dragAmount;
            rigid.angularDrag = 0.05f;
        }
    }
}
