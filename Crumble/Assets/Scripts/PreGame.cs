using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreGame : MonoBehaviour {

    enum Status
    {
        disconnected,
        connected,
        inUse,
        ready
    }

    private struct PlayerPanel
    {
        private Status myStatus;
        private Image myBackground;
        private Text myText;
        
        public PlayerPanel(string panelName)
        {
            myBackground = GameObject.Find("Canvas/" + panelName).GetComponent<Image>();
            myStatus = Status.disconnected;
            myText = GameObject.Find("Canvas/" + panelName + "/Text").GetComponent<Text>();

            myText.text = "Connect a Controller";
        }

        public Status currentStatus
        {
            get { return myStatus; }
            set
            {
                switch (value)
                {
                    case Status.disconnected:
                        myText.text = "Connect a Controller";
                        myBackground.color = Color.white;
                        break;
                    case Status.connected:
                        myText.text = "Press A to Join";
                        myBackground.color = Color.white;
                        break;
                    case Status.inUse:
                        myText.text = "In Use";
                        break;
                    case Status.ready:
                        myText.text = "Ready to Go!";
                        myBackground.color = Color.green;
                        break;
                    default:
                        break;
                }

                myStatus = value;
            }
        }
    }

    private int currentConnectedControllers;
    private int activePlayers;

    private bool readyToStart = false;

    private PlayerPanel[] playerPanels;
    private Text readyText;

    // Use this for initialization
    void Start () {
        playerPanels = new PlayerPanel[4];
        activePlayers = 0;

        readyText = GameObject.Find("Canvas/ReadyText").GetComponent<Text>();

        for (int i = 0; i < 4; i++)
        {
            string panelName = "PlayerPanel" + (i + 1);
            playerPanels[i] = new PlayerPanel(panelName);
        }


	}
	
	// Update is called once per frame
	void Update () {
        UpdateCurrentControllers();
        checkReady();

        //Entering Game
        if (Input.GetButtonDown("Jump_1"))
        {
            playerPanels[0].currentStatus = Status.ready;
            activePlayers++;
        }
        else if (Input.GetButtonDown("Jump_2"))
        {
            playerPanels[1].currentStatus = Status.ready;
            activePlayers++;
        }
        else if (Input.GetButtonDown("Jump_3"))
        {
            playerPanels[2].currentStatus = Status.ready;
            activePlayers++;
        }
        else if (Input.GetButtonDown("Jump_4"))
        {
            playerPanels[3].currentStatus = Status.ready;
            activePlayers++;
        }

        //Backing out
        if (Input.GetButtonDown("Back_1"))
        {
            playerPanels[0].currentStatus = Status.connected;
            activePlayers--;
        }
        else if (Input.GetButtonDown("Back_2"))
        {
            playerPanels[1].currentStatus = Status.connected;
            activePlayers--;
        }
        else if (Input.GetButtonDown("Back_3"))
        {
            playerPanels[2].currentStatus = Status.connected;
            activePlayers--;
        }
        else if (Input.GetButtonDown("Back_4"))
        {
            playerPanels[3].currentStatus = Status.connected;
            activePlayers--;
        }

        if (Input.GetButtonDown("Submit") && readyToStart)
        {
            SceneManager.LoadScene("Map Selector");
        }
    }

    void UpdateCurrentControllers()
    {
        currentConnectedControllers = 0;
        int currentController = 0;
        foreach (string name in Input.GetJoystickNames())
        {
            if (name == "Controller (Xbox One For Windows)")
            {
                if(playerPanels[currentController].currentStatus == Status.disconnected)
                    playerPanels[currentController].currentStatus = Status.connected;
                currentConnectedControllers++;
            }
            else if (name == "")
            {
                playerPanels[currentController].currentStatus = Status.disconnected;
            }

            if (currentController < 3)
                currentController++;

        }

    }

    void checkReady()
    {
        if (activePlayers >= 2)
        {
            readyToStart = true;
            readyText.text = "Press Start to Begin";
        }
        else
            readyText.text = "Waiting for Players...";
    }
}
