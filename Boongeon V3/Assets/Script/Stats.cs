using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    

    public Animator animator;
    private int hit = 0;
    public float currentHp;
    public float currentMana;
    public float damage;
    public Slider healthBar;
    public Text healtValue;
    public Text manaValue;
    public Slider manaBar;
    public ThirdPersonMovement movement;
    public GameObject enemy;
    public Vector3 pos;

    private void Awake()
    {
        damage = 10f;
        currentHp = movement.hp;
        healthBar.maxValue = movement.hp;
        manaBar.maxValue = movement.mana;
        currentMana = movement.mana;
        SetHealthBarUI();
        SetManaBarUI();

    }

    private void Update()
    {
        currentMana = movement.mana;
        SetManaBarUI();
        if (currentHp <= 0)
        {
            animator.SetBool("IsDead", true);
            gameObject.tag = "Dead";
        }

        if(currentHp < 1000 && currentHp > 0)
            currentHp += Time.deltaTime * 30;

        SetHealthBarUI();
        pos = gameObject.transform.position;

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SpellPlayer" && currentHp > 0)
        {

            hit = 1;
            if (gameObject.layer == 12)
                currentHp = currentHp - (50 + damage);
            SetHealthBarUI();
            if (currentHp <= 0)
            {
                animator.SetBool("IsDead", true);
                gameObject.tag = "Dead";
            }
        }
        if (other.gameObject.tag == "PlantBook")
        {
            currentHp = movement.hp;
            healthBar.maxValue = movement.hp;
            damage = 50f;
        }if (other.gameObject.tag == "FireBook")
        {
            currentHp = movement.hp;
            healthBar.maxValue = movement.hp;
            damage = 200f;
        }

        if (other.gameObject.tag == "WaterBook")
        {
            currentHp = movement.hp;
            healthBar.maxValue = movement.hp;
            damage = 500f;
        }

        if (currentHp <= 0)
        {
            SceneManager.LoadScene("GameOver");
            Cursor.lockState = CursorLockMode.None;

        }
      
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        SetHealthBarUI();
    }
    private void SetHealthBarUI()
    {
        healthBar.value = currentHp;
    }

    private void SetManaBarUI()
    {
        manaBar.value = currentMana;
    }


}
