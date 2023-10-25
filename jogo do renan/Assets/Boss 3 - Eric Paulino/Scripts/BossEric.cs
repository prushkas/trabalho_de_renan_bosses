using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEric : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rig;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
    
    
    }
}
