using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    private bool isOnWhiteSide;
    private List<GameObject> whiteCubes = new List<GameObject>();
    private List<GameObject> blackCubes = new List<GameObject>();
    private LayerMask mirrorLayer;
    private LayerMask cubeLayer;
    private Vector2 dir;
    private ColoredCube myCube;
    private GameController gameController;
    private void Start()
    {
        if (GetComponent<WhiteSideCube>())
        {
            myCube = GetComponent<WhiteSideCube>();
            isOnWhiteSide = true;
        }
        else
        {
            myCube = GetComponent<BlackSideCube>();
            isOnWhiteSide = false;
        }
        mirrorLayer = 1 << LayerMask.NameToLayer("Mirror");
        cubeLayer = 1 << LayerMask.NameToLayer("Cube");
        gameController = GameObject.FindWithTag("GameControllerTag").GetComponent<GameController>();
        float z = transform.rotation.eulerAngles.z;
        if (z == 0)
        {
            dir = new Vector2(1, 0);
        }
        else if (z == 90)
        {
            dir = new Vector2(0, 1);
        }
        else if (z == 180)
        {
            dir = new Vector2(-1, 0);
        }
        else dir = new Vector2(0, -1);
        SearchCubes();
    }
    public void PullAndPush()
    {
        if (myCube.IsWhite())
        {
            for(int i = 0; i < whiteCubes.Count; i++)
            {
                Collider2D collider = Physics2D.OverlapCircle(new Vector2(whiteCubes[i].transform.position.x - dir.x, whiteCubes[i].transform.position.y - dir.y), 0.1f);
                if (!collider)
                {
                    whiteCubes[i].transform.position = new Vector2(whiteCubes[i].transform.position.x - dir.x, whiteCubes[i].transform.position.y - dir.y);
                    if (whiteCubes[i].GetComponent<BlackSideCube>())
                    {
                        gameController.TellMirror(whiteCubes[i].GetComponent<BlackSideCube>());
                    }
                    else gameController.TellMirror(whiteCubes[i].GetComponent<WhiteSideCube>());
                    gameController.TellMultiColorCube(whiteCubes[i]);
                    gameController.TellPlunger(whiteCubes[i], this.gameObject);
                }
            }
            for(int i = 0; i < blackCubes.Count; i++)
            {
                Collider2D collider= Physics2D.OverlapCircle(new Vector2(blackCubes[i].transform.position.x + dir.x, blackCubes[i].transform.position.y + dir.y), 0.1f);
                if (!collider)
                {
                    blackCubes[i].transform.position = new Vector2(blackCubes[i].transform.position.x + dir.x, blackCubes[i].transform.position.y+dir.y);
                    if (blackCubes[i].GetComponent<BlackSideCube>())
                    {
                        gameController.TellMirror(blackCubes[i].GetComponent<BlackSideCube>());
                    }
                    else gameController.TellMirror(blackCubes[i].GetComponent<WhiteSideCube>());
                    gameController.TellMultiColorCube(blackCubes[i]);
                    gameController.TellPlunger(blackCubes[i], this.gameObject);
                }
            }
        }
        else
        {
            for (int i = 0; i < blackCubes.Count; i++)
            {
                Collider2D collider = Physics2D.OverlapCircle(new Vector2(blackCubes[i].transform.position.x - dir.x, blackCubes[i].transform.position.y - dir.y), 0.1f);
                if (!collider)
                {
                    blackCubes[i].transform.position = new Vector2(blackCubes[i].transform.position.x - dir.x, blackCubes[i].transform.position.y - dir.y);
                    if (blackCubes[i].GetComponent<BlackSideCube>())
                    {
                        gameController.TellMirror(blackCubes[i].GetComponent<BlackSideCube>());
                    }
                    else gameController.TellMirror(blackCubes[i].GetComponent<WhiteSideCube>());
                    gameController.TellMultiColorCube(blackCubes[i]);
                    gameController.TellPlunger(blackCubes[i], this.gameObject);
                }
            }
            for (int i = 0; i < whiteCubes.Count; i++)
            {
                Collider2D collider = Physics2D.OverlapCircle(new Vector2(whiteCubes[i].transform.position.x + dir.x, whiteCubes[i].transform.position.y + dir.y), 0.1f);
                if (!collider)
                {
                    whiteCubes[i].transform.position = new Vector2(whiteCubes[i].transform.position.x + dir.x, whiteCubes[i].transform.position.y + dir.y);
                    if (whiteCubes[i].GetComponent<BlackSideCube>())
                    {
                        gameController.TellMirror(whiteCubes[i].GetComponent<BlackSideCube>());
                    }
                    else gameController.TellMirror(whiteCubes[i].GetComponent<WhiteSideCube>());
                    gameController.TellMultiColorCube(whiteCubes[i]);
                    gameController.TellPlunger(whiteCubes[i], this.gameObject);
                }
            }
        }
    }
    public void SearchCubes(GameObject obj)
    {
        if (whiteCubes.Contains(obj) || blackCubes.Contains(obj))
        {
            SearchCubes();
        }
    }
    public void SearchCubes()
    {
        RaycastHit2D[] rayHits = Physics2D.RaycastAll(transform.position, dir, 20f, mirrorLayer | cubeLayer);
        for(int i = 0; i < rayHits.Length; i++)
        {
            GameObject obj = rayHits[i].collider.gameObject;
            if (obj != this.gameObject)
            {
                if (obj.GetComponent<GreyCube>() || obj.CompareTag("Mirror"))
                {
                    break;
                }
                if (isOnWhiteSide)
                {
                    if (obj.GetComponent<WhiteSideCube>())
                    {
                        if (obj.GetComponent<WhiteSideCube>().IsWhite())
                        {
                            whiteCubes.Add(obj);
                        }
                        else blackCubes.Add(obj);
                    }
                }
                else
                {
                    if (obj.GetComponent<BlackSideCube>())
                    {
                        if (obj.GetComponent<BlackSideCube>().IsWhite())
                        {
                            whiteCubes.Add(obj);
                        }
                        else blackCubes.Add(obj);
                    }
                }
            }
        }
    }
}
