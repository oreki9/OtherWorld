using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magic : MonoBehaviour
{
    public int MgcId = 0;
    [HideInInspector]
    public Main mainScr;
    [HideInInspector]
    public GameObject PlayerObj;
    int Mgcooldown;
    int MgcoolEnd;
    Text TextBtn;//TextFor Name Magic
    bool NotActive = false;//active bool button
    Vector3 StartPos;
    [HideInInspector]
    public GameObject AtField;//shield
    bool MoveFly = false;
    public Vector3 endFly;
    // Start is called before the first frame update
    void Start()
    {
        TextBtn = transform.Find("TextBtn").gameObject.GetComponent<Text>();
        PlayerObj = mainScr.playerGO;
        switch (MgcId)
        {
            case 0://Shoot
                MgcoolEnd = 50;
                TextBtn.text = "Pow";
                break;
            case 1://Blaster
                MgcoolEnd = 100;
                TextBtn.text = "Blast";
                break;
            case 2://Freeze
                MgcoolEnd = 150;
                TextBtn.text = "Free";
                break;
            case 3://Hooming
                MgcoolEnd = 100;
                TextBtn.text = "Hoom";
                break;
            case 4://Messiah
                MgcoolEnd = 400;
                TextBtn.text = "Mess";
                break;
            case 5://At-Field
                MgcoolEnd = 300;
                TextBtn.text = "AT";
                break;
            case 6:
                MgcoolEnd = 200;
                TextBtn.text = "Rain";
                break;
            case 7:
                MgcoolEnd = 200;
                TextBtn.text = "Fly";
                break;
            default:
                MgcoolEnd = 100;
                break;
        }
        Mgcooldown = MgcoolEnd;

    }
    public void InstMagic()
    {
        if (Time.timeScale != 0f)
        {
            if (Mgcooldown == MgcoolEnd)
            {
                int ManyObj = 1;
                Vector3 PlayerVec = PlayerObj.transform.position;
                PlayerVec.z = 0.1f;
                if (MgcId == 7)//Fly
                {
                    Vector3 NewPos = mainScr.PlayerStartPos;
                    NewPos.y = 0;
                    MoveFly = true;
                    endFly = NewPos;
                }
                if (MgcId == 4)
                {
                    for (int i = 0; i < mainScr.ObstacleInArea.Count; i++)
                    {
                        Destroy(mainScr.ObstacleInArea[i]);
                    }
                    PlayerVec = new Vector3(0, 0);
                    Mgcooldown = 0;

                }
                GameObject magic = null;
                if (MgcId == 6)
                {
                    int[] rainInt = new int[7];
                    int rainNow = 0;
                    ManyObj = 7;
                    for (int i = 0; i < 7; i++)
                    {
                        rainInt[i] = rainNow + Random.Range(0, 5) + 1;
                        rainNow = rainInt[i];
                        PlayerVec = new Vector3(mainScr.CameraVec.x - rainNow * (0.5f), mainScr.CameraVec.y+Random.Range(0,5));
                        Instantiate(mainScr.MgcList[MgcId], PlayerVec, Quaternion.identity);
                    }
                }
                if(MgcId == 2)
                {
                    PlayerVec = new Vector3(0, 0);
                }
                if (ManyObj == 1)
                {
                    magic = Instantiate(mainScr.MgcList[MgcId], PlayerVec, Quaternion.identity);
                    if (MgcId == 1)
                    {
                        magic.GetComponent<MagicFrame>().player = PlayerObj;
                    }
                }
                if (MgcId == 5)
                {
                    AtField = magic;
                }
                GameObject TargetPos = null;
                float jarakTarget = 0;
                if ((MgcId == 3)&&(mainScr.ObstacleInArea.Count>0))//Klik Mgid 3 dan tidak ada musuh di arena
                {
                    for (int i = 0; i < mainScr.ObstacleInArea.Count; i++)
                    {
                        if (mainScr.ObstacleInArea[i]==null)
                        {
                            continue;
                        }
                        float Jarakx = Mathf.Pow(Mathf.Abs(PlayerObj.transform.position.x - mainScr.ObstacleInArea[i].transform.position.x), 2);
                        float Jaraky = Mathf.Pow(Mathf.Abs(PlayerObj.transform.position.y - mainScr.ObstacleInArea[i].transform.position.y), 2);
                        if (i == 0)
                        {
                            TargetPos = mainScr.ObstacleInArea[0];
                            jarakTarget = Mathf.Sqrt(Jarakx + Jaraky);
                            continue;
                        }
                        if (Mathf.Sqrt(Jarakx + Jaraky) < jarakTarget)
                        {
                            TargetPos = mainScr.ObstacleInArea[i];
                            jarakTarget = Mathf.Sqrt(Jarakx + Jaraky);
                        }
                    }
                    magic.GetComponent<MagicFrame>().TargetPos = TargetPos;
                }
                else
                {
                    NotActive = true;
                    Mgcooldown = 0;
                }
                if (NotActive==false)//get Button non Active
                {
                    Mgcooldown = 0;
                }
                else
                {
                    NotActive = false;
                }
            }
        }
    }
    Vector3 MoveToPos(Vector3 start,Vector3 end,float speed)
    {
        float speedTemp = 0;
        if (start != end)
        {
            if(Mathf.Abs(start.x - end.x) < speed)
            {
                speedTemp = Mathf.Abs(start.x - end.x);
            }
            else
            {
                speedTemp = speed;
            }
            if (start.x > end.x)
            {
                start.x -= speedTemp;
            }
            if (start.x < end.x)
            {
                start.x += speedTemp;
            }

            if (Mathf.Abs(start.y - end.y) < speed)
            {
                speedTemp = Mathf.Abs(start.y - end.y);
            }
            else
            {
                speedTemp = speed;
            }
            if (start.y > end.y)
            {
                start.y -= speedTemp;
            }
            if (start.y < end.y)
            {
                start.y += speedTemp;
            }
            return start;
        }
        return end;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (MoveFly)
            {
                PlayerObj.transform.position=MoveToPos(PlayerObj.transform.position, endFly,7*Time.deltaTime);
                if (PlayerObj.transform.position == endFly)
                {
                    MoveFly = false;
                }
            }
            if ((MgcId == 5) && (!AtField))
            {
                gameObject.GetComponent<Button>().interactable = true;//Nyala
            }
            else if(MgcId==5)
            {
                gameObject.GetComponent<Button>().interactable = false;//Mati
            }
            if ((Mgcooldown < MgcoolEnd)&& (MgcId != 5))
            {
                Mgcooldown += 1;
                gameObject.GetComponent<Button>().interactable = false;
            }
            else if (MgcId != 5)
            {
                gameObject.GetComponent<Button>().interactable = true;
                if (MgcId == 7)
                {
                    MoveFly = true;
                    endFly = mainScr.PlayerStartPos;
                }
            }
            for (int i = 0; i < mainScr.ObstacleInArea.Count; i++)
            {
                if (mainScr.ObstacleInArea[i] == null)
                {
                    mainScr.ObstacleInArea.RemoveAt(i);
                }
            }
        }
        
    }
}
