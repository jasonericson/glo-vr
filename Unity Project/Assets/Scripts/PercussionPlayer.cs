/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   PercussionPlayer.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the PercussionPlayer class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PercussionPlayer : MonoBehaviour
{
    public FMODAsset[] PercussionEvents = new FMODAsset[3];
    public int ThisIndex { get; private set; }

    public static int s_index = 0;
    private FMOD_StudioEventEmitter m_emitter;
    private Metronome m_metronome;
    private bool m_activated = false;

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
        GetComponent<EventManager>().ActivateEvent += OnActivate;
        m_metronome = GameObject.Find("Dome").GetComponent<Metronome>();
        m_metronome.BeatHit += OnBeatHit;
    }

    void OnDisable()
    {
        GetComponent<EventManager>().ActivateEvent -= OnActivate;
        m_metronome.BeatHit -= OnBeatHit;
        GameObject.Find("Dome").GetComponent<PercussionManager>().Unregister(gameObject);
    }

    void OnActivate()
    {
        m_emitter = GetComponent<FMOD_StudioEventEmitter>();
        m_emitter.SwapAsset(PercussionEvents[s_index]);
        ThisIndex = s_index;
        ++s_index;
        s_index %= PercussionEvents.Length;

        GameObject.Find("Dome").GetComponent<PercussionManager>().RegisterPlayer(gameObject);

        m_activated = true;
    }

    void OnBeatHit(BeatEventArgs e)
    {
        if (!m_activated)
            return;
        
        var chancer = Random.Range(0.0f, 1.0f);
        bool play;
        switch (ThisIndex)
        {
        case 0:
            // Don't play a 32nd note cause that's ridiculous
            if (e.Beat.ThirtySecond != 1)
            {
                play = false;
            }
            // 16th note - 10% chance
            else if (e.Beat.Sixteenth != 1)
            {
                play = chancer < 0.1f;
            }
            // 8th note - 25% chance
            else if (e.Beat.Eighth != 1)
            {
                play = chancer < 0.2f;
            }
            // Quarter note - 50% chance
            else
            {
                play = chancer < 0.3f;
            }
            break;
        default:
            // 32nd note - NOPE
            if (e.Beat.ThirtySecond != 1)
            {
                play = false;
            }
            // 16th note - 25% chance
            else if (e.Beat.Sixteenth != 1)
            {
                play = chancer < 0.1f;
            }
            // 8th note - 50% chance
            else if (e.Beat.Eighth != 1)
            {
                play = chancer < 0.3f;
            }
            // Quarter note - 75% chance
            else
            {
                play = chancer < 0.5f;
            }
            break;
        }

        if (play)
        {
            var volume = 2.0f;
            if (e.Beat.Eighth != 1)
            {
                volume -= 1.0f;
            }
            if (e.Beat.Sixteenth != 1)
            {
                volume -= 1.0f;
            }
            var ev = FMOD_StudioSystem.instance.GetEvent(m_emitter.asset);
            if (ev != null)
            {
                ev.setParameterValue("VolumeLevel", volume);
                var attributes = FMOD.Studio.UnityUtil.to3DAttributes(transform.position);
                ev.set3DAttributes(attributes);
                ev.start();
                ev.release();
            }

            GetComponent<BeatReceiver>().Play(e);
        }
    }
}
