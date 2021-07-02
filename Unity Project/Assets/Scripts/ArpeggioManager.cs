/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   ArpeggioManager.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the ArpeggioManager class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArpeggioManager : MonoBehaviour
{
    private List<float> m_sets = new List<float> { 0.0f, 1.0f, 2.0f, 3.0f };
    public List<GameObject> Players = new List<GameObject>(4);
    public NodePath Path;
    public GameObject ArpLight;
    
    // Use this for initialization
    void Start ()
    {
        // TODO: Remove this
        Players.Clear ();
    }
    
    // Update is called once per frame
    void Update ()
    {
        
    }
    
    public void RegisterPlayer(GameObject obj)
    {
        var arpPlayer = obj.GetComponent<ArpeggioPlayer>();
        if (arpPlayer == null)
        {
            Debug.LogError(string.Format("{0} does not have a ArpeggioPlayer component and can't be registered with ArpeggioManager.", obj.name));
            return;
        }
        
        if (m_sets.Count > 1)
        {
            var set = m_sets.GetAndRemoveRandomValue();
            arpPlayer.Set = set;
        }
        else
        {
            var play = Players.GetAndRemoveRandomValue();
            var set = play.GetComponent<ArpeggioPlayer>().Set;
            Destroy(play);
            arpPlayer.Set = m_sets[0];
            m_sets.RemoveAt(0);
            m_sets.Add(set);
        }

        Players.Add(obj);
    }

    public void PathNodeDeleted()
    {
        if (Path.NumNodes() % 3 == 0)
        {
            if (Path.NumNodes() > 6)
            {
                var arp = Instantiate(ArpLight) as GameObject;
                arp.GetComponent<MoveAlongPath>().pathToFollow = Path;
                arp.GetComponent<EventManager>().Activate();
                RegisterPlayer(arp);
            }
            else
            {
                var play = Players.GetAndRemoveRandomValue();
                m_sets.Add(play.GetComponent<ArpeggioPlayer>().Set);
                Destroy(play);
            }
        }
    }
}
