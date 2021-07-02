/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   ChangeColorOnBeat.cs
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the ChangeColorOnBeat class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ChangeColorOnBeat : MonoBehaviour
{
    public Renderer rendererToChange = null;
    public float flashDuration = 1.0f;
    [RangeAttribute(0,1)]
    public float dimRangePercentage = 0.0f;
    [RangeAttribute(0,1)]
    public float dimIntensityPercentage = 0.0f;

    public float RUNTIME_baseIntensity = 0.0f;
    public float RUNTIME_baseRange = 0.0f;

    private List<ParticleSystem> m_particles = null;

    void Start()
    {
        RUNTIME_baseIntensity = light.intensity;
        RUNTIME_baseRange = light.range;

        var newColor = RandomColorGiver.Instance.Give();
        rendererToChange.material.SetColor ("_TintColor", newColor);
        light.color = newColor;

        light.intensity = dimIntensityPercentage;
        light.range = dimRangePercentage;

        m_particles = new List<ParticleSystem>(transform.GetComponentsInChildren<ParticleSystem>());

        foreach (var particles in m_particles)
        {
            newColor.a = particles.startColor.a;
            particles.startColor = newColor;
        }
    }

    void OnEnable()
    {
		GetComponent<BeatReceiver>().BeatHit += OnBeatHit;
    }

    void OnDisable()
    {
        GetComponent<BeatReceiver>().BeatHit -= OnBeatHit;
    }

    void OnBeatHit(BeatEventArgs e)
    {
        StartCoroutine("LightFlash");
        foreach (var particle in m_particles)
        {
            particle.Play();
        }
    }

    IEnumerator LightFlash()
    {
        float f = flashDuration;
        while (f > 0.0f)
        {
            light.intensity = Mathf.Lerp (dimIntensityPercentage, 1, f / flashDuration) * RUNTIME_baseIntensity;
            light.range = Mathf.Lerp (dimRangePercentage, 1, f / flashDuration) * RUNTIME_baseRange;
            f -= Time.deltaTime;
            yield return null;
        }
        light.intensity = dimIntensityPercentage * RUNTIME_baseIntensity;
        light.range = dimRangePercentage * RUNTIME_baseRange;
    }
}
