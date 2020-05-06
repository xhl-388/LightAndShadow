using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCC : MonoBehaviour            //光的角色控制器
{
    private float jumpForce = 600f;
    public Transform groundCheck;
    const float groundCheckRadius = 0.2f;
    [HideInInspector]
    public bool isGrounded;
    private bool facingRight =false;
    private Rigidbody2D rigidBody2D;
    private LayerMask ground;
    private LayerMask cubeLayer;
    [HideInInspector]
    public bool canJumpAgain = false;           //满足二段跳条件
    private float jumpAgainForce=450f;           //二段跳力量
    private bool ableToBeJumpAgain;
    [HideInInspector]
    public WhiteSideCube whiteSideCube;
    //[HideInInspector]
    public bool cantShoot = false;
    private void Awake()
    {
        cubeLayer = 1<<LayerMask.NameToLayer("Cube");
        rigidBody2D = GetComponent<Rigidbody2D>();
        ground = 1<<LayerMask.NameToLayer("Ground");
    }
    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius,ground|cubeLayer);
        if (colliders.Length!= 0&&Mathf.Abs(rigidBody2D.velocity.y) < 0.1f)
        {
            int flag = 0;
            for(int i = 0; i < colliders.Length; i++)
            {
                if (!colliders[i].isTrigger)
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 1)
            {
                isGrounded = true;
                canJumpAgain = false;
                ableToBeJumpAgain = true;
            }
            else isGrounded = false;
        }
        else
        {
            isGrounded = false;
        }
        if (isGrounded)
        {
            Collider2D collider2 = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 1f), 0.1f, cubeLayer);
            if (collider2)
            {
                if (collider2.GetComponent<WhiteSideCube>())
                {
                    if (collider2.GetComponent<WhiteSideCube>().IsWhite())
                    {
                        whiteSideCube = collider2.GetComponent<WhiteSideCube>();
                    }
                    else whiteSideCube = null;
                }
                else whiteSideCube = null;
            }
            else whiteSideCube = null;
        }
        else whiteSideCube = null;
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }
    public void Move(float move, bool jump)
    {
        rigidBody2D.velocity = new Vector2(move, rigidBody2D.velocity.y);
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
        if (isGrounded && jump)
        {
            isGrounded = false;
            rigidBody2D.AddForce(new Vector2(0f, jumpForce));
        }
        else if (ableToBeJumpAgain&&canJumpAgain&&jump)      //二段跳处理
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0);
            rigidBody2D.AddForce(new Vector2(0f, jumpAgainForce));
            canJumpAgain = false;
            ableToBeJumpAgain = false;
        }
    }
}
