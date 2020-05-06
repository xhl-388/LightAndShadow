using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private float lastVelocityY;
    public int HP=3;
    private void Start()
    {
        bCC = GetComponent<BlackCC>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
    }
    public void GetDamaged()
    {
        HP--;
        if (HP == 0)
        {
            Die();
        }
    }
    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (lastVelocityY < -17)
        {
            if(collision.gameObject.GetComponent<SprayCube>()&& collision.gameObject.GetComponent<SprayCube>().enabled)
            {
                return;
            }
            else
            {
                GetDamaged();
            }
        }
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
        anim.SetFloat("velocityY", rig.velocity.y);
        anim.SetFloat("velocityX", Mathf.Abs(rig.velocity.x));
        anim.SetBool("isGround", bCC.isGrounded);
    }
    private void LateUpdate()
    {
        lastVelocityY = rig.velocity.y;
    }
}
