using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{

    [SerializeField] Shooter gun;

    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            gun.Fire();
        }
    }
}
