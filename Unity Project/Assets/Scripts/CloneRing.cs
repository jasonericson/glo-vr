/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   CloneRing.cs
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the CloneRing class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class CloneRing : MonoBehaviour {

    [RangeAttribute(1, 16)]
    public int ringEdges = 4;
    public float offsetScaleFactor = 1.0f;
    public Vector3 firstOffsetFromCenter = new Vector3(0, 1, 0);
    public Vector3 rotationAxis = new Vector3(0, 0, 1);
    public float startRotation = 0.0f;
    public GameObject childToCopy = null;

    // TODO: I sense a generic class.
    private float previousOffsetScaleFactor = 0.0f;
    private Vector3 previousFirstOffsetFromCenter = Vector3.zero;
    private Vector3 previousRotationAxis = Vector3.zero;
    private float previousStartRotation = 0.0f;

    private Transform[] ringChildren = null;
    private GameObject childToCopyRef = null;


    void ResetEdges()
    {
        Transform[] newChildrenList = new Transform[ringEdges];
        if (ringEdges < ringChildren.Length)
        {
            for (int i = 0; i < ringEdges; ++i)
            {
                newChildrenList[i] = ringChildren[i];
            }
            for (int i = ringEdges; i < ringChildren.Length; ++i)
            {
                Destroy (ringChildren[i].gameObject);
            }
        }
        else
        {
            for (int i = 0; i < ringChildren.Length; ++i)
            {
                newChildrenList[i] = ringChildren[i];
            }
            for (int i = ringChildren.Length; i < newChildrenList.Length; ++i)
            {
                newChildrenList[i] = (Transform)Instantiate(childToCopyRef.transform);
                newChildrenList[i].parent = transform;
                newChildrenList[i].localScale = childToCopyRef.transform.localScale;
            }
        }
        ringChildren = newChildrenList;
    }

    void ResetPositions()
    {
        previousOffsetScaleFactor = offsetScaleFactor;
        previousFirstOffsetFromCenter = firstOffsetFromCenter;
        previousRotationAxis = rotationAxis;
        previousStartRotation = startRotation;

        float anglePerChild = 360.0f / ringChildren.Length;
        Vector3 initialOffset = offsetScaleFactor * firstOffsetFromCenter;

        for (int i = 0; i < ringChildren.Length; ++i)
        {
            ringChildren[i].localPosition = initialOffset;
            ringChildren[i].localRotation = Quaternion.identity;
            ringChildren[i].localScale = childToCopyRef.transform.localScale;
            ringChildren[i].RotateAround(transform.position, transform.rotation * rotationAxis, anglePerChild * i + startRotation);
        }
    }

	void Start () {
        childToCopyRef = childToCopy;
        if (childToCopyRef == null)
        {
            Debug.LogError("Child To Copy must be assigned!");
            return;
        }
        if (childToCopyRef.transform.parent != transform)
        {
            Debug.LogError("Child To Copy must be a child of this object!");
            return;
        }

        ringChildren = new Transform[ringEdges];
        ringChildren[0] = childToCopyRef.transform;
        ringChildren[0].parent = transform;
        for (int i = 1; i < ringChildren.Length; ++i)
        {
            ringChildren[i] = (Transform)Instantiate(childToCopyRef.transform);
            ringChildren[i].parent = transform;
        }
        ResetPositions ();
	}

    void Update()
    {
        if (childToCopyRef == null)
        {
            return;
        }

        bool needPositionUpdate =
            (previousOffsetScaleFactor != offsetScaleFactor) ||
            (previousFirstOffsetFromCenter != firstOffsetFromCenter) ||
            (previousRotationAxis != rotationAxis) ||
            (previousStartRotation != startRotation);

        if (ringEdges != ringChildren.Length)
        {
            ResetEdges();
            needPositionUpdate = true;
        }

        if (needPositionUpdate)
        {
            ResetPositions();
        }
    }
}
