using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSideCube : MonoBehaviour              //影角色那边的方块脚本
{
    private bool isWhite;
    private BoxCollider2D boxColli;                       //覆盖物体，作为碰撞箱或触发器
    private CapsuleCollider2D capColli;                     //在物体底部，在影角色蓄力时与其发生碰撞
    private SpriteRenderer spriteRenderer;
    private GameController gameController;
    private GameObject blackP;
    private void Start()
    {
        boxColli = GetComponent<BoxCollider2D>();
        capColli = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.FindWithTag("GameControllerTag").GetComponent<GameController>();
        blackP = GameObject.FindWithTag("BlackP");
        if (spriteRenderer.sprite == gameController.whiteSprite)
            isWhite = true;
        else isWhite = false;
        if (isWhite)
        {
            boxColli.isTrigger = true;
        }
        else
        {
            capColli.enabled = false;
        }
    }
    public void ChangeIntoWhite()
    {
        isWhite = true;
        spriteRenderer.sprite = gameController.whiteSprite;
        boxColli.isTrigger = true;
        capColli.enabled = true;
    }
    public void ChangeIntoBlack()
    {
        isWhite = false;
        spriteRenderer.sprite = gameController.blackSprite;
        boxColli.isTrigger = false;
        capColli.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject==blackP)
            capColli.enabled = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == blackP)
            capColli.enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == blackP&&blackP.GetComponent<BlackPlayer>().isSuperMode)
        {
            ChangeIntoBlack();
        }
    }
}
