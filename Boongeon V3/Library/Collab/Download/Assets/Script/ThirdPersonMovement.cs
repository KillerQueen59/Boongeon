using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public GameObject sword;

    [SerializeField] Shooter gun;
    List<string> animlist = new List<string>(new string[] { "IsAttack1", "IsAttack2", "IsAttack3" });
    public int combonum;
    public float reset;
    public float resetTime;
    public float mana = 100f;
    public float hp = 100f;
    public float manaTime;
    public bool spell;
    private bool jumpAttack;
    public float time = 0f;
    public bool idle;
    RaycastWeapon weapon;
    SpawnProjectile projectile;

    public GameObject slashVFX;
    public Transform slashPoint;

    public float speed = 6f;
    public float gravity = -49.18f;
    public float jumpHeight = 1f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;


    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    void Update()
    {

        if (idle)
            transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
        if (!spell)
            idle = false;
        if (Input.GetButtonDown("Fire1") && spell)
        {
            if (spell && mana > 0)
            {
                //projectile.Fire();
                gun.transform.rotation = Quaternion.Euler(cam.eulerAngles.x, cam.eulerAngles.y, 0f); ;
                //weapon.StartFiring();
                mana -= Time.deltaTime * 1000;
                gun.Fire();
            }

        }
        if (Input.GetButtonUp("Fire1") && spell)
        {
            if (spell && mana > 0)
            {
                //gun.transform.rotation = Quaternion.Euler(cam.eulerAngles.x, cam.eulerAngles.y, 0f); ;
                //weapon.StopFiring();
                //gun.Fire();
            }

        }
        if (!spell && mana <= 100)
        {
            mana += Time.deltaTime * 30;
            animator.SetBool("IsSpellMoveLeft", false);
            animator.SetBool("IsSpellMoveRight", false);
        }

        if (jumpAttack)
        {
            time += Time.deltaTime;
        }
        if (time > 2.5)
        {
            sword.tag = "Untagged";
            animator.SetBool("IsJumpAttacking", false);
            time = 0;
            animator.SetBool("IsIdle", true);
            jumpAttack = false;

        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
            animator.SetBool("IsFalling", false);
        }
        if (transform.position.y > 2.5 && !isGrounded)
        {
            animator.SetBool("IsFalling", true);
        }



        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        Attacking();
        Movement();

        if (animator.GetBool("IsIdle"))
        {
            sword.tag = "Untagged";

        }


        if (direction.magnitude >= 0.1f)
        {

            {

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                if (!animator.GetBool("IsJumpAttacking"))
                {
                    if (isGrounded)
                    {
                        transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    }
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);
                }




            }
        }

    }
    public void Attacking()
    {
        if (spell && Input.GetKeyDown("d"))
        {
            animator.SetBool("IsSpellMoveRight", true);
        }
        if (spell && Input.GetKeyDown("a"))
        {
            animator.SetBool("IsSpellMoveLeft", true);
        }
        if (spell && Input.GetKeyUp("d"))
        {
            animator.SetBool("IsSpellMoveRight", false);
        }
        if (spell && Input.GetKeyUp("a"))
        {
            animator.SetBool("IsSpellMoveLeft", false);
        }

        if (Input.GetKeyDown("q") && mana > 99)
        {
            animator.SetBool("IsSpell", true);
            spell = true;
        }
        if (Input.GetKeyUp("q"))
        {
            spell = false;
            idle = false;
            animator.SetBool("IsSpell", false);
            animator.SetBool("IsIdle", true);

        }
        if (Input.GetButtonDown("Fire1") && combonum < 3 && !spell)
        {
            animator.SetBool("IsIdle", false);
            sword.tag = "Sword";
            animator.SetTrigger(animlist[combonum]);
            speed = 0f;
            combonum++;
            reset = 0f;

            
        }
        if (combonum > 0)
        {
            reset += Time.deltaTime;
            if (reset > resetTime)
            {
                animator.SetTrigger("Reset");
                sword.tag = "Untagged";
                combonum = 0;
            }
        }
        if (combonum == 3)
        {
            resetTime = 3f;
            combonum = 0;

            TriggerSlash(); //INI SLASH
        }
        else
        {
            resetTime = 1f;
        }

        if (Input.GetKeyDown("1"))
            gun.element = 1;
        if (Input.GetKeyDown("2"))
            gun.element = 2;
        if (Input.GetKeyDown("3"))
            gun.element = 3;


        if (Input.GetButtonDown("Fire2"))
        {
            sword.tag = "JumpSword";
            jumpAttack = true;
            animator.SetBool("IsJumpAttacking", true);
            animator.SetBool("IsIdle", false);
            speed = 0f;
        }

    }


    void TriggerSlash() //INI SLASH
    {
        InstantiateSlash(slashPoint);
    }

    void InstantiateSlash(Transform slashPoint) //INI SLASH
    {
        var slashObj = Instantiate(slashVFX, slashPoint.position, Quaternion.Euler(90, 0, 0)) as GameObject;
    }



    public void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey("a") || Input.GetKey("w") || Input.GetKey("d") || Input.GetKey("s")))
        {
            idle = false;
            speed = 12f;
            animator.SetBool("IsRunning", true);
            if (Input.GetKey("space"))
            {
                animator.SetBool("IsJumpWhileRun", true);
            }
        }
        if (!Input.GetKey(KeyCode.LeftShift) || (!Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("d")))
        {
            idle = true;
            speed = 6f;
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsWalking", false);
        }

        if ((Input.GetKey("a") && !spell) || Input.GetKey("w") || (Input.GetKey("d") && !spell) || Input.GetKey("s"))
        {
            idle = false;
            animator.SetBool("IsWalking", true);
            if (Input.GetKey("space"))
            {
                animator.SetBool("IsJumpWhileRun", true);
            }
        }
        if (!Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("d"))
        {
            idle = true;
            animator.SetBool("IsWalking", false);
        }

        if (Input.GetKeyDown("space") && isGrounded)
        {
            idle = false;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetKeyUp("space"))
        {
            animator.SetBool("IsJumpWhileRun", false);
            animator.SetBool("IsJumping", false);
        }
    }
}
