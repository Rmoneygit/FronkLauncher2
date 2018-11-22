using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Button twoButton, threeButton, fourButton, fiveButton;
    public GameObject buttonParent;
    public IslandManager manager;
    public Image[] turnOrderImages; // Start in order from player 0 to 4
    private int[] imageIDs; //Initially contains numbers 0-4 in order. Parallel array to turnOrderImages
    public Vector3[] imageLocations;
    public Text roundText;
    public GameObject playerIsUpParent;
    public Text playerIsUpColorText;
    public Text playerIsUpColorTextShadow;
    public Button OKButton;
    public Launcher launch;

    // Use this for initialization
    void Start ()
    {
        manager = GameObject.Find("Main Camera").GetComponent<IslandManager>();
        twoButton.onClick.AddListener(delegate { manager.setupGame(2); });
        threeButton.onClick.AddListener(delegate { manager.setupGame(3); });
        fourButton.onClick.AddListener(delegate { manager.setupGame(4); });
        fiveButton.onClick.AddListener(delegate { manager.setupGame(5); });
        OKButton.onClick.AddListener(delegate { launch.begin(); });
        imageLocations = new Vector3[5];

        imageIDs = new int[] { 0, 1, 2, 3, 4 };

        for(int i = 0; i < turnOrderImages.Length; i++)
        {
            imageLocations[i] = turnOrderImages[i].rectTransform.position;
        }
    }

    public void orderImages(IslandManager.Player[] players)
    {
        for(int i = 0; i < players.Length; i++)
        {
            for(int j = 0; j < players.Length; j++)
            {
                if(players[i].ID == imageIDs[j])
                {
                    Image temp = turnOrderImages[i];
                    turnOrderImages[i] = turnOrderImages[j];
                    turnOrderImages[j] = temp;

                    int tempInt = imageIDs[i];
                    imageIDs[i] = imageIDs[j];
                    imageIDs[j] = tempInt;
                }
            }
        }
        physicallyReorderImages(players.Length);
    }

    void physicallyReorderImages(int numberOfPlayers)
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            turnOrderImages[i].rectTransform.position = imageLocations[i];
            turnOrderImages[i].gameObject.SetActive(true);
        }
    }

    public void updateScoreDisplay(int owner, int points)
    {
        for(int i = 0; i < imageIDs.Length; i++)
        {
            if(imageIDs[i] == owner)
            {
                turnOrderImages[i].transform.Find("Score").GetComponent<Text>().text = ": " + points;
            }
        }
    }

    public void updateRoundDisplay(int round)
    {
        roundText.text = "Round " + round;
    }

    public void toggleButtons(bool toggle)
    {
        buttonParent.SetActive(toggle);
    }

    public void toggleRoundText(bool toggle)
    {
        roundText.gameObject.SetActive(toggle);
    }

    public void togglePlayerIsUpNote(bool toggle)
    {
        playerIsUpParent.SetActive(toggle);
    }

    public void showPlayerIsUpNote(int playerID)
    {
        if(playerID == 0)
        {
            playerIsUpColorText.text = "Yellow";
            playerIsUpColorText.color = Color.yellow;
            playerIsUpColorTextShadow.text = "Yellow";
        }
        else if(playerID == 1)
        {
            playerIsUpColorText.text = "Red";
            playerIsUpColorText.color = Color.red;
            playerIsUpColorTextShadow.text = "Red";
        }
        else if (playerID == 2)
        {
            playerIsUpColorText.text = "Blue";
            playerIsUpColorText.color = Color.blue;
            playerIsUpColorTextShadow.text = "Blue";
        }
        else if (playerID == 3)
        {
            playerIsUpColorText.text = "Green";
            playerIsUpColorText.color = Color.green;
            playerIsUpColorTextShadow.text = "Green";
        }
        else if (playerID == 4)
        {
            playerIsUpColorText.text = "Purple";
            playerIsUpColorText.color = Color.magenta;
            playerIsUpColorTextShadow.text = "Purple";
        }
        togglePlayerIsUpNote(true);
    }
}
