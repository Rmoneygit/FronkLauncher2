using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

    public Rigidbody[] fronks;
    public GameObject fronkObj;
    public Transform[] targets;
    public Transform centralTarget;
    public IslandManager manager;
    public bool isLaunching;
    public bool isShooting;
    public Vector3[] launchPos;
    public Vector3 centralLaunchPos;
    public Target t;
    public MenuController controller;
    float timer = 0f;
    float timerMax = 5f;

    public float h = 25;
    public float gravity = -18;

    private void Start()
    {
        centralLaunchPos = transform.position;
        fronks = new Rigidbody[5];
        isShooting = false;
    }

    public void begin()
    {
        controller.togglePlayerIsUpNote(false);
        for(int i = 0; i < 5; i++)
        {
            GameObject f = GameObject.Instantiate(fronkObj, launchPos[i], Quaternion.identity);
            fronks[i] = f.GetComponent<Rigidbody>();
            fronks[i].useGravity = false;
        }
        t.canMove = true;
        isLaunching = true;
        t.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    void Update()
    {
        if (isLaunching && Input.GetKeyDown(KeyCode.Space))
        {
            isLaunching = false;
            isShooting = true;
            t.canMove = false;
            t.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Launch();
        }

        if(isShooting)
        {
            timer += Time.deltaTime;
            if(timer >= timerMax)
            {
                isShooting = false;
                timer = 0;
                manager.startTurn();
            }
        }

        if (isLaunching)
        {
            DrawPath();
        }


    }

    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        for(int i = 0; i < 5; i++)
        {
            fronks[i].useGravity = true;
            fronks[i].velocity = CalculateLaunchData(targets[i], i).initialVelocity;
        }
    }

    LaunchData CalculateLaunchData(Transform target, int i)
    {
        float displacementY = target.position.y - fronks[i].position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - fronks[i].position.x, 0, target.position.z - fronks[i].position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = centralTarget.position.y - centralLaunchPos.y;
        Vector3 displacementXZ = new Vector3(centralTarget.position.x - centralLaunchPos.x, 0, centralTarget.position.z - centralLaunchPos.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = centralLaunchPos;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = centralLaunchPos + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }
}
