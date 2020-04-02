﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePlayer : MonoBehaviour            //p1输入检测，动画处理
{
    private float move;
    private float speed = 5f;
    private bool jump;
    private WhiteCC wCC;
    private Rigidbody2D rig;
    private void Start()
    {
        wCC = GetComponent<WhiteCC>();
        rig = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        wCC.Move(move * speed, jump);
    }
    private void Update()
    {
        move = Input.GetAxis("Horizontal");
        jump = Input.GetKey(KeyCode.W);
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (wCC.whiteSideCube&&rig.velocity.y==0)
            {
                wCC.whiteSideCube.ColorManage(0);
            }
        }
    }
}
