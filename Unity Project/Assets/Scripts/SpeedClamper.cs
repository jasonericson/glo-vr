/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   SpeedClamper.cs
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the SpeedClamper class.
*/
/******************************************************************************/

using UnityEngine;
using System.Collections;

public class SpeedClamper : MonoBehaviour {

    public float minSpeed = 0.0f;
    public float maxSpeed = 100.0f;
    public float adjustmentSpeed = 1.0f;

    void Update()
    {
        float currSpeed = rigidbody.velocity.magnitude;
        if (currSpeed < minSpeed)
        {
            Vector3 newVelocity = rigidbody.velocity.normalized * Mathf.Min(currSpeed + adjustmentSpeed * Time.deltaTime, minSpeed);
            rigidbody.AddForce(newVelocity - rigidbody.velocity, ForceMode.VelocityChange);
        }
        else if (currSpeed > maxSpeed)
        {
            Vector3 newVelocity = rigidbody.velocity.normalized * Mathf.Max(currSpeed - adjustmentSpeed * Time.deltaTime, maxSpeed);
            rigidbody.AddForce(newVelocity - rigidbody.velocity, ForceMode.VelocityChange);
        }
    }
}
