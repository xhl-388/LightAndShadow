using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePlayer : MonoBehaviour            //p1输入检测，动画处理
{
    private float move;
    private float speed = 3f;
    private bool jump;
    private WhiteCC wCC;
    private Rigidbody2D rig;
    private Animator anim;
    [HideInInspector]
    public bool cantControl = false;
    private void Start()
    {
        wCC = GetComponent<WhiteCC>();
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
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (wCC.whiteSideCube && rig.velocity.y == 0)
                {
                    wCC.whiteSideCube.ColorManage(0);
                }
            }
        }
        wCC.Move(move * speed, jump);
        if (!wCC.isGrounded)
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
