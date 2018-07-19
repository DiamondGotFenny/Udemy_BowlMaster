using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin : MonoBehaviour {

    public float StandingThreshold = 5f;

    float distToRaise = 40f;

    Rigidbody myrigidbody;
	// Use this for initialization
	void Start () {
        myrigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public bool IsStanding()
    {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;

        //since from Unity 5.3 rotation angles never have a negative value
        //float tiltInX = Mathf.Abs(rotationInEuler.x);
        //float tiltInZ = Mathf.Abs(rotationInEuler.z);

        // if transform.eulerAngles.x < 180f==true?  if true,  tiltInX =transform.eulerAngles.x , if false, tiltInX =360 - transform.eulerAngles.x;
        float tiltInX = (transform.eulerAngles.x < 180f) ? transform.eulerAngles.x : 360 - transform.eulerAngles.x;
        float tiltInZ = (transform.eulerAngles.z < 180f) ? transform.eulerAngles.z : 360 - transform.eulerAngles.z;

        if (tiltInX>StandingThreshold||tiltInZ>StandingThreshold)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void RaiseIfStanding()
    {
        if (IsStanding())
        {
            myrigidbody.useGravity = false;
            transform.position += new Vector3(0, distToRaise, 0);
           transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Lower()
    {       
        transform.Translate(new Vector3(0, -distToRaise, 0), Space.World);
        myrigidbody.useGravity = true;
    }

}
