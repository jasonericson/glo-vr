/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   CatchAndRelease.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the CatchAndRelease class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class CatchAndRelease : MonoBehaviour
{
    public KeyCode ReleaseKey;
	public SixenseButtons HydraReleaseKey;
    public float ReleaseThreshold;
    public GameObject ArpLight;

    private GameObject m_dome;
    private ObjectSpawner m_spawner;
	private SixenseInput.Controller m_controller = null;
    private Vector3? m_lastPosition = null;
    private Vector3 m_lastVelocity;
    private bool m_holding = false;

	// Use this for initialization
	void Start ()
	{
		m_dome = GameObject.Find("Dome");
        m_spawner = GetComponent<ObjectSpawner> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (m_controller == null)
		{
			m_controller = SixenseInput.GetController(SixenseHands.RIGHT);
		}

        if (m_controller != null)
        {
            if (m_controller.GetButton(HydraReleaseKey))
            {
                if (m_spawner.SpawnedObject == null)
                {
                    m_holding = false;
                    return;
                }

                m_holding = true;
            }
            
            if (m_holding)
            {
                if (m_lastPosition == null)
                {
                    m_lastPosition = m_spawner.SpawnedObject.transform.position;
                }
                var velocity = m_spawner.SpawnedObject.transform.position - m_lastPosition.GetValueOrDefault ();

                if (!m_controller.GetButton(HydraReleaseKey))
                {
                    m_spawner.SpawnedObject.transform.parent = null;
                    m_spawner.SpawnedObject.rigidbody.velocity = m_lastVelocity.normalized;

                    switch (m_spawner.SpawnedObject.tag)
                    {
                    case "Rhodes":
                        m_dome.GetComponent<RhodesManager> ().RegisterPlayer (m_spawner.SpawnedObject);
                        break;
                    case "Pad":
                        m_dome.GetComponent<PadManager> ().RegisterPlayer (m_spawner.SpawnedObject);
                        break;
                    case "Arpeggio":
                        var path = (GameObject.Find("Path") as GameObject).GetComponent<NodePath>();
                        if (path.NumNodes() % 3 == 0)
                        {
                            var arp = Instantiate(ArpLight) as GameObject;
                            arp.GetComponent<MoveAlongPath>().pathToFollow = path;
                            arp.GetComponent<EventManager>().Activate();
                            m_dome.GetComponent<ArpeggioManager>().RegisterPlayer(arp);
                        }
                        break;
                    }
                    
                    m_spawner.SpawnedObject.GetComponent<EventManager> ().Activate ();
                
                    m_spawner.ReleaseObject ();
                    m_holding = false;
                    m_lastVelocity = Vector3.zero;
                    m_lastPosition = null;

                    return;
                }

                m_lastVelocity = velocity;
                m_lastPosition = m_spawner.SpawnedObject.transform.position;
            }
        }
        else
        {
            m_holding = false;
        }

	    if (Input.GetKeyDown(ReleaseKey))
	    {
	        var spawner = GetComponent<ObjectSpawner>();
	        if (spawner != null && spawner.SpawnedObject != null)
	        {
                spawner.SpawnedObject.transform.parent = null;
                m_spawner.SpawnedObject.rigidbody.velocity = (new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f))).normalized * 2.0f;

	            switch (spawner.SpawnedObject.tag)
	            {
                case "Rhodes":
                    m_dome.GetComponent<RhodesManager>().RegisterPlayer(spawner.SpawnedObject);
                    break;
                case "Pad":
                	m_dome.GetComponent<PadManager>().RegisterPlayer(spawner.SpawnedObject);
                	break;
                case "Arpeggio":
                    var path = (GameObject.Find("Path") as GameObject).GetComponent<NodePath>();
                    if (path.NumNodes() % 3 == 0)
                    {
                        var arp = Instantiate(ArpLight) as GameObject;
                        arp.GetComponent<MoveAlongPath>().pathToFollow = path;
                        arp.GetComponent<EventManager>().Activate();
                        m_dome.GetComponent<ArpeggioManager>().RegisterPlayer(arp);
                    }
                    break;
	            }
                
                spawner.SpawnedObject.GetComponent<EventManager>().Activate();
                    
                spawner.ReleaseObject();
	        }
	    }
	}
}
