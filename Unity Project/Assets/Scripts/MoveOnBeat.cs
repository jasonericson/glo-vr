using UnityEngine;
using System.Collections;

public class MoveOnBeat : MonoBehaviour
{
    private float m_moveSpeed;

	// Use this for initialization
	void Start ()
    {
        var metronome = Metronome.Instance;
        m_moveSpeed = 60.0f / metronome.BPM / 2.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {

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
        GetComponent<MoveAlongPath>().MoveToNextNode(m_moveSpeed);
    }
}
