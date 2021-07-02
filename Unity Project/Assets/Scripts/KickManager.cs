/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   KickManager.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the KickManager class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KickManager : MonoBehaviour
{
    public List<GameObject> Kicks;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void OnEnable()
    {
        GetComponent<Metronome>().BeatHit += OnBeatHit;
    }

    void OnDisable()
    {
        GetComponent<Metronome>().BeatHit -= OnBeatHit;
    }

    void OnBeatHit(BeatEventArgs e)
    {
        if (e.Beat.Tick == 0)
        {
            foreach (var kick in Kicks)
            {
                kick.GetComponent<BeatReceiver>().Play(e);
            }
        }
    }
}
