using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorundCheck : MonoBehaviour
{
    public Player guerreiro;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            guerreiro.anim.SetBool("isjump", false);
            guerreiro.isJump = false;
        }
    }
}
