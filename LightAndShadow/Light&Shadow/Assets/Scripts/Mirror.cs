using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour            //同化镜
{
    private float timeBeforeReflect = 0.75f;
    public bool isHorizontal = true;
    public bool isOpposite = true;
    public List<ColoredCube> leftOrDownSideCubes;
    public List<ColoredCube> rightOrUpbSideCubes;
    private LayerMask cubeLayer;
    private void Awake()
    {
        leftOrDownSideCubes = new List<ColoredCube>();
        rightOrUpbSideCubes = new List<ColoredCube>();
        cubeLayer =1<<LayerMask.NameToLayer("Cube");
        SearchCube();
    }
    public void SearchCube(ColoredCube cube)
    {
        if (leftOrDownSideCubes.Contains(cube) || rightOrUpbSideCubes.Contains(cube))
        {
            SearchCube();
        }
    }
    public void SearchCube()
    {
        RaycastHit2D[] rayHit;
        if (isHorizontal)
        {
            rayHit = Physics2D.RaycastAll(transform.position, new Vector2(-1, 0), 20f, cubeLayer);
        }
        else
        {
            rayHit = Physics2D.RaycastAll(transform.position, new Vector2(0, -1), 20f, cubeLayer);
        }
        for (int i = 0; i < rayHit.Length; i++)
        {
            WhiteSideCube wsc = rayHit[i].collider.gameObject.GetComponent<WhiteSideCube>();
            BlackSideCube bsc = rayHit[i].collider.gameObject.GetComponent<BlackSideCube>();
            GreyCube gc = rayHit[i].collider.gameObject.GetComponent<GreyCube>();
            Collider2D colli;
            if (isHorizontal)
            {
                colli = Physics2D.OverlapCircle(new Vector2(2 * this.transform.position.x - rayHit[i].transform.position.x, this.transform.position.y), 0.1f, cubeLayer);
            }
            else
            {
                colli = Physics2D.OverlapCircle(new Vector2(this.transform.position.x, 2 * this.transform.position.y - rayHit[i].transform.position.y), 0.1f, cubeLayer);
            }
            if (colli)
            {
                if (wsc)
                {
                    if (isHorizontal)
                    {
                        BlackSideCube bsc2 = colli.GetComponent<BlackSideCube>();
                        rightOrUpbSideCubes.Add(bsc2);
                        if (bsc2.mirrors[0])
                        {
                            bsc2.mirrors[1] = this;
                        }
                        else bsc2.mirrors[0] = this;
                    }
                    else
                    {
                        WhiteSideCube wsc2 = colli.GetComponent<WhiteSideCube>();
                        rightOrUpbSideCubes.Add(wsc2);
                        if (wsc2.mirrors[0])
                        {
                            wsc2.mirrors[1] = this;
                        }
                        else wsc2.mirrors[0] = this;
                    }
                    leftOrDownSideCubes.Add(wsc);
                    if (wsc.mirrors[0])
                    {
                        wsc.mirrors[1] = this;
                    }
                    else wsc.mirrors[0] = this;
                }
                else if (bsc)
                {
                    if (isHorizontal)
                    {
                        WhiteSideCube wsc2 = colli.GetComponent<WhiteSideCube>();
                        rightOrUpbSideCubes.Add(wsc2);
                        if (wsc2.mirrors[0])
                        {
                            wsc2.mirrors[1] = this;
                        }
                        else wsc2.mirrors[0] = this;
                    }
                    else
                    {
                        BlackSideCube bsc2 = colli.GetComponent<BlackSideCube>();
                        rightOrUpbSideCubes.Add(bsc2);
                        if (bsc2.mirrors[0])
                        {
                            bsc2.mirrors[1] = this;
                        }
                        else bsc2.mirrors[0] = this;
                    }
                    leftOrDownSideCubes.Add(bsc);
                    if (bsc.mirrors[0])
                    {
                        bsc.mirrors[1] = this;
                    }
                    else bsc.mirrors[0] = this;
                }
                else
                {
                    GreyCube gc2 = colli.GetComponent<GreyCube>();
                    leftOrDownSideCubes.Add(gc);
                    rightOrUpbSideCubes.Add(gc2);
                    if (gc.mirrors[0])
                    {
                        gc.mirrors[1] = this;
                    }
                    else gc.mirrors[0] = this;
                    if (gc2.mirrors[0])
                    {
                        gc2.mirrors[1] = this;
                    }
                    else gc2.mirrors[0] = this;
                }
            }
        }
    }
    public void ChangeIdentically(ColoredCube cCube)
    {
        if (leftOrDownSideCubes.Contains(cCube))
        {
            ColoredCube cCubeOpposite = rightOrUpbSideCubes[leftOrDownSideCubes.IndexOf(cCube)];
            if (isOpposite)
            {
                if (cCube.IsWhite() == cCubeOpposite.IsWhite())
                {
                    StartCoroutine(ChangeColorLater(cCubeOpposite));
                }
                else
                {
                    cCube.ColorManage(1);
                }
            }
            else
            {
                if (cCube.IsWhite() != cCubeOpposite.IsWhite())
                {
                    StartCoroutine(ChangeColorLater(cCubeOpposite));
                }
                else
                {
                    cCube.ColorManage(1);
                }
            }
        }
        else
        {
            ColoredCube cCubeOpposite = leftOrDownSideCubes[rightOrUpbSideCubes.IndexOf(cCube)];
            if (isOpposite)
            {
                if (cCube.IsWhite() == cCubeOpposite.IsWhite())
                {
                    StartCoroutine(ChangeColorLater(cCubeOpposite));
                }
                else
                {
                    cCube.ColorManage(1);
                }
            }
            else
            {
                if (cCube.IsWhite() != cCubeOpposite.IsWhite())
                {
                    StartCoroutine(ChangeColorLater(cCubeOpposite));
                }
                else
                {
                    cCube.ColorManage(1);
                }
            }
        }
    }
    IEnumerator ChangeColorLater(ColoredCube cCube)
    {
        yield return new WaitForSeconds(timeBeforeReflect);
        cCube.ColorManage(0);
    }
}
