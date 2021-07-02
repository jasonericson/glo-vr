/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   NodePath.cs
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the NodePath class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodePath : MonoBehaviour {

    public List<Transform> nodes = new List<Transform>();

    public Transform GetNode(int destinationNode, int offsetFromDestination)
    {
        int index = destinationNode + offsetFromDestination;
        while (index < 0)
        {
            index += nodes.Count;
        }
        return nodes[index % nodes.Count];
    }

    public int NumNodes()
    {
        return nodes.Count;
    }
}
