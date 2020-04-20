using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public float widthOfMap;
    private Transform blackP;
    private Transform whiteP;
    private Camera main;
    private float initSize;
    private void Awake()
    {
        blackP = GameObject.FindWithTag("BlackP").GetComponent<Transform>();
        whiteP = GameObject.FindWithTag("WhiteP").GetComponent<Transform>();
        main = GetComponent<Camera>();
        initSize = widthOfMap / 2.8f;
        main.orthographicSize = initSize;
    }
    private void LateUpdate()
    {
        Vector3 targetP = (blackP.position + whiteP.position) / 2;
        Vector3 vec = targetP-this.transform.position;
        vec.z = 0;
        this.transform.position +=vec / 20f;
        if (Mathf.Abs(blackP.position.y - whiteP.position.y) > 0.6f*widthOfMap)
        {
            main.orthographicSize = (Mathf.Abs(blackP.position.y - whiteP.position.y)*5/3 + 2f) / 2.8f;
        }
        else
        {
            main.orthographicSize = initSize;
        }
    }
}
