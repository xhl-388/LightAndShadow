﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCC : MonoBehaviour            //影的角色控制器
{
    private float jumpForce=600f;           
    public Transform groundCheck;           
    const float groundCheckRadius = 0.2f;  
    [HideInInspector]
    public bool isGrounded;                 
    private bool facingRight = false;
    private Rigidbody2D rigidBody2D;
    private LayerMask ground;
    private LayerMask cubeLayer;
    [HideInInspector]
    public bool cantShoot = false;
    private void Awake()
    {
        cubeLayer = 1<<LayerMask.NameToLayer("Cube");
        rigidBody2D = GetComponent<Rigidbody2D>();
        ground = 1<<LayerMask.NameToLayer("Ground");
    }
    private void FixedUpdate()              //检测是否着地
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, ground | cubeLayer);
        if (collider.Length != 0)
        {
            bool flag = false;
            for(int i = 0; i < collider.Length; i++)
            {
                if (!collider[i].isTrigger)
                {
                    flag = true;
                    break;
                }
            }
            isGrounded = flag;
        }
        if (Mathf.Abs(rigidBody2D.velocity.y) > 0.1f)
        {
            isGrounded = false;
        }
    }
    private void Flip()                     //角色翻转
    {
        facingRight = !facingRight;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }
    public void Move(float move, bool jump)     //角色移动处理
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
    }
}
