using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 30f;
    public GameObject target;
    public float distance;
    public Animator anim;


    NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       
    }

    void Update()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= lookRadius && distance > 5)
        {
            {
                anim.SetBool("IsInArea", true);
                agent.SetDestination(target.transform.position);
            }

        }
        else if (distance > lookRadius)
        {
            anim.SetBool("IsInArea", false); ;
        }
        else
        {
            anim.SetBool("IsInArea", false);
            agent.isStopped = true;
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
