using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePlayer : MonoBehaviour            //p1输入检测，动画处理
{
    private float move;
    private float speed = 3.5f;
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
            move = Input.GetAxis("Horizontal2");
            jump = Input.GetKeyDown(KeyCode.UpArrow);
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (wCC.whiteSideCube && rig.velocity.y == 0)
                {
                    wCC.whiteSideCube.ColorManage(0);
                }
            }
        }
        else {
            move = 0;
            jump = false;
        }
        wCC.Move(move * speed, jump);
        anim.SetFloat("velocityY", rig.velocity.y);
        anim.SetFloat("velocityX", Mathf.Abs(rig.velocity.x));
        anim.SetBool("isGround", wCC.isGrounded);

    }
}
