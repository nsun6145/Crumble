using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    [SerializeField]
    private Image pointer;

    [SerializeField]
    private Text iceText;

    [SerializeField]
    private Text lavaText;

    private bool iceLevel;

	// Use this for initialization
	void Start () {
        //pointer.transform.Translate(new Vector3(0, 300, 0));
        iceText.fontStyle = FontStyle.Bold;
        iceText.color = Color.green;
        iceLevel = true;
	}
	
	// Update is called once per frame
	void Update () {
		float moveH = Input.GetAxis("Horizontal_1");

        if(moveH == 1)
        {
            changePointerLava();
        }
        else if (moveH == -1)
        {
            changePointerIce();
        }

        if (Input.GetButtonDown("Jump_1") || Input.GetButtonDown("Submit_1"))
        {
            if (iceLevel)
            {
                SceneManager.LoadScene("Ilan_Debug");
            }
            else
            {
                SceneManager.LoadScene("Lavalevel");
            }
        }

    }

    private void changePointerIce()
    {
        if (iceLevel)
            return;
        else
        {
            lavaText.fontStyle = FontStyle.Normal;
            lavaText.color = Color.black;

            iceText.fontStyle = FontStyle.Bold;
            iceText.color = Color.green;
            iceLevel = true;
        }
    }

    private void changePointerLava()
    {
        if (iceLevel)
        {
            iceText.fontStyle = FontStyle.Normal;
            iceText.color = Color.black;

            lavaText.fontStyle = FontStyle.Bold;
            lavaText.color = Color.green;
            iceLevel = false;
        }
        else
        {
            return;
        }
    }
}
