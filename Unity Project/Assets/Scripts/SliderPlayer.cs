/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   SliderPlayer.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the SliderPlayer class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SliderPlayer : MonoBehaviour
{
    public FMODAsset QuarterHitter;

    private FMOD_StudioEventEmitter m_emitter;
    private List<float> m_pitches = new List<float> { 0.0f, 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f };
    private float m_last = -1.0f;
    private BeatData m_nextBeat;
    private bool m_firstBeat = true;

	// Use this for initialization
	void Start ()
    {
        m_emitter = GetComponent<FMOD_StudioEventEmitter>();
        m_nextBeat.Tick = 0;
        Metronome.NormalizeBeat(ref m_nextBeat);
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
        if (e.Beat.Tick % 8 == 0)
        {
            FMOD_StudioSystem.instance.PlayOneShot(QuarterHitter, transform.position);
        }

        if (e.Beat.Tick == m_nextBeat.Tick)
        {
            var param = m_emitter.getParameter("Pitch");
            var newPitch = m_pitches.GetAndRemoveRandomValue();
            param.setValue(newPitch);
            m_pitches.Add(m_last);
            m_last = newPitch;

            m_nextBeat.Tick += 12;
            Metronome.NormalizeBeat(ref m_nextBeat);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
