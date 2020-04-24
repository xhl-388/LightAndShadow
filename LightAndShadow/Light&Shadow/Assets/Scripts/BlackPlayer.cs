﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPlayer : MonoBehaviour        //p2输入检测，动画处理
{
    private float move;
    private float speed = 3.5f;
    private bool jump;
    private BlackCC bCC;
    public bool isSuperMode = false;//蓄力状态
    private bool changeMode=false;
    private Rigidbody2D rig;
    private Animator anim;
    [HideInInspector]
    public bool cantControl = false;
    private void Start()
    {
        bCC = GetComponent<BlackCC>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
    }
    private void Update()
    {
        if (!cantControl)
        {
            move = Input.GetAxis("Horizontal");
            jump = Input.GetKeyDown(KeyCode.W);
            changeMode = Input.GetKeyDown(KeyCode.S);
            if (changeMode)
            {
                isSuperMode = !isSuperMode;
            }
        }
        else
        {
            move = 0;
            jump = false;
        }
        bCC.Move(move * speed, jump);
        if (!bCC.isGrounded)
        {
            anim.SetBool("isRunning", false);
        }
        else if (Mathf.Abs(rig.velocity.x) < 0.1f)
        {
            anim.SetBool("isRunning", false);
        }
        else anim.SetBool("isRunning", true);
    }
}
