using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeByHealth_White : MonoBehaviour
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
            if (gameCon.health_White <= 10 * (num - 1))
            {
                if (this.gameObject.GetComponent<RawImage>().enabled)
                    this.gameObject.GetComponent<RawImage>().enabled = false;
            }
            else
            {
                if (!this.gameObject.GetComponent<RawImage>().enabled)
                    this.gameObject.GetComponent<RawImage>().enabled = true;
                if (gameCon.health_White < 60)
                {
                    if(gameCon.health_White>30&& gameObject.GetComponent<RawImage>().texture!= gameCon.yellowHpW)
                         gameObject.GetComponent<RawImage>().texture = gameCon.yellowHpW;
                    else
                    {
                        if(gameObject.GetComponent<RawImage>().texture != gameCon.redHpW)
                            gameObject.GetComponent<RawImage>().texture = gameCon.redHpW;
                    }
                }
                else
                {
                    if (gameObject.GetComponent<RawImage>().texture != gameCon.greenHpW&&num!=10)
                        gameObject.GetComponent<RawImage>().texture = gameCon.greenHpW;
                }
            }
        }
    }
}
