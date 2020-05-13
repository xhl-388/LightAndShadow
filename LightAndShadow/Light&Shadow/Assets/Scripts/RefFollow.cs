using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefFollow : MonoBehaviour
{
    public bool blackT;
    private Transform targetT;
    private Vector2 point;
    private float line;
    private bool isLineRef;
    private void Start()
    {
        if (blackT)
        {
            targetT = GameObject.FindWithTag("BlackP").transform;
        }
        else targetT = GameObject.FindWithTag("WhiteP").transform;
        GameController gc = GameObject.FindWithTag("GameControllerTag").GetComponent<GameController>();
        point = gc.point;
        line = gc.line;
        isLineRef = gc.isLineReflect;
    }
    private void FixedUpdate()
    {
        if (isLineRef)
        {
            this.transform.position = new Vector3(2 * line - targetT.position.x, targetT.position.y);
        }
        else
        {
            this.transform.position = new Vector3(2 * point.x - targetT.position.x, 2 * point.y - targetT.position.y);
        }
    }
}
