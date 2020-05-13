using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskFollowPlayer : MonoBehaviour
{
    public bool blackT;
    private Transform targetT;
    private void Start()
    {
        if (blackT)
        {
            targetT = GameObject.FindWithTag("BlackP").transform;
        }
        else targetT = GameObject.FindWithTag("WhiteP").transform;
    }
    private void FixedUpdate()
    {
        transform.position = targetT.position;
    }
}
