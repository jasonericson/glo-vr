/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   TurnOnCollisionWithActivate.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the TurnOnCollisionWithActivate class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class TurnOnCollisionWithActivate : MonoBehaviour
{
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
        collider.enabled = true;
    }

    // Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
