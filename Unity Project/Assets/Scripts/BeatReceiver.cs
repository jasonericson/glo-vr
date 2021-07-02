/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   BeatReceiver.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the BeatReceiver class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System;

public class BeatReceiver : MonoBehaviour
{
    public event BeatEventHandler BeatHit;

    public void Play(BeatEventArgs e)
    {
        if (BeatHit != null)
        {
			BeatHit.Invoke(e);
        }
    }
}