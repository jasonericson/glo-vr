/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   CloneRing_Editor.cs
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the CloneRing_Editor class.
*/
/******************************************************************************/

using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CloneRing))]
public class CloneRing_Editor : Editor
{
	public void OnSceneGUI()
	{
		CloneRing cr = (CloneRing)target;
		Vector3 localInitialPosition = Quaternion.AngleAxis(cr.startRotation, cr.rotationAxis) * (cr.firstOffsetFromCenter * cr.offsetScaleFactor);
		Vector3 localOppositePosition = Quaternion.AngleAxis(180, cr.rotationAxis) * localInitialPosition;
		Vector3 initialPosition = cr.transform.localToWorldMatrix * localInitialPosition;
		Vector3 oppositePosition = cr.transform.localToWorldMatrix * localOppositePosition;
		
		Handles.DrawLine (initialPosition + cr.transform.position, ((initialPosition + oppositePosition) / 2) + cr.transform.position);
		Quaternion rotation = cr.transform.rotation * Quaternion.FromToRotation(new Vector3 (0, 0, 1), cr.rotationAxis.normalized);
		Handles.color = Color.white;
		Handles.CircleCap(0, (initialPosition + oppositePosition) / 2 + cr.transform.position, rotation, (initialPosition - oppositePosition).magnitude / 2);
	}
}
