/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   ObjectSpawner.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the ObjectSpawner class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public KeyCode SpawnKey;
	public SixenseButtons HydraSpawnKey;
	public SixenseButtons HydraSwitchKey;
    public float SwitchAngle = 45.0f;
    public float SwitchDeadZone = 10.0f;
    
    public List<GameObject> Prefabs;
    public int DefaultSelectedPrefabIndex = 0;

    public GameObject SpawnedObject { get; private set; }

	private SixenseInput.Controller m_controller = null;
    private int m_selectedPrefabIndex = 0;
    private bool m_switched = false;

    // Use this for initialization
    void Start ()
    {
        if (DefaultSelectedPrefabIndex >= 0 && DefaultSelectedPrefabIndex < Prefabs.Count)
        {
            m_selectedPrefabIndex = DefaultSelectedPrefabIndex;
        }
        else
        {
            Debug.LogError(string.Format("Default selected prefab index {0} is out of range.", DefaultSelectedPrefabIndex));
            m_selectedPrefabIndex = 0;
        }
    }
    
    // Update is called once per frame
    void Update ()
    {
		if (m_controller == null)
		{
			m_controller = SixenseInput.GetController(SixenseHands.RIGHT);
		}

        if (Prefabs.Count <= 0)
            return;

        if (Input.GetKeyDown(SpawnKey) || (m_controller != null && m_controller.GetButtonDown(HydraSpawnKey)))
        {
            if (SpawnedObject == null)
            {
                CreateObject(m_selectedPrefabIndex);
            }
            else
            {
                Destroy(SpawnedObject);
                ReleaseObject();
            }
        }

        if (SpawnedObject != null)
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0.0f || (m_controller != null && m_controller.GetButtonDown(HydraSwitchKey)))
            {
                PreviousObject();
            }
            else if (scroll < 0.0f)
            {
                NextObject();
            }

            if (m_controller != null)
            {
                var angle = m_controller.Rotation.eulerAngles.z;
                while (angle > 180.0f)
                {
                    angle = angle - 360.0f;
                }
                while (angle < -180.0f)
                {
                    angle = angle + 360.0f;
                }

                if (m_switched)
                {
                    if (angle < SwitchDeadZone && angle > -SwitchDeadZone)
                    {
                        m_switched = false;
                    }
                }
                else
                {
                    if (angle > SwitchAngle)
                    {
                        PreviousObject();
                        m_switched = true;
                    }
                    else if (angle < -SwitchAngle)
                    {
                        NextObject();
                        m_switched = true;
                    }
                }
            }
        }
    }

    void NextObject()
    {
        ++m_selectedPrefabIndex;
        if (m_selectedPrefabIndex >= Prefabs.Count)
        {
            m_selectedPrefabIndex = 0;
        }
        
        Destroy(SpawnedObject);
        CreateObject(m_selectedPrefabIndex);
    }

    void PreviousObject()
    {
        --m_selectedPrefabIndex;
        if (m_selectedPrefabIndex < 0)
        {
            m_selectedPrefabIndex = Prefabs.Count - 1;
        }
        
        Destroy(SpawnedObject);
        CreateObject(m_selectedPrefabIndex);
    }

    private void CreateObject(int index)
    {
        SpawnedObject = (GameObject) Instantiate(Prefabs[m_selectedPrefabIndex]);
        SpawnedObject.transform.parent = transform.parent;
        SpawnedObject.transform.localPosition = transform.localPosition;
        SpawnedObject.transform.localRotation = transform.localRotation;
    }

    public void ReleaseObject()
    {
        SpawnedObject = null;
    }
}
