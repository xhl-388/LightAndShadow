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
    private LayerMask playerLayer;
    private bool hasChecked=false;
    private bool isPlunger = false;
    private bool hasSquare;
    private void Awake()
    {
        if (GetComponent<Plunger>()) isPlunger = true;
        colli = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.FindWithTag("GameControllerTag").GetComponent<GameController>();
        if (spriteRenderer.sprite == gameController.whiteSprite)            //初始化颜色状态
            isWhite = true;
        else isWhite = false;
    }
    private void Start()
    {
        playerLayer = 1<<LayerMask.NameToLayer("Player");
        firstMirrorIndex = GetFistMirrorIndex();
        whiteP = GameObject.FindWithTag("WhiteP");
        if (!isWhite)
        {
            colli.isTrigger = true;
        }
    }
    public bool HasSquare()
    {
        return hasSquare;
    }
    public void SetSquare(bool T)
    {
        hasSquare = T;
    }
    public void ColorManage(int x)
    {
        if (x == 0)
        {
            if (firstMirrorIndex == -1)
            {
                ChangeColor();
            }
            else if (CanChange(mirrors[firstMirrorIndex]))
            {
                ChangeColor();
                mirrors[firstMirrorIndex].ChangeIdentically(this);
            }
            else if (mirrors[1] && CanChange(mirrors[1 - firstMirrorIndex]))
            {
                ChangeColor();
                mirrors[1 - firstMirrorIndex].ChangeIdentically(this);
            }
        }
        else if (x == 1)
        {
            if (mirrors[1])
            {
                if (!hasChecked)
                {
                    if (CanChange(mirrors[1 - firstMirrorIndex]))
                    {
                        hasChecked = true;
                        mirrors[1 - firstMirrorIndex].ChangeIdentically(this);
                    }
                }
                else
                {
                    hasChecked = false;
                }
            }
        }
    }
    public void ChangeColor()
    {
        Debug.Log("WhiteSideColorChanged"+this.gameObject.name);
        if (isPlunger)
        {
            GetComponent<Plunger>().PullAndPush();
        }
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
        gameController.TellMirror(this);
        gameController.TellMultiColorCube(gameObject);
        gameController.TellPlunger(gameObject);
    }
    public bool IsWhite()
    {
        return isWhite;
    }
    public Vector2 GetPosition()
    {
        return new Vector2(this.transform.position.x, this.transform.position.y);
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
    private bool CanChange(Mirror mirror)           //判断物体关于mirror对称位置是否有玩家存在，若没有则返回可以变色
    {
       if (mirror.leftOrDownSideCubes.Contains(this))
        {
            ColoredCube cc = mirror.rightOrUpbSideCubes[mirror.leftOrDownSideCubes.IndexOf(this)];
            Collider2D collider = Physics2D.OverlapBox(cc.GetPosition(), new Vector2(0.8f, 0.8f), 0f, playerLayer);
            if (collider)
            {
               return false;
            }
              else return true;
        }
        else
        {
            ColoredCube cc = mirror.leftOrDownSideCubes[mirror.rightOrUpbSideCubes.IndexOf(this)];
            Collider2D collider = Physics2D.OverlapBox(cc.GetPosition(), new Vector2(0.8f, 0.8f), 0f, playerLayer);
            if (collider)
            {
                return false;
            }
            else return true;
        }
    }
    private int GetFistMirrorIndex()            //获取最高优先级镜子的序号
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
