using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magic : MonoBehaviour
{
    public int MgcId = 0;
    public Main mainScr;
    public GameObject PlayerObj;
    int Mgcooldown;
    int MgcoolEnd;
    bool NotActive = false;
    // Start is called before the first frame update
    void Start()
    {
        switch (MgcId)
        {
            case 0:
                MgcoolEnd = 50;
                break;
            case 1:
                MgcoolEnd = 100;
                break;
            case 2:
                MgcoolEnd = 150;
                break;
            case 3:
                MgcoolEnd = 100;
                break;
            case 4:
                MgcoolEnd = 200;
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
                Vector3 PlayerVec = PlayerObj.transform.position;
                PlayerVec.z = 0.1f;
                if (MgcId == 4)
                {
                    for (int i = 0; i < mainScr.ObstacleInArea.Count; i++)
                    {
                        Destroy(mainScr.ObstacleInArea[i]);
                    }
                    PlayerVec = new Vector3(0, 0);
                    Mgcooldown = 0;

                }
                GameObject magic = Instantiate(mainScr.MgcList[MgcId], PlayerVec, Quaternion.identity);
                GameObject TargetPos = null;
                float jarakTarget = 0;
                if ((MgcId == 3)&&(mainScr.ObstacleInArea.Count>0))
                {
                    for (int i = 0; i < mainScr.ObstacleInArea.Count; i++)
                    {
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
                }
                else
                {
                    NotActive = true;
                }
                magic.GetComponent<MagicFrame>().TargetPos = TargetPos;
                if (NotActive==false)
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
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Mgcooldown < MgcoolEnd)
            {
                Mgcooldown += 1;
                gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                gameObject.GetComponent<Button>().interactable = true;
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
