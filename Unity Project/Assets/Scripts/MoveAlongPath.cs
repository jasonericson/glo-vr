/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   MoveAlongPath.cs
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the MoveAlongPath class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class MoveAlongPath : MonoBehaviour {

    public NodePath pathToFollow = null;
    public float rotationInterpPercent = 0.1f;
    private int destinationNode = 0;
    private IEnumerator coroutine = null;

    public void MoveToNextNode(float duration)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        destinationNode = (destinationNode + 1) % pathToFollow.NumNodes();

        Transform startNode = pathToFollow.GetNode(destinationNode, -1);
        Transform endNode = pathToFollow.GetNode(destinationNode, 0);

        Transform prevNode = null;
        Transform nextNode = null;
        if (pathToFollow.NumNodes() > 2)
        {
            prevNode = pathToFollow.GetNode(destinationNode, -2);
            nextNode = pathToFollow.GetNode(destinationNode, 1);
        }
        else if (pathToFollow.NumNodes() == 1)
        {
            prevNode = pathToFollow.GetNode(destinationNode, 0);
            prevNode = pathToFollow.GetNode(destinationNode, 0);
        }

        coroutine = NodeInterpolation(prevNode, startNode, endNode, nextNode, duration);
        StartCoroutine(coroutine);
    }

    IEnumerator NodeInterpolation(Transform previous, Transform start, Transform end, Transform next, float duration)
    {
        float timeLeft = duration;
        while (timeLeft > 0.0f)
        {
            float percentAlong = 1.0f - timeLeft / duration;
            transform.rotation = Quaternion.LookRotation(end.position - start.position);
            if (previous != null && percentAlong < rotationInterpPercent)
            {
                float percentTowards = percentAlong / rotationInterpPercent;
                Vector3 fromPrev = (previous.position - start.position).normalized;
                Vector3 fromEnd = (end.position - start.position).normalized;
                Vector3 cornerDirection = ((fromPrev + fromEnd) / 2.0f).normalized;
                Vector3 planeNormal = Vector3.Cross(fromPrev, fromEnd);
                cornerDirection = Quaternion.AngleAxis(90.0f, planeNormal) * cornerDirection;
                Quaternion cornerRotation = Quaternion.LookRotation(cornerDirection);
                transform.rotation = Quaternion.Slerp(cornerRotation, transform.rotation, percentTowards);
            }
            else if (next != null && (1.0f - percentAlong) < rotationInterpPercent)
            {
                float percentTowards = (1.0f - percentAlong) / rotationInterpPercent;
                Vector3 fromPrev = (start.position - end.position).normalized;
                Vector3 fromEnd = (next.position - end.position).normalized;
                Vector3 cornerDirection = ((fromPrev + fromEnd) / 2.0f).normalized;
                Vector3 planeNormal = Vector3.Cross(fromPrev, fromEnd);
                cornerDirection = Quaternion.AngleAxis(90.0f, planeNormal) * cornerDirection;
                Quaternion cornerRotation = Quaternion.LookRotation(cornerDirection);
                transform.rotation = Quaternion.Slerp(cornerRotation, transform.rotation, percentTowards);
            }
            transform.position = Vector3.Lerp (start.position, end.position, percentAlong);
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        transform.position = end.position;
        if (next != null)
        {
            Vector3 fromPrev = (start.position - end.position).normalized;
            Vector3 fromEnd = (next.position - end.position).normalized;
            Vector3 cornerDirection = ((fromPrev + fromEnd) / 2.0f).normalized;
            Vector3 planeNormal = Vector3.Cross(fromPrev, fromEnd);
            cornerDirection = Quaternion.AngleAxis(90.0f, planeNormal) * cornerDirection;
            Quaternion cornerRotation = Quaternion.LookRotation(cornerDirection);
            transform.rotation = cornerRotation;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation (end.position - start.position);
        }
    }
}
