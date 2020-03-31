using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSideCube : MonoBehaviour,ColoredCube              //影角色那边的方块脚本
{
    public bool isWhite;
    private BoxCollider2D boxColli;                       //覆盖物体，作为碰撞箱或触发器
    private CapsuleCollider2D capColli;                     //在物体底部，在影角色蓄力时与其发生碰撞
    private SpriteRenderer spriteRenderer;
    private GameController gameController;
    private GameObject blackP;
    public Mirror[] mirrors = new Mirror[2];
    private int firstMirrorIndex;
    private void Start()
    {
        firstMirrorIndex = GetFistMirrorIndex();
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
            boxColli.enabled = false;
        }
    }
    public void ChangeColor()
    {
        Debug.Log("BlackSideColorChanged");
        if (isWhite)
        {
            isWhite = false;
            spriteRenderer.sprite = gameController.blackSprite;
            boxColli.enabled = true;
        }
        else
        {
            isWhite = true;
            spriteRenderer.sprite = gameController.whiteSprite;
            boxColli.enabled = false;
        }
        if(firstMirrorIndex!=-1)
        mirrors[firstMirrorIndex].ChangeIdentically(this);
    }
    public bool IsWhite()
    {
        return isWhite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isWhite && collision.gameObject == blackP && blackP.GetComponent<BlackPlayer>().isSuperMode)
            boxColli.enabled = true ;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isWhite&&collision.gameObject == blackP)
            boxColli.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isWhite)
        {
            ChangeColor();
        }
    }
    private int GetFistMirrorIndex()
    {
        if (!mirrors[0]) { 
            return -1;
        }
        else if (!mirrors[1])
        {
            return 0;
        }
        else
        {
            if (mirrors[0].leftOrDownSideCubes.Contains(this))
            {
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
