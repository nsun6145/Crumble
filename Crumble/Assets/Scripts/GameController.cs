using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject blockPref;
    public GameObject map;
    public RuntimeAnimatorController fallingAnimator;
    public bool createBlocksMap; //create default cube map
    private List<GameObject> blocks = new List<GameObject>();
    private float timer = 0;
    private int animNum = -1;
    private int dropNum = -1;

	// Use this for initialization
	void Start () {
        if (createBlocksMap)
        {
            for (int i = -4; i < 5; i++)
                for (int j = -4; j < 5; j++)
                {
                    GameObject temp = Instantiate<GameObject>(blockPref, new Vector3(i, 0, j), new Quaternion());
                    blocks.Add(temp);
                }
        }
        else
        {

            foreach (Transform c in map.transform)
            {
                GameObject child = c.gameObject;
                child.AddComponent<Block>();        
                child.AddComponent<Animator>().runtimeAnimatorController = fallingAnimator;
                blocks.Add(child);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= 2)
        {
            if(animNum == -1)
            {
                animNum = dropNum = Random.Range(0, blocks.Count - 1);
                animate();
            }
            else
            {
                drop();
                animNum = dropNum = Random.Range(0, blocks.Count - 1);
                animate();
            }
            timer -= 2;
        }
    }

    void animate()
    {
        blocks[animNum].GetComponent<Block>().animate();
    }

    void drop()
    {
        blocks[dropNum].GetComponent<Block>().fallen = true;
        blocks.RemoveAt(dropNum);
    }
}
