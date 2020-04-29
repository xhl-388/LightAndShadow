using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACamera : MonoBehaviour
{
    private Transform whiteT;
    private void Awake()
    {
        whiteT = GameObject.FindWithTag("WhiteP").GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        Vector3 pos =whiteT.position - this.transform.position;
        pos.z = 0;
        this.transform.position += pos / 20;
    }
}
