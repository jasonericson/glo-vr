/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   RhodesManager.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the RhodesManager class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerData
{
    public GameObject Obj;
    public BeatReceiver Receiver;
    public int TickAssigned;
    public int TickBefore;
    public int TickAfter;
    public bool WaitTillNext = false;
}

public class RhodesManager : MonoBehaviour
{
    public GameObject Player;
    public Material MaterialToCopy;

    private Dictionary<int, List<PlayerData>> m_players = new Dictionary<int, List<PlayerData>>(8);

    private List<int> m_tickPattern = new List<int> { 0, 16, 8, 24, 4, 20, 12, 28 };

	// Use this for initialization
	void Start ()
    {
	}

    void OnEnable()
    {
        GetComponent<Metronome>().BeatHit += OnBeatHit;
    }

    void OnDisable()
    {
        GetComponent<Metronome>().BeatHit -= OnBeatHit;
    }

    void OnBeatHit(BeatEventArgs e)
    {
        List<PlayerData> nextList;
        if (m_players.TryGetValue(e.Beat.Tick, out nextList))
        {
            List<PlayerData> sameTick = new List<PlayerData>();
            foreach (var player in nextList)
            {
                if (player.WaitTillNext)
                {
                    player.WaitTillNext = false;
                    sameTick.Add(player);
                    continue;
                }

                player.Receiver.Play(e);
                int check = UnityEngine.Random.Range(0, 5);
                // Play early
                if (check == 0)
                {
                    // If this is playing the same time next time
                    if (e.Beat.Tick == player.TickBefore)
                    {
                        sameTick.Add(player);
                    }
                    // It won't play again soon, so need to deal with that case.
                    else
                    {
                        AddToPlayers(player.TickBefore, player);
                    }
                }
                // Play late
                else if (check == 1)
                {
                    // If this next tick will come later (but soon)
                    if (e.Beat.Tick == player.TickBefore || e.Beat.Tick == player.TickAssigned)
                    {
                        player.WaitTillNext = true;
                        AddToPlayers(player.TickAfter, player);
                    }
                    // The only other option is it plays at the same time as before
                    else
                    {
                        sameTick.Add(player);
                    }
                }
                // Play normally
                else
                {
                    // If this next tick will come later (but soon)
                    if (e.Beat.Tick == player.TickBefore)
                    {
                        player.WaitTillNext = true;
                        AddToPlayers(player.TickAssigned, player);
                    }
                    // If this will play at the same time as before
                    else if (e.Beat.Tick == player.TickAssigned)
                    {
                        sameTick.Add(player);
                    }
                    // Otherwise, no special case
                    else
                    {
                        AddToPlayers(player.TickAssigned, player);
                    }
                }
            }

            m_players[e.Beat.Tick] = sameTick;
        }
    }

    public void RegisterPlayer(GameObject obj)
    {
        if (obj.GetComponent<BeatReceiver>() == null)
        {
            Debug.LogWarning(string.Format("{0} does not have a BeatReciever component and can't be registered with RhodesManager.", obj.name));
        }

        if (m_tickPattern.Count > 0)
        {
            int tick = m_tickPattern[0];
            m_tickPattern.RemoveAt(0);
            
            int tickBefore = tick - 4;
            if (tickBefore < 0)
            {
                tickBefore += 32;
            }
            int tickAfter = tick + 4;
            if (tickAfter >= 32)
            {
                tickAfter -= 32;
            }
            
            AddToPlayers(tick, new PlayerData { Obj = obj, Receiver = obj.GetComponent<BeatReceiver>(), TickAssigned = tick, TickBefore = tickBefore, TickAfter = tickAfter, WaitTillNext = false });
        }
        else
        {
            // Switch out a random player with this one
            var occupiedTicks = m_players.Where(kvp => kvp.Value.Count > 0).ToList();
            var randTick = Random.Range(0, occupiedTicks.Count);
            var randPlayer = Random.Range(0, occupiedTicks[randTick].Value.Count);

            var playerData = occupiedTicks[randTick].Value[randPlayer];
            Destroy(playerData.Obj);
            playerData.Obj = obj;
            playerData.Receiver = obj.GetComponent<BeatReceiver>();
        }
    }

    public void Unregister(GameObject obj)
    {
        foreach (var kvp in m_players)
        {
            PlayerData toDestroy = null;
            foreach (var player in kvp.Value)
            {
                if (player.Obj == obj)
                {
                    toDestroy = player;
                    break;
                }
            }

            if (toDestroy != null)
            {
                kvp.Value.Remove(toDestroy);
                m_tickPattern.Add(kvp.Key);
                break;
            }
        }
    }

    private void AddToPlayers(int tick, PlayerData player)
    {
        if (!m_players.ContainsKey(tick))
        {
            m_players.Add(tick, new List<PlayerData>());
        }

        m_players[tick].Add(player);
    }
}
