/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   SlowToStop.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the SlowToStop class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class SlowToStop : MonoBehaviour
{
    public float SlowRate = 0.9f;

    private bool m_getToSlowing = false;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_getToSlowing)
        {
            rigidbody.velocity *= SlowRate;
        }
    }
    
    void OnEnable()
    {
        GetComponent<EventManager>().ActivateEvent += OnActivate;
    }
    
    void OnDisable()
    {
        GetComponent<EventManager>().ActivateEvent -= OnActivate;
    }
    
    void OnActivate()
    {
        m_getToSlowing = true;
    }
}
