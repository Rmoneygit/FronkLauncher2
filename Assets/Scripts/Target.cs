using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public float speed;
    public bool canMove;

    void Start()
    {
        canMove = false;
    }
    
    // Update is called once per frame
	void Update ()
    {
        if (canMove)
        {

            float z = Input.GetAxis("Vertical") * speed;
            float x = Input.GetAxis("Horizontal") * speed;

            z *= Time.deltaTime;
            x *= Time.deltaTime;

            transform.Translate(x, 0, z);
        }

    }
}
