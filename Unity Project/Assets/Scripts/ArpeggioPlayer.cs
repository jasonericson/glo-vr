/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   ArpeggioPlayer.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the ArpeggioPlayer class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class ArpeggioPlayer : MonoBehaviour
{
    public float Set;

    private FMOD_StudioEventEmitter m_emitter;
    private bool m_active;

    // Use this for initialization
    void Start()
    {
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    void OnEnable()
    {
        GetComponent<BeatReceiver>().BeatHit += OnBeatHit;
        GetComponent<EventManager>().ActivateEvent += OnActivate;
    }

    void OnDisable()
    {
        GetComponent<BeatReceiver>().BeatHit -= OnBeatHit;
        GetComponent<EventManager>().ActivateEvent -= OnActivate;
    }

    void OnBeatHit(BeatEventArgs e)
    {
        if (!m_active)
        {
            return;
        }

        if (m_emitter == null)
        {
            m_emitter = GetComponent<FMOD_StudioEventEmitter>();
        }

        var ev = FMOD_StudioSystem.instance.GetEvent(m_emitter.asset);
        ev.setParameterValue("Set", Set);
        var attributes = FMOD.Studio.UnityUtil.to3DAttributes(transform.position);
        ev.set3DAttributes(attributes);
        ev.start();
        ev.release();
    }

    void OnActivate()
    {
        m_active = true;
    }
}
