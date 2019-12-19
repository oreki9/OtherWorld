using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EffectSelect : MonoBehaviour {

    public float speed;
    public int Mode;
    public int EndTime;
    int InsSLash,EndTimeSlash,SlashNow;
    //mode 3
    public List<GameObject> SlashList;
    public int EndTimeChild;
    void Start()
    {
        float randomNum = Random.Range(-10, 10);
        switch (Mode)
        {
            case 1:
                transform.rotation = new Quaternion(0, 0, randomNum / 10, 1);
                break;
            case 2:

                break;
            case 3:
                if (SlashList.Count != 0)
                {
                    InsSLash = EndTime / 6;
                    EndTimeSlash = EndTime;
                    SlashNow = 0;
                    SetChildEndTime();
                }
                break;
            default:

                break;
        }
    }
    void SetChildEndTime()
    {
        foreach (GameObject i in SlashList)
        {
            i.GetComponent<EffectSelect>().EndTime = EndTimeChild;
        }
    }
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (EndTime == 0)
            {
                if (Mode != 3)
                {
                    Destroy(gameObject);
                }
                else
                {
                    if (SlashList.Count != 0)
                    {
                        foreach (GameObject i in SlashList)
                        {
                            Color NewColor = i.GetComponent<Image>().color;
                            NewColor.a = NewColor.a - (0.1f);
                            i.GetComponent<Image>().color = NewColor;
                        }
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
            if (EndTime < -InsSLash)
            {
                EndTime = EndTimeSlash;
                InsSLash = EndTime / 6;
                SlashNow = 0;
                SetChildEndTime();
                foreach (GameObject i in SlashList)
                {
                    Color NewColor = i.GetComponent<Image>().color;
                    NewColor.a = 1;
                    i.GetComponent<Image>().color = NewColor;
                    i.SetActive(false);
                }
                gameObject.SetActive(false);
            }
            switch (Mode)
            {
                case 1:
                    transform.Translate(transform.up * speed);
                    transform.Rotate(new Vector3(0, 0, 2));
                    break;
                case 2:
                    transform.Translate(transform.up * speed);
                    break;
                case 3:
                    if (SlashList.Count != 0)
                    {
                        if ((EndTimeSlash - EndTime) % InsSLash == 0)
                        {
                            if (SlashNow < SlashList.Count)
                            {
                                SlashList[SlashNow].SetActive(true);
                                SlashNow += 1;
                            }
                        }
                    }
                    break;
                default:

                    break;
            }
            EndTime -= 1;
        }
    }
}
