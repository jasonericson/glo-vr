/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   TripletBeatReceiver.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the TripletBeatReceiver class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TripletBeatReceiver : MonoBehaviour
{
    private FMOD_StudioEventEmitter m_emitter;
    private List<float> m_pitches = new List<float> { 0.0f, 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f };
    private float m_last = -1.0f;

	// Use this for initialization
	void Start ()
    {
        m_emitter = GetComponent<FMOD_StudioEventEmitter>();
    }
    
    void OnEnable()
    {
        TripletMetronome.Instance.BeatHit += OnBeatHit;
    }
    
    void OnDisable()
    {
        TripletMetronome.Instance.BeatHit -= OnBeatHit;
    }
    
    void OnBeatHit()
    {
        var param = m_emitter.getParameter("Pitch");
        var newPitch = m_pitches.GetAndRemoveRandomValue();
        param.setValue(newPitch);
        m_pitches.Add(m_last);
        m_last = newPitch;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
