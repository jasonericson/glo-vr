/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   Helpers.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the Helpers class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Helpers
{
    public static T GetAndRemoveRandomValue<T>(this IList<T> list)
    {
        var randPick = Random.Range(0, list.Count);
        var value = list[randPick];
        list.RemoveAt(randPick);

        return value;
    }
}
