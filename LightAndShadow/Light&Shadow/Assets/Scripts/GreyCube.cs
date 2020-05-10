using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyCube : MonoBehaviour,ColoredCube  //通用灰色脚本
{
    public bool initExist=true;                         //存在状态 IsWhite()返回此值
    private bool isExist;                               //颜色状态
    private Collider2D colli;
    private SpriteRenderer spriteRenderer;
    public Mirror[] mirrors = new Mirror[2];
    private int firstMirrorIndex;
    private LayerMask playerLayer;
    private bool hasChecked = false;
    private BlackPlayer blackP;
    private GameController gameController;
    private void Awake()
    {
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colli = GetComponent<Collider2D>();
        gameController = GameObject.FindWithTag("GameControllerTag").GetComponent<GameController>();
        //spriteRenderer.sprite = gameController.greySprite[Random.Range(0, 3)];
        if (!initExist)
        {
            colli.enabled = false;
            spriteRenderer.enabled = false;
        }
        isExist = initExist;
        playerLayer = 1 << LayerMask.NameToLayer("Player");
        blackP = GameObject.FindWithTag("BlackP").GetComponent<BlackPlayer>();
        firstMirrorIndex = GetFistMirrorIndex();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BlackP")&&blackP.isSuperMode)
        {
            if(Physics2D.OverlapBox(new Vector2(this.transform.position.x,this.transform.position.y-0.6f),new Vector2(0.8f, 0.2f), 0, playerLayer))
            {
                ColorManage(0);
                blackP.isSuperMode = false;
            }
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
    public void ChangeColor()       //此处是改变灰色方块存在状态
    {
        Debug.Log("GeryCubeChanged"+this.gameObject.name);
        if (isExist)
        {
            isExist = false;
            spriteRenderer.enabled = false;
            colli.enabled = false;
        }
        else
        {
            isExist = true;
            spriteRenderer.enabled = true;
            colli.enabled = true;
        }
    }
    public bool IsWhite()
    {
        return isExist;
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
