using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    private float timeNow;
    private Transform blackT;
    private Transform whiteT;
    [HideInInspector]
    public ColoredCube OnCube;
    private void Start()
    {
        timeNow = Time.time;
        blackT = GameObject.FindWithTag("BlackP").transform;
        whiteT = GameObject.FindWithTag("WhiteP").transform;
    }
    private void Update()
    {
        if (Time.time > timeNow + 1f)
        {
            timeNow = Time.time;
            if ((transform.position - blackT.position).magnitude > 5f && (transform.position - whiteT.position).magnitude > 5f)
            {
                OnCube.SetSquare(false);
                Destroy(this.gameObject);
            }
        }
    }
}
