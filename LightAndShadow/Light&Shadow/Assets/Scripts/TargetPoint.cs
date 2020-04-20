using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    private GameController gameCon;
    private void Start()
    {
        gameCon = GameObject.FindWithTag("GameControllerTag").GetComponent<GameController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BlackP"))
        {
            gameCon.blackIsOnPlace = true;
        }
        else if (collision.gameObject.CompareTag("WhiteP"))
        {
            gameCon.whiteIsOnPlace = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BlackP"))
        {
            gameCon.blackIsOnPlace = false;
        }
        else if (collision.gameObject.CompareTag("WhiteP"))
        {
            gameCon.whiteIsOnPlace =false;
        }
    }
}
