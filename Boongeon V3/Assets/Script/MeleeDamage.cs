using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    float speed = 200f;
    public float damage = 200f;
    public GameObject enemy;
    public bool attack;
    EnemyController controller;
    public GameObject asta;
    ThirdPersonMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        movement = asta.GetComponentInChildren<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (movement.plantBook)
            damage = 50;
        if (movement.fireBook)
            damage = 100;


        controller = enemy.GetComponent<EnemyController>();
        transform.Rotate(Vector3.up * Time.deltaTime * speed);

        if(controller.enemy.currentHp <= 0)
        {
            Destroy(gameObject);
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
       
       other.gameObject.GetComponent<Stats>().TakeDamage(damage);
       
    }
}
