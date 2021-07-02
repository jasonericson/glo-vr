/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   PlayEventOnBeat.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the PlayEventOnBeat class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class PlayEventOnBeat : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnEnable()
    {
        GetComponent<BeatReceiver>().BeatHit += OnBeatHit;
    }

    void OnDisable()
    {
        GetComponent<BeatReceiver>().BeatHit -= OnBeatHit;
    }

    void OnBeatHit(BeatEventArgs e)
    {
        GetComponent<FMOD_StudioEventEmitter>().Play();
    }
}
