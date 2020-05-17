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
    private List<GameObject> cubesNearby = new List<GameObject>();
    private Animator anim;
    public int extraGrey=0;
    private void Start()
    {
        anim = GetComponent<Animator>();
        cubeLayer = 1<<LayerMask.NameToLayer("Cube");
        gameController = GameObject.FindWithTag("GameControllerTag").GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colli = GetComponent<Collider2D>();
        SearchCubesNearby();
    }
    public void SearchCubesNearby(GameObject obj)
    {
        if (cubesNearby.Contains(obj))
        {
            SearchCubesNearby();
        }
    }
    public void SearchCubesNearby()
    {
        int greyCount = extraGrey;
        int blackCount = 0;
        int whiteCount = 0;
        List<Collider2D> colliders = new List<Collider2D>();
        if (!isOnWhiteSide)
        {
            RaycastHit2D[] extra = Physics2D.RaycastAll(new Vector2(transform.position.x - 1f, transform.position.y - 1.51f), Vector2.right, 2f, cubeLayer);
            //RaycastHit2D[] extra1 = Physics2D.RaycastAll(new Vector2(transform.position.x - 1f, transform.position.y +1f), Vector2.right, 2f, cubeLayer);
            RaycastHit2D[] extra2 = Physics2D.RaycastAll(new Vector2(transform.position.x - 1f, transform.position.y + 0.49f), Vector2.right, 2f, cubeLayer);
            RaycastHit2D[] extra3 = Physics2D.RaycastAll(new Vector2(transform.position.x - 1f, transform.position.y - 0.51f), Vector2.right, 2f, cubeLayer);
            for (int i = 0; i < extra.Length; i++)
            {
                if (extra[i].collider.gameObject.GetComponent<BlackSideCube>() && extra[i].collider.gameObject.GetComponent<BlackSideCube>().IsWhite())
                {
                    colliders.Add(extra[i].collider);
                }
            }
            //for(int i = 0; i < extra1.Length; i++)
            //{
            //    colliders.Add(extra1[i].collider);
            //}
            for (int i = 0; i < extra2.Length; i++)
            {
                colliders.Add(extra2[i].collider);
            }
            for (int i = 0; i < extra3.Length; i++)
            {
                colliders.Add(extra3[i].collider);
            }
        }
        else
        {
            Collider2D[] collider = Physics2D.OverlapBoxAll(transform.position, new Vector2(2f, 2f), 0, cubeLayer);
            for(int i = 0; i < collider.Length; i++)
            {
                colliders.Add(collider[i]);
            }
        }
        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i].gameObject != this.gameObject)
            {
                cubesNearby.Add(colliders[i].gameObject);
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
            if (colliders[i].gameObject.GetComponent<SprayCube>())
            {
                colliders[i].gameObject.GetComponent<SprayCube>().enabled = false;
            }
        }
        int countAll = colliders.Count;
        Debug.Log("CountAll=" + countAll + ";black=" + blackCount + ";white=" + whiteCount + ";Grey=" + greyCount+gameObject.name);
        if (greyCount >= 0.5f * countAll)
        {
            ChangeColor(0);
            if (blackCount == whiteCount)
            {
                for (int i = 0; i < colliders.Count; i++)
                {
                    if (colliders[i].gameObject.GetComponent<GreyCube>())
                    {
                        if (colliders[i].gameObject.GetComponent<SprayCube>())
                        {
                            colliders[i].gameObject.GetComponent<SprayCube>().enabled = true;
                        }
                        else colliders[i].gameObject.AddComponent<SprayCube>();
                    }
                }
            }
        }
        else if (blackCount > whiteCount)
        {
            ChangeColor(-1);
            if (whiteCount == 0)
            {
                for (int i = 0; i < colliders.Count; i++)
                {
                    if (isOnWhiteSide)
                    {
                        if (colliders[i].gameObject.GetComponent<WhiteSideCube>())
                        {
                            if (colliders[i].gameObject.GetComponent<SprayCube>())
                            {
                                colliders[i].gameObject.GetComponent<SprayCube>().enabled = true;
                            }
                            else colliders[i].gameObject.AddComponent<SprayCube>();
                        }
                    }
                    else
                    {
                        if (colliders[i].gameObject.GetComponent<BlackSideCube>())
                        {
                            if (colliders[i].gameObject.GetComponent<SprayCube>())
                            {
                                colliders[i].gameObject.GetComponent<SprayCube>().enabled = true;
                            }
                            else colliders[i].gameObject.AddComponent<SprayCube>();
                        }
                    }
                }
            }
        }
        else if (blackCount < whiteCount)
        {
            ChangeColor(1);
            if (blackCount == 0)
            {
                for (int i = 0; i < colliders.Count; i++)
                {
                    if (isOnWhiteSide)
                    {
                        if (colliders[i].gameObject.GetComponent<WhiteSideCube>())
                        {
                            if (colliders[i].gameObject.GetComponent<SprayCube>())
                            {
                                colliders[i].gameObject.GetComponent<SprayCube>().enabled = true;
                            }
                            else colliders[i].gameObject.AddComponent<SprayCube>();
                        }
                    }
                    else
                    {
                        if (colliders[i].gameObject.GetComponent<BlackSideCube>())
                        {
                            if (colliders[i].gameObject.GetComponent<SprayCube>())
                            {
                                colliders[i].gameObject.GetComponent<SprayCube>().enabled = true;
                            }
                            else colliders[i].gameObject.AddComponent<SprayCube>();
                        }
                    }
                }
            }
        }
        else { 
            ChangeColor(0);
            if (blackCount == whiteCount)
            {
                for (int i = 0; i < colliders.Count; i++)
                {
                    if (colliders[i].gameObject.GetComponent<GreyCube>())
                    {
                        if (colliders[i].gameObject.GetComponent<SprayCube>())
                        {
                            colliders[i].gameObject.GetComponent<SprayCube>().enabled = true;
                        }
                        else colliders[i].gameObject.AddComponent<SprayCube>();
                    }
                }
            }
        }
    }
    private void ChangeColor(int x)                 //0是灰色 1是白色 -1是黑色
    {
        if (x == 0)
        {
            colli.enabled = true;
            anim.SetInteger("ColorState", 0);
        }
        else if (x == -1)
        {
            anim.SetInteger("ColorState", -1);
            if (isOnWhiteSide)
            {
                colli.enabled = false;
            }
            else colli.enabled = true;
        }
        else
        {
            anim.SetInteger("ColorState", 1);
            if (isOnWhiteSide)
            {
                colli.enabled = true;
            }
            else colli.enabled = false;
        }
    }
}
