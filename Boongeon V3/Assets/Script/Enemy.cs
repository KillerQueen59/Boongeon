using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float hp = 100;
    public Animator animator;
    private int hit = 0;
    public float damage;
    public bool imHit;
    public float currentHp;
    public Slider healthBar;
    public float timeToDestroy;
    public GameObject asta;
    ThirdPersonMovement mine;
    


    private void Start()
    {
        mine = asta.GetComponentInChildren<ThirdPersonMovement>();
        currentHp = hp;
        SetHealthBarUI();
    }

    private void Update()
    {
        if (mine.plantBook)
        {
            damage = 40;
        }if (mine.fireBook)
        {
            damage = 100;
        }if (mine.waterBook)
        {
            damage = 200;
        }
        SetHealthBarUI();
        if(currentHp <= 0)
        {
            gameObject.tag = "Dead";
            timeToDestroy += Time.deltaTime;
           
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sword" && currentHp > 0)
        {
            currentHp = currentHp - (5 + damage);
            imHit = true;
            animator.SetBool("IsHit", true);
            SetHealthBarUI();
            if (currentHp <= 0)
            {
                animator.SetBool("IsDead", true);
            }
        }
        if (collision.gameObject.tag == "JumpSword" && currentHp > 0)
        {
            currentHp = currentHp - (40 + damage);
            imHit = true;
            animator.SetBool("IsHit", true);
            SetHealthBarUI();
            if (currentHp <= 0)
            {
                animator.SetBool("IsDead", true);
            }
        }
        if (collision.gameObject.tag == "SpellFire" && currentHp > 0 )
        {

            if (gameObject.layer == 9)
                currentHp = currentHp - (50 + damage);
            else if (gameObject.layer == 10 || gameObject.layer == 11)
                currentHp = currentHp - (10 + damage);

            animator.SetBool("IsHit", true);
            SetHealthBarUI();
            if (currentHp <= 0)
            {
                animator.SetBool("IsDead", true);
            }
        }
        if (collision.gameObject.tag == "SpellWater" && currentHp > 0)
        {

            if (gameObject.layer == 10)
                currentHp = currentHp - (50+ damage);
            else if (gameObject.layer == 9 || gameObject.layer == 11)
                currentHp = currentHp - (10 + damage);

            animator.SetBool("IsHit", true);
            SetHealthBarUI();
            if (currentHp <= 0)
            {
                animator.SetBool("IsDead", true);
            }
        }
        if (collision.gameObject.tag == "SpellPlant" && currentHp > 0)
        {

            if (gameObject.layer == 11)
                currentHp = currentHp - (50 + damage);
            else if (gameObject.layer == 10 || gameObject.layer == 9)
                currentHp = currentHp - (10 + damage);

            animator.SetBool("IsHit", true);
            SetHealthBarUI();
            if (currentHp <= 0)
            {
                animator.SetBool("IsDead", true);
            }
        }
    }



    private void SetHealthBarUI()
    {
        healthBar.value = currentHp;

    }
}
