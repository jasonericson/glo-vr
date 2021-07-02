/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   PadManager.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the PadManager class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PadNotePair
{
    public FMODAsset Primary;
    public FMODAsset Secondary;
}

public class PadPlayerData
{
    public GameObject Player;
    public int Index;
}

public class PadManager : MonoBehaviour
{
    public List<FMODAsset> PrimaryPads;
    public List<FMODAsset> SecondaryPads;

    private List<PadPlayerData> m_resolvedPlayers;
    private List<PadPlayerData> m_unresolvedPlayers;
    private List<PadNotePair> m_padEvents;
    private List<int> m_availableIndices;

    // Use this for initialization
    void Start ()
    {
        m_resolvedPlayers = new List<PadPlayerData>();
        m_unresolvedPlayers = new List<PadPlayerData>();
        m_padEvents = new List<PadNotePair>();
        m_availableIndices = new List<int>();

        for (int i = 0; i < PrimaryPads.Count; ++i)
        {
            var pair = new PadNotePair();
            pair.Primary = PrimaryPads[i];
            if (i < SecondaryPads.Count)
            {
                pair.Secondary = SecondaryPads[i];
            }

            m_padEvents.Add(pair);
            m_availableIndices.Add(i);
        }
    }
    
    // Update is called once per frame
    void Update ()
    {

    }

    public void RegisterPlayer(GameObject obj)
    {
        int index;
        int randPick;
        if (m_availableIndices.Count == 0)
        {
            PadPlayerData dataToRemove;
            if (m_resolvedPlayers.Count == 0)
            {
                dataToRemove = m_unresolvedPlayers.GetAndRemoveRandomValue();
            }
            else if (m_unresolvedPlayers.Count == 0)
            {
                dataToRemove = m_resolvedPlayers.GetAndRemoveRandomValue();
            }
            else
            {
                randPick = Random.Range(0, 3);
                if (randPick == 0)
                {
                    dataToRemove = m_unresolvedPlayers.GetAndRemoveRandomValue();
                }
                else
                {
                    dataToRemove = m_resolvedPlayers.GetAndRemoveRandomValue();
                }
            }

            Destroy(dataToRemove.Player);
            index = dataToRemove.Index;
        }
        else if (m_availableIndices.Count == 1)
        {
            index = m_availableIndices[0];
            m_availableIndices.RemoveAt(0);
        }
        else
        {
            index = m_availableIndices.GetAndRemoveRandomValue();
        }

        if (m_resolvedPlayers.Count + m_unresolvedPlayers.Count >= 3)
        {
            AdjustChord();
        }

        var pair = m_padEvents[index];
        var data = new PadPlayerData { Player = obj, Index = index };
        var emitter = obj.GetComponent<FMOD_StudioEventEmitter>();
        randPick = Random.Range(0, 3);
        // 1/3 chance of an unresolved note
        if (randPick == 0)
        {
            emitter.SwapAsset(pair.Secondary);
            m_unresolvedPlayers.Add(data);
        }
        // 2/3 chance of a resolved note
        else
        {
            emitter.SwapAsset(pair.Primary);
            m_resolvedPlayers.Add(data);
        }
        emitter.Play();
    }

    public void Unregister(GameObject obj)
    {
        PadPlayerData toDestroy = null;
        foreach (var data in m_resolvedPlayers)
        {
            if (data.Player == obj)
            {
                toDestroy = data;
                break;
            }
        }

        if (toDestroy != null)
        {
            m_resolvedPlayers.Remove(toDestroy);
            m_availableIndices.Add(toDestroy.Index);
        }
        else
        {
            foreach (var data in m_unresolvedPlayers)
            {
                if (data.Player == obj)
                {
                    toDestroy = data;
                    break;
                }
            }

            if (toDestroy != null)
            {
                m_unresolvedPlayers.Remove(toDestroy);
                m_availableIndices.Add(toDestroy.Index);
            }
        }
    }

    public void AdjustChord()
    {
        bool resolve;
        // If there are no resolved pads, we will resolve one.
        if (m_resolvedPlayers.Count == 0)
        {
            resolve = true;
        }
        // If there are no unresolved pads, we will resolve one.
        else if (m_unresolvedPlayers.Count == 0)
        {
            resolve = false;
        }
        // Otherwise, it's a 1/3 chance we will unresolve a note (2/3 to resolve)
        else
        {
            resolve = Random.Range(0, 3) > 0;
        }

        // Randomly choose a pad to resolve
        if (resolve)
        {
            var data = m_unresolvedPlayers.GetAndRemoveRandomValue();

            var emitter = data.Player.GetComponent<FMOD_StudioEventEmitter>();
            emitter.SwapAsset(m_padEvents[data.Index].Primary);
            emitter.Play();

            m_resolvedPlayers.Add(data);
        }
        // Randomly choose a pad to unresolve
        else
        {
            var data = m_resolvedPlayers.GetAndRemoveRandomValue();

            var emitter = data.Player.GetComponent<FMOD_StudioEventEmitter>();
            emitter.SwapAsset(m_padEvents[data.Index].Secondary);
            emitter.Play();

            m_unresolvedPlayers.Add(data);
        }
    }
}
