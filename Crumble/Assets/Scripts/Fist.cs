using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour {

    private Player parentPlayerScript;

	// Use this for initialization
	void Start () {
        parentPlayerScript = this.transform.parent.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void punch(float time, Vector3 direction)
    {
        StartCoroutine(Punch(time, direction));
    }

    IEnumerator Punch(float time, Vector3 direction)
    {
        float timer = 0.0f;
        Vector3 originalPos = this.transform.position;
        direction.Normalize();
        Debug.Log("Above the loop");
        while (timer <= time)
        {

            this.transform.position = originalPos + (Mathf.Sin(timer / time * Mathf.PI) + 1.0f) * direction;
            Debug.Log(this.transform.position);
            yield return null;
            timer += Time.deltaTime;
        }
        this.transform.position = originalPos;

    }
}
