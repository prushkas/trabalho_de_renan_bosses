using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private bool isJump;
    private bool doubleJump;
    public float timeToExitAttack;
    private bool isFire;
    private bool isAttacking;

    public GameObject fireBall;
    public GameObject sword;
    public Transform firePoint;
    public Transform swordPoint;


    private Rigidbody2D rig;
    private Animator anim;

    private float movement;
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        SoulFire();
        Espadada();
    }
    
    void Move()
    {
        float movement = Input.GetAxisRaw("Horizontal");

        rig.velocity = new Vector2(movement * speed,rig.velocity.y);

        if (movement > 0)
        {
            anim.SetBool("isrun", true);
            transform.eulerAngles = new Vector3(0,0,0);
        }

        if (movement < 0)
        {
            anim.SetBool("isrun", true);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (movement == 0)
        {
            anim.SetBool("isrun", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                anim.SetBool("isjump", true);
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                isJump = true;
            }
            else
            {
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0, jumpForce * 2), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    void SoulFire()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isFire == false)
            {
                isFire = true;
                anim.SetBool("isfiring", true);
                GameObject bolaFogo = Instantiate(fireBall, firePoint.position, firePoint.rotation);
                Invoke(nameof(exitAttack), timeToExitAttack);

                if (transform.rotation.y == 0)
                {
                    bolaFogo.GetComponent<BolaDeAlmaDeFogo>().isRight = true;
                }
                if(transform.rotation.x == 180)
                {
                    bolaFogo.GetComponent<BolaDeAlmaDeFogo>().isRight = false;
                }

                yield return new WaitForSeconds(0.3f);
                anim.SetBool("isfiring", false);
            }
        }
    }

    void Espadada()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isAttacking == false)
            {
                isAttacking = true;
                anim.SetBool("isattacking", true);
                Instantiate(sword, swordPoint.position, swordPoint.rotation);
                Invoke(nameof(exitAttack), timeToExitAttack);


                if (movement > 0)
                {
                    sword.transform.eulerAngles = new Vector3(0, 0, 0);
                }

                if (movement < 0)
                {
                    sword.transform.eulerAngles = new Vector3(0, 180, 0);
                }

                isAttacking = false;
                anim.SetBool("isattacking", false);
            }
        }
    }


    void exitAttack()
    {
        isFire = false;
        anim.SetBool("isfiring", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            anim.SetBool("isjump", false);
            isJump = false;
        }
    }
}
