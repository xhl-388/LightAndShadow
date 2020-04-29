using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCamera : MonoBehaviour
{
    private Transform blackT;
    private void Awake()
    {
        blackT = GameObject.FindWithTag("BlackP").GetComponent<Transform>();
    }
    /*private void LateUpdate()
    {
        Vector3 pos = blackT.Pos - this.transform.position;
        pos.z = 0;
        this.transform.position += pos / 20;
    }*/
    private void FixedUpdate()
    {
        Vector3 pos = blackT.position - this.transform.position;
        pos.z = 0;
        this.transform.position += pos / 20;
    }
}
