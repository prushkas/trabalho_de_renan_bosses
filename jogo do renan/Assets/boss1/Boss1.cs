using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour
{
    public estadosboss1 estados;
    public float speed;
    public estadosboss1 atual;

    private int animacao;
    public int maxhealh;
    public int VidaAtualBoss;
    public int health;
    public int damage;
    
    private bool atacando;
    private Animator anim;
    private SpriteRenderer sprite;
    private Transform player;
    public Player playerScript;
    public AudioSource hit;

    public Image BarraDeVida;

    public TextMeshProUGUI contadordevida;
   // Start is called before the first frame update
   void Awake()
   {
       health = maxhealh;
       anim = GetComponent<Animator>();
       sprite = GetComponent<SpriteRenderer>();
       player = GameObject.FindGameObjectWithTag("Player").transform;
   }

   // Update is called once per frame
   void Update()
   {
       if (health <= 0)
       {
           contadordevida.text = "morreu";
       }
       contadordevida.text = "Vida Boss: " + health;
       if (estados == estadosboss1.estado1)
       {
           animacao = 1;
           
       }
       else
       {
           animacao = 2;
           speed = 7;
       }

       if (atacando == false)
       {
           Vector3 target = new Vector3(player.position.x, transform.position.y, 0);
           transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); 
           sprite.flipX = target.x >= transform.position.x;
       }
   }

   public void Damage(int dmg)
   {
       health -= dmg;
       anim.SetInteger("transition", 3);
       if (health <= maxhealh * 0.5f)
       {
           estados = estadosboss1.estado2;
       }
       if (health <= 0)
       {
           anim.SetInteger("transition", 4);
           Destroy(gameObject);
           SceneManager.LoadScene(2);

       }

       BarraDeVida.fillAmount = health / maxhealh;
   }

   

   private void OnTriggerEnter2D(Collider2D col)
   {
       if (col.transform.CompareTag("HitDoPlayer"))
       {
           Damage(1);
       }
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Player"))
       {
           playerScript.Damage(1);
       }
   }

   private void OnCollisionStay2D(Collision2D col)
   {
       if (atacando == false)
       {
           if (col.transform.CompareTag("Player"))
           {
               StartCoroutine(ataque());
           }
       }
   }

   IEnumerator ataque()
   {
       atacando = true;
       hit.Play();
       anim.SetInteger("transition", animacao);
       yield return new WaitForSeconds(1f);
       anim.SetInteger("transition", 0);
       atacando = false;
   }
   
   public void HitBoss(int danoparareceber)
   {
       health -= danoparareceber;

       if (health <= maxhealh / 2)
       {
           atual = estadosboss1.estado2;
       }

       BarraDeVida.fillAmount = VidaAtualBoss / maxhealh;

       if (health <= 0)
       {
           Destroy(gameObject);
       }
   }

   
}


public enum estadosboss1
{
    estado1, estado2
}

