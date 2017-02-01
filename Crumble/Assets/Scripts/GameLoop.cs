﻿using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;


public class GameLoop : MonoBehaviour
{
    public int value; //Checks the current state
    public bool gameOver = false;

    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        //Code to ignore keys

        //while (value != 2) {
        //    use keys here for pausing
        //}

    }

    bool LoadSceneOkay()
    { //To be finished method

        return false;
    }

    public void LoadState(int load)
    {

        switch (load)
        {
            case 0:
                gameOver = false;
                SceneManager.LoadScene("StartMenu"); //Opens start screen
                break;


            case 1:
                gameOver = false;
                SceneManager.LoadScene("Main"); //Loads level (Change name of level as needed)
                break;

            case 2:
                SceneManager.LoadScene("GameOver", LoadSceneMode.Additive); //Currently loads game over screen
                break;
        }
    }
}
