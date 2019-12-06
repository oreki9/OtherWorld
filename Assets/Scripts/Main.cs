using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    int TimeInst = 0;
    public Vector3 CameraVec;
    public List<GameObject> ObstacleList = new List<GameObject>();
    public List<GameObject> ObstacleInArea = new List<GameObject>();
    public List<GameObject> MgcList = new List<GameObject>();
    public List<GameObject> MagicBtnList = new List<GameObject>();
    public GameObject playerGO;
    public Vector3 PlayerStartPos;
    ButtonSc MenuScene = new ButtonSc();
    public Text ScoreText;
    public int Score = 0;//score in screen

    //Magic Select Panel
    public int SelMagNow;
    public Text SelMagText;//Text Name Magic
    public List<int> MagIdSel;
    int SelMagBtn = 0;
    public GameObject MenuSelPanel;
    string MgcSaveStr = "7 1 2 3 4 5 6";//list magic yang di dapat player

    //Buy new Magic panel
    public Text AllPointTxt;
    public GameObject GetMagicPanel;
    int GetMagicNow = 0;
    public Text NameGetMag, GetMagPrice;
    public List<int> MgcPossId;
    public GameObject BtnBuyMgc;

    //Pause panel
    public GameObject PausePnl;
    public GameObject PasueBtn;
    void Start()
    {
        MenuSelPanel.SetActive(true);
        MagIdSel = GetEveryMagicPossible();
        SelMagNow = 0;
        SelMagText.text = MgcList[MagIdSel[SelMagNow]].name;
        Time.timeScale = 0f;
        
        for(int i = 0; i < MgcList.Count; i++)
        {
            if(MagIdSel.IndexOf(i)<0)
            {
                MgcPossId.Add(i);
            }
        }

        TimeInst = 100;
        CameraVec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        PlayerStartPos = playerGO.transform.position;
    }
    public List<int> GetEveryMagicPossible()
    {
        List<int> HasilInt = new List<int>();
        string MagicListGet = PlayerPrefs.GetString("Magic", "0");
        MgcSaveStr = MagicListGet;
        char[] spearator = { ' ' };
        System.String[] MagIdSelStr = MgcSaveStr.Split(spearator, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < MagIdSelStr.Length; i++)
        {
            int outInt = System.Int32.Parse(MagIdSelStr[i]);
            HasilInt.Add(outInt);
        }
        return HasilInt;
    }
    public void BuyMag()
    {
        string MagicListGet = PlayerPrefs.GetString("Magic", "0");
        MagicListGet += " "+ MgcPossId[GetMagicNow].ToString();
        PlayerPrefs.SetString("Magic", MagicListGet);
        int PointBuy = System.Int32.Parse((AllPointTxt.text));
        PlayerPrefs.SetInt("Point", PointBuy - MagicPrice(MgcPossId[GetMagicNow]));
        MenuScene.ToMenuScene();
    }
    public void GetMagNext()
    {
        if (GetMagicNow + 1 != MgcPossId.Count)
        {
            GetMagicNow += 1;
        }
        if (NameGetMag != null)
        {
            NameGetMag.text = MgcList[MgcPossId[GetMagicNow]].name;
        }
        if(GetMagPrice != null)
        {
            GetMagPrice.text = MagicPrice(MgcPossId[GetMagicNow]).ToString();
        }
        IsCanBuy();
    }
    public void GetMagPref()
    {
        if (GetMagicNow > 0)
        {
            GetMagicNow -= 1;
        }
        if (NameGetMag != null)
        {
            NameGetMag.text = MgcList[MgcPossId[GetMagicNow]].name;
        }
        if (GetMagPrice != null)
        {
            GetMagPrice.text = MagicPrice(MgcPossId[GetMagicNow]).ToString();
        }
        IsCanBuy();
    }
    public void SelMagNext()
    {
        if (SelMagNow + 1 != MagIdSel.Count)
        {
            SelMagNow += 1;
        }
        if (SelMagText!=null)
        {
            SelMagText.text = MgcList[MagIdSel[SelMagNow]].name;
        }
    }
    public void SelMagPref()
    {
        if (SelMagNow > 0)
        {
            SelMagNow -= 1;
        }
        if (SelMagText != null)
        {
            SelMagText.text = MgcList[MagIdSel[SelMagNow]].name;
        }
    }
    public void SelMag()
    {
        if (SelMagBtn + 1 <= MagicBtnList.Count)
        {
            MagicBtnList[SelMagBtn].GetComponent<Magic>().SetBtnMgcId(MagIdSel[SelMagNow]);
            MagIdSel.RemoveAt(SelMagNow);
            SelMagBtn +=1;
            if (SelMagNow > 0)
            {
                SelMagNow -= 1;
            }
            if (MagIdSel.Count > 0)
            {
                SelMagText.text = MgcList[MagIdSel[SelMagNow]].name;
            }
            if (MagIdSel.Count == 0)
            {
                StartPlay();
            }
        }
    }
    public void ReplayScene()
    {
        MenuScene.ToPlayScene();
    }
    public void ToMenuScene()
    {
        MenuScene.ToMenuScene();
    }
    public void PauseMenu()
    {
        Time.timeScale = 0;
        PausePnl.SetActive(true);
    }
    public void ResumeScene()
    {
        Time.timeScale = 1;
        PausePnl.SetActive(false);
    }
    public void StartPlay()
    {
        Time.timeScale = 1f;
        MenuSelPanel.SetActive(false);
        PasueBtn.SetActive(true);
        transform.GetComponent<AudioSource>().Play();
    }
    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString();
        if (Time.timeScale != 0f)
        {
            if ((ObstacleInArea.Count>1))
            {
                transform.GetComponent<AudioSource>().pitch = 1+(((float)ObstacleInArea.Count/2)/10);
            }
            TimeInst -= 1;
            if (TimeInst <= 0)
            {
                int InsRndm = Random.Range(0, ObstacleList.Count);
                Vector3 NewObsPos = new Vector3(CameraVec.x + transform.position.x, -2f, 0);
                if (InsRndm == 2)
                {
                    NewObsPos.y += 1.5f;
                }
                GameObject Obstacle = Instantiate(ObstacleList[InsRndm], NewObsPos, Quaternion.identity);
                Score++;
                ObstacleInArea.Add(Obstacle);
                ObsFrame ObsScript = Obstacle.GetComponent<ObsFrame>();
                ObsScript.EndWalk = -(CameraVec.x + transform.position.x+1f);
                ObsScript.playerGO = playerGO;
                TimeInst = 50+(Random.Range(0,10)*10);
            }
        }
    }
    public int MagicPrice(int i)
    {
        switch (i)
        {
            case 0:
                return 10;
            case 1:
                return 15;
            case 2:
                return 15;
            case 3:
                return 15;
            case 4:
                return 25;
            case 5:
                return 20;
            case 6:
                return 25;
            case 7:
                return 10;
            default:
                return 10;
        }
    }
    void IsCanBuy()
    {
        int PointBuy = PlayerPrefs.GetInt("Point", 0);
        if ((PointBuy - MagicPrice(MgcPossId[GetMagicNow])) >= 0)
        {
            BtnBuyMgc.GetComponent<Button>().interactable = true;
        }
        else
        {
            BtnBuyMgc.GetComponent<Button>().interactable = false;
        }
    }
    public void GameOver()
    {
        PasueBtn.SetActive(false);
        Time.timeScale = 0f;
        int GetPoint = PlayerPrefs.GetInt("Point", 0);
        GetPoint = System.Int32.Parse((ScoreText.text)) + GetPoint;
        PlayerPrefs.SetInt("Point", GetPoint);
        GetMagicPanel.SetActive(true);
        if (NameGetMag != null)
        {
            NameGetMag.text = MgcList[MgcPossId[0]].name;
        }
        if (GetMagPrice != null)
        {
            GetMagPrice.text = MagicPrice(MgcPossId[0]).ToString();
        }
        IsCanBuy();
        int HighestScore = PlayerPrefs.GetInt("Score", 0);
        AllPointTxt.text = GetPoint.ToString();
        if (Score > HighestScore)
        {
            PlayerPrefs.SetInt("Score", Score);
        }
    }
}
