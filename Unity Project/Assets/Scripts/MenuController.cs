/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   MenuController.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the MenuController class.
*/
/******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
    public GameObject Dome = null;

    private List<GameObject> m_parentChain = new List<GameObject>();
    private GameObject m_currentMenu = null;

	// Use this for initialization
	void Start ()
    {
        Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnEnable()
    {
        foreach (Transform t in transform)
        {
            if (t.name == "Main")
            {
                t.gameObject.SetActive(true);
                m_currentMenu = t.gameObject;
                SelectDefault(t.gameObject, "Resume", "Quit");
            }
            else
            {
                t.gameObject.SetActive(false);
            }
        }
    }

    void SelectDefault(GameObject menu, string def, string other)
    {
        var otherButton = menu.transform.FindChild(other).GetComponent<Button>();
        otherButton.Select();
        var defaultButton = menu.transform.FindChild(def).GetComponent<Button>();
        defaultButton.Select();
    }

    void SelectDefault(GameObject menu)
    {
        switch (menu.name)
        {
        case "Main":
            SelectDefault(menu, "Resume", "Quit");
            break;
        case "Quit CODA":
            SelectDefault(menu, "No", "Yes");
            break;
        case "Restart CODA":
            SelectDefault(menu, "No", "Yes");
            break;
        }
    }

    public void QuitCoda()
    {
        m_parentChain.Add(m_currentMenu);
        m_currentMenu.SetActive(false);

        var coda = transform.FindChild("Quit CODA");
        coda.gameObject.SetActive(true);
        SelectDefault(coda.gameObject);
        m_currentMenu = coda.gameObject;
    }
    
    public void RestartCoda()
    {
        m_parentChain.Add(m_currentMenu);
        m_currentMenu.SetActive(false);
        
        var coda = transform.FindChild("Restart CODA");
        coda.gameObject.SetActive(true);
        SelectDefault(coda.gameObject);
        m_currentMenu = coda.gameObject;
    }

    public void Credits()
    {
        m_parentChain.Add(m_currentMenu);
        m_currentMenu.SetActive(false);
        
        var coda = transform.FindChild("Restart CODA");
        coda.gameObject.SetActive(true);
        SelectDefault(coda.gameObject);
        m_currentMenu = coda.gameObject;
    }

    public void OpenSubMenu(GameObject menu)
    {
        m_parentChain.Add(m_currentMenu);
        m_currentMenu.SetActive(false);

        menu.SetActive(true);
        SelectDefault(menu);
        m_currentMenu = menu;
    }

    public void Back()
    {
        if (m_parentChain.Count == 0)
        {
            GameObject.Find("Dome").GetComponent<GameController>().Resume();
        }
        else
        {
            foreach (Transform t in transform)
            {
                if (t.gameObject != m_parentChain[m_parentChain.Count - 1])
                {
                    t.gameObject.SetActive(false);
                }
            }

            var newMenu = m_parentChain[m_parentChain.Count - 1];
            newMenu.SetActive(true);
            SelectDefault(newMenu);
            m_parentChain.RemoveAt(m_parentChain.Count - 1);
            m_currentMenu = newMenu;
        }
    }
}
