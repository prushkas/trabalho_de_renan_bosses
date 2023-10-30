using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEric : MonoBehaviour
{
    public float Speed;
    public float Distance;
    private Animator Anim;

    bool isRight = true;
    
    public Transform groundCheck;

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
        RaycastHit2D Ground = Physics2D.Raycast(groundCheck.position, Vector2.down, Distance);

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
    
}
