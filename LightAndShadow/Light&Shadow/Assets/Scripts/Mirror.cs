using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour            //同化镜
{
    public bool isHorizontal = true;
    public bool isOpposite = true;
    public List<ColoredCube> leftOrDownSideCubes;
    public List<ColoredCube> rightOrUpbSideCubes;
    private LayerMask cubeLayer;
    private void Awake()
    {
        leftOrDownSideCubes = new List<ColoredCube>();
        rightOrUpbSideCubes = new List<ColoredCube>();
        cubeLayer = LayerMask.NameToLayer("Cube");
        RaycastHit2D[] rayHit;
        RaycastHit2D[] rayHit2;
        if (isHorizontal)
        {
            rayHit = Physics2D.RaycastAll(transform.position, new Vector2(-1, 0), 20f, 1 << cubeLayer);
            rayHit2 = Physics2D.RaycastAll(transform.position, new Vector2(1,0), 20f, 1 << cubeLayer);
        }
        else
        {
            rayHit = Physics2D.RaycastAll(transform.position, new Vector2(0,-1), 20f, 1 << cubeLayer);
            rayHit2 = Physics2D.RaycastAll(transform.position, new Vector2(0,1), 20f, 1 << cubeLayer);
        }
        for(int i = 0; i < rayHit.Length; i++)
        {
            WhiteSideCube wsc = rayHit[i].collider.gameObject.GetComponent<WhiteSideCube>();
            BlackSideCube bsc = rayHit[i].collider.gameObject.GetComponent<BlackSideCube>();
            if (wsc)
            {
                leftOrDownSideCubes.Add(wsc);
                if (wsc.mirrors[0])
                {
                    wsc.mirrors[1] = this;
                }
                else wsc.mirrors[0] = this;
            }
            else
            {
                leftOrDownSideCubes.Add(bsc);
                if (bsc.mirrors[0])
                {
                    bsc.mirrors[1] = this;
                }
                else bsc.mirrors[0] = this;
            }
        }
        for (int i = 0; i < rayHit2.Length; i++)
        {
            WhiteSideCube wsc = rayHit2[i].collider.gameObject.GetComponent<WhiteSideCube>();
            BlackSideCube bsc = rayHit2[i].collider.gameObject.GetComponent<BlackSideCube>();
            if (wsc)
            {
                rightOrUpbSideCubes.Add(wsc);
                if (wsc.mirrors[0])
                {
                    wsc.mirrors[1] = this;
                }
                else wsc.mirrors[0] = this;
            }
            else
            {
                rightOrUpbSideCubes.Add(bsc);
                if (bsc.mirrors[0])
                {
                    bsc.mirrors[1] = this;
                }
                else bsc.mirrors[0] = this;
            }
        }
    }
    public void ChangeIdentically(ColoredCube cCube)
    {
        if (leftOrDownSideCubes.Contains(cCube))
        {
            ColoredCube cCubeOpposite = rightOrUpbSideCubes[leftOrDownSideCubes.IndexOf(cCube)];
            if (cCube.IsWhite() == cCubeOpposite.IsWhite()&&isOpposite)
            {
                StartCoroutine(ChangeColorLater(cCubeOpposite));
            }
            else if (cCube.IsWhite() != cCubeOpposite.IsWhite() && !isOpposite)
            {
                StartCoroutine(ChangeColorLater(cCubeOpposite));
            }
        }
        else
        {
            ColoredCube cCubeOpposite = leftOrDownSideCubes[rightOrUpbSideCubes.IndexOf(cCube)];
            if (cCube.IsWhite() == cCubeOpposite.IsWhite() && isOpposite)
            {
                StartCoroutine(ChangeColorLater(cCubeOpposite));
            }
            else if (cCube.IsWhite() != cCubeOpposite.IsWhite() && !isOpposite)
            {
                StartCoroutine(ChangeColorLater(cCubeOpposite));
            }
        }
    }
    IEnumerator ChangeColorLater(ColoredCube cCube)
    {
        yield return new WaitForSeconds(1f);
        cCube.ChangeColor();
    }
}
