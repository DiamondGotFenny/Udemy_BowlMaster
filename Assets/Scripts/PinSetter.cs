using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
    
    
    public float distanceToRaise = 50f;
    public GameObject pins;
    public GameManager GameManager;
    private Animator myAnimator;
    private PinCounter pinCounter;

    // Use this for initialization
    void Start () {
        myAnimator = GetComponent<Animator>();
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
        GameManager = GameObject.FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
      
	}

 

    public void PerformAction(ActionMasterOld.myAction action)
    {
        
        if (action == ActionMasterOld.myAction.Tidy)
        {
            myAnimator.SetTrigger("onTidy");
        }
        else if (action == ActionMasterOld.myAction.Reset)
        {
            myAnimator.SetTrigger("onReset");
            pinCounter.ResetlastSettleCount();
        }
        else if (action == ActionMasterOld.myAction.EndTurn)
        {
            myAnimator.SetTrigger("onReset");
            pinCounter.ResetlastSettleCount();
        }
        else if (action == ActionMasterOld.myAction.EndGame)
        {
            //throw new UnityException("Don't know what to do with endgame yet");
            GameManager.EndGame();
        }
    }

    public void RaisePins()
    {
        //raise standing pins only by distanceToRaise
        foreach (pin pin in GameObject.FindObjectsOfType<pin>())
        {
            pin.RaiseIfStanding();
        }
    }

    public void LowerPins()
    {
        foreach (pin pin in GameObject.FindObjectsOfType<pin>())
        {
            pin.Lower();
        }
    }

    public void RenewPins()
    {
        Instantiate(pins, new Vector3(0, distanceToRaise, 1845), Quaternion.identity);
        //GameObject newPins = Instantiate(pins);
        //newPins.transform.position = new Vector3(0, 50, 1845);

    }

    private void OnTriggerExit(Collider other)
    {
        GameObject thingLeft = other.gameObject;  //what you get here is the child object of pin, the one own that convex collider, not the pin itself;
        if (thingLeft.GetComponentInParent<pin>())
        {
            Destroy(other.transform.parent.gameObject);
        }
    }

}
