using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Button twoButton, threeButton, fourButton, fiveButton;
    public IslandManager manager;
    public Image[] turnOrderImages; // Start in order from player 0 to 4
    public int[] imageIDs; //Initially contains numbers 0-4 in order. Parallel array to turnOrderImages
    public Vector3[] imageLocations;

    // Use this for initialization
    void Start ()
    {
        manager = GameObject.Find("Main Camera").GetComponent<IslandManager>();
        twoButton.onClick.AddListener(delegate { manager.setupGame(2); });
        threeButton.onClick.AddListener(delegate { manager.setupGame(3); });
        fourButton.onClick.AddListener(delegate { manager.setupGame(4); });
        fiveButton.onClick.AddListener(delegate { manager.setupGame(5); });
        imageLocations = new Vector3[5];

        for(int i = 0; i < turnOrderImages.Length; i++)
        {
            imageLocations[i] = turnOrderImages[i].rectTransform.position;
        }
    }

    public void orderImages(IslandManager.Player[] players)
    {
        for(int i = 0; i < players.Length; i++)
        {
            for(int j = 0; j < players.Length - i - 1; j++)
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
        /*for(int i = 0; i < numberOfPlayers; i++)
        {
            for(int j = 0; j < numberOfPlayers - i - 1; j++)
            {
                if(i < j && turnOrderImages[i].rectTransform.position.y < turnOrderImages[j].rectTransform.position.y )
                {
                    float temp = turnOrderImages[j].rectTransform.position.y;
                    turnOrderImages[j].rectTransform.position += new Vector3(0f, turnOrderImages[i].rectTransform.position.y, 0f);
                    turnOrderImages[i].rectTransform.position = new Vector3(0f, temp, 0f);

                }
            }
        }*/

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
}
