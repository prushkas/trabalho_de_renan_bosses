using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public estadosboss1 estados;
    public float speed;

    private int animacao;
    public int maxhealh;
    public int health;
    public int damage;
    
    private bool atacando;
    private Animator anim;
    private SpriteRenderer sprite;
    private Transform player;
  
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
       anim.SetInteger("transition", animacao);
       yield return new WaitForSeconds(1f);
       anim.SetInteger("transition", 0);
       atacando = false;
   }
   
}

public enum estadosboss1
{
    estado1, estado2
}

