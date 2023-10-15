using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
   
    public float floatSpeed = 2.0f; // Velocidade de flutuação
    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        FloatBoss();
    }

    void FloatBoss()
    {
        
        float newY = startY + Mathf.Sin(Time.time * floatSpeed);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}


