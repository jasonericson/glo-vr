/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   CreateLightArc.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the CreateLightArc class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Reflection;

public class CreateLightArc : MonoBehaviour
{
    public GameObject Prefab = null;

    private GameObject m_lightArc = null;

    void OnEnable()
    {
        GetComponent<EventManager>().ActivateEvent += OnActivate;
    }

    void OnDisable()
    {
        GetComponent<EventManager>().ActivateEvent -= OnActivate;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        if (m_lightArc != null)
        {
            Destroy(m_lightArc);
        }
    }

    private void OnActivate()
    {
        m_lightArc = Instantiate(Prefab) as GameObject;
        m_lightArc.transform.GetComponentInChildren<FollowArc>().objectToAimAt = gameObject;
        m_lightArc.transform.GetComponentInChildren<ChaseObject>().objectToChase = gameObject;
        var color = GetComponent<RandomColor>().CreatedColor;
        var particles = m_lightArc.transform.GetComponentInChildren<ParticleSystem>();
        color.a = particles.startColor.a;
        particles.startColor = color;
    }
}
