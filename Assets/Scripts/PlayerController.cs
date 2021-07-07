using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public Collider2D coll;
    public Collider2D DisColl;//关闭碰撞体
    public Transform CeilingCheck,GroundCheck;
    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public int cherry = 0;
    public int gem = 0;

    public Text CherryNum;
    public Text GemNum;
    public bool isHurt;//默认是false
    private bool isGround;
    private int extraJump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() 
        //Update每一帧的变化
    {
        if(!isHurt)
        {
            Movement();
        }
        SwitchAnim();
        isGround = Physics2D.OverlapCircle(GroundCheck.position,0.2f,ground);
    }

    private void Update()
    {
        Crouch();
        NewJump();
        Attack();
    }

    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");//获取角色的输入（-1——1之间的小数）
        float facedirection = Input.GetAxisRaw("Horizontal");//判断角色的输入（-1或者1）

        //角色移动
        if(horizontalmove!=0)
        {
            rb.velocity = new Vector2(horizontalmove*speed*Time.fixedDeltaTime,rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(facedirection));
        }
        if(facedirection!=0)
        {
            transform.localScale = new Vector3(facedirection,1,1);//改变朝向
        }

        ////角色跳跃
        //if (input.getbutton("jump") && anim.getbool("idle"))
        //{
        //    rb.velocity = new vector2(rb.velocity.x, jumpforce * time.deltatime);
        //    anim.setbool("jumping", true);
        //}



    }

    void SwitchAnim()
    {
        anim.SetBool("idle",false);

        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y<0)
            {
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }
        else if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetBool("running", false);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling",false);
            anim.SetBool("idle",true);
        }
        
    }

    //收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "CollectionCherry")//规定了只有标签为CollectionCherry时才触发
        {
            //cherry += 1;
            //collision.GetComponent<Animator>().Play("Cherry");
            collision.GetComponent<Animator>().Play("CherryIsGot");
            //CherryNum.text = cherry.ToString();
            //Destroy(collision.gameObject);
        }
        if (collision.tag == "CollectionGem")
        {
            Destroy(collision.gameObject);
            gem += 1;
            GemNum.text = gem.ToString();
        }
        if (collision.tag == "DeadLine")
        {
            Invoke("Restart", 0.2f);
        }
    }

    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {
                enemy.JumpOn();

                //踩完后跳起
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10,rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }
        }
    }



    void Crouch()
    {
        float Crouchmove = Input.GetAxisRaw("Crouch");
        //角色下蹲
        if (!Physics2D.OverlapCircle(CeilingCheck.position, 0.2f, ground))
        {
            if (Crouchmove != 0)
            {
                anim.SetBool("crouch", true);
                DisColl.enabled = false;
            }
            else
            {
                anim.SetBool("crouch", false);
                DisColl.enabled = true;
            }
        }

    }

    void NewJump()
    {
        if (isGround)
        {
            extraJump = 2;
        }
        if(Input.GetButtonDown("Jump") && extraJump > 0)
        {
            rb.velocity = Vector2.up * jumpforce;//Vector2.up = new Vector2(0,1)
            extraJump--;
            anim.SetBool("jumping", true);
        }
        //if(Input.GetButtonDown("Jump") && extraJump == 0 && isGround)
        //{
        //    rb.velocity = Vector2.up * jumpforce;//Vector2.up = new Vector2(0,1)
        //    anim.SetBool("jumping", true);
        //}
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            anim.SetBool("flag", true);
            //角色攻击
            if (anim.GetBool("jumping") || anim.GetBool("falling"))
            {
                anim.SetBool("attack1", true);
            }
            else if(anim.GetBool("running") || anim.GetBool("idle"))
            {
                anim.SetBool("attack2", true);
            }
        }
    }

    void AttackStop()
    {
        anim.SetBool("attack1", false);
        anim.SetBool("attack2", false);
    }

    void Restart()
    {
        
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void CherryCount()
    {
        cherry += 1;
        CherryNum.text = cherry.ToString();
    }
}
