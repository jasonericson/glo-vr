/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   WeakForced.cs
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the WeakForced class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeakForced : MonoBehaviour
{
    WeakForceProjector[] projectors = null;
    bool isRegistered = false;

    public List<MonoBehaviour> disableWhenForced = new List<MonoBehaviour>();

    void OnEnable()
    {
        if (projectors == null)
        {
            projectors = FindObjectsOfType<WeakForceProjector>();
            isRegistered = false;
        }
        GetComponent<EventManager>().ActivateEvent += OnActivate;
    }
    
    void OnDisable()
    {
        SetRegister(false);
        GetComponent<EventManager>().ActivateEvent -= OnActivate;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnActivate()
    {
        SetRegister(true);
    }

    void SetRegister(bool state)
    {
        int i = 0;
        if (isRegistered != state)
        {
            if (isRegistered)
            {
                for (i = 0; i < projectors.Length; ++i)
                {
                    projectors[i].Unregister(this);
                }
            }
            else
            {
                for (i = 0; i < projectors.Length; ++i)
                {
                    projectors[i].Register(this);
                }
            }
            isRegistered = state;
        }
    }
}
