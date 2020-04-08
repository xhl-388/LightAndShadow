using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiColorCube : MonoBehaviour
{
    public bool isOnWhiteSide=true;
    private LayerMask cubeLayer;
    private GameController gameController;
    private SpriteRenderer spriteRenderer;
    private Collider2D colli;
    private void Start()
    {
        cubeLayer = 1<<LayerMask.NameToLayer("Cube");
        gameController = GameObject.FindWithTag("GameControllerTag").GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colli = GetComponent<Collider2D>();
        GetColorByOthers();
    }
    public void GetColorByOthers()
    {
        int greyCount = 0;
        int blackCount = 0;
        int whiteCount = 0;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(2f, 2f), 0,cubeLayer);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != this.gameObject)
            {
                if (colliders[i].gameObject.GetComponent<GreyCube>())
                {
                    greyCount++;
                }
                else if (!isOnWhiteSide)
                {
                    if (colliders[i].gameObject.GetComponent<BlackSideCube>().IsWhite())
                    {
                        whiteCount++;
                    }
                    else
                    {
                        blackCount++;
                    }
                }
                else if (colliders[i].gameObject.GetComponent<WhiteSideCube>().IsWhite())
                {
                    whiteCount++;
                }
                else {
                    blackCount++;
                }
            }
        }
        int countAll = colliders.Length;
        Debug.Log(countAll);
        if (greyCount >= 0.5f * countAll)
        {
            ChangeColor(0);
        }
        else if (blackCount > whiteCount)
        {
            ChangeColor(-1);
        }
        else if (blackCount < whiteCount)
        {
            ChangeColor(1);
        }
        else ChangeColor(0);
    }
    private void ChangeColor(int x)                 //0是灰色 1是白色 -1是黑色
    {
        if (x == 0)
        {
            colli.enabled = true;
            spriteRenderer.sprite = gameController.greySprite;
        }
        else if (x == -1)
        {
            spriteRenderer.sprite = gameController.blackSprite;
            if (isOnWhiteSide)
            {
                colli.enabled = false;
            }
            else colli.enabled = true;
        }
        else
        {
            spriteRenderer.sprite = gameController.whiteSprite;
            if (isOnWhiteSide)
            {
                colli.enabled = true;
            }
            else colli.enabled = false;
        }
    }
}
