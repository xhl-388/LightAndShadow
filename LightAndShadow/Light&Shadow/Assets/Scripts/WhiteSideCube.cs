using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSideCube : MonoBehaviour              //光角色那边的方块脚本
{
    private bool isWhite;                               //颜色状态
    private Collider2D colli;
    private GameObject whiteP;
    private SpriteRenderer spriteRenderer;
    private GameController gameController;
    private void Start()
    {
        colli = GetComponent<Collider2D>();
        whiteP = GameObject.FindWithTag("WhiteP");
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.FindWithTag("GameControllerTag").GetComponent<GameController>();
        if (spriteRenderer.sprite == gameController.whiteSprite)            //初始化颜色状态
            isWhite = true;
        else isWhite = false;
        if (!isWhite)
        {
            colli.isTrigger = true;
        }
    }
    public void ChangeIntoWhite()               //颜色变为白色调用的方法
    {
        isWhite = true;
        spriteRenderer.sprite = gameController.whiteSprite;
        colli.isTrigger = false;
    }
    public void ChangeIntoBlack()                   //颜色变为黑色调用的方法
    {
        isWhite = false;
        spriteRenderer.sprite = gameController.blackSprite;
        colli.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)         //赋予光角色二段跳能力
    {
        if(collision.gameObject==whiteP)
           whiteP.GetComponent<WhiteCC>().canJumpAgain = true;
    }
    private void OnTriggerExit2D(Collider2D collision)             //取消光角色二段跳能力
    {
        if (collision.gameObject == whiteP)
            whiteP.GetComponent<WhiteCC>().canJumpAgain = false;
    }
}
