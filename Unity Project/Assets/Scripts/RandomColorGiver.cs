/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   RandomColorGiver.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the RandomColorGiver class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RandomColorGiver : MonoBehaviour
{
    public static RandomColorGiver Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = GameObject.Find("Dome").GetComponent<RandomColorGiver>();
            }

            return s_instance;
        }
    }
    private static RandomColorGiver s_instance = null;

    public List<Color> Colors = null;

    private List<Color> m_actualColors = new List<Color>(8);
    private Color m_lastColor;

	// Use this for initialization
	void Start ()
    {
        foreach (var color in Colors)
        {
            m_actualColors.Add(color);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public Color Give()
    {
        var color = m_actualColors.GetAndRemoveRandomValue();
        if (!(m_lastColor.r == 0.0f && m_lastColor.g == 0.0f && m_lastColor.b == 0.0f))
        {
            m_actualColors.Add(m_lastColor);
        }
        m_lastColor = color;
        return color;
    }
}
