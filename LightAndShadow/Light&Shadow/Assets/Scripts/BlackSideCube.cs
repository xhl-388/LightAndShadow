using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSideCube : MonoBehaviour,ColoredCube              //影角色那边的方块脚本
{
    private bool isWhite;
    private BoxCollider2D boxColli;                       //覆盖物体，作为碰撞箱或触发器
    private CapsuleCollider2D capColli;                     //在物体底部，在影角色蓄力时与其发生碰撞
    private SpriteRenderer spriteRenderer;
    private GameController gameController;
    private GameObject blackP;
    private BlackPlayer blackPlayer;
    public Mirror[] mirrors = new Mirror[2];
    private int firstMirrorIndex;
    private LayerMask playerLayer;
    private bool hasChecked = false;
    private bool isPlunger=false;
    private void Awake()
    {
        if (GetComponent<Plunger>())
        {
            isPlunger = true;
        }
        boxColli = GetComponent<BoxCollider2D>();
        capColli = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.FindWithTag("GameControllerTag").GetComponent<GameController>();
        if (spriteRenderer.sprite == gameController.whiteSprite)
            isWhite = true;
        else isWhite = false;
    }
    private void Start()
    {
        playerLayer = 1 << LayerMask.NameToLayer("Player");
        firstMirrorIndex = GetFistMirrorIndex();
        blackP = GameObject.FindWithTag("BlackP");
        blackPlayer = blackP.GetComponent<BlackPlayer>();
        if (isWhite)
        {
            boxColli.enabled = false;
        }
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
        Debug.Log("BlackSideColorChanged"+this.gameObject.name);
        if (isPlunger)
        {
            GetComponent<Plunger>().PullAndPush();
        }
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
        gameController.TellPlunger(gameObject);
        gameController.TellMirror(this);
        gameController.TellMultiColorCube(gameObject);
    }
    public bool IsWhite()
    {
        return isWhite;
    }
    public Vector2 GetPosition()
    {
        return new Vector2(this.transform.position.x, this.transform.position.y);
    }
    private bool CanChange(Mirror mirror)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isWhite && collision.gameObject == blackP && blackPlayer.isSuperMode&&blackP.GetComponent<Rigidbody2D>().velocity.y>0)
        { boxColli.enabled = true;
            Debug.Log("Chnnn" + gameObject.name);
        }
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
            if (blackPlayer.isSuperMode)
            {
                ColorManage(0);
                blackPlayer.isSuperMode = false;
            }
        }
        else
        {
            if (blackPlayer.isSuperMode && collision.collider == blackP.GetComponent<CircleCollider2D>())
            {
                ColorManage(0);
                blackPlayer.isSuperMode = false;
            }
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
