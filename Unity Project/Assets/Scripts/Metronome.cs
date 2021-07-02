/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   Metronome.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the Metronome class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System;

public struct BeatData
{
    public byte Quarter;
    public byte Eighth;
    public byte Sixteenth;
    public byte ThirtySecond;

    public byte Tick;
}

public class BeatEventArgs : EventArgs
{
    public BeatData Beat { get; set; }
}
public delegate void BeatEventHandler(BeatEventArgs e);
public delegate void EmptyEventHandler();

public class TripletMetronome
{
    public event EmptyEventHandler BeatHit = delegate {};

    private static TripletMetronome s_instance;
    public static TripletMetronome Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = Metronome.Instance.Triplets;
            }

            return s_instance;
        }
    }

    public void PlayBeat()
    {
        BeatHit();
    }
}

public class Metronome : MonoBehaviour
{
    public event BeatEventHandler BeatHit;

    private static Metronome s_instance;
    public static Metronome Instance
    {
        get
        {
            if (s_instance == null)
            {
                var dome = GameObject.Find("Dome");
                s_instance = dome.GetComponent<Metronome>();
            }

            return s_instance;
        }
    }
    
    public TripletMetronome Triplets = new TripletMetronome();
    public float BPM = 120.0f;

    private float m_timer = -1.0f;
    private float m_tripletTimer = -1.0f;
    private BeatData m_currentBeat;
    private bool m_firstFrame = true;

	// Use this for initialization
	void Start ()
    {
        NormalizeBeat(ref m_currentBeat);
	}
	
	void FixedUpdate ()
    {
        if (m_firstFrame)
        {
            NormalizeBeat(ref m_currentBeat);

            //if (BeatHit != null)
            //{
            //    BeatHit.Invoke(new BeatEventArgs { Beat = m_currentBeat });
            //}

            m_firstFrame = false;
        }
        else
        {
            float beatResolution = 60.0f / BPM / 8.0f;
            m_timer += Time.deltaTime;
            if (m_timer >= beatResolution)
            {
                m_timer -= beatResolution;

                m_currentBeat.Tick += 1;
                NormalizeBeat(ref m_currentBeat);

                if (BeatHit != null)
                {
                    BeatHit.Invoke(new BeatEventArgs { Beat = m_currentBeat });
                }
            }
            
            float tripletResolution = 60.0f / BPM * 2.0f / 3.0f;
            m_tripletTimer += Time.deltaTime;
            if (m_tripletTimer >= tripletResolution)
            {
                m_tripletTimer -= tripletResolution;

                Triplets.PlayBeat();
            }
        }

	}

    public static void NormalizeBeat(ref BeatData beat)
    {
        while (beat.Tick >= 32)
        {
            beat.Tick -= 32;
        }

        beat.ThirtySecond = (byte) (beat.Tick % 2 + 1);
        beat.Sixteenth = (byte) ((beat.Tick / 2) % 2 + 1);
        beat.Eighth = (byte) ((beat.Tick / 4) % 2 + 1);
        beat.Quarter = (byte) ((beat.Tick / 8) + 1);
    }
}
