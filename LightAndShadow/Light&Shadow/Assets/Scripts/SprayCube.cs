using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCube : MonoBehaviour
{
    private float sprayForce=900f;                             //弹跳力
    private LayerMask playerLayer;
    private WhiteCC wcc;
    private BlackCC bcc;
    private void Start()
    {
        playerLayer = 1<<LayerMask.NameToLayer("Player");
        wcc = GameObject.FindWithTag("WhiteP").GetComponent<WhiteCC>();
        bcc = GameObject.FindWithTag("BlackP").GetComponent<BlackCC>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y + 0.6f), new Vector2(0.9f,0.1f),0f, playerLayer);
        if (collider)
        {
            if (collider.gameObject.CompareTag("WhiteP"))
            {
                if (!wcc.cantShoot) {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, sprayForce));
                    wcc.cantShoot = true;
                    StartCoroutine(Cure(wcc.gameObject));
                }
            }
            else
            {
                if (!bcc.cantShoot)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, sprayForce));
                    bcc.cantShoot = true;
                    StartCoroutine(Cure(bcc.gameObject));
                }
            }
        }
    }
    IEnumerator Cure(GameObject obj)
    {
        yield return new WaitForSeconds(0.2f);
        if (obj.GetComponent<WhiteCC>())
        {
            wcc.cantShoot = false;
        }
        else bcc.cantShoot = false;
    }
}
