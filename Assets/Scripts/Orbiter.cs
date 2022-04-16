using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{      
    Vector3 velocity;
    private bool isMoving = false;
    [SerializeField]
    private Transform userCamera;
    
    private void Update()
    {
        if (userCamera == null) return;
        
        FollowCamera();
    }
    private void FollowCamera()
    {
        transform.position = userCamera.position;
        
        float edgeThreshold = isMoving ? 1 : 45;
	        
        var containerAngle = transform.eulerAngles;
        var rotationDelta = Math.Abs(containerAngle.y - userCamera.eulerAngles.y);
        
        if (rotationDelta > edgeThreshold)
        {
            isMoving = true;
            
            transform.rotation = SmoothDampQuaternion(transform.rotation,
                Quaternion.Euler(new Vector3(containerAngle.x, userCamera.eulerAngles.y, containerAngle.z)),
                ref velocity,
                0.9f);
        }
        else
        {
            isMoving = false;
        }
    }
    private static Quaternion SmoothDampQuaternion(Quaternion current, Quaternion target, ref Vector3 currentVelocity, float smoothTime)
    {
        Vector3 c = current.eulerAngles;
        Vector3 t = target.eulerAngles;
        return Quaternion.Euler(
            Mathf.SmoothDampAngle(c.x, t.x, ref currentVelocity.x, smoothTime),
            Mathf.SmoothDampAngle(c.y, t.y, ref currentVelocity.y, smoothTime),
            Mathf.SmoothDampAngle(c.z, t.z, ref currentVelocity.z, smoothTime)
        );
    }
}
