/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   SplashScreen.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the SplashScreen class.
*/
/******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SplashScreen : MonoBehaviour
{
    public List<GameObject> Screens;
    public float FadeTime = 1.0f;
    public float HoldTime = 2.0f;

    private int m_currentScreen = 0;
    private float m_timer = 0.0f;

	// Use this for initialization
	void Start ()
    {
	    foreach (Transform screen in transform)
        {
            FadeSprite(screen.gameObject, 0.0f);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_timer < FadeTime)
        {
            FadeSprite(Screens[m_currentScreen], m_timer / FadeTime);
        }
        else if (m_timer < FadeTime + HoldTime)
        {
            // Do nothing
        }
        else if (m_timer < FadeTime + HoldTime + FadeTime)
        {
            FadeSprite(Screens[m_currentScreen], 1.0f - (m_timer - FadeTime - HoldTime) / FadeTime);
        }
        else
        {
            ++m_currentScreen;
            if (m_currentScreen >= Screens.Count)
            {
                Destroy(gameObject);
            }
            else
            {
                m_timer = 0.0f;
            }
        }

        m_timer += Time.deltaTime;
	}

    void FadeSprite(GameObject sprite, float amount)
    {
        amount = Mathf.Clamp(amount, 0.0f, 1.0f);
        var spriteComp = sprite.GetComponent<Image>();
        var color = spriteComp.color;
        color.a = amount;
        spriteComp.color = color;
    }
}
