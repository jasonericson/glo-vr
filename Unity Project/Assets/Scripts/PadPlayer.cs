/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   PadPlayer.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the PadPlayer class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PadPlayer : MonoBehaviour
{
    public bool IsResolved;
    

    void OnEnable()
    {
        GetComponent<EventManager>().ActivateEvent += OnActivate;
    }

    void OnDisable()
    {
        GetComponent<EventManager>().ActivateEvent -= OnActivate;
    }

    private void OnActivate()
    {
        // var index = Random.Range(0, PrimaryPadEvents.Count - 1);
        // var emitter = GetComponent<FMOD_StudioEventEmitter>();
        // emitter.SwapAsset(PrimaryPadEvents[index]);
        // emitter.Play();
    }

    // Use this for initialization
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
}
