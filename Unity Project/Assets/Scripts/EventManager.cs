/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   EventManager.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the EventManager class.
*/
/******************************************************************************/

using System;
using UnityEngine;
using System.Collections;

public class ActivateEventArgs : EventArgs
{
}
public delegate void ActivateEventHandler();

public class EventManager : MonoBehaviour
{
    public event ActivateEventHandler ActivateEvent;

    public void Activate()
    {
        if (ActivateEvent != null)
            ActivateEvent.Invoke();
    }
}
