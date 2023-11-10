 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 3;
    public float speed;
    public float jumpForce;
    public bool isJump;
    private bool doubleJump;
    public float timeToExitAttack;
    public float timeToExitAttackEspada;
    private bool isFire;
    private bool isAttacking;

    public bool podeAtirar = false;
    public bool podePuloDuplo = false;
    public bool podeDash = false;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    public GameObject fireBall;
    public GameObject sword;
    public Transform firePoint;
    public Transform swordPoint;
    public Boss1 boss1;
    public AudioSource source;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;

    [SerializeField] private TrailRenderer tr;

    private Rigidbody2D rig;
    public Animator anim;

    private float movement;
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameController.invocar.UpdateLives(health);
    }

    void Update()
    {
        Move();
        Jump();
        if (podeAtirar == true)
        {
            SoulFire();
        }
        Espadada();

        if(podeDash == true)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
            {
                StartCoroutine(Dash());
            }
        }
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
                Soundjump();
            }
            else
            {
                if(podePuloDuplo == true)
                {
                    if (doubleJump)
                    {                       
                        rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                        doubleJump = false;
                        Soundjump();
                    }
                }
            }
        }
    }

    void Soundjump()
    {
        source.volume = 0.3f;
        source.pitch = 1;
        source.PlayOneShot(clip1);
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
                Soundtiro();
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

    void Soundtiro()
    {
        source.volume = 0.2f;
        source.pitch = 1;
        source.PlayOneShot(clip2);
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
                SoundEspada();
                Invoke(nameof(exitAttackEspada), timeToExitAttackEspada);              

                if (movement > 0)
                {
                    sword.transform.eulerAngles = new Vector3(0, 0, 0);
                }

                if (movement < 0)
                {
                    sword.transform.eulerAngles = new Vector3(0, 180, 0);
                }
                
            }
        }
    }

    void SoundEspada()
    {
        source.volume = 0.4f;
        source.pitch = 1;
        source.PlayOneShot(clip3);
    }

    void exitAttack()
    {
        isFire = false;
        anim.SetBool("isfiring", false);
    }
    
    void exitAttackEspada()
    {
        isAttacking = false;
        anim.SetBool("isattacking", false);
    }

    private IEnumerator Dash()
    {
        Sounddash();
        canDash = false;
        isDashing = true;
        float originalGravity = rig.gravityScale;
        rig.gravityScale = 0f;
        rig.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rig.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void Sounddash()
    {
        source.volume = 1.5f;
        source.pitch = 1;
        source.PlayOneShot(clip4);
    }

    public void Damage(int dmg)
    {        
        health -= dmg;
        GameController.invocar.UpdateLives(health);
        Sounddamage();

        if (health <= 0)
        {
            StartCoroutine(Morte());
        }
    }

    void Sounddamage()
    {
        source.volume = 1f;
        source.pitch = 1;
        source.PlayOneShot(clip5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "acertaroboss")
        {
            anim.SetBool("ishit", true);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "acertaroboss")
        {
            anim.SetBool("ishit", false);           
        }
    }

    private IEnumerator Morte()
    {
        speed = 0;
        anim.SetBool("isdead", true);
        Sounddeath();
        yield return new WaitForSeconds(0.6f);
        GameController.invocar.RestartGame();
    }

    void Sounddeath()
    {
        source.volume = 0.5f;
        source.pitch = 1;
        source.PlayOneShot(clip6);
    }
}
