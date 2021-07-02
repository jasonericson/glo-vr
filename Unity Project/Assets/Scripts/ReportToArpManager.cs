/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   ReportToArpManager.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the ReportToArpManager class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class ReportToArpManager : MonoBehaviour
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
        GameObject.Find("Dome").GetComponent<ArpeggioManager>().PathNodeDeleted();
    }
}
