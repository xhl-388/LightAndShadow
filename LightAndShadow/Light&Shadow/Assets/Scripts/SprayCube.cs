using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCube : MonoBehaviour
{
    [Tooltip("0 for right;1 for up;2 for left;3 for down")]     //之后通过scale直接判断方向（现在没素材，不好决定）
    public int direction=0;
    private float sprayForce=1000f;                             //弹跳力
    private BlackCC blackCC;
    private WhiteCC whiteCC;
    private void Start()
    {
        blackCC = GameObject.FindWithTag("BlackP").GetComponent<BlackCC>();
        whiteCC = GameObject.FindWithTag("WhiteP").GetComponent<WhiteCC>();
    }
    IEnumerator Cure(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        if (obj.GetComponent<WhiteCC>())
        {
            whiteCC.cantControl = false;
        }
        else blackCC.cantControl = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Ok");
        if (collision.gameObject.CompareTag("WhiteP"))
        {
            Rigidbody2D rig= collision.gameObject.GetComponent<Rigidbody2D>();
            if (direction == 0)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    rig.AddForce(new Vector2(sprayForce, 0));
                    whiteCC.cantControl = true;
                    StartCoroutine(Cure(whiteCC.gameObject));
                }
            }
            else if (direction == 1)
            {
                if (Input.GetKey(KeyCode.S))
                {
                    rig.AddForce(new Vector2(0, sprayForce));
                }
            }
            else if (direction == 2)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    rig.AddForce(new Vector2(-sprayForce, 0));
                    whiteCC.cantControl = true;
                    StartCoroutine(Cure(whiteCC.gameObject));
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rig.AddForce(new Vector2(0, -sprayForce));
                }
            }
        }
        else
        {
            Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
            if (direction == 0)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    rig.AddForce(new Vector2(sprayForce, 0));
                    blackCC.cantControl = true;
                    StartCoroutine(Cure(blackCC.gameObject));
                }
            }
            else if (direction == 1)
            {
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    rig.AddForce(new Vector2(0, sprayForce));
                }
            }
            else if (direction == 2)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    rig.AddForce(new Vector2(-sprayForce, 0));
                    blackCC.cantControl = true;
                    StartCoroutine(Cure(blackCC.gameObject));
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    rig.AddForce(new Vector2(0, -sprayForce));
                }
            }
        }
    }
}
