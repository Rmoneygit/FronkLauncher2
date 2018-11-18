using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

    public Rigidbody fronk;
    public GameObject fronkObj;
    public Transform target;
    public bool isLaunching;
    public Vector3 launchPos;
    public Target t;

    public float h = 25;
    public float gravity = -18;



    public void begin()
    {
        GameObject f = GameObject.Instantiate(fronkObj, launchPos, Quaternion.identity);
        fronk = f.GetComponent<Rigidbody>();
        fronk.useGravity = false;
        isLaunching = true;
    }

    void Update()
    {
        if (isLaunching && Input.GetKeyDown(KeyCode.Space))
        {
            isLaunching = false;
            t.canMove = false;
            Launch();
        }

        if (isLaunching)
        {
            DrawPath();
        }
    }

    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        fronk.useGravity = true;
        fronk.velocity = CalculateLaunchData().initialVelocity;
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - fronk.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - fronk.position.x, 0, target.position.z - fronk.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = fronk.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = fronk.position + displacement;
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
