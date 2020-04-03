using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCC : MonoBehaviour            //光的角色控制器
{
    private float jumpForce = 500f;
    public Transform groundCheck;
    const float groundCheckRadius = 0.1f;
    [HideInInspector]
    public bool isGrounded;
    private bool facingRight = true;
    const float groundCheckBreak = 0.1f;
    private Rigidbody2D rigidBody2D;
    private LayerMask ground;
    private LayerMask cubeLayer;
    [HideInInspector]
    public bool canJumpAgain = false;       //满足二段跳条件
    private float jumpAgainForce=300f;           //二段跳力量
    private bool ableToBeJumpAgain;
    [HideInInspector]
    public WhiteSideCube whiteSideCube;
    private void Awake()
    {
        cubeLayer = 1<<LayerMask.NameToLayer("Cube");
        rigidBody2D = GetComponent<Rigidbody2D>();
        ground = 1<<LayerMask.NameToLayer("Ground");
    }
    private void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,ground|cubeLayer);
        if (collider)
        {
             if (!collider.isTrigger&&Mathf.Abs(rigidBody2D.velocity.y)<0.1f)
             {
                isGrounded = true;
                canJumpAgain = false;
                ableToBeJumpAgain = true;
             }
            if (collider.GetComponent<WhiteSideCube>())
            {if(collider.GetComponent<WhiteSideCube>().IsWhite())
                whiteSideCube = collider.GetComponent<WhiteSideCube>();
            }
        }
        else
        {
            whiteSideCube = null;
            isGrounded = false;
        }
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
