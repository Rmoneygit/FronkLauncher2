using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class IslandManager : MonoBehaviour {

    public struct Player
    {
        public int ID;
        public int score;

        public Player(int ID, int score)
        {
            this.ID = ID;
            this.score = score;
        }
    }
    
    public Player[] players;
    int currentRound;
    public int hotseat; //Array location of the player in the hotseat
    public int hotseatID; //ID number of the player in the hotseat
    public Launcher launch;
    public MenuController menuController;


    public void setupGame(int numberOfPlayers)
    {

        currentRound = 0;
        hotseat = 0;
        players = new Player[numberOfPlayers];
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = new Player(i, 0);
        }

        //Randomly shuffle for starting order
        for (int i = 0; i < players.Length; i++)
        {
            int r = Random.Range(0, players.Length);
            Player temp = players[r];
            players[r] = players[i];
            players[i] = temp;
        }

        menuController.toggleButtons(false);


        menuController.orderImages(players);
        menuController.toggleRoundText(true);
        startRound();
    }
	
	void startRound()
    {
        currentRound++;
        menuController.updateRoundDisplay(currentRound);
        orderByScore();
        hotseat = 0;
        hotseatID = players[0].ID;
        menuController.showPlayerIsUpNote(hotseatID);
    }

    public void startTurn()
    {
        if (hotseat + 1 < players.Length)
        {
            hotseat++;
            hotseatID = players[hotseat].ID;
            menuController.showPlayerIsUpNote(hotseatID);
        }
        else
        {
            hotseatID = 0;
            startRound();
        }
    }

    void orderByScore()
    {
        for(int i = 0; i < players.Length; i++)
        {
            for(int j = 1; j < players.Length - i; j++)
            {
                if(players[j-1].score < players[j].score)
                {
                    Player temp = players[j];
                    players[j] = players[j - 1];
                    players[j - 1] = temp;
                }
            }
        }

        menuController.orderImages(players);
    }

    public void updateScore(int pointsToAdd, int owner)
    {

        int location = 0;

        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].ID == owner)
            {
                location = i;
                break;
            }
        }

        players[location].score += pointsToAdd;
        Debug.Log("Player " + owner + ": " + players[location].score + " points.");
        menuController.updateScoreDisplay(players[location].ID, players[location].score);
    }
}
