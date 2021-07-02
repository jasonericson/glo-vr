/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   WeakForceProjector.cs
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the WeakForceProjector class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeakForceProjector : MonoBehaviour {

    public float coneAngle = 20.0f;
    public float pullStrength = 10.0f;
    public float maxSpeed = 5.0f;
    public float centerOffset = 1.0f;
    public float grabBubbleRadius = 1.0f;
    [RangeAttribute(0.0f, 1.0f)]
    public float grabDamping = 0.9f;
    public float grabStrength = 200.0f;
    public float launchSpeed = 3.0f;
    WeakForced grabbed = null;
    WeakForced lastDragged = null;
    List<WeakForced> forceables = new List<WeakForced>();

    public SixenseButtons PullButton;
    public SixenseButtons PushButton;
    public SixenseButtons DestroyButton;
    private SixenseInput.Controller m_controller;
    public AutoRotate TwirlThis;

    void SetClamper(WeakForced obj, bool state)
    {
        for (int i = 0; i < obj.disableWhenForced.Count; ++i)
        {
            obj.disableWhenForced[i].enabled = state;
        }
    }

	public void Register(WeakForced forceable)
    {
        forceables.Add(forceable);
    }

    public void Unregister(WeakForced forceable)
    {
        if (forceable == grabbed)
        {
            SetClamper(grabbed, true);
            grabbed = null;
        }
        if (forceable == lastDragged)
        {
            SetClamper(lastDragged, true);
            lastDragged = null;
        }
        forceables.Remove(forceable);
    }

    void Update()
    {
        if (m_controller == null)
        {
            m_controller = SixenseInput.GetController(SixenseHands.LEFT);
        }

        Vector3 direction = transform.up.normalized;

        ApplyHold(direction);

        if (Input.GetKey(KeyCode.B) || (m_controller != null && m_controller.GetButton(PullButton)))
        {
            ApplyDragIn(direction);
            
            if (TwirlThis != null)
            {
                TwirlThis.degreesPerSecond = 360.0f;
            }
        }
        else
        {
            if (lastDragged != null)
            {
                SetClamper(lastDragged, true);
                lastDragged = null;
            }
            
            if (TwirlThis != null)
            {
                TwirlThis.degreesPerSecond = 0.0f;
            }
        }

        if (grabbed)
        {
            if (Input.GetKeyDown(KeyCode.V) || (m_controller != null && m_controller.GetButtonUp(PushButton)))
            {
                grabbed.rigidbody.velocity = direction * launchSpeed;
                SetClamper(grabbed, true);
                grabbed = null;
            }
            else if (m_controller != null && m_controller.GetButtonDown(DestroyButton))
            {
                Destroy(grabbed.gameObject);
                grabbed = null;
            }
        }
    }

    void ApplyHold(Vector3 direction)
    {
        if (grabbed == null)
        {
            return;
        }

        Vector3 pullCenter = transform.position + direction * centerOffset;
        Vector3 grabDirection = pullCenter - grabbed.transform.position;
        Vector3 grabVelocity = grabDirection * grabStrength * Time.deltaTime;
        Vector3 newVelocity = grabbed.rigidbody.velocity * grabDamping + grabVelocity;
        grabbed.rigidbody.AddForce(newVelocity - grabbed.rigidbody.velocity, ForceMode.VelocityChange);
    }
    
    void ApplyDragIn(Vector3 direction)
    {
        if (grabbed != null)
        {
            return;
        }

        float dotView = Mathf.Cos(Mathf.Deg2Rad * coneAngle);
        WeakForced closest = null;
        float closestDistance = float.PositiveInfinity;
        Vector3 pullCenter = transform.position + direction * centerOffset;
        for (int i = 0; i < forceables.Count; ++i)
        {
            Vector3 toForceable = forceables[i].transform.position - transform.position;
            float dotProd = Vector3.Dot (toForceable.normalized, direction.normalized);
            if (dotProd >= dotView)
            {
                float distance = toForceable.magnitude;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = forceables[i];
                }
            }
        }

        if (closest != null)
        {
            if (lastDragged != closest)
            {
                if (lastDragged != null)
                {
                    SetClamper(lastDragged, true);
                }
                SetClamper(closest, false);
                lastDragged = closest;
            }

            Vector3 velocity = closest.rigidbody.velocity;
            float additiveSpeed = Time.deltaTime * pullStrength;
            Vector3 moveDir = (pullCenter - closest.transform.position).normalized;
            Vector3 wantToAdd = additiveSpeed * moveDir;
            Vector3 newVelocity = velocity + wantToAdd;
            if (newVelocity.magnitude >= maxSpeed)
            {
                newVelocity = newVelocity.normalized * maxSpeed;
                closest.rigidbody.AddForce(newVelocity - velocity, ForceMode.VelocityChange);
            }
            else
            {
                closest.rigidbody.AddForce(wantToAdd, ForceMode.VelocityChange);
            }

            if ((pullCenter - closest.transform.position).magnitude < grabBubbleRadius)
            {
                grabbed = closest;
                lastDragged = null;
            }
        }
    }
}
