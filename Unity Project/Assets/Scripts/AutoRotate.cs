/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   AutoRotate.cs
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the AutoRotate class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

    public Vector3 rotationAxis = new Vector3(0, 0, 1);
    public float degreesPerSecond = 90.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate (rotationAxis, degreesPerSecond * Time.deltaTime);
	}
}
