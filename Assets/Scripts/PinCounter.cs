using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {
    public Text standingPinCounts;
    
    private int lastStandingCount = -1;  //state of the pins is in default, indicates that no pin fall over yet. it is not an actual pin count number;
    //private ActionMasterOld actionMaster = new ActionMasterOld();   //we only need one instance , so we need it here;
    private int lastSettleCount = 10;
    private float lastChangeTime;  // keep track of when the count number last updated;
    private GameManager gameManager;
    bool ballLeftBox = false;
    int intialPins = 10;

    GameObject ball, pins;

    // Use this for initialization
    void Start()
    {
        standingPinCounts.text = intialPins.ToString();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        standingPinCounts.color = Color.green;

        ball = GameObject.Find("Ball");
        pins = GameObject.Find("Pins");
    }

    // Update is called once per frame
    void Update()
    {
        //standingPinCounts.text = CountStanding().ToString();
        if (ballLeftBox)
        {
            UpdateStandingCountsAndSettle();
            standingPinCounts.color = Color.red;
        }
    }

    int CountStanding()
    {
        int standingpins = 0;
        foreach (pin pin in GameObject.FindObjectsOfType<pin>())
        {
            bool isStand = pin.IsStanding();
            if (isStand == true)
            {
                standingpins++;
            }
        }
        return standingpins;
    }

    private void OnTriggerEnter(Collider other)
    {
        float ballPinDis = Vector3.Distance(ball.transform.position, pins.transform.position);
        GameObject thingHit = other.gameObject;
        if (thingHit.GetComponent<BallMovement>())
        {
            ballLeftBox = false;
            if (ballPinDis<3)
            {
                standingPinCounts.color = Color.red;
            }
            else
            {
                standingPinCounts.color = Color.green;
            }
            
            InvokeRepeating("PrintStanding", 0.25f, 0.25f);
        }
    }

    private void PrintStanding()
    {
        string text = CountStanding().ToString();
        standingPinCounts.text = text;
    }

    void UpdateStandingCountsAndSettle()
    {
        //Update the lastStandingCount;
        //Call PinsHaveSettle if they have;
        int currentStanding = CountStanding();

        if (currentStanding != lastStandingCount)  
        {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;   //we put the actual pin number into it;
            return;
        }

        float settleTime = 3f;  //how long to wait to consider pins settled;
        if ((Time.time - lastChangeTime) > settleTime)
        {
            //if time have gone over 3 seconds(settleTime) since last change, 
            PinsHaveSettle();

        }

    }

    void PinsHaveSettle()
    {
        int standing = CountStanding();
        int fallenPins = lastSettleCount - standing;
        lastSettleCount = standing;
        gameManager.Bowl(fallenPins);
        lastStandingCount = -1; // indicates new frame begin, pins have settle, and ball not back in box;
        ballLeftBox = false;
        standingPinCounts.color = Color.green;
    }

    public void ResetlastSettleCount()
    {
        lastSettleCount = 10;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            ballLeftBox = true;
            ball.GetComponent<BallMovement>().Sfx.Stop();
        }
    }
}
