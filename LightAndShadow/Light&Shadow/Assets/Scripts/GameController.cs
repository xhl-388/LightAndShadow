using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ColoredCube        //方块通用接口
{
    void ChangeColor();             //直接改变颜色 
    bool IsWhite();                 //返回是否为白色或是否存在
    void ColorManage(int x);        //x接受0和1 0表示被动触发 1是主动触发(判断过程)
    Vector2 GetPosition();          //返回x，y坐标
}
public class GameController : MonoBehaviour     //游戏中的工具类脚本
{
    public Sprite whiteSprite;
    public Sprite blackSprite;
    public GameObject gobj;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            gobj.GetComponent<GreyCube>().ColorManage(0);
        }
    }
}
