/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   ChaseObject.cs
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the ChaseObject class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class ChaseObject : MonoBehaviour {

	public GameObject objectToChase = null;
	public bool useDamping = false;
	public float chaseSpeedOrDamping = 1.0f;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		if (objectToChase == null)
		{
			return;
		}

		if (useDamping)
		{
			float damping = chaseSpeedOrDamping;

			transform.position = Vector3.Lerp(objectToChase.transform.position, transform.position, damping);
		}
		else
		{
			float chaseSpeed = chaseSpeedOrDamping;
			float distToMove = Time.fixedDeltaTime * chaseSpeed;

			Vector3 toTarget = objectToChase.transform.position - transform.position;
			if (toTarget.magnitude < distToMove)
			{
				transform.position = objectToChase.transform.position;
			}
			else
			{
				transform.position += toTarget.normalized * distToMove;
			}
		}
	}
}
