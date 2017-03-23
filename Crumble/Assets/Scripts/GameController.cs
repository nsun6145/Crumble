using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject blockPref;
    public GameObject map;
    public RuntimeAnimatorController fallingAnimator;
    public bool createBlocksMap; //create default cube map
    private List<GameObject> blocks = new List<GameObject>();
    private float timer = 0;
    private int animNum = -1;
    private int dropNum = -1;
    private int numPlayers = 0;
    public GameObject playerPrefab;
    public GameObject parentOfPlayers;
    public GameObject[] listOfPlayers;

    public Color[] playerColorMain;
    public Color[] playerColorHair;

    // Use this for initialization
    void Start () {

        numPlayers = PlayerPrefs.GetInt("numberPlayers");

        for (int i = 0; i < numPlayers; i++)
        {
            Instantiate(playerPrefab, parentOfPlayers.transform.position, parentOfPlayers.transform.rotation, parentOfPlayers.transform);
        }
       
        listOfPlayers = new GameObject[parentOfPlayers.transform.childCount];
        for (int i = 0; i < listOfPlayers.Length; i++)
        {
            listOfPlayers[i] = parentOfPlayers.transform.GetChild(i).gameObject;
            Player playerScript = listOfPlayers[i].GetComponent<Player>();
            playerScript.playerNumber = i;
            listOfPlayers[i].transform.position += new Vector3(2 * i, 0, 2 * i);
            
            //Color Setup
            listOfPlayers[i].transform.GetChild(1).FindChild("Cloth").gameObject.GetComponent<Renderer>().material.color = playerColorMain[i];
            listOfPlayers[i].transform.GetChild(1).FindChild("Shirt").gameObject.GetComponent<Renderer>().material.color = playerColorMain[i] - Color.grey;
            listOfPlayers[i].transform.GetChild(1).FindChild("Pants").gameObject.GetComponent<Renderer>().material.color = playerColorMain[i] + Color.grey;
            listOfPlayers[i].transform.GetChild(1).FindChild("LeftArmShirt").gameObject.GetComponent<Renderer>().material.color = playerColorMain[i] - Color.grey;
            listOfPlayers[i].transform.GetChild(1).FindChild("RightArmShirt").gameObject.GetComponent<Renderer>().material.color = playerColorMain[i] - Color.grey;
            listOfPlayers[i].transform.GetChild(1).FindChild("Hair").gameObject.GetComponent<Renderer>().material.color = playerColorHair[i];

        }
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
        if(playersAlive())
        {
            timer += Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }

        if(blocks.Count > 1 && timer >= 2)
        {
            if(animNum == -1)
            {
                animNum = dropNum = Random.Range(0, blocks.Count - 1);
                //Debug.Log("1 : " + animNum);
                animate();
            }
            else
            {
                drop();
                //Debug.Log("2a : " + animNum);
                animNum = dropNum = Random.Range(0, blocks.Count - 1);
                //Debug.Log("2 : " + animNum);
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

    bool playersAlive()
    {
        int numberAlive = 0;
        for(int i = 0; i < listOfPlayers.Length; i++)
        {
            Player playerScript = listOfPlayers[i].GetComponent <Player> ();
            if (playerScript.alive)
            {
                numberAlive++;
            }
        }
        if(numberAlive > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
