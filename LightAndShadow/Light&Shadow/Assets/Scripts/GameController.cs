using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ColoredCube
{
    void ChangeColor();
    bool IsWhite();
}
public class GameController : MonoBehaviour     //游戏中的工具类脚本
{
    public Sprite whiteSprite;
    public Sprite blackSprite;
}
