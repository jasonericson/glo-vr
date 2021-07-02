/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   ReportToRhodesManager.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the ReportToRhodesManager class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class ReportToRhodesManager : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnDestroy()
    {
        GameObject.Find("Dome").GetComponent<RhodesManager>().Unregister(gameObject);
    }
}
