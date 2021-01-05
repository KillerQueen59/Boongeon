using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUi : MonoBehaviour
{
    public Camera cam;

    private void FixedUpdate()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer() {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }

}
