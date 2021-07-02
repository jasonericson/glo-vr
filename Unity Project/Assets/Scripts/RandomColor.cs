/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   RandomColor.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the RandomColor class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour
{
    public Color CreatedColor;

	// Use this for initialization
	void Start()
	{
        CreatedColor = RandomColorGiver.Instance.Give();

        if (light != null)
        {
            light.color = CreatedColor;
        }
        foreach (var l in transform.GetComponentsInChildren<Light>())
        {
            l.color = CreatedColor;
        }

        if (renderer != null)
        {
            renderer.material.color = CreatedColor;
        }
        foreach (var r in transform.GetComponentsInChildren<Renderer>())
        {
            r.material.color = CreatedColor;
        }

	    var color = CreatedColor;
	    foreach (var particles in transform.GetComponentsInChildren<ParticleSystem>())
	    {
	        color.a = particles.startColor.a;
	        particles.startColor = color;
	    }

        var trail = GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.material.color = CreatedColor;
        }
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
