/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   FogScript.cs
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the FogScript class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class FogScript : MonoBehaviour {

	public float timescale = 1.0f;
	public Vector2 topLayerDirection = Vector2.zero;
	public Vector2 midLayerDirection = Vector2.zero;
	public Vector2 lowLayerDirection = Vector2.zero;

	private Material material = null;



	// Use this for initialization
	void Start () {
		material = renderer.material;
	}

	void UpdateOffset(string propertyName, Vector2 amount)
	{
		var offset = material.GetTextureOffset (propertyName);
		material.SetTextureOffset (propertyName, offset + amount);
	}
	
	// Update is called once per frame
	void Update () {
		float totalScale = Time.deltaTime * timescale;
		UpdateOffset ("_TopLayer", topLayerDirection * totalScale);
		UpdateOffset ("_MidLayer", midLayerDirection * totalScale);
		UpdateOffset ("_LowLayer", lowLayerDirection * totalScale);
	}
}
