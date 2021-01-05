using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSShooter_v2 : MonoBehaviour
{
public Camera cam;
    public GameObject projectile;
    public Transform  firepoint;
    public float projectileSpeed = 30;
    public float fireRate = 4;
    public float arcRange = 1;

    private Vector3 destination;
    private bool leftHand;
    private float timeToFire;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1/fireRate;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        InstantiateProjectile(firepoint);
    }

    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;

        firePoint.rotation = Quaternion.LookRotation(destination);
    }
}
