/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   FollowArc.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the FollowArc class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class FollowArc : MonoBehaviour {
	
	public GameObject objectToAimAt = null;
	public GameObject controlPointObject = null;
	public float distBetweenParticles = 1.0f;

	private ParticleSystem particles = null;

	private ParticleSystem.Particle[] particleBuffer = new ParticleSystem.Particle[1000];

	void Start()
	{
		particles = GetComponent<ParticleSystem> ();
	}
	
	void Update ()
	{
		if (distBetweenParticles <= 0.0f)
		{
			return;
		}

		Vector3 actualTarget = objectToAimAt.transform.position;
		Vector3 delayedTarget = controlPointObject.transform.position;
		Vector3 startPosition = transform.position;

		float distToActual = (startPosition - actualTarget).magnitude;
		float distToDelayed = (startPosition - delayedTarget).magnitude;
		float maxDist = Mathf.Max (distToActual, distToDelayed);

		int expectedParticleCount = Mathf.CeilToInt(maxDist / distBetweenParticles);
		if (expectedParticleCount > particles.particleCount)
		{
			particles.Emit(expectedParticleCount - particles.particleCount);
		}

		int particleCount = particles.GetParticles (particleBuffer);
		if (particleCount > particleBuffer.Length)
		{
			particleCount = particleBuffer.Length;
		}
		if (expectedParticleCount > particleBuffer.Length)
		{
			expectedParticleCount = particleBuffer.Length;
        }
        for (int i = 0; i < expectedParticleCount; ++i)
		{
			float percentAlong = (float)(i) / (float)(expectedParticleCount - 1);
			Vector3 interpStart = Vector3.Lerp (startPosition, delayedTarget, percentAlong);
			Vector3 interpEnd = Vector3.Lerp (startPosition, actualTarget, percentAlong);
			Vector3 finalPos = Vector3.Lerp (interpStart, interpEnd, percentAlong);
			particleBuffer[i].position = finalPos;
			particleBuffer[i].lifetime = particleSystem.startLifetime * (1 - percentAlong);
		}
		particles.SetParticles (particleBuffer, expectedParticleCount);
	}
}
