using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSideCube : MonoBehaviour,ColoredCube              //光角色那边的方块脚本
{
    private bool isWhite;                               //颜色状态
    private Collider2D colli;
    private GameObject whiteP;
    private SpriteRenderer spriteRenderer;
    private GameController gameController;
    public Mirror[] mirrors = new Mirror[2];
    private int firstMirrorIndex;
    private void Start()
    {
        firstMirrorIndex = GetFistMirrorIndex();
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
    public void ChangeColor()
    {
        Debug.Log("WhiteSideColorChanged");
        if (isWhite)
        {
            isWhite = false;
            spriteRenderer.sprite = gameController.blackSprite;
            colli.isTrigger = true;
        }
        else
        {
            isWhite = true;
            spriteRenderer.sprite = gameController.whiteSprite;
            colli.isTrigger = false;
        }
        if(firstMirrorIndex!=-1)
        mirrors[firstMirrorIndex].ChangeIdentically(this);
    }
    public bool IsWhite()
    {
        return isWhite;
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
    private int GetFistMirrorIndex()
    {
        if (!mirrors[0])
        {
            return -1;
        }
        else if (!mirrors[1])
        {
            return 0;
        }
        else
        {
            if (mirrors[0].leftOrDownSideCubes.Contains(this)) {
                if (mirrors[0].isHorizontal)
                {
                    if (mirrors[1].leftOrDownSideCubes.Contains(this))
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    if (mirrors[1].leftOrDownSideCubes.Contains(this))
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            else
            {
                if (mirrors[0].isHorizontal)
                {
                    if (mirrors[1].leftOrDownSideCubes.Contains(this))
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (mirrors[1].leftOrDownSideCubes.Contains(this))
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
    }
}
