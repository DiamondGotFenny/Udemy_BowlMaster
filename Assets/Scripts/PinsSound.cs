using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinsSound : MonoBehaviour {

    private AudioSource Sfx;

    // Use this for initialization
    void Start () {
        Sfx = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playPinSound()
    {
        Sfx.Play();
    }
}
