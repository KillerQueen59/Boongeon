using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{

    public bool isFiring = false;
    public Transform raycastOrigin;

    Ray ray;
    RaycastHit hitInfo;

    public void StartFiring()
    {
        isFiring = true;
        ray.origin = raycastOrigin.position;
        ray.direction = raycastOrigin.forward;
        if(Physics.Raycast(ray,out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 10.0f);
        }
        
    }
    public void StopFiring()
    {
        isFiring = false;
    }
}
