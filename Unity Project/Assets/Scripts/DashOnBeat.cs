/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   DashOnBeat.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the DashOnBeat class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class DashOnBeat : MonoBehaviour
{
    public float Speed = 2.0f;
    public float NewSlowRate = 0.9f;

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
        GetComponent<BeatReceiver>().BeatHit += OnBeatHit;
    }
    
    void OnDisable()
    {
        GetComponent<BeatReceiver>().BeatHit -= OnBeatHit;
    }
    
    void OnBeatHit(BeatEventArgs e)
    {
        rigidbody.drag = NewSlowRate;

        var randomVel = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        randomVel.Normalize();
        randomVel *= Speed;
        rigidbody.velocity = randomVel;
    }
}
