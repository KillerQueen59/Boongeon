using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public float lookRadius = 30f;
    public float attackRadius = 6f;
    public GameObject target;
    public float distance;
    public Animator anim;
    public Shooter gun;
    public Boss boss;
    public float timeToJump;
    public bool attack;
    public float timeToShoot;
    public float hp;
    public bool run;
    public float timeToAttack;
    public float timeToCast;
    public bool melee;
    public bool range;
    public int element;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        melee = true;
        range = false;
    }



    void Update()
    {
        if (hp <= 0)
        {
            anim.SetBool("IsRun", false);
        }


        hp = boss.currentHp;
        attack = false;

        distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= lookRadius)
        {
            timeToShoot += Time.deltaTime;
            {
                if (melee)
                {
                    agent.speed = 2;
                    agent.SetDestination(target.transform.position);
                    run = true;
                    anim.SetBool("IsRun", true);
                    anim.SetBool("IsAttack", false);
                    anim.SetBool("IsRoar", false);



                    if (distance <= agent.stoppingDistance)
                    {
                        attack = true;
                        anim.SetBool("IsRun", false);
                        FaceTarget();
                        if (distance <= attackRadius)
                        {
                            timeToAttack += Time.deltaTime;
                            JumpAttack();
                        }
                        
                    }
                }
                if (range)
                {
                    timeToCast += Time.deltaTime;
                    agent.speed = 0;
                    anim.SetBool("SpellOut", true);
                    anim.SetBool("SpellIn", false);
                    if (target.tag != "Dead" && boss.currentHp > 0)
                    {
                         FaceTargetAttack(timeToShoot);
                    }

                }

            }

        }
        if (distance > lookRadius)
        {
            anim.SetBool("IsRun", false);
            anim.SetBool("SpellOut", false);
            anim.SetBool("SpellIn", false);

        }



    }

    void JumpAttack()
    {
        anim.SetBool("IsAttack", true);
        if (timeToAttack > 2)
        {
            range = true;
            melee = false;
            anim.SetBool("IsAttack", false);
            timeToAttack = 0;
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
        if (timeToCast > 5)
        {
            anim.SetBool("SpellOut", false);
            melee = true;
            range = false;
            timeToCast = 0;
        }


        if ((int)time % 3 == 1){
            element++;
            gun.element = (element % 3) + 1;
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
