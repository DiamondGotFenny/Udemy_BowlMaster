using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BallMovement : MonoBehaviour {

    //public float speed;
    public Vector3 launchSpeed;

    private Rigidbody rd;
    public AudioSource Sfx;
    private Vector3 ballOriPos;
    private PinsSound PinsSound; 

    GameObject pins;

    public bool isPlay = false;

	// Use this for initialization
	void Start () {
        rd = GetComponent<Rigidbody>();
        Sfx = GetComponent<AudioSource>();
        rd.useGravity = false;
        ballOriPos = transform.position;
        PinsSound = GameObject.FindObjectOfType<PinsSound>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    //private void OnMouseDown()
    //{
    //    Launch(launchSpeed);
    //}

    public void Launch(Vector3 velocity)
    {
        isPlay = true;
        Sfx.Play();
        // rd.velocity = transform.forward * speed;
        rd.useGravity = true;
        rd.velocity = velocity;        // this method gives more control;
        int velocityCheck=Mathf.RoundToInt(velocity.z); 
        if (velocityCheck==0)  // if the user do not drag the screen but only point down, reset the ball . or the game will not able to continue.
        {
            ballReset();
        }
    }

    public void ballReset()
    {
        isPlay = false;
        transform.position = ballOriPos;
        transform.rotation = Quaternion.identity;
        rd.useGravity = false;
        rd.velocity = Vector3.zero;
        rd.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInChildren<pin>())
        {
            PinsSound.playPinSound();
        }
    }
}
