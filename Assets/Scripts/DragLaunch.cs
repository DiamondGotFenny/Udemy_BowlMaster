using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallMovement))]
public class DragLaunch : MonoBehaviour {

    private BallMovement ball;

    Vector3  dragStart, dragEnd;
    float startTime, endTime;

	// Use this for initialization
	void Start () {
        ball = GetComponent<BallMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DragStart()
    {
        //Capture time &position to drag start;
        if (!ball.isPlay)
        {
            startTime = Time.time;
            dragStart = Input.mousePosition;
        }
    }

    public void DragEnd()
    {
        //Launch the ball;
        if (!ball.isPlay)
        {
            endTime = Time.time;
            dragEnd = Input.mousePosition;

            float dragDuration = endTime - startTime;

            float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
            float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

            Vector3 launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedZ);

            ball.Launch(launchVelocity);
        }
    }

    public void MoveStart(float amount)
    {
        if (!ball.isPlay )
        {
            float xPos = Mathf.Clamp(ball.transform.position.x + amount, -50f, 50f);
            float yPos = ball.transform.position.y;
            float zPos = ball.transform.position.z;
            ball.transform.position = new Vector3(xPos, yPos, zPos);
        }
    }
}
