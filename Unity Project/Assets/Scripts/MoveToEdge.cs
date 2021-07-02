/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   MoveToEdge.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the MoveToEdge class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MoveToEdge : MonoBehaviour
{
	public float Distance;
	public float Height;
	public float Rotation;

	private float m_initRotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnEnable()
	{
		m_initRotation = transform.eulerAngles.y;

		var newPosition = transform.forward.normalized * Distance;
		newPosition.y = Height;
		transform.position = newPosition;
		var newRotation = transform.eulerAngles;
		newRotation.x = Rotation;
		newRotation.y = m_initRotation;
		transform.eulerAngles = newRotation;
	}
}
