using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int hotseat;
    public int hotseatID;
    Launcher launch;

	// Use this for initialization
	void Start ()
    {
        //How many players?
        currentRound = 0;
        hotseat = 0;
        players = new Player[4];
        for (int i = 0; i < 4; i++)
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

        launch = GameObject.Find("Launcher").GetComponent<Launcher>();
        startRound();
    }
	
	void startRound()
    {
        currentRound++;
        hotseat = 0;
        hotseatID = players[0].ID;
        startTurn();
        
    }

    public void startTurn()
    {
        if (hotseat + 1 < players.Length - 1)
        {
            hotseat++;
            hotseatID = players[hotseat].ID;
            launch.begin();
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
            for(int j = 0; j < players.Length - i - 1; j++)
            {
                if(players[j].score > players[j+1].score)
                {
                    Player temp = players[j];
                    players[j] = players[j + 1];
                    players[j + 1] = temp;
                }
            }
        }
    }
}
