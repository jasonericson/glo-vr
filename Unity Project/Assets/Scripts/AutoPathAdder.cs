/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   AutoPathAdder.cs
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the AutoPathAdder class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class AutoPathAdder : MonoBehaviour
{
    NodePath path = null;

    void OnEnable()
    {
        GetComponent<EventManager>().ActivateEvent += OnActivate;
    }
    
    void OnDisable()
    {
        path.nodes.Remove(transform);
        GetComponent<EventManager>().ActivateEvent -= OnActivate;
    }
    
    private void OnActivate()
    {
        path = FindObjectOfType<NodePath>();
        if (path.nodes.Count < 2)
        {
            path.nodes.Add(transform);
        }
        else
        {
            float closestTransformDist = float.PositiveInfinity;
            int closestTransformIndex = -1;
            for (int i = 0; i < path.nodes.Count; ++i)
            {
                Transform node = path.nodes[i];
                float currDist = (node.position - transform.position).magnitude;
                if (currDist < closestTransformDist)
                {
                    closestTransformDist = currDist;
                    closestTransformIndex = i;
                }
            }
            int indexOfPrevious = (closestTransformIndex + (path.nodes.Count - 1)) % path.nodes.Count;
            int indexOfNext = (closestTransformIndex + 1) % path.nodes.Count;
            float previousLength = (path.nodes[indexOfPrevious].position - transform.position).magnitude;
            float nextLength = (path.nodes[indexOfNext].position - transform.position).magnitude;
            if (previousLength < nextLength)
            {
                path.nodes.Insert(indexOfPrevious + 1, transform);
            }
            else
            {
                path.nodes.Insert(closestTransformIndex + 1, transform);
            }
        }
	}
}
