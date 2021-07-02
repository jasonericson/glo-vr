/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   FlashOnKickBeat.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the FlashOnKickBeat class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class FlashOnKickBeat : MonoBehaviour
{
	public float MaxIntensity;
	public float MinIntensity;
	public float FadeRate;

	private BeatReceiver m_kickReceiver;

	// Use this for initialization
	void Start ()
	{
		m_kickReceiver = GameObject.Find ("Kick Left").GetComponent<BeatReceiver>();
		m_kickReceiver.BeatHit += OnBeat;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (light.intensity > MinIntensity)
		{
			light.intensity -= FadeRate * Time.deltaTime;
		}
	}

	void OnBeat(BeatEventArgs e)
	{
		light.intensity = MaxIntensity;
	}
}
