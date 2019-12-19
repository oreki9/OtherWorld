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
    List<string> descriptionMgc = new List<string>() {
        "mengeluarkan bola sihir dari badan","mengumpulkan sihir dan membuat sihir besar",
        "membuat musuh lambat","mengeluarkan bola sihir yang mengejar musuh",
        "menghancurkan semua musuh yang ada","membuat pelindung di sekitar player",
        "mengeluarkan hujan sihir dari atas","terbang beberapa saat"
    };
    public GameObject playerGO;
    public Vector3 PlayerStartPos;
    public ButtonSc MenuScene;
    public Text ScoreText;
    public int Score = 0;//score in screen

    //Magic Select Panel
    public int SelMagNow;
    public Text SelMagText;//Text Name Magic
    public List<int> MagIdSel;
    int SelMagBtn = 0;
    public GameObject MenuSelPanel;
    public GameObject PlayBtnSel;
    string MgcSaveStr = "7 1 2 3 4 5 6";//list magic yang di dapat player
    public Text DescriptMgc;

    //Buy new Magic panel
    public Text AllPointTxt;
    public GameObject GetMagicPanel;
    int GetMagicNow = 0;
    public Text NameGetMag, GetMagPrice;
    public List<int> MgcPossId;
    public GameObject BtnPref,BtnBuyMgc, BtnNext,DescTextPnl;
    public Text DescMgcNewPnl;

    //Pause panel
    public GameObject PausePnl;
    public GameObject PasueBtn;

    //Nyanko-sensei Panel
    public GameObject NyanPnl;
    void Start()
    {
        MenuSelPanel.SetActive(true);
        MagIdSel = GetEveryMagicPossible();
        SelMagNow = 0;
        SetDescriptText(DescriptMgc, MagIdSel[SelMagNow]);
        SelMagText.text = GetMagicName(SelMagNow);
        transform.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume", 0);//set music
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
        string MagicListGet = PlayerPrefs.GetString("Magic", "0 7");
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
        if ((GetMagicNow<MgcPossId.Count)&&(GetMagicNow>=0))
        {
            string MagicListGet = PlayerPrefs.GetString("Magic", "0 7");
            MagicListGet += " " + MgcPossId[GetMagicNow].ToString();
            PlayerPrefs.SetString("Magic", MagicListGet);
            int PointBuy = System.Int32.Parse((AllPointTxt.text));
            int pointNow = PointBuy - MagicPrice(MgcPossId[GetMagicNow]);
            PlayerPrefs.SetInt("Point", pointNow);
            MgcPossId.RemoveAt(GetMagicNow);
            if (MgcPossId.Count != GetMagicNow)
            {
                GetMagNext();
            }
            else if (GetMagicNow != 0)
            {
                GetMagPref();
            }
            else
            {
                BtnBuyMgc.SetActive(false);
                BtnNext.SetActive(false);
                BtnPref.SetActive(false);
                DescTextPnl.SetActive(false);
            }
            AllPointTxt.text = pointNow.ToString();
        }
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
        SetDescriptText(DescMgcNewPnl, MgcPossId[GetMagicNow]);
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
            NameGetMag.text =  MgcList[MgcPossId[GetMagicNow]].name;
        }
        if (GetMagPrice != null)
        {
            GetMagPrice.text = MagicPrice(MgcPossId[GetMagicNow]).ToString();
        }
        SetDescriptText(DescMgcNewPnl, MgcPossId[GetMagicNow]);
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
            SelMagText.text = GetMagicName(SelMagNow);
            SetDescriptText(DescriptMgc, MagIdSel[SelMagNow]);
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
            SelMagText.text =  GetMagicName(SelMagNow);
            SetDescriptText(DescriptMgc, MagIdSel[SelMagNow]);
        }
    }
    public void SelMag()
    {
        if (SelMagBtn + 1 <= MagicBtnList.Count)
        {
            PlayBtnSel.SetActive(true);
            MagicBtnList[SelMagBtn].SetActive(true);
            MagicBtnList[SelMagBtn].GetComponent<Magic>().SetBtnMgcId(MagIdSel[SelMagNow]);
            MagIdSel.RemoveAt(SelMagNow);
            SelMagBtn +=1;
            if (SelMagNow > 0)
            {
                SelMagNow -= 1;
            }
            if (MagIdSel.Count > 0)
            {
                SelMagText.text = GetMagicName(SelMagNow);//MgcList[MagIdSel[SelMagNow]].name;
                SetDescriptText(DescriptMgc, MagIdSel[SelMagNow]);
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
        NyanPnl.SetActive(false);
    }
    public void ResumeScene()
    {
        Time.timeScale = 1;
        PausePnl.SetActive(false);
        if (NyanPnl.GetComponent<DialogSC>().time != 0)
        {
            NyanPnl.SetActive(true);
        }
    }
    public void StartPlay()
    {
        Time.timeScale = 1f;
        MenuSelPanel.SetActive(false);
        PasueBtn.SetActive(true);
        NyanPnl.SetActive(true);
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
                transform.GetComponent<AudioSource>().pitch = 1+(((float)ObstacleInArea.Count/5)/10);
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
                TimeInst = 10+(Random.Range(0,10)*10);
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
        if ((GetMagicNow < MgcPossId.Count) && (GetMagicNow >= 0))
        {
            if ((PointBuy - MagicPrice(MgcPossId[GetMagicNow])) >= 0)
            {
                BtnBuyMgc.GetComponent<Button>().interactable = true;
            }
            else
            {
                BtnBuyMgc.GetComponent<Button>().interactable = false;
            }
        }
    }
    string GetMagicName(int i)
    {
        string name = MgcList[MagIdSel[i]].name;
        name = "Add " + name;
        return name;
    }
    void SetDescriptText(Text Desc,int i)
    {
        Desc.text = descriptionMgc[i];
    }
    public void GameOver()
    {
        PasueBtn.SetActive(false);
        Time.timeScale = 0f;
        if ((GetMagicNow >= MgcPossId.Count) && (MgcPossId.Count == 0))
        {
            BtnBuyMgc.SetActive(false);
            BtnNext.SetActive(false);
            BtnPref.SetActive(false);
            DescTextPnl.SetActive(false);
        }
        int GetPoint = PlayerPrefs.GetInt("Point", 0);
        GetPoint = System.Int32.Parse((ScoreText.text)) + GetPoint;
        PlayerPrefs.SetInt("Point", GetPoint);
        GetMagicPanel.SetActive(true);
        if (MgcPossId.Count != 0)
        {
            SetDescriptText(DescMgcNewPnl, MgcPossId[GetMagicNow]);
        }
        if (NameGetMag != null)
        {
            if (MgcPossId.Count > 0)
            {
                NameGetMag.text = MgcList[MgcPossId[0]].name;
            }
        }
        if (GetMagPrice != null)
        {
            if (MgcPossId.Count > 0)
            {
                GetMagPrice.text = MagicPrice(MgcPossId[0]).ToString();
            }
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
