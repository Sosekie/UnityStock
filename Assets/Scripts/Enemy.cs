using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Anim;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
    }

    void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        Anim.SetTrigger("death");
    }
}
