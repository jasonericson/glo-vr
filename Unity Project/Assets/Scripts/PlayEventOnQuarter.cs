/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   PlayEventOnQuarter.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the PlayEventOnQuarter class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class PlayEventOnQuarter : MonoBehaviour
{
    private BeatReceiver m_receiver;

    // Use this for initialization
    void Start()
    {
        m_receiver = GetComponent<BeatReceiver>();
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    void OnEnable()
    {
        Metronome.Instance.BeatHit += OnBeatHit;
    }

    void OnDisable()
    {
        Metronome.Instance.BeatHit -= OnBeatHit;
    }

    void OnBeatHit(BeatEventArgs e)
    {
        if (e.Beat.Tick % 4 == 0) // Eighth note
        {
            m_receiver.Play(e);
        }
    }
}
