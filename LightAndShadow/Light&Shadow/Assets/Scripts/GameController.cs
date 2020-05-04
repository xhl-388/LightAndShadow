using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Sprite[] greySprite=new Sprite[3];
    private GameObject[] mirrors;
    private GameObject[] multis;
    private GameObject[] plungers;
    private GameObject blackP;
    private GameObject whiteP;
    [HideInInspector]
    public bool blackIsOnPlace=false;
    [HideInInspector]
    public bool whiteIsOnPlace=false;
    private bool hasSucceed = false;
    private bool isPause=false;
    private GameObject UI_pausing;
    private GameObject UI_settings;
    private float minApartDistance=5f;
    private GameObject cameraA;
    private GameObject cameraB;
    private GameObject cameraMap;
    private bool isApart;
    private void Start()
    {
        cameraA = GameObject.Find("Camera_A");
        cameraB = GameObject.Find("Camera_B");
        cameraMap = GameObject.Find("Camera_Map");
        cameraMap.SetActive(false);
        isApart = true;
        mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        multis = GameObject.FindGameObjectsWithTag("MultiColorCube");
        plungers = GameObject.FindGameObjectsWithTag("Plunger");
        blackP = GameObject.FindWithTag("BlackP");
        whiteP = GameObject.FindWithTag("WhiteP");
        //if (Mathf.Abs(blackP.transform.position.x - whiteP.transform.position.x) < minApartDistance)
        //{
        //    cameraA.SetActive(false);
        //    cameraB.GetComponent<BCamera>().enabled = false;
        //    cameraB.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
        //    isApart = false;
        //}
        //else
        //{
        //    cameraB.GetComponent<Follow>().enabled = false;
        //    isApart = true;
        //}
        UI_pausing = GameObject.FindWithTag("UI_Pausing");
        UI_settings = GameObject.FindWithTag("UI_Settings");
        UI_pausing.SetActive(false);
        UI_settings.SetActive(false);
    }
    private void Update()
    {
        if (!hasSucceed)
        {
            if (blackIsOnPlace && whiteIsOnPlace)
            {
                hasSucceed = true;
                Succeed();
            }
            if (!isPause&&Input.GetKeyDown(KeyCode.Escape))
            {
                isPause = true;
                Time.timeScale = 0;
                UI_pausing.SetActive(true);
            }
        }
        //if (Mathf.Abs(blackP.transform.position.x - whiteP.transform.position.x) < minApartDistance)
        //{
        //    if (isApart)
        //    {
        //        cameraA.SetActive(false);
        //        cameraB.GetComponent<BCamera>().enabled = false;
        //        cameraB.GetComponent<Follow>().enabled = true;
        //        cameraB.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
        //        isApart = false;
        //    }
        //}
        //else
        //{
        //    if (!isApart)
        //    {
        //        cameraB.GetComponent<Follow>().enabled = false;
        //        cameraA.SetActive(true);
        //        cameraB.GetComponent<BCamera>().enabled = true;
        //        cameraB.GetComponent<Camera>().orthographicSize = 5f;
        //        cameraB.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
        //        isApart = true;
        //    }
        //}
        if (Input.GetKey(KeyCode.Space))
        {
            if (isApart)
            {
                cameraA.SetActive(false);
                cameraB.SetActive(false);
                cameraMap.SetActive(true);
                isApart = false;
            }
        }
        else
        {
            if (!isApart)
            {
                cameraA.SetActive(true);
                cameraB.SetActive(true);
                cameraMap.SetActive(false);
                isApart = true;
            }
        }
    }


    public void PauseOnClick()
    {
        isPause = false;
        Time.timeScale = 1;
        UI_pausing.SetActive(false);
    }
    public void ReplayOnClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SettingsOnClick()
    {
        UI_pausing.SetActive(false);
        UI_settings.SetActive(true);
    }
    public void ExitOnClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void BackOnClick()
    {
        UI_settings.SetActive(false);
        UI_pausing.SetActive(true);
    }
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Succeed()
    {
        blackP.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        whiteP.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        blackP.GetComponent<BlackPlayer>().cantControl = true;
        whiteP.GetComponent<WhitePlayer>().cantControl = true;
        Debug.Log("Succeed");
        if(SceneManager.GetActiveScene().buildIndex+1<SceneManager.sceneCountInBuildSettings)
        StartCoroutine(LoadNextScene());
    }
    public void TellMirror(ColoredCube cube)
    {
        for(int i = 0; i < mirrors.Length; i++)
        {
            mirrors[i].GetComponent<Mirror>().SearchCube(cube);
        }
    }
    public void TellMultiColorCube(GameObject obj)
    {
        for(int i = 0; i < multis.Length; i++)
        {
            multis[i].GetComponent<MultiColorCube>().SearchCubesNearby(obj);
        }
    }
    public void TellPlunger(GameObject obj)
    {
        for (int i = 0; i < plungers.Length; i++)
        {
            plungers[i].GetComponent<Plunger>().SearchCubes(obj);
        }
    }
    public void TellPlunger(GameObject obj,GameObject plungerExcepted)
    {
        for (int i = 0; i < plungers.Length; i++)
        {
            if(plungers[i]!=plungerExcepted)
            plungers[i].GetComponent<Plunger>().SearchCubes(obj);
        }
    }
}
