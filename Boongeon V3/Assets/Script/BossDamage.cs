using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public float damage = 100f;
    public GameObject enemy;
    public bool attack;
    EnemyController controller;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

      


        controller = enemy.GetComponent<EnemyController>();

        if (controller.enemy.currentHp <= 0)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {

        other.gameObject.GetComponent<Stats>().TakeDamage(damage);

    }
}
