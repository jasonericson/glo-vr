/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   SliderManager.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the SliderManager class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SliderManager : MonoBehaviour
{
    private List<GameObject> m_players;

	// Use this for initialization
	void Start ()
    {
	
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void RegisterPlayer(GameObject obj)
    {
        m_players.Add(obj);
    }
}
