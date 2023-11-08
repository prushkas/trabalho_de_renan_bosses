using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEric : MonoBehaviour
{
    
    public int Health;
    public float Speed;
    public float Distance;
    public float DistanciaAtaque;
    public bool Atacando;
    public int Damage = 4;
    public int SubtrairVida;
    public bool Fase2 = false; 
    
    private Animator Anim;
    private float DistanciadoJogador;
    private Transform Jogador;

    bool isRight = true;
    
    public Transform groundCheck;

    private void Start()
    {
        Jogador = GameObject.FindWithTag("Player").transform;
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!Atacando)
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
            Anim.SetInteger("transition",1); 
        }
        else
        {
            Anim.SetInteger("transition",2); 
        }
        

        RaycastHit2D Ground = Physics2D.Raycast(groundCheck.position, Vector2.down, Distance);

            DistanciadoJogador = Vector2.Distance(transform.position, Jogador.position);
            //a parte do ataque do boss
            Atacando = DistanciadoJogador <= DistanciaAtaque;

            if (Ground.collider == false)
            {
                if (isRight == true)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    isRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    isRight = true;
                }
            }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("HitDoPLayer"))
        {
            col.gameObject.GetComponent<Player>().Damage(Damage);
            SubtrairVida = Health - 1;
            Anim.SetInteger("transition", 3);

            if (!Fase2 && Health <= 10)
            {
                Fase2 = true;
                Comportamento2();
            }

        }
    }


    public void Comportamento2()
    {
        Speed *= 2;
        Damage *= 2;
        Anim.GetComponent<SpriteRenderer>().color = Color.red;
        
    } 
    
    
    
    
    void Morreu()
    {
       Destroy(gameObject);
    }
    public void damage(int Valor)
    {
        Health -= Valor;
        if ( Health <= 0)
        {
            Health = 0;
            Morreu();
        }
    }
}
