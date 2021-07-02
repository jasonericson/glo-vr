using UnityEngine;
using System.Collections;

public class ReportToPercManager : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnDestroy()
    {
        GameObject.Find("Dome").GetComponent<PadManager>().Unregister(gameObject);
    }
}
