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
                GameObject magic = Instantiate(mainScr.MgcList[MgcId], PlayerVec, Quaternion.identity);
                Mgcooldown = 0;
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
        }
    }
}
