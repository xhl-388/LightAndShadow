using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCC : MonoBehaviour            //光的角色控制器
{
    private float jumpForce = 300f;
    public Transform groundCheck;
    const float groundCheckRadius = 0.1f;
    public bool isGrounded;
    private bool facingRight = true;
    const float groundCheckBreak = 0.1f;
    private float nextGroundCheckTime;
    private Rigidbody2D rigidBody2D;
    private LayerMask ground;
    [HideInInspector]
    public bool canJumpAgain = false;       //满足二段跳条件
    private float jumpAgainForce;           //二段跳力量
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        ground = LayerMask.NameToLayer("Ground");
        jumpAgainForce = jumpForce / 2.0f;  //二段跳力量为一段跳一半
    }
    private void FixedUpdate()
    {
        isGrounded = false;
        if (Time.time > nextGroundCheckTime)
        {
            Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
            if (collider)
            {
                isGrounded = true;
                canJumpAgain = false;
            }
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
            nextGroundCheckTime = Time.time + groundCheckBreak;
        }
        else if (canJumpAgain&&jump)      //二段跳处理
        {
            rigidBody2D.AddForce(new Vector2(0f, jumpAgainForce));
            nextGroundCheckTime = Time.time + groundCheckBreak;
        }
    }
}
