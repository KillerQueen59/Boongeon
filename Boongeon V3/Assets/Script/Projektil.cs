using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projektil : MonoBehaviour
{

    public float speed;
    public float timeToLive;
    public float damage;
    public Transform muzzle;

    private void Update()
    {
        timeToLive += Time.deltaTime;

        

        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(timeToLive > 9)
        {
            Destroy(gameObject);
        }
    }

}
