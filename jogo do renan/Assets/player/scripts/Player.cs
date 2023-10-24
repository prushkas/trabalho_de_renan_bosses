using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private Rigidbody2D rig;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    void Move()
    {
        float movement = Input.GetAxisRaw("Horizontal");

        rig.velocity = new Vector2(movement * speed,rig.velocity.y);

        if (movement > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }

        if (movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Space"))
        {
            rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
