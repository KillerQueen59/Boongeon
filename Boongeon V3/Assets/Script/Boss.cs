using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Boss : MonoBehaviour
{
    public float hp = 20000;
    public Animator animator;
    private int hit = 0;
    public float damage;
    public bool imHit;
    public float currentHp;
    public Slider healthBar;
    public float timeToDestroy;
    public GameObject asta;
    ThirdPersonMovement mine; 
    
    void Start()
    {
        damage = 100;
        currentHp = hp;
        SetHealthBarUI();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealthBarUI();
        if (currentHp <= 0)
        {
            print("ai");
            SceneManager.LoadScene("GameOver");
            Cursor.lockState = CursorLockMode.None;
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
        if (collision.gameObject.tag == "SpellFire" && currentHp > 0)
        {

            currentHp = currentHp - (100 + damage);


            animator.SetBool("IsHit", true);
            SetHealthBarUI();
            if (currentHp <= 0)
            {
                animator.SetBool("IsDead", true);
            }
        }
        if (collision.gameObject.tag == "SpellWater" && currentHp > 0)
        {

            currentHp = currentHp - (100 + damage);

            animator.SetBool("IsHit", true);
            SetHealthBarUI();
            if (currentHp <= 0)
            {
                animator.SetBool("IsDead", true);
            }
        }
        if (collision.gameObject.tag == "SpellPlant" && currentHp > 0)
        {
            currentHp = currentHp - (100 + damage);

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
