using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fronk : MonoBehaviour {

    public int owner;
    public IslandManager manager;
    public int points;
    public Target target;
    public Launcher launch;
    public bool madeInitialContact;

    // Use this for initialization
    void Start ()
    {
        manager = GameObject.Find("Main Camera").GetComponent<IslandManager>();
        target = GameObject.Find("Target").GetComponent<Target>();
        launch = GameObject.Find("Launcher").GetComponent<Launcher>();
        madeInitialContact = false;

        owner = manager.hotseatID;
        if(owner == 1)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (owner == 2)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        if (owner == 3)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }
	
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "target" && !madeInitialContact)
        {
            target.canMove = true;
            launch.isLaunching = true;
            manager.startTurn();
            madeInitialContact = true;
        }
    }
}
