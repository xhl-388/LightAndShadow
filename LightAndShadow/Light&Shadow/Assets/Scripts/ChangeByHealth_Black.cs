using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeByHealth_Black : MonoBehaviour
{
    public int num;
    private GameController gameCon;
    private float lastTime;
    private void Start()
    {
        lastTime = Time.time;
        gameCon = GameObject.FindWithTag("GameControllerTag").GetComponent<GameController>();
    }
    private void Update()
    {
        if (Time.time > lastTime + 1f)
        {
            lastTime = Time.time;
            if (gameCon.health_Black <=10 * (num - 1))
            {
                if(this.gameObject.GetComponent<RawImage>().enabled)
                this.gameObject.GetComponent<RawImage>().enabled = false;
            }
            else
            {
                if (!this.gameObject.GetComponent<RawImage>().enabled)
                    this.gameObject.GetComponent<RawImage>().enabled = true;
                if (gameCon.health_Black < 60)
                {
                    if (gameCon.health_Black > 30 && gameObject.GetComponent<RawImage>().texture!= gameCon.yellowHpB)
                        gameObject.GetComponent<RawImage>().texture = gameCon.yellowHpB;
                    else
                    {
                        if(gameObject.GetComponent<RawImage>().texture != gameCon.redHpB)
                        gameObject.GetComponent<RawImage>().texture = gameCon.redHpB;
                    }
                }
                else
                {
                    if(gameObject.GetComponent<RawImage>().texture != gameCon.greenHpB&&num!=10)
                    gameObject.GetComponent<RawImage>().texture = gameCon.greenHpB;
                }
            }
        }
    }
}
