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

    public void Damage(int DMG)
    {
        Health -= DMG;
        if (Health == 0)
        {
            //boss derrotado
        }
    }
}
