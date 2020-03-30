using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPlayer : MonoBehaviour        //p2输入检测，动画处理
{
    private float move;
    private float speed = 5f;
    private bool jump;
    private BlackCC bCC;
    public bool isSuperMode = false;//蓄力状态
    private bool changeMode=false;
    private void Start()
    {
        bCC = GetComponent<BlackCC>();
    }
    private void FixedUpdate()
    {
        bCC.Move(move*speed, jump);
    }
    private void Update()
    {
        move = Input.GetAxis("Horizontal2");
        jump = Input.GetKey(KeyCode.UpArrow);
        changeMode = Input.GetKeyDown(KeyCode.DownArrow);
        {
            if (changeMode)
            {
                isSuperMode = !isSuperMode;
            }
        }
    }
}
