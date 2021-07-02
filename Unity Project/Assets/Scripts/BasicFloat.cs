/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   BasicFloat.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the BasicFloat class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class BasicFloat : MonoBehaviour
{
	public float Speed;

    private bool m_active = false;

    void OnEnable()
    {
        GetComponent<EventManager>().ActivateEvent += OnActivate;
    }

    void OnDisable()
    {
        GetComponent<EventManager>().ActivateEvent -= OnActivate;
    }

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (!m_active)
	        return;

		rigidbody.velocity = rigidbody.velocity.normalized * Speed;
	}

    void OnActivate()
    {
        m_active = true;

        Vector3 dir = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        dir.Normalize();
        rigidbody.AddForce(dir * Speed);
    }
}
