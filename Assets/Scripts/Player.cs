using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;

    float hDirection;

    bool jDown;

    bool isFalling;

    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        hDirection = Input.GetAxisRaw("Horizontal");
        jDown = Input.GetButtonDown("Jump");
    }

    void Move()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            bool flag = Input.GetAxisRaw("Horizontal") > 0;
            spriteRenderer.flipX = flag;
        }

        rigid.AddForce(Vector2.right * hDirection, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);


        if (hDirection > 0)
        {
            transform.localScale = new Vector2(1, 1);
            anim.SetBool("isRun", true);
        }
        else if (hDirection < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }

        if (jDown)
        {
            anim.SetBool("isJump", true);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        else if (rigid.velocity.y < 0)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFalling", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground" && isFalling == true)
        {
            anim.SetTrigger("doLand");
        }
    }

}
