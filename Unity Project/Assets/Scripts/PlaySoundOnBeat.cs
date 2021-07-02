/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   PlaySoundOnBeat.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the PlaySoundOnBeat class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System;

public class PlaySoundOnBeat : MonoBehaviour
{
    private FMODAsset m_asset;

    // Use this for initialization
    void Start ()
    {
        var emitter = GetComponent<FMOD_StudioEventEmitter>();
        m_asset = emitter.asset;
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
        FMOD_StudioSystem.instance.PlayOneShot(m_asset, transform.position);
    }
}
