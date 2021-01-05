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
    public float currentHp;
    public Slider healthBar;


    private void Start()
    {
        currentHp = hp;
        SetHealthBarUI();
    }


    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Sword" && currentHp > 0 && hit == 0)
        {
            hit = 1;
            currentHp = currentHp - 5;
           
            gameObject.tag = "Hit";
            animator.SetBool("IsHit", true);
            print("HIT");
            SetHealthBarUI();
            if (currentHp <= 0){
                animator.SetBool("IsDead", true);
            }
        }
        if (collision.gameObject.tag == "JumpSword" && currentHp > 0 && hit == 0)
        {
            hit = 1;
            currentHp = currentHp - 40;

            gameObject.tag = "Hit";
            animator.SetBool("IsHit", true);
            print("HIT");
            SetHealthBarUI();
            if (currentHp <= 0)
            {
                animator.SetBool("IsDead", true);
            }
        }
        if (collision.gameObject.tag == "SpellFire" && currentHp > 0 && hit == 0)
        {
            hit = 1;
    
            if (gameObject.layer == 9)
                currentHp = currentHp - 50;
            else if(gameObject.layer == 10 || gameObject.layer == 11)
                currentHp = currentHp - 10;

            gameObject.tag = "Hit";
            animator.SetBool("IsHit", true);
            print("HIT");
            SetHealthBarUI();
            if (currentHp <= 0)
            {
                animator.SetBool("IsDead", true);
            }
        }
        if (collision.gameObject.tag == "SpellWater" && currentHp > 0 && hit == 0)
        {
            hit = 1;

            if (gameObject.layer == 10)
                currentHp = currentHp - 50;
            else if (gameObject.layer == 9 || gameObject.layer == 11)
                currentHp = currentHp - 10;

            gameObject.tag = "Hit";
            animator.SetBool("IsHit", true);
            print("HIT");
            SetHealthBarUI();
            if (currentHp <= 0)
            {
                animator.SetBool("IsDead", true);
            }
        }
        if (collision.gameObject.tag == "SpellPlant" && currentHp > 0 && hit == 0)
        {
            hit = 1;

            if (gameObject.layer == 11)
                currentHp = currentHp - 50;
            else if (gameObject.layer == 10 || gameObject.layer == 9)
                currentHp = currentHp - 10;

            gameObject.tag = "Hit";
            animator.SetBool("IsHit", true);
            print("HIT");
            SetHealthBarUI();
            if (currentHp <= 0)
            {
                animator.SetBool("IsDead", true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        hit = 0;
        gameObject.tag = "Untagged";
        animator.SetBool("IsHit", false);
    }

    private void SetHealthBarUI()
    {
        healthBar.value = currentHp ;

    }
}
