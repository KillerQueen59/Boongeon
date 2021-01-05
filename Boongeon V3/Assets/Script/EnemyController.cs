using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 30f;
    public float attackRadius = 6f;
    public GameObject target;
    public float distance;
    public Animator anim;
    public Shooter gun;
    public Enemy enemy;
    public float timeToJump;
    public bool attack;
    public float timeToShoot;
    public float hp;

 
    NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }



    void Update()
    {
        if(hp <= 0)
        {
            anim.SetBool("IsInArea", false);
        }

       
        hp = enemy.currentHp;
        attack = false;

        distance = Vector3.Distance(target.transform.position, transform.position);
       
        if (distance <= lookRadius)
        {
            timeToShoot += Time.deltaTime;
            {
                if(gameObject.tag == "Melee")
                {
                    
                    agent.SetDestination(target.transform.position);
                    anim.SetBool("IsInArea", true);
                    anim.SetBool("JumpAttack", false);



                    if (distance <= agent.stoppingDistance)
                    {
                        anim.SetBool("IsInArea", false);
                        FaceTarget();
                        if (distance <= attackRadius)
                        {
                            JumpAttack(timeToShoot);
                        }
                        else if (distance > attackRadius)
                        {
                            anim.SetBool("IsInArea", true);
                            anim.SetBool("JumpAttack", false);
                        }
                    }
                }
                if(gameObject.tag == "Range")
                {

                    agent.speed = 0;
                    if (target.tag != "Dead" && enemy.currentHp > 0)
                    {
                        FaceTargetAttack(timeToShoot);
                    }
                        
                }
                
            }

        }
        else if (distance > lookRadius)
        {
            anim.SetBool("IsInArea", false); ;
        }
       


    }

    void JumpAttack(float time)
    {
        anim.SetBool("JumpAttack", true);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack"))
        {
            anim.SetBool("JumpAttack", false);
            attack = true;
           
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void FaceTargetAttack(float time)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        if ((int)time % 3 == 1)
        {
            gun.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            gun.Fire();
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius); 
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
