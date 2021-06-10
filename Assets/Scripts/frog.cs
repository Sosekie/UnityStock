using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : Enemy
{
    private Rigidbody2D rb;
    //private Animator Anim;
    private Collider2D Coll;
    public LayerMask Ground;
    public Transform leftpoint, rightpoint;
    public float leftx, rightx;
    public float Speed, JumpForce;
    private bool faceleft = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>(); 
        //Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    {
        if (faceleft)
        {
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
            
            if (transform.position.x < leftx && Coll.IsTouchingLayers(Ground))
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceleft = false;
            }
        }
        else
        {
            if(Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, JumpForce);
            }
            if (transform.position.x > rightx && Coll.IsTouchingLayers(Ground))
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceleft = true;
            }
        }
    }
    
    void SwitchAnim()
    {
        if (Anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0.1)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        if (Coll.IsTouchingLayers(Ground) && Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);
        }
    }

}
