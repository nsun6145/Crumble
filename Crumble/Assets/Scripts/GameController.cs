using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour {

    public GameObject blockPref;
    public GameObject playerPref;
    private List<GameObject> blocks = new List<GameObject>();
    private float timer = 0;

	// Use this for initialization
	void Start () {
		for(int i = -4; i < 5; i++)
            for(int j = -4; j < 5; j++)
            {
                GameObject temp = Instantiate<GameObject>(blockPref, new Vector3(i, 0, j), new Quaternion());
                blocks.Add(temp);
            }

       GameObject player = Instantiate<GameObject>(playerPref, new Vector3(0, 2, 0), new Quaternion()); 
        
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= 2)
        {
            timer -= 2;
            drop();
        }
	}

    void drop()
    {
        int num = Random.Range(0, blocks.Count - 1);
        blocks[num].GetComponent<Block>().fallen = true;
        blocks.RemoveAt(num);
    }

    void weather() {

        int rng = Random.Range(1,4); //random number generator

        //switch (rng): 

        }

    }

}
