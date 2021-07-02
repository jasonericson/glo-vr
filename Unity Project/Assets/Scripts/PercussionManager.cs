/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   PercussionManager.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the PercussionManager class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PercussionManager : MonoBehaviour
{
    public int MaxPlayers = 2;

    private Dictionary<int, List<GameObject>> m_players = new Dictionary<int, List<GameObject>>(3);

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void RegisterPlayer(GameObject obj)
    {
        var percPlayer = obj.GetComponent<PercussionPlayer>();
        List<GameObject> playList;
        if (m_players.TryGetValue(percPlayer.ThisIndex, out playList))
        {
            if (playList.Count < MaxPlayers)
            {
                playList.Add(obj);
            }
            else
            {
                var toDestroy = playList[0];
                playList.RemoveAt(0);
                Destroy(toDestroy);
                playList.Add(obj);
            }
        }
        else
        {
            playList = new List<GameObject>(MaxPlayers);
            playList.Add(obj);
            m_players.Add(percPlayer.ThisIndex, playList);
        }
    }

    public void Unregister(GameObject obj)
    {
        var percPlayer = obj.GetComponent<PercussionPlayer>();
        var playList = m_players[percPlayer.ThisIndex];
        playList.Remove(obj);
        PercussionPlayer.s_index = percPlayer.ThisIndex;
    }
}
