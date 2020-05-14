using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public interface ColoredCube        //方块通用接口
{
    void ChangeColor();             //直接改变颜色 
    bool IsWhite();                 //返回是否为白色或是否存在
    void ColorManage(int x);        //x接受0和1 0表示被动触发 1是主动触发(判断过程)
    Vector2 GetPosition();          //返回x，y坐标
    bool HasSquare();
    void SetSquare(bool T);
}
public class GameController : MonoBehaviour     //游戏中的工具类脚本
{
    private float timeNow;
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
    private GameObject UI_play;
    private GameObject cameraA;
    private GameObject cameraB;
    private GameObject cameraMap;
    private bool isApart;
    [HideInInspector]
    public float health_Black=100f;
    [HideInInspector]
    public float health_White=100f;
    public bool isLineReflect;
    public Vector2 point;
    public float line;
    public GameObject explosion_Blue;
    public GameObject explosion_Purple;
    public Texture yellowHpB;
    public Texture redHpB;
    public Texture greenHpB;
    public Texture yellowHpW;
    public Texture redHpW;
    public Texture greenHpW;
    private GameObject partOfUI;
    public GameObject whiteSquareOpp;
    public GameObject whiteSquareRef;
    public GameObject blackSquareOpp;
    public GameObject blackSquareRef;
    private LayerMask cubeLayer;
    private GameObject mask;
    private bool isAllSeen;
    private Scrollbar scroBGM;
    private AudioSource BGM;
    private void Start()
    {
        if (GameObject.FindWithTag("BGM")!=null)
        {
            BGM = GameObject.FindWithTag("BGM").GetComponent<AudioSource>();
        }
        mask = GameObject.FindWithTag("Mask").transform.GetChild(0).gameObject;
        cubeLayer =1<< LayerMask.NameToLayer("Cube");
        timeNow = Time.time;
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
        UI_pausing = GameObject.FindWithTag("UI_Pausing");
        UI_settings = GameObject.FindWithTag("UI_Settings");
        scroBGM = UI_settings.transform.GetChild(7).gameObject.GetComponent<Scrollbar>();
        UI_play = GameObject.FindWithTag("UI_Play");
        partOfUI = UI_play.transform.GetChild(0).gameObject;
        UI_pausing.SetActive(false);
        UI_settings.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (Time.time > timeNow + 1f)
        {
            timeNow = Time.time;
            Collider2D[] colliBlack = Physics2D.OverlapCircleAll(blackP.transform.position, 4f, cubeLayer);
            Collider2D[] colliWhite = Physics2D.OverlapCircleAll(whiteP.transform.position, 4f, cubeLayer);
            for(int i = 0; i < colliBlack.Length; i++)
            {
                if (colliBlack[i].GetComponent<BlackSideCube>()) {
                    BlackSideCube bsc= colliBlack[i].GetComponent<BlackSideCube>();
                    if (!bsc.HasSquare()&&bsc.mirrors[0])
                    {
                        bsc.SetSquare(true);
                        GameObject obj;
                        if (bsc.mirrors[0].GetComponent<Mirror>().isOpposite)
                        {
                            obj=Instantiate(whiteSquareOpp, colliBlack[i].transform);
                        }
                        else
                        {
                            obj=Instantiate(whiteSquareRef, colliBlack[i].transform);
                        }
                        obj.GetComponent<DestroySelf>().OnCube = bsc;
                    }
                }
                else if(colliBlack[i].GetComponent<GreyCube>())
                {
                    GreyCube gc = colliBlack[i].GetComponent<GreyCube>();
                    if (!gc.HasSquare() && gc.mirrors[0])
                    {
                        gc.SetSquare(true);
                        GameObject obj;
                        if (gc.mirrors[0].GetComponent<Mirror>().isOpposite)
                        {
                            obj=Instantiate(blackSquareOpp, colliBlack[i].transform);
                        }
                        else
                        {
                            obj=Instantiate(blackSquareRef, colliBlack[i].transform);
                        }
                        obj.GetComponent<DestroySelf>().OnCube = gc;
                        gc.onSquare = obj;
                    }
                }
            }
            for(int i = 0; i < colliWhite.Length; i++)
            {
                if (colliWhite[i].GetComponent<WhiteSideCube>())
                {
                    WhiteSideCube wsc = colliWhite[i].GetComponent<WhiteSideCube>();
                    if (!wsc.HasSquare() && wsc.mirrors[0])
                    {
                        wsc.SetSquare(true);
                        GameObject obj;
                        if (wsc.mirrors[0].GetComponent<Mirror>().isOpposite)
                        {
                            obj=Instantiate(whiteSquareOpp, colliWhite[i].transform);
                        }
                        else
                        {
                            obj=Instantiate(whiteSquareRef, colliWhite[i].transform);
                        }
                        obj.GetComponent<DestroySelf>().OnCube = wsc;
                    }
                }
                else if (colliWhite[i].GetComponent<GreyCube>())
                {
                    GreyCube gc = colliWhite[i].GetComponent<GreyCube>();
                    if (!gc.HasSquare() && gc.mirrors[0])
                    {
                        gc.SetSquare(true);
                        GameObject obj;
                        if (gc.mirrors[0].GetComponent<Mirror>().isOpposite)
                        {
                            obj=Instantiate(blackSquareOpp, colliWhite[i].transform);
                        }
                        else
                        {
                            obj=Instantiate(blackSquareRef, colliWhite[i].transform);
                        }
                        obj.GetComponent<DestroySelf>().OnCube = gc;
                        gc.onSquare = obj;
                    }
                }
            }
        }
    }
    private void Update()
    {
        if (UI_settings.activeSelf&&BGM!=null)
        {
            BGM.volume = scroBGM.value;
        }
        if (!hasSucceed)
        {
            if (blackIsOnPlace && whiteIsOnPlace)
            {
                hasSucceed = true;
                Succeed();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseOnClick();
            }
            if (isLineReflect)
            {
                float distance = new Vector2(blackP.transform.position.x + whiteP.transform.position.x - 2 * line, blackP.transform.position.y - whiteP.transform.position.y).magnitude;
                if (distance> 5f)
                {
                    health_Black = Mathf.Clamp(health_Black - 10 * Time.deltaTime, 0f, 100f);
                    health_White = Mathf.Clamp(health_White - 10 * Time.deltaTime, 0f, 100f);
                }
                else
                {
                    health_Black =Mathf.Clamp(health_Black + 20 * Time.deltaTime, 0f, 100f);
                    health_White = Mathf.Clamp(health_White + 20 * Time.deltaTime, 0f, 100f);
                }
                if (health_Black == 0f || health_White == 0f)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                if (distance < 4)
                {
                    if (!isAllSeen)
                    {
                        isAllSeen = true;
                        mask.transform.localScale = new Vector3(200, 200, 1);
                    }
                }
                else
                {
                    if (isAllSeen)
                    {
                        isAllSeen = false;
                        mask.transform.localScale = new Vector3(10, 10, 1);
                    }
                }
            }
            else
            {
                float distance = new Vector2(blackP.transform.position.x + whiteP.transform.position.x - 2 * point.x, blackP.transform.position.y + whiteP.transform.position.y - 2 * point.y).magnitude;
                if (distance> 5f)
                {
                    health_Black = Mathf.Clamp(health_Black - 10 * Time.deltaTime, 0f, 100f);
                    health_White = Mathf.Clamp(health_White - 10 * Time.deltaTime, 0f, 100f);
                }
                else
                {
                    health_Black = Mathf.Clamp(health_Black + 20 * Time.deltaTime, 0f, 100f);
                    health_White = Mathf.Clamp(health_White + 20 * Time.deltaTime, 0f, 100f);
                }
                if (health_Black == 0f || health_White == 0f)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                if (distance < 4)
                {
                    if (!isAllSeen)
                    {
                        isAllSeen = true;
                        mask.transform.localScale = new Vector3(200, 200, 1);
                    }
                }
                else
                {
                    if (isAllSeen)
                    {
                        isAllSeen = false;
                        mask.transform.localScale = new Vector3(10, 10, 1);
                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (isApart)
            {
                partOfUI.SetActive(false);
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
                partOfUI.SetActive(true);
                cameraA.SetActive(true);
                cameraB.SetActive(true);
                cameraMap.SetActive(false);
                isApart = true;
            }
        }
    }

    public void PauseOnClick()
    {
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            UI_pausing.SetActive(false);
            UI_play.SetActive(true);
        }
        else
        {
            isPause = true;
            Time.timeScale = 0;
            UI_pausing.SetActive(true);
            UI_play.SetActive(false) ;
        }
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
