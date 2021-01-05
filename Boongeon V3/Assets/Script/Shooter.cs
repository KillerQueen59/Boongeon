using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    
    public GameObject fireProjectile;
    public GameObject waterProjectile;
    public GameObject plantProjectile;
    private GameObject projectile;
    public Transform muzzle;
    public int element;

    float timeToFire;
    public bool canFire;
    public bool isFire;
    public bool isWater;
    public bool isPlant;
    public bool enemy;


    private void Start()
    {
        projectile.transform.position = muzzle.transform.position;
    }

    public virtual void Fire()
    {
        if ( Time.time >= timeToFire)
        {
            if (!enemy)
                timeToFire = Time.time + 1 / 2;
            else
                timeToFire = Time.time + 1;
            Instantiate(projectile, muzzle.position, muzzle.rotation);
        }

    }
    private void Update()
    {
        if (element == 1)
        {
            projectile = fireProjectile;
            isFire = true;
            isWater = false;
            isPlant = false;

        }
        else if (element == 2)
        {
            projectile = waterProjectile;
            isFire = false;
            isWater = true;
            isPlant = false;
        }
        else if (element == 3)
        {
            projectile = plantProjectile;
            isFire = false;
            isWater = false;
            isPlant = true;
        }
        muzzle = transform.Find("Muzzle");

    }

}
