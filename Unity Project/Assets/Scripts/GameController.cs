/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   GameController.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the GameController class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject Menu = null;

    private GameObject m_currentMenu = null;

	// Use this for initialization
	void Start ()
    {
        Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
	}

    public void Resume()
    {
        Destroy(m_currentMenu);
        m_currentMenu = null;
    }

    void Pause()
    {
        m_currentMenu = GameObject.Instantiate(Menu) as GameObject;
        m_currentMenu.SetActive(true);
    }

    public void TogglePause()
    {
        if (m_currentMenu == null)
        {
            Pause();
        }
        else
        {
            m_currentMenu.GetComponent<MenuController>().Back();
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }
}
